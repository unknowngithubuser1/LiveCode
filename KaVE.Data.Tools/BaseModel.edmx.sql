
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/18/2018 13:46:51
-- Generated from EDMX file: C:\Users\Alireza\Documents\Visual Studio 2017\Projects\livecode\KaVE.Data.Tools\BaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Events_Sessions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_Sessions];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_BuildEvent] DROP CONSTRAINT [FK_BuildEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_CommandEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_CommandEvent] DROP CONSTRAINT [FK_CommandEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_DebuggerEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_DebuggerEvent] DROP CONSTRAINT [FK_DebuggerEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_DocumentEvent] DROP CONSTRAINT [FK_DocumentEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_EditEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_EditEvent] DROP CONSTRAINT [FK_EditEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_ErrorEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_ErrorEvent] DROP CONSTRAINT [FK_ErrorEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_IDEStateEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_IDEStateEvent] DROP CONSTRAINT [FK_IDEStateEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_InfoEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_InfoEvent] DROP CONSTRAINT [FK_InfoEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_NavigationEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_NavigationEvent] DROP CONSTRAINT [FK_NavigationEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_SolutionEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_SolutionEvent] DROP CONSTRAINT [FK_SolutionEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_SystemEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_SystemEvent] DROP CONSTRAINT [FK_SystemEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_TestEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_TestEvent] DROP CONSTRAINT [FK_TestEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_VersionControlEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_VersionControlEvent] DROP CONSTRAINT [FK_VersionControlEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_WindowEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_WindowEvent] DROP CONSTRAINT [FK_WindowEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_AppEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_AppEvent] DROP CONSTRAINT [FK_AppEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_ActivityEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_ActivityEvent] DROP CONSTRAINT [FK_ActivityEvent_inherits_Event];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeEvent_inherits_Event]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events_CodeEvent] DROP CONSTRAINT [FK_CodeEvent_inherits_Event];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Sessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sessions];
GO
IF OBJECT_ID(N'[dbo].[Events_BuildEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_BuildEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_CommandEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_CommandEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_DebuggerEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_DebuggerEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_DocumentEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_DocumentEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_EditEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_EditEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_ErrorEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_ErrorEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_IDEStateEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_IDEStateEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_InfoEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_InfoEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_NavigationEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_NavigationEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_SolutionEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_SolutionEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_SystemEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_SystemEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_TestEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_TestEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_VersionControlEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_VersionControlEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_WindowEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_WindowEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_AppEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_AppEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_ActivityEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_ActivityEvent];
GO
IF OBJECT_ID(N'[dbo].[Events_CodeEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events_CodeEvent];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [EventId] int IDENTITY(1,1) NOT NULL,
    [SessionId] int  NOT NULL,
    [InFileId] int  NOT NULL,
    [SequenceId] int  NOT NULL,
    [Type] nvarchar(100)  NOT NULL,
    [Details] nvarchar(max)  NULL,
    [TriggeredAt] datetime  NULL,
    [TimeStamp] bigint  NULL,
    [TriggeredBy] nvarchar(50)  NULL,
    [Duration] nvarchar(50)  NULL,
    [ActiveDocumentName] nvarchar(400)  NULL,
    [ActiveDocumentType] nvarchar(100)  NULL,
    [ActiveWindowCaption] nvarchar(400)  NULL,
    [ActiveWindowType] nvarchar(100)  NULL,
    [Result] nvarchar(50)  NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [SessionId] int IDENTITY(1,1) NOT NULL,
    [IDESessionId] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Events_BuildEvent'
CREATE TABLE [dbo].[Events_BuildEvent] (
    [Project] nvarchar(200)  NULL,
    [Action] nvarchar(200)  NULL,
    [BuildStatus] bit  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_CommandEvent'
CREATE TABLE [dbo].[Events_CommandEvent] (
    [CommandId] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_DebuggerEvent'
CREATE TABLE [dbo].[Events_DebuggerEvent] (
    [Mode] nvarchar(50)  NULL,
    [Reason] nvarchar(max)  NULL,
    [Action] nvarchar(50)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_DocumentEvent'
CREATE TABLE [dbo].[Events_DocumentEvent] (
    [DocumentName] nvarchar(200)  NULL,
    [DocumentAction] nvarchar(50)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_EditEvent'
CREATE TABLE [dbo].[Events_EditEvent] (
    [NumberOfChanges] int  NULL,
    [SizeOfChanges] int  NULL,
    [TypeName] nvarchar(200)  NULL,
    [Context] nvarchar(max)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_ErrorEvent'
CREATE TABLE [dbo].[Events_ErrorEvent] (
    [ErrorContent] nvarchar(500)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_IDEStateEvent'
CREATE TABLE [dbo].[Events_IDEStateEvent] (
    [Phase] nvarchar(50)  NOT NULL,
    [OpenDocuments] nvarchar(max)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_InfoEvent'
CREATE TABLE [dbo].[Events_InfoEvent] (
    [InfoContent] nvarchar(200)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_NavigationEvent'
CREATE TABLE [dbo].[Events_NavigationEvent] (
    [Location] nvarchar(max)  NULL,
    [Target] nvarchar(max)  NULL,
    [NavigationType] nvarchar(50)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_SolutionEvent'
CREATE TABLE [dbo].[Events_SolutionEvent] (
    [Action] nvarchar(50)  NULL,
    [Target] nvarchar(max)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_SystemEvent'
CREATE TABLE [dbo].[Events_SystemEvent] (
    [Action] nvarchar(50)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_TestEvent'
CREATE TABLE [dbo].[Events_TestEvent] (
    [MethodName] nvarchar(200)  NULL,
    [TestStatus] nvarchar(50)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_VersionControlEvent'
CREATE TABLE [dbo].[Events_VersionControlEvent] (
    [Solution] nvarchar(200)  NULL,
    [ActionType] nvarchar(100)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_WindowEvent'
CREATE TABLE [dbo].[Events_WindowEvent] (
    [Window] nvarchar(200)  NOT NULL,
    [Action] nvarchar(50)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_AppEvent'
CREATE TABLE [dbo].[Events_AppEvent] (
    [ProcessHandle] nvarchar(max)  NOT NULL,
    [ProcessName] nvarchar(max)  NOT NULL,
    [ProcessPath] nvarchar(max)  NOT NULL,
    [WindowTitle] nvarchar(max)  NOT NULL,
    [AppType] nvarchar(30)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CommandLine] nvarchar(max)  NULL,
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_ActivityEvent'
CREATE TABLE [dbo].[Events_ActivityEvent] (
    [EventId] int  NOT NULL
);
GO

-- Creating table 'Events_CodeEvent'
CREATE TABLE [dbo].[Events_CodeEvent] (
    [FullPath] nvarchar(max)  NOT NULL,
    [Exists] bit  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [EventId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EventId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [SessionId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([SessionId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_BuildEvent'
ALTER TABLE [dbo].[Events_BuildEvent]
ADD CONSTRAINT [PK_Events_BuildEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_CommandEvent'
ALTER TABLE [dbo].[Events_CommandEvent]
ADD CONSTRAINT [PK_Events_CommandEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_DebuggerEvent'
ALTER TABLE [dbo].[Events_DebuggerEvent]
ADD CONSTRAINT [PK_Events_DebuggerEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_DocumentEvent'
ALTER TABLE [dbo].[Events_DocumentEvent]
ADD CONSTRAINT [PK_Events_DocumentEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_EditEvent'
ALTER TABLE [dbo].[Events_EditEvent]
ADD CONSTRAINT [PK_Events_EditEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_ErrorEvent'
ALTER TABLE [dbo].[Events_ErrorEvent]
ADD CONSTRAINT [PK_Events_ErrorEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_IDEStateEvent'
ALTER TABLE [dbo].[Events_IDEStateEvent]
ADD CONSTRAINT [PK_Events_IDEStateEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_InfoEvent'
ALTER TABLE [dbo].[Events_InfoEvent]
ADD CONSTRAINT [PK_Events_InfoEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_NavigationEvent'
ALTER TABLE [dbo].[Events_NavigationEvent]
ADD CONSTRAINT [PK_Events_NavigationEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_SolutionEvent'
ALTER TABLE [dbo].[Events_SolutionEvent]
ADD CONSTRAINT [PK_Events_SolutionEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_SystemEvent'
ALTER TABLE [dbo].[Events_SystemEvent]
ADD CONSTRAINT [PK_Events_SystemEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_TestEvent'
ALTER TABLE [dbo].[Events_TestEvent]
ADD CONSTRAINT [PK_Events_TestEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_VersionControlEvent'
ALTER TABLE [dbo].[Events_VersionControlEvent]
ADD CONSTRAINT [PK_Events_VersionControlEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_WindowEvent'
ALTER TABLE [dbo].[Events_WindowEvent]
ADD CONSTRAINT [PK_Events_WindowEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_AppEvent'
ALTER TABLE [dbo].[Events_AppEvent]
ADD CONSTRAINT [PK_Events_AppEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_ActivityEvent'
ALTER TABLE [dbo].[Events_ActivityEvent]
ADD CONSTRAINT [PK_Events_ActivityEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- Creating primary key on [EventId] in table 'Events_CodeEvent'
ALTER TABLE [dbo].[Events_CodeEvent]
ADD CONSTRAINT [PK_Events_CodeEvent]
    PRIMARY KEY CLUSTERED ([EventId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SessionId] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Events_Sessions]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[Sessions]
        ([SessionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Events_Sessions'
CREATE INDEX [IX_FK_Events_Sessions]
ON [dbo].[Events]
    ([SessionId]);
GO

-- Creating foreign key on [EventId] in table 'Events_BuildEvent'
ALTER TABLE [dbo].[Events_BuildEvent]
ADD CONSTRAINT [FK_BuildEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_CommandEvent'
ALTER TABLE [dbo].[Events_CommandEvent]
ADD CONSTRAINT [FK_CommandEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_DebuggerEvent'
ALTER TABLE [dbo].[Events_DebuggerEvent]
ADD CONSTRAINT [FK_DebuggerEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_DocumentEvent'
ALTER TABLE [dbo].[Events_DocumentEvent]
ADD CONSTRAINT [FK_DocumentEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_EditEvent'
ALTER TABLE [dbo].[Events_EditEvent]
ADD CONSTRAINT [FK_EditEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_ErrorEvent'
ALTER TABLE [dbo].[Events_ErrorEvent]
ADD CONSTRAINT [FK_ErrorEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_IDEStateEvent'
ALTER TABLE [dbo].[Events_IDEStateEvent]
ADD CONSTRAINT [FK_IDEStateEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_InfoEvent'
ALTER TABLE [dbo].[Events_InfoEvent]
ADD CONSTRAINT [FK_InfoEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_NavigationEvent'
ALTER TABLE [dbo].[Events_NavigationEvent]
ADD CONSTRAINT [FK_NavigationEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_SolutionEvent'
ALTER TABLE [dbo].[Events_SolutionEvent]
ADD CONSTRAINT [FK_SolutionEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_SystemEvent'
ALTER TABLE [dbo].[Events_SystemEvent]
ADD CONSTRAINT [FK_SystemEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_TestEvent'
ALTER TABLE [dbo].[Events_TestEvent]
ADD CONSTRAINT [FK_TestEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_VersionControlEvent'
ALTER TABLE [dbo].[Events_VersionControlEvent]
ADD CONSTRAINT [FK_VersionControlEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_WindowEvent'
ALTER TABLE [dbo].[Events_WindowEvent]
ADD CONSTRAINT [FK_WindowEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_AppEvent'
ALTER TABLE [dbo].[Events_AppEvent]
ADD CONSTRAINT [FK_AppEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_ActivityEvent'
ALTER TABLE [dbo].[Events_ActivityEvent]
ADD CONSTRAINT [FK_ActivityEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [EventId] in table 'Events_CodeEvent'
ALTER TABLE [dbo].[Events_CodeEvent]
ADD CONSTRAINT [FK_CodeEvent_inherits_Event]
    FOREIGN KEY ([EventId])
    REFERENCES [dbo].[Events]
        ([EventId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------