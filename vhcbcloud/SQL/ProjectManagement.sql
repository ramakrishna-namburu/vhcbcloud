use vhcbsandbox
go

drop procedure [dbo].GetProjectNumbers 
go

create procedure GetProjectNumbers
(
	@ProjectNum varchar(50)
)  
as
--exec GetProjectNameById 6588
begin
	if(CHARINDEX('-', @ProjectNum) > 0)
	begin
		select top 25 proj_num
		from project
		where  proj_num like @ProjectNum+ '%'
	end
	else
	begin
		select top 25 proj_num 
		from project
		where  replace(proj_num, '-', '') like @ProjectNum+ '%'
	end
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectNameById]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectNameById 
go

create procedure GetProjectNameById
(
	@ProjectId int
)  
as
--exec GetProjectNameById 6588
begin

	select rtrim(ltrim(lpn.description)) as ProjectName, p.proj_num as ProjNumber, p.LkProjectType
	from project p(nolock)
	join projectname pn(nolock) on p.projectid = pn.projectid
	join lookupvalues lpn on lpn.typeid = pn.lkprojectname
	where p.ProjectId = @ProjectId and pn.DefName = 1
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[getprojectslist]') and type in (N'P', N'PC'))
drop procedure [dbo].getprojectslist 
go

create procedure getprojectslist  
as
begin

	select p.projectid, proj_num, rtrim(ltrim(lpn.description)) description,  convert(varchar(25), p.projectid) +'|' + rtrim(ltrim(lpn.description)) as project_id_name
	from project p(nolock)
	join projectname pn(nolock) on p.projectid = pn.projectid
	join lookupvalues lpn on lpn.typeid = pn.lkprojectname
	where defname = 1
	order by proj_num
end
go


if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[get_managers]') and type in (N'P', N'PC'))
drop procedure [dbo].get_managers
go

create procedure dbo.get_managers
as	
begin
-- dbo.get_managers
	set nocount on   
	  
	select UserId, ltrim(rtrim(Lname)) + ', ' + ltrim(rtrim(Fname)) Name from userinfo
	order by Lname

	if (@@error <> 0)    
    begin  
        raiserror ( 'get_managers' , 0 ,1)  
        return 1  
    end  
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetBoardDates]') and type in (N'P', N'PC'))
drop procedure [dbo].GetBoardDates
go

create procedure dbo.GetBoardDates
as	
begin
-- dbo.GetBoardDates
	set nocount on   
	  
	select TypeID, convert(varchar(10), BoardDate, 101) as BoardDate1
	from [dbo].[LkBoardDate]
	order by BoardDate desc

	if (@@error <> 0)    
    begin  
        raiserror ( 'get_managers' , 0 ,1)  
        return 1  
    end  
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[add_new_project]') and type in (N'P', N'PC'))
drop procedure [dbo].add_new_project
go

create procedure dbo.add_new_project
(
	@projNum			nvarchar(12),
	@LkProjectType		int,
	@LkProgram			int,
	@Manager			int,
	--@verified			bit,
	@appName			varchar(150),
	@projName			varchar(75),
	@goal				int,
	@IsTBDAddress		bit,
	@isDuplicate		bit output,
	@ProjectId			int output
) as
begin transaction

	begin try

	set @isDuplicate = 1

	 if not exists
        (
			select 1
			from project
			where proj_num = @projnum
        )
		begin

			Declare @nameId as int;
			--Declare @projectId as int;
			declare @recordId int
			declare @applicantId int

			select  @recordId = RecordID from LkLookups where Tablename = 'LkProjectName' 
	
			insert into LookupValues(LookupType, Description)
			values (@recordId, @projName)

			set @nameId = @@IDENTITY

			insert into Project (Proj_num, LkProjectType, LkProgram, Manager, userid, Goal)
			values (@projNum, @LkProjectType, @LkProgram, @Manager, 123, @Goal)
	
			set @ProjectId = @@IDENTITY

			insert into ProjectName (ProjectID, LkProjectname, DefName)
			values (@ProjectId, @nameId, 1)


			Select @applicantId = a.ApplicantId 
			from AppName an(nolock)
			join ApplicantAppName aan(nolock) on aan.appnameid = an.appnameid
			join Applicant a(nolock) on a.ApplicantId = aan.ApplicantID
			where an.Applicantname = @appName

			insert into ProjectApplicant (ProjectId, ApplicantId, LkApplicantRole, IsApplicant)
			values (@ProjectId, @applicantId, 358, 1)

			if(@IsTBDAddress = 1)
			begin
				insert into ProjectAddress(ProjectId, AddressId, PrimaryAdd)
				values(@ProjectId, 171, 1)
			end

			set @isDuplicate = 0
		end
	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
        RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go


