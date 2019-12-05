using BusinessManager.Interface;
using CommonLayerModel.NotesModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BusinessManager.Services
{
    public class NotesRL : INotesRL
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localDB)\localhost;Initial Catalog=EmployeeDetails;Integrated Security=True");
        public string CreateNotes(AddNotesRequestModel model)
        {
            SqlCommand command = new SqlCommand("spAddNote", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Title", model.Title);
            command.Parameters.AddWithValue("MeassageDescription", model.Message);
            command.Parameters.AddWithValue("NoteImage", model.Image);
            command.Parameters.AddWithValue("Color", model.Color);
            command.Parameters.AddWithValue("CreatedDATETime", model.CreatedDate);
            command.Parameters.AddWithValue("ModifiedDateTime", model.ModifiedDate);
            command.Parameters.AddWithValue("AddReminder", model.AddReminder);
            command.Parameters.AddWithValue("UserId", model.UserId);
            command.Parameters.AddWithValue("IsPin", model.IsPin);
            command.Parameters.AddWithValue("IsNote", model.IsNote);
            command.Parameters.AddWithValue("IsArchive", model.IsArchive);
            command.Parameters.AddWithValue("IsTrash", model.IsTrash);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if (result != 0)
            {
                return "Note added";
            }
            else
            {
                return "Note did not added";
            };
        }
    }
}
