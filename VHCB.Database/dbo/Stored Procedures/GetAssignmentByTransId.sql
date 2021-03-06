﻿CREATE procedure [dbo].[GetAssignmentByTransId]
(	
	@TransID int
	--@fromProjId int
	--,@toTransId int
)
as
Begin	
	
	select d.projectid, p.proj_num, d.detailid, f.FundId, f.account, f.name, format(d.Amount, 'N2') as amount, lv.Description, 
		d.LkTransType, t.LkTransaction,t.TransId, d.DetailGuId as projguid, t.datemodified 
	from TransAssign ta(nolock)
	join Trans t(nolock) on ta.ToTransID = t.TransId
	join Detail d(nolock) on t.transid = d.transid
	join project p on p.projectid = d.projectid
	join LookupValues lv on lv.TypeID = d.LkTransType
	join fund f on d.FundId = f.FundId
	where ta.TransID =  @TransID
	order by d.DateModified desc

	--select d.projectid, p.proj_num, d.detailid, f.FundId, f.account, f.name, format(d.Amount, 'N2') as amount, lv.Description, 
	--	d.LkTransType, t.LkTransaction,t.TransId, d.DetailGuId as projguid, t.datemodified
	--from Detail d 
	--	join fund f on d.FundId = f.FundId
	--	join Trans t on t.TransId = d.TransId
	--	join project p on p.projectid = d.projectid
	--	join LookupValues lv on lv.TypeID = d.LkTransType
	--where d.TransId = @TransID and d.Amount > 0 
	--order by d.DateModified desc

	--declare  @temp table ( transid int, toProjId int, projGuid varchar(100) )
	--insert into @temp (transid, toProjId, projGuid)
	--select FromTransID, ToProjectId, ReallocateGUID from ReallocateLink where FromProjectId = @fromProjId
	--insert into @temp (transid, toProjId, projGuid)
	--select ToTransID, ToProjectId, ReallocateGUID from ReallocateLink  where FromProjectId = @fromProjId

	--declare @tblDistinct  table (transid int, projGuid varchar(100))
	--insert into @tblDistinct (transid, projGuid)
	--select distinct transid, projguid from @temp
	
	--Select t.projectid, p.proj_num, d.detailid, f.FundId, f.account, f.name, format(d.Amount, 'N2') as amount, lv.Description, d.LkTransType, t.LkTransaction,t.TransId 
	--	,td.projguid, t.datemodified
	--	 from Fund f 
	--	join Detail d on d.FundId = f.FundId
	--	join Trans t on t.TransId = d.TransId
	--	join project p on p.projectid = t.projectid
	--	join LookupValues lv on lv.TypeID = d.LkTransType
	--	join @tblDistinct td on td.transid = t.TransId
	--	--right outer join #temp te on te.transid = t.transid
	--Where     f.RowIsActive=1 and t.LkTransaction = 26552 and t.lkstatus = 261
	--and t.TransId in(select distinct transid from @temp)
	-- --and p.ProjectId in (select distinct toprojid from #temp)
	--order by d.DateModified desc

End