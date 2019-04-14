using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace livecode.wpf.Util
{
    public class TypeHelper
    {
        private static Dictionary<string, Type> _typesCache = new Dictionary<string, Type>();

        public static Type TypeOf(string screen)
        {
            if (_typesCache.ContainsKey(screen))
                return _typesCache[screen];

            var T = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                     from type in asm.GetTypes()
                     where type.IsClass && type.Name == screen && type.IsSubclassOf(typeof(FrameworkElement))
                     select type).Single();

            _typesCache.Add(screen, T);

            return T;
        }

        public static Task<T> StartSTATask<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

    }
}
