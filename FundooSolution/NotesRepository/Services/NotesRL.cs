namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayerModel.LabelModels;
    using CommonLayerModel.NotesModels;
    using CommonLayerModel.NotesModels.Request;
    using CommonLayerModel.NotesModels.Responce;
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
                command.Parameters.AddWithValue("CreatedDATETime", DateTime.Now);
                command.Parameters.AddWithValue("ModifiedDateTime", DateTime.Now);
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
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<bool> AddNoteLabel(AddNoteLabelRequest model, int userId, int NoteId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spAddNoteLabel", connection);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("NoteId", NoteId);
                command.Parameters.AddWithValue("LabelId", model.LabelId);
                command.Parameters.AddWithValue("LabelName", model.LabelName);
                command.Parameters.AddWithValue("CreatedDATETime", DateTime.Now);
                command.Parameters.AddWithValue("ModifiedDateTime", DateTime.Now);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result > 0)
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
        /// Displays the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<IList<DisplayResponceModel>> DisplayNotes(int userId)
        {
            try
            {
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();
                
                List<SpParameterData> paramsList = new List<SpParameterData>();
                paramsList.Add(new SpParameterData("@UserId", userId));
                DataTable table = await spExecuteReader("spDisplayNotesByUserId", paramsList);
               
                foreach(DataRow row in table.Rows)
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)row["Id"];
                    userDetails.Title = row["Title"].ToString();
                    userDetails.Message = row["MeassageDescription"].ToString();
                    userDetails.Image = row["NoteImage"].ToString();
                    userDetails.Color = row["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)row["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)row["ModifiedDateTime"];
                    if (row["AddReminder"] == null || row["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)row["AddReminder"];
                    }
                    userDetails.UserId = (int)row["UserId"];
                    userDetails.collaborators = await GetNoteallCollaborator(userDetails.Id, userId);
                    userDetails.Labels = await GetNoteLabels(userDetails.Id, userId);
                    userDetails.IsPin = (bool)row["IsPin"];
                    userDetails.IsNote = (bool)row["IsNote"];
                    userDetails.IsArchive = (bool)row["IsArchive"];
                    userDetails.IsTrash = (bool)row["IsTrash"];
                    notes.Add(userDetails);
                }
                return notes;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private async Task<IList<GetNoteCollaborator>> GetNoteallCollaborator(int noteId, int UserId)
        {
            IList<GetNoteCollaborator> noteCollaborator = new List<GetNoteCollaborator>();
            List<SpParameterData> paramsList = new List<SpParameterData>();
            paramsList.Add(new SpParameterData("@UserId", UserId));
            paramsList.Add(new SpParameterData("@NoteId", noteId));
            DataTable table = await spExecuteReader("spGetNoteAllCollaborator", paramsList);
            foreach (DataRow row in table.Rows)
            {
                GetNoteCollaborator collaborator = new GetNoteCollaborator();
                collaborator.CollabratorId = (int)row["CollabratorId"];
                collaborator.UserId = (int)row["UserId"];
                collaborator.CollabratorWithId = (int)row["CollabratorWithId"];
                collaborator.NoteId = (int)row["NoteId"];
                if(row["CreatedDateTime"]== null || row["CreatedDateTime"].Equals(DBNull.Value)){
                    collaborator.CreatedDateTime = null;
                }
                else
                {
                    collaborator.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                }
                if (row["ModifiedDateTime"] == null || row["ModifiedDateTime"].Equals(DBNull.Value)) { 
                    collaborator.ModifiedDateTime = null;
                }
                else
                {
                    collaborator.ModifiedDateTime = (DateTime)row["ModifiedDateTime"];
                }
                collaborator.ProfilePhoto = row["ProfilePhoto"].ToString();
                collaborator.CollaboratorEmail = row["Email"].ToString();
                noteCollaborator.Add(collaborator);
            }
            return noteCollaborator;
        }

        private async Task<IList<DisplayNoteLabelsResponce>> GetNoteLabels(int noteId, int UserId)
        {
            IList<DisplayNoteLabelsResponce> noteLabel = new List<DisplayNoteLabelsResponce>();
            List<SpParameterData> paramsList = new List<SpParameterData>();
            paramsList.Add(new SpParameterData("@UserId", UserId));
            paramsList.Add(new SpParameterData("@NoteId", noteId));
            DataTable table = await spExecuteReader("spGetNoteLabels", paramsList);
            foreach (DataRow row in table.Rows)
            {
                DisplayNoteLabelsResponce label = new DisplayNoteLabelsResponce();
                label.Id = (int)row["Id"];
                label.UserId = (int)row["UserId"];
                label.NoteId = (int)row["NoteId"];
                label.LabelId = (int)row["LabelId"];
                label.LabelName = row["LabelName"].ToString();
                label.CreatedTime = (DateTime)row["CreatedDATETime"];
                if(row["ModifiedDateTime"] == null || row["ModifiedDateTime"] == DBNull.Value)
                {
                    label.ModifiedTime = null;
                }
                else
                {
                    label.ModifiedTime = (DateTime)row["ModifiedDateTime"];
                }
                
                noteLabel.Add(label);
            }
            return noteLabel;
        }
        public async Task<IList<DisplayResponceModel>> DisplayArchive(int userId)
        {
            try
            {
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();

                List<SpParameterData> paramsList = new List<SpParameterData>();
                paramsList.Add(new SpParameterData("@UserId", userId));
                DataTable table = await spExecuteReader("spDisplayArchiveByUserId", paramsList);

                foreach (DataRow row in table.Rows)
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)row["Id"];
                    userDetails.Title = row["Title"].ToString();
                    userDetails.Message = row["MeassageDescription"].ToString();
                    userDetails.Image = row["NoteImage"].ToString();
                    userDetails.Color = row["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)row["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)row["ModifiedDateTime"];
                    if (row["AddReminder"] == null || row["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)row["AddReminder"];
                    }
                    userDetails.UserId = (int)row["UserId"];
                    userDetails.collaborators = await GetNoteallCollaborator(userDetails.Id, userId);
                    userDetails.Labels = await GetNoteLabels(userDetails.Id, userId);
                    userDetails.IsPin = (bool)row["IsPin"];
                    userDetails.IsNote = (bool)row["IsNote"];
                    userDetails.IsArchive = (bool)row["IsArchive"];
                    userDetails.IsTrash = (bool)row["IsTrash"];
                    notes.Add(userDetails);
                }
                return notes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IList<DisplayResponceModel>> DisplayLabel(string labelName, int userId)
        {
            try
            {
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();

                List<SpParameterData> paramsList = new List<SpParameterData>();
                paramsList.Add(new SpParameterData("@UserId", userId));
                paramsList.Add(new SpParameterData("@LabelName", labelName));
                DataTable table = await spExecuteReader("spDisplayLabelNotes", paramsList);

                foreach (DataRow row in table.Rows)
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)row["Id"];
                    userDetails.Title = row["Title"].ToString();
                    userDetails.Message = row["MeassageDescription"].ToString();
                    userDetails.Image = row["NoteImage"].ToString();
                    userDetails.Color = row["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)row["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)row["ModifiedDateTime"];
                    if (row["AddReminder"] == null || row["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)row["AddReminder"];
                    }
                    userDetails.collaborators = await GetNoteallCollaborator(userDetails.Id, userId);
                    userDetails.Labels = await GetNoteLabels(userDetails.Id, userId);
                    userDetails.UserId = (int)row["UserId"];
                    userDetails.IsPin = (bool)row["IsPin"];
                    userDetails.IsNote = (bool)row["IsNote"];
                    userDetails.IsArchive = (bool)row["IsArchive"];
                    userDetails.IsTrash = (bool)row["IsTrash"];
                    notes.Add(userDetails);
                }
                return notes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<IList<DisplayResponceModel>> DisplayTrash(int userId)
        {
            try
            {
                IList<DisplayResponceModel> notes = new List<DisplayResponceModel>();

                List<SpParameterData> paramsList = new List<SpParameterData>();
                paramsList.Add(new SpParameterData("@UserId", userId));
                DataTable table = await spExecuteReader("spDisplayTrashByUserId", paramsList);

                foreach (DataRow row in table.Rows)
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)row["Id"];
                    userDetails.Title = row["Title"].ToString();
                    userDetails.Message = row["MeassageDescription"].ToString();
                    userDetails.Image = row["NoteImage"].ToString();
                    userDetails.Color = row["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)row["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)row["ModifiedDateTime"];
                    if (row["AddReminder"] == null || row["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)row["AddReminder"];
                    }
                    userDetails.UserId = (int)row["UserId"];
                    userDetails.collaborators = await GetNoteallCollaborator(userDetails.Id, userId);
                    userDetails.Labels = await GetNoteLabels(userDetails.Id, userId);
                    userDetails.IsPin = (bool)row["IsPin"];
                    userDetails.IsNote = (bool)row["IsNote"];
                    userDetails.IsArchive = (bool)row["IsArchive"];
                    userDetails.IsTrash = (bool)row["IsTrash"];
                    notes.Add(userDetails);
                }
                return notes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IList<GetCollabratorResponce>> GetCollaborators(int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                IList<GetCollabratorResponce> notes = new List<GetCollabratorResponce>();
                SqlCommand command = StoreProcedureConnection("spGetCollaborators", connection);
                connection.Open();
                command.Parameters.AddWithValue("UserId", userId);
                command.ExecuteNonQuery();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    GetCollabratorResponce collabratorDetails = new GetCollabratorResponce();
                    collabratorDetails.CollaboratorId = (int)dataReader["Id"];
                    collabratorDetails.Email = dataReader["Email"].ToString();
                    notes.Add(collabratorDetails);
                }
                connection.Close();
                return notes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AddCollaboratorResponce> AddCollaborators(int NoteId,int userId, AddCollaboratorRequest collaborator)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spAddValidCollaborator", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@CollaboratorWithId", collaborator.CollaboratorId);
                command.Parameters.AddWithValue("@NoteId", NoteId);
                command.Parameters.AddWithValue("@CreatedDateTime", DateTime.Now);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                AddCollaboratorResponce collaboratorResponce = new AddCollaboratorResponce();
                if (dataReader.Read())
                {
                    collaboratorResponce.CollaborationRecordId = (int)dataReader["CollabratorId"];
                    collaboratorResponce.UserId = (int)dataReader["UserId"];
                    collaboratorResponce.CollaboratorId = (int)dataReader["CollabratorWithId"];
                    collaboratorResponce.NoteId = (int)dataReader["NoteId"];
                    collaboratorResponce.CreatedDateTime = (DateTime)dataReader["CreatedDateTime"];
                }
                connection.Close();
                return collaboratorResponce;
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
        public async Task<bool> EditNote(int noteId,EditNoteRequestModel model, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spEditNote", connection);
                connection.Open();
                command.Parameters.AddWithValue("Id", noteId);
                command.Parameters.AddWithValue("Title", model.Title);
                command.Parameters.AddWithValue("MeassageDescription", model.Message);
                command.Parameters.AddWithValue("AddReminder", model.Reminder);
                command.Parameters.AddWithValue("ModifiedDateTime", DateTime.Now);
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

        public async Task<List<DisplayResponceModel>> BulkTrash(List<int> NoteId,int UserId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spBulkTrash", connection);
                List<DisplayResponceModel> responceList = new List<DisplayResponceModel>();
                string noteIdString = string.Empty;
                foreach(int i in NoteId)
                {
                    noteIdString = noteIdString+ i + ",";
                }
                string noteId = noteIdString.Remove(noteIdString.Length - 1);
                command.Parameters.AddWithValue("@Id", noteId);
                command.Parameters.AddWithValue("@UId", UserId);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while(dataReader.Read())
                {
                    DisplayResponceModel userDetails = new DisplayResponceModel();
                    userDetails.Id = (int)dataReader["Id"];
                    userDetails.Title = dataReader["Title"].ToString();
                    userDetails.Message = dataReader["MeassageDescription"].ToString();
                    userDetails.Image = dataReader["NoteImage"].ToString();
                    userDetails.Color = dataReader["Color"].ToString();
                    userDetails.CreatedDate = (DateTime)dataReader["CreatedDATETime"];
                    userDetails.ModifiedDate = (DateTime)dataReader["ModifiedDateTime"];
                    if (dataReader["AddReminder"] == null || dataReader["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)dataReader["AddReminder"];
                    }
                    userDetails.UserId = (int)dataReader["UserId"];
                    userDetails.IsPin = (bool)dataReader["IsPin"];
                    userDetails.IsNote = (bool)dataReader["IsNote"];
                    userDetails.IsArchive = (bool)dataReader["IsArchive"];
                    userDetails.IsTrash = (bool)dataReader["IsTrash"];
                    responceList.Add(userDetails);
                }
                connection.Close();
                return responceList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<DisplayResponceModel>> SearchKeyword(string keyword, int UserId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spSearch", connection);
                List<DisplayResponceModel> responceList = new List<DisplayResponceModel>();
                command.Parameters.AddWithValue("@SearchKeyword", keyword);
                command.Parameters.AddWithValue("@UserId", UserId);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
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
                    if (dataReader["AddReminder"] == null || dataReader["AddReminder"].Equals(DBNull.Value))
                    {
                        userDetails.AddReminder = null;
                    }
                    else
                    {
                        userDetails.AddReminder = (DateTime)dataReader["AddReminder"];
                    }
                    userDetails.UserId = (int)dataReader["UserId"];
                    userDetails.IsPin = (bool)dataReader["IsPin"];
                    userDetails.IsNote = (bool)dataReader["IsNote"];
                    userDetails.IsArchive = (bool)dataReader["IsArchive"];
                    userDetails.IsTrash = (bool)dataReader["IsTrash"];
                    responceList.Add(userDetails);
                }
                connection.Close();
                return responceList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public async Task<List<GetCollaboratorResponse>> SearchCollaborators(string keyword, int UserId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spSearchCollaborators", connection);
                List<GetCollaboratorResponse> responceList = new List<GetCollaboratorResponse>();
                command.Parameters.AddWithValue("@SearchKeyword", keyword);
                command.Parameters.AddWithValue("@UserId", UserId);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    GetCollaboratorResponse userDetails = new GetCollaboratorResponse();
                    userDetails.Id = (int)dataReader["Id"];
                    userDetails.FirstName = dataReader["FirstName"].ToString();
                    userDetails.LastName = dataReader["LastName"].ToString();
                    userDetails.UserAddress = dataReader["UserAddress"].ToString();
                    userDetails.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    userDetails.email = dataReader["Email"].ToString();
                    responceList.Add(userDetails);
                }
                connection.Close();
                return responceList;
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
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spReminder", connection);
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                command.Parameters.AddWithValue("@AddReminder", reminder.Reminder.ToLocalTime());
                command.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
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

        public async Task<bool> DeleteReminderNote(int noteId, int userId, DeleteReminderRequest reminder)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spReminder", connection);
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Id", noteId);
                command.Parameters.AddWithValue("@AddReminder", reminder.Reminder);
                command.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
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

        public async Task<bool> DeleteNoteLabel(int NotelabelId, int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spDeleteNoteLabel", connection);
                connection.Open();
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@NoteLabelId", NotelabelId);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result > 0)
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
                command.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
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

        private async Task<DataTable> spExecuteReader(string spName, IList<SpParameterData> spParams)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection(spName, connection);
                for(int i = 0; i < spParams.Count; i++)
                {
                    command.Parameters.AddWithValue(spParams[i].name, spParams[i].value);
                }
                connection.Open();
                DataTable table = new DataTable();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                table.Load(dataReader);
                connection.Close();
                return table;
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

    class SpParameterData
    {
        public SpParameterData(string name,dynamic value)
        {
            this.name = name;
            this.value = value;
        }
        public string name { get; set; }
        public dynamic value { get; set; }
    }
}
