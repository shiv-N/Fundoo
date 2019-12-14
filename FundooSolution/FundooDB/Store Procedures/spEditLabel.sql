CREATE PROCEDURE spEditLabel 
	@Id int,
   @UserId int,
   @LabelName VARCHAR(50)=null,
   @ModifiedDateTime datetime
   AS
   BEGIN
   Update Label
set    LabelName = IsNull(@LabelName, LabelName),
		ModifiedDateTime = @ModifiedDateTime
Where
Id =@Id AND UserId = @UserId;
END