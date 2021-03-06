

alter procedure [dbo].[GetVHCBUser]
as
begin
	select ui.userid,  (LNAME+', '+FNAME) as Name, ui.Username, ui.password, ui.email 
		from UserInfo ui(nolock)	where rowisactive = 1	
	 order by ui.Lname  
end

go

ALTER procedure [dbo].[GetUserSecurityGroup]
as
Begin
	select usergroupid, userGroupName  from UserSecurityGroup where rowisactive = 1
end
go

alter procedure AddUsersToSecurityGroup
(
	@userid int,
	@usergroupid int
)
as
Begin
	insert into UsersUserSecurityGroup (userid, usergroupid) 
		values (@userid, @usergroupid)
End
go

alter procedure GetUsersUserSecurityGroup
as
Begin

	select ui.userid,  (ui.LNAME+', '+ui.FNAME) as Name, usg.usergroupname, uus.usergroupid,uus.UsersUserSecurityGrpId
	from UserInfo ui join UsersUserSecurityGroup uus on uus.userid = ui.userid
	join UserSecurityGroup usg on uus.UserGroupId = usg.UserGroupId
	where ui.rowisactive = 1

end
go

alter procedure DeleteUsersUserSecurityGroup
(
	@UsersUserSecurityGrpId int
)
as
Begin
	Delete from UsersUserSecurityGroup where UsersUserSecurityGrpId = @UsersUserSecurityGrpId
End
go


alter procedure GetPageSecurityBySelection
(
	@recordId int
)
as
Begin
	select typeid, description from lookupvalues 
	where lookuptype = @recordid
End
go

alter procedure AddUserPageSecurity
(
	@userid int,
	@pageid int
)
as 
Begin
	insert into UserPageSecurity(userid,pageid)
	values (@userid, @pageid)
end
go

alter procedure GetuserPageSecurity
(
	@userid int
)
as
Begin
	select distinct ups.PageSecurityId, ui.userid,  (ui.LNAME+', '+ui.FNAME) as username, 		
		(select description from LookupValues where typeid = ups.pageid) pagedesc		
		from UserPageSecurity ups join UserInfo ui on ui.UserId = ups.Userid
	where ui.UserId = @userid
End

go

alter procedure DeletePageSecurity
(
	@pagesecurityid int
)
as
Begin
	Delete from UserPageSecurity where pagesecurityid = @pagesecurityid
End

go
alter procedure IsValidUser
(
	@Username			varchar(100),
	@Password			varchar(40),
	@ReturnData			varchar(5) output
)
as
begin
--exec IsValidUser 'aduffy', 'Vhcb123!', ''

	declare @IsValidUser bit, @IsFirstTimeUser bit, @userid int

	if exists(select 1 from UserInfo(nolock) where Username = @Username and password = @password)
		select @IsValidUser = 1, @IsFirstTimeUser = IsFirstTimeUser, @userid = userid from UserInfo(nolock) where Username = @Username and password = @password
	else
		select @IsValidUser = 0, @IsFirstTimeUser = 0, @userid = 0

	select @ReturnData = convert(varchar(1), @IsValidUser) + '|' + convert(varchar(1), @IsFirstTimeUser ) + '|' + convert(varchar(10), @userid)
end
go


alter procedure GetMasterPageSecurity
(
	@userid int
)
as
Begin
	select distinct ui.userid,  (ui.LNAME+', '+ui.FNAME) as username, 
		 case when lv.lookuptype = 193 then lv.Description else '' end 'PageDescription'		
	from UserInfo ui join UserPageSecurity ups on ups.Userid = ui.UserId
		left join LookupValues lv on lv.lookuptype in (193, 194, 195)
	where ui.UserId = @userid
End
go

alter procedure GetUserSecurityByUserId
(
	@userid int
)
as
Begin
	select ui.userid,  (ui.LNAME+', '+ui.FNAME) as Name, ui.dfltprg, usg.usergroupname, usg.UserGroupId
	from UserInfo ui join UserSecurityGroup usg on ui.securityLevel = usg.UserGroupId  
	where ui.rowisactive = 1 and ui.userid = @userid  
	
end
go

alter procedure GetManagerByProjId
(
	@projId int
)
as
begin
	select projectid, manager from Project where RowIsActive = 1 and projectid = @projId
end
go

alter procedure GetProjectsByProgram
(
	@progId int,
	@projId int
)
as
begin
	select projectid, lkprogram from Project where RowIsActive = 1 and lkprogram = @progId and projectid = @projId
end
go