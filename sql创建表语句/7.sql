USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[person]    Script Date: 2017/11/25 16:25:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[person](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[姓名] [nchar](20) NULL,
	[拼音] [nchar](20) NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_person_删除]  DEFAULT ((0)),
	[微信ID] [nchar](30) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

