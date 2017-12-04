USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[permoney_lr]    Script Date: 2017/11/25 16:25:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[permoney_lr](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[文字内容] [nchar](100) NULL,
 CONSTRAINT [PK_permoney_lr] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'保存工资表中一些文字内的东西' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'permoney_lr', @level2type=N'COLUMN',@level2name=N'文字内容'
GO

