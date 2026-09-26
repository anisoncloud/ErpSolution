INSERT INTO [ErpSolution].[crm].[Contact]
(
    Name,
    Designation,
    [Email]
      ,[Phone]
      ,[Photo]
      ,[Comments]
      ,[CrmCompanyId]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt]
      ,[IsDeleted]
      ,[IsActive]
)
SELECT
    [Name]
      ,[Designation]
      ,[Email]
      ,[Phone]
      ,[Photo]
      ,[Comments]
      ,[CrmCompanyId]
      ,[CreatedBy]
      ,[CreatedAt]
      ,[UpdatedBy]
      ,[UpdatedAt],            
    0 AS IsDeleted,
    1 AS IsActive    
FROM [MyErp].[dbo].[CrmContacts];