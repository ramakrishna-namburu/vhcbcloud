﻿
create procedure dbo.UpdateEntityNotes
(
	@EntityNotesID int,
	@LkCategory		int, 
	@Notes			nvarchar(max),
	@URL			nvarchar(1500),
	@RowIsActive	bit
)
as
begin transaction

	begin try
		update EntityNotes set LkCategory = @LkCategory, Notes = @Notes, URL = @URL, RowIsActive = @RowIsActive, DateModified = getdate()
		from EntityNotes 
		where EntityNotesID = @EntityNotesID

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