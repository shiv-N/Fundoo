CREATE PROCEDURE [dbo].[spReminder]
@UserId int,
@Id int,
@AddReminder DATETIME,
@ModifiedDateTime DATETIME
AS  
BEGIN  
 UPDATE  FundooNotes 
 SET AddReminder=@AddReminder,
 ModifiedDateTime=@ModifiedDateTime
 where UserId = @UserId and Id = @Id;  
END