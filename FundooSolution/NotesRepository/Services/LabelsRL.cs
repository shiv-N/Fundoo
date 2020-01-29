
namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.LabelModels;
    using CommonLayerModel.NotesModels.Responce;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// this is class LabelsRL
    /// </summary>
    /// <seealso cref="BusinessManager.Interface.ILabelsRL" />
    public class LabelsRL : ILabelsRL
    {
        IConfiguration configuration;
        public LabelsRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> AddLabel(AddLabel model,int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spAddLabel", connection);
                connection.Open();
                command.Parameters.AddWithValue("LabelName", model.LabelName);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("CreatedDateTime",DateTime.Now);
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
        /// Edits the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> EditLabel(EditLabel model,int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spEditLabel", connection);
                connection.Open();
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("LabelName", model.LabelName);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("ModifiedDateTime",DateTime.Now);
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
        /// Deletes the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteLabel(DeleteLabelRequest model,int userId)
        {
            try
            {
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spDeleteLabel", connection);
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("UserId", userId);
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

        public async Task<IList<GetAllLabelsResponce>> GetAllLabels(int userId)
        {
            try
            {
                IList<GetAllLabelsResponce> noteLabels = new List<GetAllLabelsResponce>();
                SqlConnection connection = DBConnection();
                SqlCommand command = StoreProcedureConnection("spLabelByUserId", connection);
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();
                SqlDataReader dataReader = await command.ExecuteReaderAsync();
                while (dataReader.Read())
                {
                    GetAllLabelsResponce label = new GetAllLabelsResponce();
                    label.Id = (int)dataReader["Id"];
                    label.LabelName = dataReader["LabelName"].ToString();
                    label.UserId = (int)dataReader["UserId"];
                    label.CreatedDateTime = (DateTime)dataReader["CreatedDateTime"];
                    if(dataReader["ModifiedDateTime"] == null || dataReader["ModifiedDateTime"] == DBNull.Value)
                    {
                        label.ModifiedDateTime = null;
                    }
                    else
                    {
                        label.ModifiedDateTime = (DateTime)dataReader["ModifiedDateTime"];
                    }
                    noteLabels.Add(label);
                }
                connection.Close();
                return noteLabels;
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
