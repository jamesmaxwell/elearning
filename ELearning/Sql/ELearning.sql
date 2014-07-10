/*创建用户表*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_CATALOG = 'DaXue-ELearning' 
                 AND  TABLE_NAME = 'EL_Users'))
BEGIN
	CREATE TABLE [dbo].[EL_Users] (
			[Id]                   NVARCHAR (128) NOT NULL,
			[Email]                NVARCHAR (256) NULL,
			[PasswordHash]         NVARCHAR (MAX) NULL,
			[SecurityStamp]        NVARCHAR (MAX) NULL,
			[UserName]             NVARCHAR (256) NOT NULL,
			CONSTRAINT [PK_dbo.EL_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
		);

	CREATE UNIQUE NONCLUSTERED INDEX [EL_Users_UserNameIndex]
			ON [dbo].[EL_Users]([UserName] ASC);
END

/*创建角色表*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_CATALOG = 'DaXue-ELearning' 
                 AND  TABLE_NAME = 'EL_Roles'))
BEGIN
	CREATE TABLE [dbo].[EL_Roles] (
		[Id]   NVARCHAR (128) NOT NULL,
		[Name] NVARCHAR (256) NOT NULL,
		CONSTRAINT [PK_dbo.EL_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
	);

	CREATE UNIQUE NONCLUSTERED INDEX [EL_Roles_RoleNameIndex]
		ON [dbo].[EL_Roles]([Name] ASC);
END    

/*创建用户角色映射表*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_CATALOG = 'DaXue-ELearning' 
                 AND  TABLE_NAME = 'EL_UserRoles'))
BEGIN
	CREATE TABLE [dbo].[EL_UserRoles] (
		[UserId] NVARCHAR (128) NOT NULL,
		[RoleId] NVARCHAR (128) NOT NULL,
		CONSTRAINT [PK_dbo.EL_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
	);
	
	ALTER TABLE [dbo].[EL_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EL_UserRoles_dbo.EL_Roles_RoleId] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[EL_Roles] ([Id])
	ON DELETE CASCADE;
	
	ALTER TABLE [dbo].[EL_UserRoles] CHECK CONSTRAINT [FK_dbo.EL_UserRoles_dbo.EL_Roles_RoleId];

	ALTER TABLE [dbo].[EL_UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EL_UserRoles_dbo.EL_Roles_UserId] FOREIGN KEY([UserId])
	REFERENCES [dbo].[EL_Roles] ([Id])
	ON DELETE CASCADE;
	
	ALTER TABLE [dbo].[EL_UserRoles] CHECK CONSTRAINT [FK_dbo.EL_UserRoles_dbo.EL_Roles_UserId];

	CREATE NONCLUSTERED INDEX [EL_UserRoles_IX_UserId]
		ON [dbo].[EL_UserRoles]([UserId] ASC);
	CREATE NONCLUSTERED INDEX [EL_UserRoles_IX_RoleId]
		ON [dbo].[EL_UserRoles]([RoleId] ASC);
END    

/*创建角色授权声明*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_CATALOG = 'DaXue-ELearning' 
                 AND  TABLE_NAME = 'EL_RoleClaims'))
BEGIN
	CREATE TABLE [dbo].[EL_RoleClaims] (
		[Id]         INT            IDENTITY (1, 1) NOT NULL,
		[RoleId]     NVARCHAR (128) NOT NULL,
		[ClaimType]  NVARCHAR (100) NULL,
		[ClaimValue] NVARCHAR (200) NULL,
		[ClaimName]  NVARCHAR (200) NULL,
		[ClaimGroup] NVARCHAR (200) NULL
		CONSTRAINT [PK_dbo.EL_RoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
		CONSTRAINT [FK_dbo.EL_RoleClaims_dbo.EL_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[EL_Roles] ([Id]) ON DELETE CASCADE
	);

	CREATE NONCLUSTERED INDEX [EL_RoleClaims_IX_UserId]
		ON [dbo].[EL_RoleClaims]([RoleId] ASC);
END

/*用户登录信息记录表*/
IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_CATALOG = 'DaXue-ELearning' 
                 AND  TABLE_NAME = 'EL_UserLogins'))
BEGIN
	CREATE TABLE [dbo].[EL_UserLogins] (
		[LoginProvider] NVARCHAR (128) NOT NULL,
		[ProviderKey]   NVARCHAR (128) NOT NULL,
		[UserId]        NVARCHAR (128) NOT NULL,
		CONSTRAINT [PK_dbo.EL_UserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
		CONSTRAINT [FK_dbo.EL_UserLogins_dbo.EL_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[EL_Users] ([Id]) ON DELETE CASCADE
	);

	CREATE NONCLUSTERED INDEX [EL_UserLogins_IX_UserId]
		ON [dbo].[EL_UserLogins]([UserId] ASC);
END    
