CREATE PROCEDURE [dbo].[spTrash]
@UserId int,
@Id int
AS
BEGIN
DECLARE
@AId int=null,
@AUId int =null,
@IsPin BIT=null,
@IsTrash BIT=null

SELECT 
@AId=Id
      ,@AUId = UserId
      ,@IsPin=IsPin
      ,@IsTrash=IsTrash
 FROM FundooNotes
 WHERE UserId = @UserId and Id = @Id
 DECLARE
@UId int = @AUId,
@NId int = @AId,
@Pin BIT = @IsPin,
@Trash BIT = @IsTrash
 --SELECT @UId, @NId,@Pin,@Note,@Archive,@Trash;
 BEGIN
if(@Trash=0)
UPDATE  FundooNotes 
 SET IsTrash=1,
 IsPin=0
 where UserId = @UId and Id = @NId    
	   ELSE
	   UPDATE  FundooNotes 
 SET IsTrash=0
 where UserId = @UId and Id = @NId
 END
END