use PTConvert
go

truncate table VHCB.dbo.Housing
go


DECLARE @ProjectId as int, @HousingId as int, @hntm int, @hsptot int, @hbldgs int, @hsqft int

declare NewCursor Cursor for
select mp.[ProjectId], hntm, hsptot, hbldgs, hsqft
from pthousingua ptHouse(nolock)
join ptapplctn ap(nolock) on ap.[key] = ptHouse.applctnkey
join MasterProj mp(nolock) on ap.[projkey] = mp.[key]

open NewCursor
fetch next from NewCursor into @ProjectId, @hntm, @hsptot, @hbldgs, @hsqft
WHILE @@FETCH_STATUS = 0
begin
	insert into  VHCB.dbo.Housing(ProjectID, NewUnits, ServSuppUnits, Bldgs, Hsqft)
	values(@ProjectId, @hntm, @hsptot, @hbldgs, @hsqft)

	FETCH NEXT FROM NewCursor INTO @ProjectId, @hntm, @hsptot, @hbldgs, @hsqft
END

Close NewCursor
deallocate NewCursor
go