if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateProjectInfo]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateProjectInfo
go

create procedure [dbo].[UpdateProjectInfo]
(
	@ProjectId			int,
	@LkProjectType		int,
	@LkProgram			int,
	@Manager			int,
	--@ClosingDate		datetime,
	@verified			bit = false,
	@appName			varchar(150),
	@goal				int
	--@projName			varchar(75)
) as
begin transaction

	begin try

	declare @applicantId int
	declare @CurrentApplicantId int

	update Project set LkProjectType = @LkProjectType, LkProgram = @LkProgram,
		Manager = @Manager, Goal = @goal, verified = @verified, verifieddate = getdate() --ClosingDate = @ClosingDate
	from Project
	where ProjectId = @ProjectId

	Select @applicantId = a.ApplicantId 
	from AppName an(nolock)
	join ApplicantAppName aan(nolock) on aan.appnameid = an.appnameid
	join Applicant a(nolock) on a.ApplicantId = aan.ApplicantID
	where an.Applicantname = @appName

	--Select @applicantId = a.ApplicantId 
	--from AppName an(nolock)
	--join ApplicantAppName aan(nolock) on aan.appnameid = an.appnameid
	--join Applicant a(nolock) on a.ApplicantId = aan.ApplicantID
	--where an.AppNameID = @appNameId

	select @CurrentApplicantId = pa.ApplicantId
	from ProjectApplicant pa
	where projectId = @projectId and pa.LkApplicantRole = 358

	if(isnull(@CurrentApplicantId, '') != @applicantId)
	begin
		--Update Current Primary Applicant
		update pa set LkApplicantRole = '', IsApplicant = 0, FinLegal = 0, DateModified = getdate()
		from ProjectApplicant pa
		where projectId = @projectId and pa.LkApplicantRole = 358

		--Delete If the Entity/Applicant already assigned some time back.
		delete from ProjectApplicant 
		where ProjectId = @ProjectId and ApplicantId = @applicantId

		--Insert New Primary Applicant
		insert into ProjectApplicant (ProjectId, ApplicantId, LkApplicantRole, IsApplicant, FinLegal)
		values (@ProjectId, @applicantId, 358, 1, 1)
	end

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go
GO

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[getProjectDetails]') and type in (N'P', N'PC'))
drop procedure [dbo].getProjectDetails
go

create procedure dbo.getProjectDetails
(
	@ProjectId int
) as
--getProjectDetails 6583
begin transaction

	begin try
	declare @AppNameID int
	declare @AppName varchar(150)
	declare @projectName varchar(75)

	Select @AppNameID =  an.AppNameID, @AppName = an.Applicantname
	from AppName an(nolock)
	join ApplicantAppName aan(nolock) on aan.appnameid = an.appnameid
	join Applicant a(nolock) on a.ApplicantId = aan.ApplicantID
	join ProjectApplicant pa(nolock) on pa.ApplicantId = a.ApplicantId
	where pa.ProjectId = @ProjectId and pa.LkApplicantRole = 358 --Primary Applicant

	select @projectName = rtrim(ltrim(lpn.description))
	from project p(nolock)
	join projectname pn(nolock) on p.projectid = pn.projectid
	join lookupvalues lpn on lpn.typeid = pn.lkprojectname
	where p.ProjectId = @ProjectId and pn.DefName = 1

	select p.Proj_num, @projectName as projectName, p.LkProjectType, p.LkProgram, lv.Description as program,
		p.AppRec, p.LkAppStatus, p.Manager, p.LkBoardDate, p.ClosingDate, p.ExpireDate, p.verified,  p.userid, 
		@AppNameID as AppNameId, @AppName AppName, Goal, p.verified, p.VerifiedDate
	from project p(nolock) 
	left join projectname pn(nolock) on p.projectid = pn.projectid
	left join LookupValues lv(nolock) on lv.TypeID = p.LkProgram 
	where pn.defname = 1 and p.ProjectId = @ProjectId

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[AddProjectName]') and type in (N'P', N'PC'))
drop procedure [dbo].AddProjectName
go

