﻿CREATE TABLE [dbo].[ProjectHomeAffordUnits] (
    [ProjectHomeAffordUnitsID] INT      IDENTITY (1, 1) NOT NULL,
    [HousingID]                INT      NOT NULL,
    [LkAffordunits]            INT      NOT NULL,
    [Numunits]                 INT      CONSTRAINT [DF_ProjectHomeAffordUnits_Numunits] DEFAULT ((0)) NOT NULL,
    [RowIsActive]              BIT      CONSTRAINT [DF_ProjectHomeAffordUnits_RowIsActive] DEFAULT ((1)) NOT NULL,
    [DateModified]             DATETIME CONSTRAINT [DF_ProjectHomeAffordUnits_DateModified] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ProjectHomeAffordUnits] PRIMARY KEY CLUSTERED ([ProjectHomeAffordUnitsID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date last modified', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'DateModified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Is row active?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'RowIsActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of units occupied', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'Numunits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'LkAffordunit Record ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'LkAffordunits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'HousingID Record ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'HousingID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Record ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits', @level2type = N'COLUMN', @level2name = N'ProjectHomeAffordUnitsID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Affordable units in covenant', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ProjectHomeAffordUnits';

