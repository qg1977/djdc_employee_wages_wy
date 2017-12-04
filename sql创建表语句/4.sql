USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[p_role]    Script Date: 2017/11/25 16:25:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[p_role](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[角色] [nchar](20) NULL,
	[类型] [decimal](1, 0) NULL CONSTRAINT [DF_p_role_类型]  DEFAULT ((0)),
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_p_role_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_p_role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

