Create procedure spDeleteEmployee     

(      

   @Id int      

)      

as       

begin      

   Delete from EmployeeTable where Id=@Id      

End