create procedure dbo.AddProjectName
(
	@ProjectId	int,
	@projName	varchar(75),
	@DefName	bit
) as
begin transaction

	begin try
	declare @recordId int
	declare @nameId as int
	declare @ProjectNameID int

	select  @recordId = RecordID from LkLookups where Tablename = 'LkProjectName' 
	
	insert into LookupValues(LookupType, Description)
	values (@recordId, @projName)

	set @nameId = @@IDENTITY

	insert into ProjectName (ProjectID, LkProjectname, DefName)
	values (@ProjectId, @nameId, @DefName)

	set @ProjectNameID = @@IDENTITY

	if(@DefName = 1)
	begin
	 update ProjectName set DefName = 0 where ProjectId = @ProjectId and ProjectNameID != @ProjectNameID
	end

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go


if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectNames]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectNames
go

create procedure dbo.GetProjectNames
(
	@ProjectId		int,
	@IsActiveOnly	bit
) as
--GetProjectNames 6588, 1
begin transaction

	begin try

	select TypeID, Description, 
		case isnull(pn.DefName, '') when '' then 'No' when 0 then 'No' else 'Yes' end DefName, pn.DefName as DefName1,
		lv.RowIsActive
	from LookupValues lv(nolock)
	join ProjectName pn(nolock) on lv.TypeID = pn.LkProjectname
	where pn.ProjectID = @ProjectId 
		and (@IsActiveOnly = 0 or lv.RowIsActive = @IsActiveOnly)
	order by pn.DefName desc, pn.DateModified desc

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateProjectName]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateProjectName
go

create procedure dbo.UpdateProjectName
(
	@ProjectId		int,
	@TypeId			int,
	@ProjectName	varchar(70),
	@DefName		bit,
	@RowIsActive	bit
) as
begin transaction

	begin try
	
	update lookupvalues set description = @projectName, RowIsActive = @RowIsActive
	where TypeId = @TypeId

	if(@DefName = 1 and @RowIsActive = 1)
	begin
	 update ProjectName set DefName = 0 where ProjectId = @ProjectId
	end

	if(@RowIsActive = 1)
	begin
		update ProjectName set DefName = @DefName, DateModified = getdate()
		where LKProjectName = @TypeId and ProjectId = @ProjectId
	end
		
	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go


if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetAddressDetails]') and type in (N'P', N'PC'))
drop procedure [dbo].GetAddressDetails
go 

create procedure GetAddressDetails
(
	@StreetNo	nvarchar(24)
	--@Address1	nvarchar(120)	
)
as 
Begin

	select  distinct top 10 Street#, Address1, isnull(Address2, '') Address2,
	case when convert(decimal(10,7), isnull(latitude, '')) = 0 then '' else convert(varchar, convert(decimal(10,7), isnull(latitude, ''))) end latitude,
	case when convert(decimal(10,7), isnull(longitude, '')) = 0 then '' else convert(varchar, convert(decimal(10,7), isnull(longitude, ''))) end longitude, 
	isnull(Town, '' ) Town, State, Zip, isnull(County, '') County, isnull(Village, '') Village 
	from address(nolock)
	where Street# like @StreetNo +'%'
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[AddNewProjectAddress]') and type in (N'P', N'PC'))
drop procedure [dbo].AddNewProjectAddress
go

