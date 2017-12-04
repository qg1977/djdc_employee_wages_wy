USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[Subject_name]    Script Date: 2017/11/25 16:25:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Subject_name](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[条目名称] [nchar](20) NULL,
	[删除] [decimal](1, 0) NULL CONSTRAINT [DF_Subject_name_删除]  DEFAULT ((0)),
 CONSTRAINT [PK_Subject_name] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

