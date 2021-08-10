
GO
CREATE NONCLUSTERED INDEX [CustomReferenceLAB_TargetLAB]
ON [CPSync4D].[CustomReferenceLAB] ([SourceProfileID],[ReferenceChartID])
INCLUDE ([TargetL*],[Targeta*],[Targetb*])
GO





