USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[p_passpass]    Script Date: 2017/11/25 16:25:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[p_passpass](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[员工ID] [bigint] NULL,
	[用户名] [nchar](20) NULL,
	[密码] [nchar](20) NULL,
	[角色ID] [bigint] NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_p_passpass_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_passpass] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[p_passpass]  WITH CHECK ADD  CONSTRAINT [FK_p_passpass_p_role] FOREIGN KEY([角色ID])
REFERENCES [dbo].[p_role] ([ID])
GO

ALTER TABLE [dbo].[p_passpass] CHECK CONSTRAINT [FK_p_passpass_p_role]
GO

ALTER TABLE [dbo].[p_passpass]  WITH CHECK ADD  CONSTRAINT [FK_p_passpass_person] FOREIGN KEY([员工ID])
REFERENCES [dbo].[person] ([ID])
GO

ALTER TABLE [dbo].[p_passpass] CHECK CONSTRAINT [FK_p_passpass_person]
GO

