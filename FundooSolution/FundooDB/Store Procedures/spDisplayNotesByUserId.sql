CREATE PROCEDURE spDisplayNotesByUserId
@UserId int
AS
BEGIN
SELECT 
Id
      ,Title
      ,MeassageDescription
      ,NoteImage
      ,Color
      ,CreatedDATETime
      ,ModifiedDateTime
      ,AddReminder
      ,UserId
      ,IsPin
      ,IsNote
      ,IsArchive
      ,IsTrash
 FROM FundooNotes
 WHERE UserId = @UserId;
END