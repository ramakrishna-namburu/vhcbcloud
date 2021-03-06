﻿CREATE procedure dbo.GetProgramTabsForViability
(
	@ProgramId		int,
	@LKProgramID	int
)
as
begin transaction
--exec GetProgramTabsForViability 6530, 144
	begin try
		declare @ProjectType varchar(40)
		declare @ProgramName varchar(40)

		select @ProjectType =  rtrim(ltrim(lv.Description)) 
		from project p(nolock)
		left join LookupValues lv(nolock) on lv.TypeID = p.LkProjectType
		where ProjectId = @ProgramId

		select @ProgramName =  rtrim(ltrim(lv.Description)) 
		from LookupValues lv(nolock)
		where lv.TypeID = @LKProgramID

		if(@ProgramName = 'Viability' and @ProjectType = 'Viability Implementation Grant')
		begin
			select TabName, URL
			from programtab(nolock)
			where TabName = 'Viability Grants'
			order by taborder
		end
		else if(@ProgramName = 'Viability' and @ProjectType = 'Viability Service Provider')
		begin
			select TabName, URL
			from programtab(nolock)
			where TabName = 'Service Providers'
			order by taborder
		end
		else if(@ProgramName = 'Americorps')
		begin
			if not exists
			(   select 1
				from applicant a(nolock)
				join projectapplicant pa(nolock) on pa.applicantid = a.applicantid
				where pa.LkApplicantRole =  26294 --Americorps Member
				and pa.RowIsActive = 1 
				and pa.ProjectId = @ProgramId
			)
			begin
				select TabName, URL
				from programtab(nolock)
				where LKVHCBProgram = @LKProgramID and TabName not in('Member Tracking')
				order by taborder
			end
			else
			begin
				select TabName, URL
				from programtab(nolock)
				where LKVHCBProgram = @LKProgramID
				order by taborder
			end
		end
		else if(@ProgramName = 'Housing'and @ProjectType = 'HOPWA')
		begin
		 select 'HOPWA' as TabName, 'Housing/HOPWAManagement.aspx' as URL
		end
		else
		begin
			select TabName, URL
			from programtab(nolock)
			where LKVHCBProgram = @LKProgramID and TabName not in('Viability Grants',  'Service Providers')
			order by taborder
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