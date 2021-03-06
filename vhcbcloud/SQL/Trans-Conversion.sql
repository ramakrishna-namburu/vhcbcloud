use PTConvert
go

sp_fkeys @fktable_name='Trans'

select * from [dbo].[pttrans]
go

select * from [dbo].[Trans]
go

select * from Detail
go

truncate table Trans
go

truncate table Detail
go

--drop table Trans
--go

--drop table Detail
--go


insert into Trans(ProjectID, Date, TransAmt, LkTransaction, LkStatus, TRANSKEY)
select p.ProjectId, t.Date, Total as TransAmt,
case t.Action
	when 'Decommitment' then 239
	when 'Commitment' then 238
	when 'Reallocation' then 240
	when 'Disbursement' then 236
	when 'Refund' then 237
	else 0
end as LKTransaction, 262 as LKStatus, t.[Key]
from pttrans t(nolock) --dbftest$ t(nolock)
join Project p(nolock) on t.ProjKey = p.Proj_num  --26146
where t.Date is not null
go
