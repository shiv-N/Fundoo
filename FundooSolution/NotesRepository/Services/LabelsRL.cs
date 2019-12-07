using BusinessManager.Interface;
using CommonLayerModel.LabelModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BusinessManager.Services
{
    public class LabelsRL : ILabelsRL
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(localDB)\localhost;Initial Catalog=EmployeeDetails;Integrated Security=True");
        public string AddLabel(AddLabel model,int userId)
        {
            SqlCommand command = StoreProcedureConnection("spAddLabel");
            connection.Open();
            command.Parameters.AddWithValue("LabelName", model.LabelName);
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("CreatedDateTime", model.CreatedDateTime);
            int result = command.ExecuteNonQuery();
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
        public string EditLabel(EditLabel model,int userId)
        {
            SqlCommand command = StoreProcedureConnection("spEditLabel");
            connection.Open();
            command.Parameters.AddWithValue("Id", model.Id);
            command.Parameters.AddWithValue("LabelName", model.LabelName);
            command.Parameters.AddWithValue("UserId", userId);
            command.Parameters.AddWithValue("ModifiedDateTime", model.ModifiedDateTime);
            int result = command.ExecuteNonQuery();
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
        public string DeleteLabel(DeleteLabelRequest model,int userId)
        {
            SqlCommand command = StoreProcedureConnection("spDeleteLabel");
            command.Parameters.AddWithValue("Id", model.Id);
            command.Parameters.AddWithValue("UserId", userId);
            connection.Open();
            int result = command.ExecuteNonQuery();
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
        private SqlCommand StoreProcedureConnection(string Name)
        {
            SqlCommand command = new SqlCommand(Name, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
