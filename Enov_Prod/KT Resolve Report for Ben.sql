select orderid as service_call_id
	  ,convert(varchar(20),c_service_call_env.eta_date,101) as createdOn --change this in future
	  ,ProblemStatement
	  ,service_call.bpart_id as model
		,service_call.serial_no
	  ,order_line.problem_desc
	  ,TRCNotes
	  ,ObjectIsNot
	  ,DeviationIsNot
	  ,KTwhere.Location
	  ,LocationIsNot
	  ,OntheObject
	  ,WhenFirst
	  ,SinceWhen
	  ,WhenFirstNot 
	  ,LifeCycle
	  ,LifeCycleIsNot
	  ,NumOfObject
	  ,NumOfObjectsIsNot
	  ,SizeOfDeviation
	  ,NumOfDeviation
	  ,case trend when 1 then 'Increasing' 
		    when 2 then 'Decreasing'
		    when 3 then 'Stable'
		    else 
				''
		end  as Trend
	  ,person.search_name as field_tech
	  ,COALESCE(t.search_name,'') as phone_tech
FROM 
c_service_call as  service_call 
	inner join c_order_line as order_line on service_call.order_id = order_line.order_id
	inner join dbo.KTProblemSpec AS PS on order_line.order_id = PS.orderid
INNER JOIN  dbo.KTWhat as KTWhat on KTWhat.ProblemSpecID = PS.ProblemSpecID
INNER JOIN dbo.KTWhere As KTWhere ON KTWhere.problemspecid = ps.problemspecid
INNER JOIN dbo.KTWhen as KTWhen ON KTWhen.problemspecid = ps.problemspecid
INNER JOIN dbo.KTExtent as KTExtent ON KTExtent.problemspecid = ps.problemspecid
INNER JOIN dbo.person on  person.person_id = service_call.sa_person_id
LEFT JOIN 
(

select distinct demand_done.order_id, sa_person_id, search_name from demand_done inner join demand_labor on demand_done.demand_id = demand_labor.demand_id
inner join person on person.person_id = demand_done.sa_person_id where 
bpart_id = 'phone_act' and demand_labor.duration is not null and  comment_text is not null

) as T ON T.order_id = service_call.order_id
left outer join c_service_call_env on c_service_call_env.order_id = order_line.order_id 
union all  
select orderid as service_call_id
	   ,convert(varchar(20),service_call_env.eta_date,101) as createdOn
	  ,ProblemStatement
	  ,service_call.bpart_id as model
		,service_call.serial_no
	  ,order_line.problem_desc
	  ,TRCNotes
	  ,ObjectIsNot
	  ,DeviationIsNot
	  ,KTwhere.Location
	  ,LocationIsNot
	  ,OntheObject
	  ,WhenFirst
	  ,SinceWhen
	  ,WhenFirstNot 
	  ,LifeCycle
	  ,LifeCycleIsNot
	  ,NumOfObject
	  ,NumOfObjectsIsNot
	  ,SizeOfDeviation
	  ,NumOfDeviation
	  ,case trend when 1 then 'Increasing' 
		    when 2 then 'Decreasing'
		    when 3 then 'Stable'
		    else
				''
		end  as Trend
      ,person.search_name as field_tech
	   ,COALESCE(t.search_name,'') as phone_tech
FROM 
service_call as  service_call 
	inner join order_line as order_line on service_call.order_id = order_line.order_id
	inner join dbo.KTProblemSpec AS PS on order_line.order_id = PS.orderid
INNER JOIN  dbo.KTWhat as KTWhat on KTWhat.ProblemSpecID = PS.ProblemSpecID
INNER JOIN dbo.KTWhere As KTWhere ON KTWhere.problemspecid = ps.problemspecid
INNER JOIN dbo.KTWhen as KTWhen ON KTWhen.problemspecid = ps.problemspecid
INNER JOIN dbo.KTExtent as KTExtent ON KTExtent.problemspecid = ps.problemspecid
inner join dbo.person on  person.person_id = service_call.sa_person_id
LEFT JOIN 
(

select distinct demand.order_id, sa_person_id, search_name from demand inner join demand_labor on demand.demand_id = demand_labor.demand_id
inner join person on person.person_id = demand.sa_person_id where 
bpart_id = 'phone_act' and demand_labor.duration is not null and  comment_text is not null

) as T ON T.order_id = service_call.order_id
left outer join service_call_env on service_call_env.order_id = order_line.order_id
order by service_call_id
