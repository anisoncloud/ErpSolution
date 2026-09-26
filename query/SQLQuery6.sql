

TRUNCATE TABLE [ErpSolution].[crm].[Company];






--If the TRUNCATE Is not Work Use the following command
DELETE FROM [ErpSolution].[crm].[Contact];
DBCC CHECKIDENT ('[ErpSolution].[crm].[Contact]', RESEED, 0);