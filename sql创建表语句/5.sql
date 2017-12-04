USE [qds16172939_db]
GO

/****** Object:  Table [dbo].[permoney]    Script Date: 2017/11/25 16:25:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[permoney](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[部门ID] [bigint] NULL,
	[员工ID] [bigint] NULL,
	[条目ID] [bigint] NULL,
	[金额] [decimal](12, 2) NULL CONSTRAINT [DF_permoney_金额]  DEFAULT ((0)),
	[月份] [nchar](6) NULL,
	[操作员] [bigint] NULL,
	[日期] [datetime] NULL CONSTRAINT [DF_permoney_日期]  DEFAULT (CONVERT([varchar](10),getdate(),(120))),
	[排序] [decimal](9, 0) NULL CONSTRAINT [DF_permoney_排序]  DEFAULT ((0)),
 CONSTRAINT [PK_permoney] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_permoney] FOREIGN KEY([部门ID])
REFERENCES [dbo].[z_fcname] ([ID])
GO

ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_permoney]
GO

ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_person] FOREIGN KEY([员工ID])
REFERENCES [dbo].[person] ([ID])
GO

ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_person]
GO

ALTER TABLE [dbo].[permoney]  WITH CHECK ADD  CONSTRAINT [FK_permoney_Subject_name] FOREIGN KEY([条目ID])
REFERENCES [dbo].[Subject_name] ([ID])
GO

ALTER TABLE [dbo].[permoney] CHECK CONSTRAINT [FK_permoney_Subject_name]
GO

