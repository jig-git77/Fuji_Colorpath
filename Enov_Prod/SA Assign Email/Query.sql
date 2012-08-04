select prv_order_stat_id, order_stat_uniq_id,last_action_dt, order_line.order_id, service_level_id, service_call.sa_person_id
, sce.o_actgr_id,  search_name
from  order_line 
inner join service_call on order_line.order_id=service_call.order_id 
inner join service_call_env as sce on sce.order_id = service_call.order_id
inner join person on person.person_id = service_call.sa_person_id
where order_type_id = 'service_order' and order_stat_uniq_id = 200 and last_action_dt >= dateadd(mi,-15,getdate())
order by last_action_Dt desc



select coalesce(cust_company_descr, site_company_descr) as company, address.city, address.state_prov_id as state, address.zip, order_line.order_id as servicecall, service_level_id, service_call.sa_person_id,  search_name, person.email_id, order_line.caller_name, order_line.phone_id as caller_phone
, order_line.problem_desc, service_call.bpart_id as model
from  order_line 
inner join service_call on order_line.order_id=service_call.order_id 
inner join person on person.person_id = service_call.sa_person_id
inner join address on order_line.ship_to_address_id=address.address_id
where order_type_id = 'service_order' and order_stat_uniq_id = 200 and last_action_dt >= dateadd(mi,-15,getdate())
and order_line.order_id = '40020859@@1'
order by last_action_Dt desc


/****for phone activity**/
--select  * from demand_labor where order_id = '40020859@@1'
select comment_text, * from demand where order_id = '40020859@@1' and bpart_id = 'phone_act' and comment_text is not null

--parts ordered
select order_line.order_id, bpart.descr as partname, cast(def_cost as decimal(10,2)) as cost 
from  order_line inner join demand on. order_line.order_id  = demand.order_id and demand.bpart_gcl_id = 'Goods' 
                             inner join  bpart on bpart.bpart_id = demand.bpart_id  
                            where order_line.order_id = '40020859@@1'