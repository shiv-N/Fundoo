namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayerModel.NotesModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
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
        IConfiguration configuration;
        public NotesRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> AddNotes(AddNotesRequestModel model, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spAddNote", connection);
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
                    return true;
                }
                else
                {
                    return false;
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
                SqlConnection connection = DBConnection();
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();
                SqlCommand command = StoreProcedureConnection("spDisplayNotesByUserId", connection);
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
        public async Task<bool> EditNote(EditNoteRequestModel model, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spEditNote", connection);
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
                    return true;
                }
                else
                {
                    return false;
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
        public async Task<bool> DeleteNote(int Id, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spDeleteNote", connection);
                command.Parameters.AddWithValue("Id", Id);
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if(result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Uploads the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> UploadImage(IFormFile file,int noteId, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                Account account = new Account(configuration["Data:CloudName"], configuration["Data:API_Key"], configuration["Data:API_Secret"]);
                var Path = file.OpenReadStream();
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, Path),
                };
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                SqlCommand command = StoreProcedureConnection("spUpdateImage", connection);
                command.Parameters.AddWithValue("Id", noteId);
                command.Parameters.AddWithValue("NoteImage", uploadResult.Uri.ToString());
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> ArchiveNote(int noteId, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spArchive", connection);
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch(Exception e)
            {
                throw e;
            } 
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> PinNote(int noteId, int userId)
        {
            SqlConnection connection = DBConnection();
            SqlCommand command = StoreProcedureConnection("spPin", connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> TrashNote(int noteId, int userId)
        {
            SqlConnection connection = DBConnection();
            SqlCommand command = StoreProcedureConnection("spTrash", connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Reminders the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns></returns>
        public async Task<bool> ReminderNote(int noteId, int userId, AddReminderRequest reminder)
        {
            SqlConnection connection = DBConnection();
            SqlCommand command = StoreProcedureConnection("spReminder", connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                command.Parameters.AddWithValue("@AddReminder", reminder.AddReminder);
                command.Parameters.AddWithValue("@ModifiedDateTime", reminder.ModifiedDate);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Colours the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="colourRequest">The colour request.</param>
        /// <returns></returns>
        public async Task<bool> ColourNote(int noteId, int userId, ColourRequestModel colourRequest)
        {
            SqlConnection connection = DBConnection();
            SqlCommand command = StoreProcedureConnection("spColour", connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                command.Parameters.AddWithValue("@Color", colourRequest.Color);
                command.Parameters.AddWithValue("@ModifiedDateTime", colourRequest.ModifiedDate);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private SqlConnection DBConnection()
        {
            return new SqlConnection(configuration["Data:ConnectionString"]);
        }

        /// <summary>
        /// Stores the procedure connection.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns></returns>
        private SqlCommand StoreProcedureConnection(string Name, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(Name, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

    }
}
