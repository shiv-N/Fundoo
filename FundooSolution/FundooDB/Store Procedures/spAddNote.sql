CREATE PROCEDURE [dbo].[spAddNote] 
   @Title VARCHAR(100)=null,
   @MeassageDescription VARCHAR(1000),
   @NoteImage VARCHAR(100)=null,
   @Color VARCHAR(20)=null,
   @CreatedDATETime DATETIME,
   @ModifiedDateTime DATETIME=null,
   @AddReminder DATETIME,
   @UserId int,
   @IsPin Bit,
   @IsNote bit,
   @IsArchive bit,
   @IsTrash bit  
AS  
BEGIN  
 INSERT INTO FundooNotes
 (Title,
 MeassageDescription, 
 NoteImage,
 Color,
 CreatedDATETime,
 ModifiedDateTime,
 AddReminder,
 UserId, 
 IsPin, 
 IsNote,
 IsArchive,
 IsTrash)  
VALUES (@Title,
   @MeassageDescription,
   @NoteImage,
   @Color,
   @CreatedDATETime,
   @ModifiedDateTime,
   @AddReminder,
   @UserId,
   @IsPin,
   @IsNote,
   @IsArchive,
   @IsTrash);  
END