CREATE procedure spDeleteLabel     

(      

   @Id int,
   @UserId int      

)      

as       

begin      

   Delete from Label Where
Id =@Id AND UserId = @UserId  

End