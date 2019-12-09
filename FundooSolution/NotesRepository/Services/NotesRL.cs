﻿namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.NotesModels;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is class NotesRL
    /// </summary>
    /// <seealso cref="BusinessManager.Interface.INotesRL" />
    public class NotesRL : INotesRL
    {
        /// <summary>
        /// The connection
        /// </summary>
        SqlConnection connection = new SqlConnection(@"Data Source=(localDB)\localhost;Initial Catalog=EmployeeDetails;Integrated Security=True");

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> AddNotes(AddNotesRequestModel model, int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spAddNote");
                command.Parameters.AddWithValue("Title", model.Title);
                command.Parameters.AddWithValue("MeassageDescription", model.Message);
                command.Parameters.AddWithValue("NoteImage", model.Image);
                command.Parameters.AddWithValue("Color", model.Color);
                command.Parameters.AddWithValue("CreatedDATETime", model.CreatedDate);
                command.Parameters.AddWithValue("ModifiedDateTime", model.ModifiedDate);
                command.Parameters.AddWithValue("AddReminder", model.AddReminder);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("IsPin", model.IsPin);
                command.Parameters.AddWithValue("IsNote", model.IsNote);
                command.Parameters.AddWithValue("IsArchive", model.IsArchive);
                command.Parameters.AddWithValue("IsTrash", model.IsTrash);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return "Note added";
                }
                else
                {
                    return "Note did not added";
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<DisplayResponceModel> DisplayNotes(int userId)
        {
            try
            {
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();
                SqlCommand command = StoreProcedureConnection("spDisplayNotesByUserId");
                connection.Open();
                command.Parameters.AddWithValue("UserId", userId);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)dataReader["Id"];
                    userDetails.Title = dataReader["Title"].ToString();
                    userDetails.Message = dataReader["MeassageDescription"].ToString();
                    userDetails.Image = dataReader["NoteImage"].ToString();
                    userDetails.Color = dataReader["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)dataReader["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)dataReader["ModifiedDateTime"];
                    userDetails.AddReminder = (DateTime)dataReader["AddReminder"];
                    userDetails.UserId = (int)dataReader["UserId"];
                    userDetails.IsPin = (bool)dataReader["IsPin"];
                    userDetails.IsNote = (bool)dataReader["IsNote"];
                    userDetails.IsArchive = (bool)dataReader["IsArchive"];
                    userDetails.IsTrash = (bool)dataReader["IsTrash"];
                    notes.Add(userDetails);
                }
                connection.Close();
                return notes;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Edits the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> EditNote(EditNoteRequestModel model, int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spEditNote");
                connection.Open();
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("Title", model.Title);
                command.Parameters.AddWithValue("MeassageDescription", model.Message);
                command.Parameters.AddWithValue("NoteImage", model.Image);
                command.Parameters.AddWithValue("Color", model.Color);
                command.Parameters.AddWithValue("ModifiedDateTime", model.ModifiedDate);
                command.Parameters.AddWithValue("UserId", userId);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return "Note edited";
                }
                else
                {
                    return "Note did not edited";
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> DeleteNote(DeleteNoteRequestModel model,int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spDeleteNote");
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if(result != 0)
                {
                    return "Note deleted";
                }
                else
                {
                    return "Note did not deleted";
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Stores the procedure connection.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        private SqlCommand StoreProcedureConnection(string Name)
        {
            SqlCommand command = new SqlCommand(Name, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
