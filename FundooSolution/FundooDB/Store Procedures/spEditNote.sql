CREATE PROCEDURE [dbo].[spEditNote] 
	@Id int,
   @Title VARCHAR(100)=NULL,
   @MeassageDescription VARCHAR(1000)=NULL,
   @NoteImage VARCHAR(100)=NULL,
   @Color VARCHAR(20)=NULL,
   @ModifiedDateTime DATETIME,
   @UserId int
   AS
   BEGIN
   Update [FundooNotes]
set    Title = IsNull(@Title, Title),
       MeassageDescription = IsNull(@MeassageDescription, MeassageDescription),
	   NoteImage = IsNull(@NoteImage, NoteImage),
	   Color = IsNull(@Color, Color),
	   ModifiedDateTime = IsNull(@ModifiedDateTime, ModifiedDateTime)
Where
Id =@Id AND UserId = @UserId;
END