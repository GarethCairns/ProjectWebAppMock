IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    ALTER TABLE [Projects] DROP CONSTRAINT [FK_Projects_Workstream_WorkstreamId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    ALTER TABLE [Workstream] DROP CONSTRAINT [PK_Workstream];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    EXEC sp_rename N'Workstream', N'Workstreams';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    ALTER TABLE [Workstreams] ADD CONSTRAINT [PK_Workstreams] PRIMARY KEY ([WorkstreamId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_Workstreams_WorkstreamId] FOREIGN KEY ([WorkstreamId]) REFERENCES [Workstreams] ([WorkstreamId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180616190731_newIntital')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180616190731_newIntital', N'2.0.2-rtm-10011');
END;

GO

