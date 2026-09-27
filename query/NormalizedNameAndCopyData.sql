ALTER TABLE [MyErp].[dbo].[CrmCompanies]
ADD NormalizedName NVARCHAR(255) NULL;

UPDATE [MyErp].[dbo].[CrmCompanies]
SET NormalizedName = LOWER(
    REPLACE(
        REPLACE(Name, ' ', ''),
        '.', ''
    )
);