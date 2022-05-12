IF NOT EXISTS (SELECT * FROM sys.columns where Name = 'RankId' and Object_ID = Object_ID('CPSyncV.MetricTarget'))
BEGIN
    ALTER TABLE CPSyncV.MetricTarget ADD RankId INT NULL DEFAULT 0
END



