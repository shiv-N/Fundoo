CREATE PROCEDURE spUpdateImage
(
@Id int,
@NoteImage VARCHAR(100),
@UserId int
)
As
Begin

UPDATE FundooNotes
SET NoteImage = @NoteImage
Where Id=@Id and UserId=@UserId;
end