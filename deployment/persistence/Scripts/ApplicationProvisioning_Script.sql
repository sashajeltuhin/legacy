SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [AccountData](
[dataID] [int] IDENTITY(1,1) NOT NULL,
[tupleType] [int],
[jan] [float],
[feb] [float],
[mar] [float],
[apr] [float],
[may] [float],
[jun] [float],
[jul] [float],
[aug] [float],
[sep] [float],
[oct] [float],
[nov] [float],
[dec] [float],
[tupleName] [nvarchar](50)
)
GO

INSERT INTO [AccountData] ([tupleType], [jan], [feb], [mar], [apr], [may], [jun], [jul], [aug], [sep], [oct], [nov], [dec], [tupleName]) VALUES (1, 350, 245, 70, 0, 0, 156, 0, 876, 0, 240, 432, 0, 'claims')
GO

INSERT INTO [AccountData] ([tupleType], [jan], [feb], [mar], [apr], [may], [jun], [jul], [aug], [sep], [oct], [nov], [dec], [tupleName]) VALUES (2, 80, 190, 70, 0, 0, 25, 0, 453, 0, 45, 187, 0, 'pocket')
GO

INSERT INTO [AccountData] ([tupleType], [jan], [feb], [mar], [apr], [may], [jun], [jul], [aug], [sep], [oct], [nov], [dec], [tupleName]) VALUES (3, 0.01, 0.013, 0.013, 0.013, 0.013, 0.011, 0.013, 0.02, 0.01, 0.0129, 0.0131, 0.011, 'interest')
GO