create procedure dbo.AddNewProjectAddress
(
	@ProjectId	int,
	@StreetNo nvarchar(24),
	@Address1 nvarchar(120),
	@Address2 nvarchar(120),
	@Town nvarchar(100),
	@Village nvarchar(35),
	@State nchar(4),
	@Zip nchar(20),
	@County nvarchar(40),
	@latitude float,
	@longitude	float,
	--@IsActive bit,
	@DefAddress bit,
	@LkAddressType	int,
	@isDuplicate	bit output,
	@isActive		bit Output
) as
begin transaction

	begin try
	declare @AddressId int;
	declare @ProjectAddressId int;

	set @isDuplicate = 1
	set @isActive = 1

	if not exists
    (
		select 1 
		from Address a(nolock) 
		join ProjectAddress pa(nolock) on a.AddressId = pa.AddressId
		where a.Street# = @StreetNo and a.Address1 = @Address1 and Town = @Town and pa.ProjectId = @ProjectId
	)
	begin
		insert into [Address] (LkAddressType, Street#, Address1, Address2, Town, State, Zip, County, latitude, longitude, Village, UserID)
		values(@LkAddressType, @StreetNo, @Address1, @Address2, @Town, @State, @Zip, @County, @latitude, @longitude, @Village, 123)

		set @AddressId = @@identity	

		insert into ProjectAddress(ProjectId, AddressId, PrimaryAdd, DateModified)
		values(@ProjectId, @AddressId, @DefAddress, getdate())

		set @ProjectAddressId = @@identity

		if(@DefAddress = 1)
		begin
		 update ProjectAddress set PrimaryAdd = 0 where ProjectId = @ProjectId and ProjectAddressID != @ProjectAddressId
		end

		set @isDuplicate = 0
	end

	if(@isDuplicate = 1)
	begin
		select @isActive =  a.RowIsActive 
		from Address a(nolock) 
		join ProjectAddress pa(nolock) on a.AddressId = pa.AddressId
		where a.Street# = @StreetNo and a.Address1 = @Address1 and Town = @Town and pa.ProjectId = @ProjectId
	end

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateProjectAddress]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateProjectAddress
go

create procedure [dbo].UpdateProjectAddress
(
	@ProjectId int,
	@AddressId int,
	@LkAddressType	int,
	@StreetNo nvarchar(24),
	@Address1 nvarchar(120),
	@Address2 nvarchar(120),
	@Town nvarchar(100),
	@Village nvarchar(35),
	@State nchar(4),
	@Zip nchar(20),
	@County nvarchar(40),
	@latitude float,
	@longitude	float,
	@IsActive bit,
	@DefAddress bit	
)
as
begin transaction

	begin try

	update Address
		set Street# = @StreetNo,
		LkAddressType = @LkAddressType,
		Address1 = @Address1,
		Address2 = @Address2,
		Village = @Village,
		Town = @Town,
		State = @State,
		Zip = @Zip,
		County = @County,
		latitude = @latitude,
		longitude = @longitude,
		RowIsActive = @IsActive,
		DateModified = getdate()
	from Address
	where AddressId= @AddressId

	if(@Defaddress = 1 and @IsActive = 1)
	begin
	 update ProjectAddress set PrimaryAdd = 0 where ProjectId = @ProjectId
	end
	
	update ProjectAddress
	set PrimaryAdd = @Defaddress
	from ProjectAddress
	where ProjectId = @ProjectId and AddressId= @AddressId

	end try
		begin catch
			if @@trancount > 0
			rollback transaction;

			DECLARE @msg nvarchar(4000) = error_message()
		  RAISERROR (@msg, 16, 1)
			return 1  
		end catch

		if @@trancount > 0
			commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectAddressDetailsById]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectAddressDetailsById
go

