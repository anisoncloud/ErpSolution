INSERT INTO [ErpSolution].[crm].[Company]
(
    Name,
    Description,
    CompanyEmail,
    CompanyPhone,
    CompanyAddress,
    CreatedBy,
    CreatedAt,
    UpdatedBy,
    UpdatedAt,
    IsDeleted,
    IsActive,
    [NormalizedName]
)
SELECT
    [Name]
      ,[Description]
      ,[CompanyEmail]           
      ,[CompanyPhone]
      ,[CompanyAddress]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt],
       0 AS IsDeleted,
       1 AS IsActive,
      [NormalizedName]
FROM [MyErp].[dbo].[CrmCompanies];