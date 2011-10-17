IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'uspGetPressDashboardZoneDeltaESummary')
	BEGIN
		DROP  Procedure  [Press].[uspGetPressDashboardZoneDeltaESummary]
	END

GO

CREATE Procedure [Press].[uspGetPressDashboardZoneDeltaESummary]( @JobID INT , @DongleID VARCHAR(10) ) AS
BEGIN

	/*-----  Gets The Average Delta E Per Zone -----*/
	SELECT 
		Press.MeasuredPatches.ZoneNumber
		, AVG(COALESCE(DeltaE,0)) AS AVG_DeltaE 
	FROM Press.MeasuredPatches
	INNER JOIN Press.Colors ON Press.Colors.ColorName = Press.MeasuredPatches.ColorName 
	INNER JOIN Press.ColorTarTol ON Press.ColorTarTol.ColorID = Press.Colors.ColorID
	WHERE
		Press.MeasuredPatches.JobID = @JobID AND Press.Colors.JobID = @JobID AND Press.Colors.JobID = @JobID
		AND Press.MeasuredPatches.DongleID = @DongleID AND Press.Colors.DongleID = @DongleID AND Press.ColorTarTol.DongleID = @DongleID
	GROUP BY 
		Press.MeasuredPatches.ZoneNumber 
	ORDER BY
		Press.MeasuredPatches.ZoneNumber

	/*-----  Gets The Delta E To Find the Patch Color -----*/
	SELECT 
		Press.MeasuredPatches.ZoneNumber
		, (DeltaE) AS DeltaE
		, (Press.ColorTarTol.deltaE_C) AS DeltaE_C
		, (Press.ColorTarTol.detlaE_L) AS DeltaE_L		
	FROM Press.MeasuredPatches
	INNER JOIN Press.Colors ON Press.Colors.ColorName = Press.MeasuredPatches.ColorName 
	INNER JOIN Press.ColorTarTol ON Press.ColorTarTol.ColorID = Press.Colors.ColorID
	WHERE
		Press.MeasuredPatches.JobID = @JobID AND Press.Colors.JobID = @JobID AND Press.Colors.JobID = @JobID
		AND Press.MeasuredPatches.DongleID = @DongleID AND Press.Colors.DongleID = @DongleID AND Press.ColorTarTol.DongleID = @DongleID
	ORDER BY
		Press.MeasuredPatches.ZoneNumber

END