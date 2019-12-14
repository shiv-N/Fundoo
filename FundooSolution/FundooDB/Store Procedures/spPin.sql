﻿CREATE PROCEDURE [dbo].[spPin]
@UserId int,
@Id int
AS
BEGIN
DECLARE
@AId int=null,
@AUId int =null,
@IsPin BIT=null,
@IsNote BIT=null,
@IsArchive BIT=null,
@IsTrash BIT=null

SELECT 
@AId=Id
      ,@AUId = UserId
      ,@IsPin=IsPin
      ,@IsNote=IsNote
      ,@IsArchive=IsArchive
      ,@IsTrash=IsTrash
 FROM FundooNotes
 WHERE UserId = @UserId and Id = @Id
 DECLARE
@UId int = @AUId,
@NId int = @AId,
@Pin BIT = @IsPin,
@Note BIT = @IsNote,
@Archive BIT = @IsArchive,
@Trash BIT = @IsTrash
 --SELECT @UId, @NId,@Pin,@Note,@Archive,@Trash;
 BEGIN
if(@Pin=0 AND @Trash=0)
UPDATE  FundooNotes 
 SET IsPin=1,
 IsNote=1,
 IsArchive=0
 where UserId = @UId and Id = @NId    
	   ELSE
	   UPDATE  FundooNotes 
 SET IsPin=0,
 IsNote = 1 
 where UserId = @UId and Id = @NId
 END
END