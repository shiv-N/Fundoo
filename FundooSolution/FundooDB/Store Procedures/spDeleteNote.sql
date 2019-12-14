CREATE procedure [dbo].[spDeleteNote]     

(      

   @Id int,
   @UserId int      

)      

as       

begin      

   Delete from FundooNotes Where
Id =@Id AND UserId = @UserId  

End