create procedure [dbo].GetProjectAddressDetailsById
(
	@ProjectId int,
	@AddressId int
)
as 
--exec GetProjectAddressDetailsById 20, 20
Begin

	select a.AddressId, a.LkAddressType, isnull(a.Street#, '') as Street#, isnull(a.Address1, '') as Address1, isnull(a.Address2, '') as Address2, 
	isnull(a.latitude, '') as latitude, isnull(a.longitude, '') as longitude, isnull(a.Town, '') as Town, isnull(a.State, '') as State, isnull(a.Zip, null) as Zip, 
	isnull(a.County, '') as County, isnull(Village, '') as Village,
	a.RowIsActive, pa.PrimaryAdd
	from projectAddress pa(nolock) 
	join Address a(nolock) on a.Addressid = pa.AddressId
	where a.AddressId= @AddressId and pa.ProjectId = @ProjectId
end
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectAddressList]') and type in (N'P', N'PC'))
drop procedure [dbo].[GetProjectAddressList]
go

create procedure dbo.GetProjectAddressList
(
	@ProjectId		int,
	@IsActiveOnly	bit
)
as 
--exec GetProjectAddressList 6588, 0
Begin

	select a.LkAddressType, 
	case (rtrim(ltrim(lv.description))) when 'Physical Location'then 'Physical'
	else  (rtrim(ltrim(lv.description))) end as AddressType, 
	a.AddressId, a.Street#, a.Address1, a.Address2, a.latitude, a.longitude, a.Town, a.State, a.Zip, a.County, 
	case isnull(pa.PrimaryAdd, '') when '' then 'No' when 0 then 'No' else 'Yes' end PrimaryAdd, a.RowIsActive
	from ProjectAddress pa(nolock) 
	join Address a(nolock) on a.Addressid = pa.AddressId
	left join LookupValues lv(nolock) on lv.typeid = a.LkAddressType
	where pa.ProjectId = @ProjectId
		and (@IsActiveOnly = 0 or a.RowIsActive = @IsActiveOnly)
	order by pa.PrimaryAdd desc, pa.DateModified desc
end
go

--@@@@@@@@@@@@@@@@@@ Project Applicant @@@@@@@@@@@@@@@@@@

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[AddProjectApplicant]') and type in (N'P', N'PC'))
drop procedure [dbo].AddProjectApplicant
go

create procedure dbo.AddProjectApplicant
(
	@ProjectId			int,
	@ApplicantID		int,
	@LkApplicantRole	int,
	@IsApplicant		bit
	--@Defapp				bit, 
	--@FinLegal			bit	
) as
begin transaction

	begin try

	--declare @ApplicantId int

	--select @ApplicantId = ApplicantID from ApplicantAppName(nolock) where AppNameID = @AppNameId

	insert into ProjectApplicant(ProjectId, ApplicantId,  LkApplicantRole, IsApplicant, UserID)--, Defapp, FinLegal,
	values(@ProjectId, @ApplicantId,  @LkApplicantRole, @IsApplicant, 123)-- 358 @LkApplicantRole, @Defapp, @IsApplicant, @FinLegal,

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateProjectApplicant]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateProjectApplicant
go

create procedure dbo.UpdateProjectApplicant
(
	@ProjectApplicantId	int,
	@IsApplicant		bit, 
	@IsFinLegal			bit,
	@LkApplicantRole	int,
	@IsRowIsActive		bit
) as
begin transaction

	begin try

	update ProjectApplicant set IsApplicant = @IsApplicant, FinLegal = @IsFinLegal, 
		LkApplicantRole = @LkApplicantRole, RowIsActive = @IsRowIsActive, DateModified = getdate()
	where ProjectApplicantId = @ProjectApplicantId
	
	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectApplicantList]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectApplicantList
go

