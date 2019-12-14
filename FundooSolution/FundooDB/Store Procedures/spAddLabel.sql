CREATE PROCEDURE [dbo].[spAddLabel] 
   @LabelName VARCHAR(50),
   @UserId int,
   @CreatedDateTime DATETIME 
AS  
BEGIN  
 INSERT INTO Label
 (LabelName,
 UserId, 
 CreatedDateTime)  
VALUES (@LabelName,
   @UserId,
   @CreatedDateTime);  
END