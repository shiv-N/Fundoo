
namespace BusinessManager.Services
{
    using BusinessManager.Interface;
    using CommonLayerModel.LabelModels;
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
        /// <summary>
        /// The connection
        /// </summary>
        SqlConnection connection = new SqlConnection(@"Data Source=(localDB)\localhost;Initial Catalog=EmployeeDetails;Integrated Security=True");

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> AddLabel(AddLabel model,int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spAddLabel");
                connection.Open();
                command.Parameters.AddWithValue("LabelName", model.LabelName);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("CreatedDateTime", model.CreatedDateTime);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return "Label added";
                }
                else
                {
                    return "Label did not added";
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
        public async Task<string> EditLabel(EditLabel model,int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spEditLabel");
                connection.Open();
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("LabelName", model.LabelName);
                command.Parameters.AddWithValue("UserId", userId);
                command.Parameters.AddWithValue("ModifiedDateTime", model.ModifiedDateTime);
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return "Label edited";
                }
                else
                {
                    return "Label did not edited";
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> DeleteLabel(DeleteLabelRequest model,int userId)
        {
            try
            {
                SqlCommand command = StoreProcedureConnection("spDeleteLabel");
                command.Parameters.AddWithValue("Id", model.Id);
                command.Parameters.AddWithValue("UserId", userId);
                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                connection.Close();
                if (result != 0)
                {
                    return "Label deleted";
                }
                else
                {
                    return "Label did not deleted";
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
