USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[Subject_mon]    Script Date: 2017/11/25 16:25:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Subject_mon](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[月份] [nchar](6) NULL,
	[条目ID] [bigint] NULL,
	[排序] [decimal](3, 0) NULL CONSTRAINT [DF_Subject_mon_排序]  DEFAULT ((0)),
	[类型] [decimal](1, 0) NULL CONSTRAINT [DF_Subject_mon_类型]  DEFAULT ((0)),
 CONSTRAINT [PK_Subject_mon] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'为0表示是金额数字，为1表示为文字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Subject_mon', @level2type=N'COLUMN',@level2name=N'类型'
GO

