USE [LigueHockey]
GO

/****** Object:  Table [dbo].[StatsEquipe]    Script Date: 2023-08-10 13:59:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatsEquipe]') AND type in (N'U'))
DROP TABLE [dbo].[StatsEquipe]
GO

/****** Object:  Table [dbo].[StatsEquipe]    Script Date: 2023-08-10 13:59:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StatsEquipe](
	[AnneeStats] [smallint] NOT NULL,
	[EquipeId] [int] NOT NULL,
	[NbPartiesJouees] [smallint] NOT NULL,
	[NbVictoires] [smallint] NOT NULL,
	[NbDefaites] [smallint] NOT NULL,
	[NbDefProlo] [smallint] NOT NULL,
	[NbButsPour] [smallint] NOT NULL,
	[NbButsContre] [smallint] NOT NULL,
	CONSTRAINT PK_StatsEquipe PRIMARY KEY (AnneeStats, EquipeId)
) ON [PRIMARY]
GO