create procedure dbo.GetProjectApplicantList
(
	@ProjectId		int,
	@IsActiveOnly	bit
) as
begin transaction
-- GetProjectApplicantList 6586, 1
	begin try

	select pa.ProjectApplicantID, 
			case isnull(pa.IsApplicant, 0) when 0 then 'No' else 'Yes' end IsApplicant1,
			case isnull(pa.FinLegal, 0) when 0 then 'No' else 'Yes' end FinLegal1,
			isnull(pa.IsApplicant, 0) as IsApplicant, 
			isnull(pa.FinLegal, 0) as FinLegal,
			pa.LkApplicantRole, lv.Description as 'ApplicantRoleDescription',
			pa.RowIsActive,
			a.ApplicantId, a.Individual, a.LkEntityType, a.FYend, a.website, a.Stvendid, a.LkPhoneType, a.Phone, a.email, 
			an.applicantname,
			--c.LkPrefix, c.Firstname, c.Lastname, c.LkSuffix, c.LkPosition, c.Title, 
			--ac.ApplicantID, ac.ContactID, ac.DfltCont, 
			aan.appnameid, aan.defname
		from ProjectApplicant pa(nolock)
		join applicantappname aan(nolock) on pa.ApplicantId = aan.ApplicantID
		join appname an(nolock) on aan.appnameid = an.appnameid
		join applicant a(nolock) on a.applicantid = aan.applicantid
		--left join applicantcontact ac(nolock) on a.ApplicantID = ac.ApplicantID
		--left join contact c(nolock) on c.ContactID = ac.ContactID
		left join LookupValues lv(nolock) on lv.TypeID = pa.LkApplicantRole
		where pa.ProjectId = @ProjectId
			and (@IsActiveOnly = 0 or pa.RowIsActive = @IsActiveOnly)
		order by pa.DateModified desc
	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

--@@@@@@@@@@@@@@@@@@ Related Projects @@@@@@@@@@@@@@@@@

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[AddRelatedProject]') and type in (N'P', N'PC'))
drop procedure [dbo].AddRelatedProject
go

create procedure dbo.AddRelatedProject
(
	@ProjectId			int,
	@RelProjectId		int,
	@isDuplicate		bit output,
	@isActive			bit Output

) as
begin transaction

	begin try

	set @isDuplicate = 1
	set @isActive = 1

	 if not exists
        (
			select 1
			from projectrelated(nolock)
			where ProjectID = @ProjectId and RelProjectID = @RelProjectId
        )
		begin
			insert into projectrelated(ProjectID, RelProjectID)
			values(@ProjectId, @RelProjectId)

			insert into projectrelated(ProjectID, RelProjectID)
			values(@RelProjectId, @ProjectId)

			set @isDuplicate = 0
		end

	if(@isDuplicate = 1)
	begin
		select @isActive =  RowIsActive
		from projectrelated(nolock)
			where ProjectID = @ProjectId and RelProjectID = @RelProjectId
	end
	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go


if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateRelatedProject]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateRelatedProject
go

create procedure dbo.UpdateRelatedProject
(
	@ProjectId			int,
	@RelProjectId		int,
	@RowIsActive		bit

) as
begin transaction

	begin try

	update projectrelated set RowIsActive = @RowIsActive
	where  ProjectID = @ProjectID and RelProjectId = @RelProjectId

	update projectrelated set RowIsActive = @RowIsActive
	where  ProjectID = @RelProjectId and RelProjectId = @ProjectID

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetRelatedProjectList]') and type in (N'P', N'PC'))
drop procedure [dbo].GetRelatedProjectList
go

create procedure dbo.GetRelatedProjectList
(
	@ProjectId		int,
	@IsActiveOnly	bit
) as
begin transaction
--exec GetRelatedProjectList 6588
	begin try

	select  pr.RelProjectId, p.Proj_num, lv.Description as ProjectName, pr.RowIsActive, p.LkProgram, lv1.Description as Program
	from projectrelated pr(nolock)
	join project p(nolock) on pr.RelProjectId = p.ProjectId
	join projectname pn(nolock) on pr.RelProjectId = pn.ProjectID
	join LookupValues lv(nolock) on pn.LkProjectName = lv.TypeId
	join  LookupValues lv1(nolock) on p.LkProgram = lv1.TypeId
	where pn.DefName = 1 and pr.ProjectId = @ProjectId
		and (@IsActiveOnly = 0 or pr.RowIsActive = @IsActiveOnly)
	order by pr.DateModified desc

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

--@@@@@@@@@@@@@@@@@@ Project Status @@@@@@@@@@@@@@@@@

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectStatusList]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectStatusList
go

