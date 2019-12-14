CREATE PROCEDURE [dbo].[spColour]
@UserId int,
@Id int,
@Color VARCHAR(20),
@ModifiedDateTime DATETIME
AS  
BEGIN  
 UPDATE  FundooNotes 
 SET Color=@Color,
 ModifiedDateTime=@ModifiedDateTime
 where UserId = @UserId and Id = @Id;  
END