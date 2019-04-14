using System;
using System.Linq;
using System.Text.RegularExpressions;
using DiffPlex.DiffBuilder.Model;
using livecode.ComponentBase.FilePoint;

namespace livecode.CCodeTracker.Language
{
    public class ChangeHelper
    {
        public static CodeChangeType InsertedLineType(DiffPiece line)
        {
            var tokens = containsTokens(line);

            return classifyChangeType(tokens);
        }

        public static CodeChangeType ModifiedLineType(DiffPiece oldLine, DiffPiece newLine)
        {
            var tokens1 = containsTokens(oldLine);
            var tokens2 = containsTokens(newLine);

            return classifyChangeType(tokens1) | classifyChangeType(tokens2);
        }

        public static CodeChangeType RemovedLineType(DiffPiece oldLine)
        {
            var tokens = containsTokens(oldLine);

            return classifyChangeType(tokens);
        }

        [Flags]
        private enum TokenType
        {
            Comment = 4096,
            ProcedureCall = 2048,
            Operator = 1024,
            Assignment = 512,
            ClassDefinition = 256,
            MethodDefinition = 128,
            DataTypeDefinition = 64,
            IfStatement = 32,
            TryStatement = 16,
            CatchStatement = 8,
            ForStatement = 4,
            WhileStatement = 2,
            SwitchStatement = 1,
            None = 0
            
        }

        private static TokenType containsTokens(DiffPiece line)
        {
            TokenType t = TokenType.None;

            string text = line.Text;
            string changes = string.Join("", line.SubPieces.Where(p => p.Type == ChangeType.Modified || p.Type == ChangeType.Inserted).Select(p => p.Text));

            string context = string.IsNullOrWhiteSpace(changes) ? text : changes;

            if (text.Trim().StartsWith("//") || text.StartsWith("/*"))
                t |= TokenType.Comment;
            else if (line.SubPieces.Count > 0 && context.Contains("//"))
                t |= TokenType.Comment;

            if (context.Contains("="))
                t |= TokenType.Assignment;

            if (new [] {'+','-','*','/','=','%'}.Any(c => context.Replace("//","").Contains(c)))
                t |= TokenType.Operator;

            if (!text.Contains(";"))
            {
                if (text.Contains(" class ") || context.Contains(" class "))
                    t |= TokenType.ClassDefinition;
                else if (Regex.Matches(context, "\\(.*\\)").Count == 1)
                {
                    if (new[] { "private", "static", "void", "public", "internal" }.Any(k => context.Split(' ').Contains(k)))
                        t |= TokenType.MethodDefinition;
                }
            }
            else
            {
                if (Regex.IsMatch(context, "\\(.*\\)"))
                    t |= TokenType.ProcedureCall;
            }
            
            if (new[] { " enum ", " typedef ", " union " , " struct " , " template<" }.Any(k => context.Contains(k)) ||
                Regex.IsMatch(context, "\\[(\\d*)\\]"))
                t |= TokenType.DataTypeDefinition;

            if (new[] {"Exception ", " try", "catch"}.Any(k => text.Contains(k)))
                t |= TokenType.TryStatement | TokenType.CatchStatement;

            if (new[] {" if ", " if(", "else ", " else", "else if"}.Any(k => text.Contains(k)))
                t |= TokenType.IfStatement;
            
            if (new[] { " for ", " for("}.Any(k => text.Contains(k)) || Regex.IsMatch(text, "for\\s*\\(.*;.*\\)"))
                t |= TokenType.ForStatement;
            
            if (new[] { " while ", " while(" }.Any(k => text.Contains(k)) || Regex.IsMatch(text, "while\\s*\\(.*\\)"))
                t |= TokenType.WhileStatement;

            if (new[] { " switch ", "case ", " default:", "break;" }.Any(k => text.Contains(k)))
                t |= TokenType.SwitchStatement;

            return t;
        }

        private static CodeChangeType classifyChangeType(TokenType tokens)
        {
            CodeChangeType t = CodeChangeType.None;

            if ((tokens & TokenType.Comment) != 0)
                t |= CodeChangeType.Documentation;

            if ((tokens & TokenType.ProcedureCall) != 0 || (tokens & TokenType.Operator) != 0 || (tokens & TokenType.Assignment) != 0)
                t |= CodeChangeType.Assignment;

            if ((tokens & TokenType.ClassDefinition) != 0 || (tokens & TokenType.MethodDefinition) != 0 || (tokens & TokenType.DataTypeDefinition) != 0)
                t |= CodeChangeType.Interface;

            if ((tokens & TokenType.DataTypeDefinition) != 0)
                t |= CodeChangeType.Data;

            if ((tokens & TokenType.IfStatement) != 0 || (tokens & TokenType.TryStatement) != 0 || (tokens & TokenType.CatchStatement) != 0)
                t |= CodeChangeType.Checking;

            if ((tokens & TokenType.Assignment) != 0)
                t |= CodeChangeType.Function;

            if ((tokens & TokenType.ForStatement) != 0 || (tokens & TokenType.IfStatement) != 0 || (tokens & TokenType.WhileStatement) != 0 || (tokens & TokenType.SwitchStatement) != 0)
                t |= CodeChangeType.Function;

            return t;
        }
    }
}