create procedure dbo.GetProjectStatusList
(
	@ProjectId		int,
	@IsActiveOnly	bit
) as
begin transaction
--exec GetProjectStatusList 6588, 0
	begin try
	
	select ProjectStatusID, lv.Description as ProjectStatus, LKProjStatus,
		convert(varchar(10), StatusDate, 101) as StatusDate,
		ps.RowIsActive
	from ProjectStatus ps(nolock)
	join lookupvalues lv(nolock) on ps.LKProjStatus = lv.TypeID
	where ps.ProjectId = @ProjectId
		and (@IsActiveOnly = 0 or ps.RowIsActive = @IsActiveOnly)
	order by ps.DateModified desc

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[AddProjectStatus]') and type in (N'P', N'PC'))
drop procedure [dbo].AddProjectStatus
go

create procedure dbo.AddProjectStatus
(
	@ProjectId			int,
	@LKProjStatus		int,
	@StatusDate			datetime
) as
begin transaction

	begin try

	insert into ProjectStatus(ProjectID, LKProjStatus, StatusDate)
	values(@ProjectID, @LKProjStatus, @StatusDate)

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[UpdateProjectStatus]') and type in (N'P', N'PC'))
drop procedure [dbo].UpdateProjectStatus
go

create procedure dbo.UpdateProjectStatus
(
	@ProjectStatusId	int,
	@LKProjStatus		int,
	@StatusDate			DateTime, 
	@isActive			bit
) as
begin transaction

	begin try

	update ProjectStatus set LKProjStatus = @LKProjStatus,  StatusDate = @StatusDate, RowIsActive = @isActive
	where ProjectStatusId = @ProjectStatusId

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go
---@@@@@@@@@@@@@@ Villages @@@@@@@@@@@@@@@@@@@@@@
if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetVillages]') and type in (N'P', N'PC'))
drop procedure [dbo].GetVillages
go

create procedure dbo.GetVillages
(
	@zip	int
) as
begin transaction
--exec GetVillages 05041
	begin try

	select village from village_v where zip = @zip

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetAllVillages]') and type in (N'P', N'PC'))
drop procedure [dbo].GetAllVillages
go

create procedure dbo.GetAllVillages
(
	@VillagePrefix	varchar(50)
) as
begin transaction
--exec GetllAllVillages 'a'
	begin try

	select top 10 village from village_v where village like @VillagePrefix  + '%'

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;

go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[IsProjectNumberExist]') and type in (N'P', N'PC'))
drop procedure [dbo].IsProjectNumberExist
go

create procedure dbo.IsProjectNumberExist
(
	@ProjectNumber	nvarchar(12),
	@isDuplicate	bit output
) as
begin transaction
--exec IsProjectNumberExist '1111-111-111', null
	begin try

	set @isDuplicate = 1

	 if not exists
        (
			select 1
			from project
			where proj_num = @ProjectNumber
        )
		begin
			set @isDuplicate = 0
		end

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetCurrentProjectApplicants]') and type in (N'P', N'PC'))
drop procedure [dbo].GetCurrentProjectApplicants
go

create procedure dbo.GetCurrentProjectApplicants
(
	@ProjectId		int
) as
begin transaction
--exec GetCurrentProjectApplicants 6588
	begin try

	select
		an.applicantname,
		aan.appnameid
	from ProjectApplicant pa(nolock)
	join applicantappname aan(nolock) on pa.ApplicantId = aan.ApplicantID
	join appname an(nolock) on aan.appnameid = an.appnameid
	where pa.ProjectId = @ProjectId and pa.RowIsActive =1
	order by an.applicantname
end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetApplicantAppRole]') and type in (N'P', N'PC'))
drop procedure [dbo].GetApplicantAppRole
go

