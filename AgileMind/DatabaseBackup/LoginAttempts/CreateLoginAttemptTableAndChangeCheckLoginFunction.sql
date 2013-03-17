GO

/****** Object:  Table [dbo].[t_LoginAttempts]    Script Date: 03/10/2013 20:18:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[t_LoginAttempts](
	[LoginAttemptId] [int] IDENTITY(1,1) NOT NULL,
	[LoginId] [int] NULL,
	[Created] [datetime] NOT NULL,
	[Success] [bit] NOT NULL,
	[IPAddress] [varchar](30) NOT NULL,
	[LoginName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_t_LoginAttempts] PRIMARY KEY CLUSTERED 
(
	[LoginAttemptId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[t_LoginAttempts]  WITH CHECK ADD  CONSTRAINT [FK_t_LoginAttempts_Logins] FOREIGN KEY([LoginId])
REFERENCES [dbo].[Logins] ([LoginId])
ON DELETE SET NULL
GO

ALTER TABLE [dbo].[t_LoginAttempts] CHECK CONSTRAINT [FK_t_LoginAttempts_Logins]
GO


GO
-- =============================================
-- Author:		Kendall Bingham
-- Create date: 03.03.2013
-- Description:	Try to login user.  Return if they were successful or not.
-- =============================================
ALTER PROCEDURE [dbo].[Logins_CheckLogin]
	-- Add the parameters for the stored procedure here
	@LoginName varchar(50),
	@Password varchar(500),
	@IPAddress varchar(30)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	-- Record the attempt
	DECLARE @LoginId int
	SELECT @LoginId = Loginid FROM Logins WHERE LoginName = @LoginName
	DECLARE @Successful bit
	SET @Successful = 0
	IF (EXISTS (SELECT * FROM Logins WHERE LoginName = @LoginName AND [Password] = @Password)) 
		SET @Successful = 1

    -- Insert statements for procedure here
	INSERT INTO [t_LoginAttempts]
			   ([LoginId],[Created],[Success],[IPAddress],[LoginName])
		 VALUES
			   (@LoginId,
				GETDATE(),
				@Successful,
				@IPAddress,
				@LoginName)

    -- Insert statements for procedure here
	SELECT [LoginId],[LoginName],[Password],[EmailAddress],[Active],[Created],[LastLogin]
	FROM [Logins]
		WHERE LoginName = @LoginName 
			AND [Password] = @Password
 
	
	
END
