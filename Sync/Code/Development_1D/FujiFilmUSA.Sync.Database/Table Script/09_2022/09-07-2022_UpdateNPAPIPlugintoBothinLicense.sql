UPDATE [CP].[License]
   SET [IsCustomPlugin] = 0 ----Both
 WHERE [IsCustomPlugin] = 1 -----NPAPIPlugin
GO