create procedure dbo.GetApplicantAppRole
(
	@AppNameId		int
) as
begin transaction
--exec GetApplicantAppRole 416
begin try
	declare @AppRole varchar(5)
	set @AppRole = '';
	select @AppRole =  isnull(a.AppRole, '')
	from applicantappname aan(nolock) 
	join appname an(nolock) on aan.appnameid = an.appnameid
	join applicant a(nolock) on a.applicantid = aan.applicantid
	where aan.AppNameID =  @AppNameId

	select @AppRole as AppRole

end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetNextProjectId]') and type in (N'P', N'PC'))
drop procedure [dbo].GetNextProjectId
go

create procedure dbo.GetNextProjectId
(
	@Proj_num			varchar(50),
	@Action				int, --1: Next 2:Previous
	@ReturnProjectId	int output
)
as
begin transaction
--exec GetNextProjectId '0000-000-000', 1, null
	begin try
	
	create table #Temp
	(
		RowNumber	int, 
		Proj_num	varchar(50),
		ProjectId	int
	)

	declare @CurrentRowNumber int

	insert into #Temp 
	select row_number() over (order by Proj_num) as RowNumber, Proj_num, ProjectId
	from project

	--select * from #Temp
	select @CurrentRowNumber = RowNumber from #Temp where Proj_num = @Proj_num

	if(@Action = 1)
	begin
		declare @MaxRowNumber int
		select @MaxRowNumber = max(RowNumber) from #Temp

		if(@CurrentRowNumber + 1 <= @MaxRowNumber)
			select @ReturnProjectId = ProjectId from #Temp where RowNumber = @CurrentRowNumber + 1
		else
			select @ReturnProjectId = ProjectId from #Temp where RowNumber = @CurrentRowNumber
	end
	else
	begin
		declare @MinRowNumber int
		select @MinRowNumber = min(RowNumber) from #Temp
		--select @MinRowNumber
		--select @CurrentRowNumber 'Current'
		if(@CurrentRowNumber - 1 < @MinRowNumber)
			select @ReturnProjectId = ProjectId from #Temp where RowNumber = @CurrentRowNumber
		else
			select @ReturnProjectId = ProjectId from #Temp where RowNumber = @CurrentRowNumber - 1
	end

	drop table #Temp

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
		RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectNum]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectNum
go

create procedure dbo.GetProjectNum
(
	@ProjectId			int
) as
begin transaction
-- exec GetProjectNum 4834
	begin try

	select proj_num from Project where ProjectId = @ProjectId
		

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

if  exists (select * from sys.objects where object_id = object_id(N'[dbo].[GetProjectId]') and type in (N'P', N'PC'))
drop procedure [dbo].GetProjectId
go

create procedure dbo.GetProjectId
(
	@ProjectNum		nvarchar(20),
	@ProjectId		int output 
) as
begin transaction
-- exec GetProjectId '2001-001-001', ''
	begin try

	declare @ProjNum1 varchar(50)
	select @ProjNum1 = dbo.GetHyphenProjectNumber(@ProjectNum);

	select @ProjectId = ProjectId from Project where proj_num = @ProjNum1
		

	end try
	begin catch
		if @@trancount > 0
		rollback transaction;

		DECLARE @msg nvarchar(4000) = error_message()
      RAISERROR (@msg, 16, 1)
		return 1  
	end catch

	if @@trancount > 0
		commit transaction;
go

drop procedure [dbo].GetProjectNumbersWithName
go

create procedure GetProjectNumbersWithName
(
	@ProjectNum varchar(50)
)  
as
--exec GetProjectNumbersWithName 2016
begin
	if(CHARINDEX('-', @ProjectNum) > 0)
	begin
		select top 25 proj_num, rtrim(ltrim(project_name)) as project_name, convert(varchar(25), proj_num) +'|' + rtrim(ltrim(project_name)) as project_num_name
		from project_v
		where  proj_num like @ProjectNum+ '%' and defname = 1
	end
	else
	begin
		select top 25 proj_num, rtrim(ltrim(project_name)) as project_name, convert(varchar(25), proj_num) +'|' + rtrim(ltrim(project_name)) as project_num_name
		from project_v
		where  replace(proj_num, '-', '') like @ProjectNum+ '%' and defname = 1
	end
end
go


