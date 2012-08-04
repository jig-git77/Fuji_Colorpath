alter procedure [dbo].[uspGetProblemSpec] 
(@order_id varchar(15) )
as
begin

	if exists (select * from service_call where order_id = @order_id)
	begin
	
		select  
		 service_call.order_id
		,service_call.bpart_id
		,service_call.serial_no
		,order_line.problem_desc
		,PS.ProblemStatement
		,PS.TRCNotes
		,KTWhat.Object
		,KTWhat.ObjectIsNot
		,KTWhat.Deviation
		,KTWhat.DeviationIsNot
		,KTWhere.Location
		,KTWhere.LocationIsNot
		,KTWhere.OntheObject
		,KTWhen.WhenFirst
		,KTWhen.SinceWhen
		,KTWhen.WhenFirstNot
		,KTWhen.LifeCycle
		,KTWhen.LifeCycleIsNot
		,KTExtent.NumOfObject
		,KTExtent.NumOfObjectsIsNot
		,KTExtent.SizeOfDeviation
		,KTExtent.NumOfDeviation
		,KTExtent.Trend
		from  service_call  as service_call 
		inner join order_line on service_call.order_id = order_line.order_id
		left join KTProblemSpec as PS on order_line.order_id = PS.orderid
		left join KTWhen  ON KTWhen.ProblemSpecID = PS.ProblemSpecID
		left join KTWhat  ON KTWhat.ProblemSpecID = PS.ProblemSpecID	
		left join KTWhere ON KTWhere.ProblemSpecID = PS.ProblemSpecID
		left join KTExtent ON KTExtent.ProblemSpecID = PS.ProblemSpecID
		where service_call.order_id = @order_id
	
	end
	else
	begin
		
		select  
		service_call.order_id
		,service_call.bpart_id
		,service_call.serial_no
		,order_line.problem_desc
		,PS.ProblemStatement
		,PS.TRCNotes
		,KTWhat.Object
		,KTWhat.ObjectIsNot
		,KTWhat.Deviation
		,KTWhat.DeviationIsNot
		,KTWhere.Location
		,KTWhere.LocationIsNot
		,KTWhere.OntheObject
		,KTWhen.WhenFirst
		,KTWhen.SinceWhen
		,KTWhen.WhenFirstNot
		,KTWhen.LifeCycle
		,KTWhen.LifeCycleIsNot
		,KTExtent.NumOfObject
		,KTExtent.NumOfObjectsIsNot
		,KTExtent.SizeOfDeviation
		,KTExtent.NumOfDeviation
		,KTExtent.Trend
		from  c_service_call  as service_call 
		inner join c_order_line as order_line on service_call.order_id = order_line.order_id
		left join KTProblemSpec as PS on order_line.order_id = PS.orderid
		left join KTWhen  ON KTWhen.ProblemSpecID = PS.ProblemSpecID
		left join KTWhat  ON KTWhat.ProblemSpecID = PS.ProblemSpecID	
		left join KTWhere ON KTWhere.ProblemSpecID = PS.ProblemSpecID
		left join KTExtent ON KTExtent.ProblemSpecID = PS.ProblemSpecID
		where service_call.order_id = @order_id

	end 
end
