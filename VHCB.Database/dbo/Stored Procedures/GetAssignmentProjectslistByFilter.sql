﻿CREATE procedure GetAssignmentProjectslistByFilter 
(
	@filter varchar(20)
)
as
begin

	select distinct  top 35 p.Proj_num
	from project p(nolock)	
	join trans tr on tr.projectid = p.projectid
	where tr.lkstatus = 261 and tr.LkTransaction = 26552
		and tr.RowIsActive=1 and p.Proj_num like @filter +'%'	
	order by p.proj_num 
end