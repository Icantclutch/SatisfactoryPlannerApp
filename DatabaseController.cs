using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SatisfactoryBuildPlanner
{
    public static class DatabaseController
    {
        public static SqlConnection DatabaseConnection { get; set; }

        public static void InitializeDatabase(string connString)
        {
            DatabaseConnection = new SqlConnection(connString);
        }

        private static DataTable GetQueryData(SqlCommand command)
        {
            SqlDataReader reader;

            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An exception Occured: " + ex.Message);
                DatabaseConnection.Close();
                return new DataTable();
            }

            DataTable resultTable = new DataTable();

            foreach (DbColumn column in reader.GetColumnSchema())
            {
                resultTable.Columns.Add(column.ColumnName);
            }

            while (reader.Read())
            {
                IDataRecord record = reader;
                object[] values = new object[record.FieldCount];
                record.GetValues(values);
                resultTable.Rows.Add(values);
            }

            reader.Close();
            return resultTable;
        }

        public static void AddItem(string itemName)
        {
            DatabaseConnection.Open();

            SqlCommand command = new SqlCommand("AddItem", DatabaseConnection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@itemName", itemName));

            command.ExecuteNonQuery();
            DatabaseConnection.Close();
        }

        public static int AddRecipe(string powerUsage, string recipeTime, string recipeName)
        {
            DatabaseConnection.Open();
            SqlCommand command = new SqlCommand("AddRecipe", DatabaseConnection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@powerUsage", powerUsage));
            command.Parameters.Add(new SqlParameter("@recipeTime", recipeTime));
            command.Parameters.Add(new SqlParameter("@recipeName", recipeName));

            int recipeID = Convert.ToInt32(command.ExecuteScalar());
            DatabaseConnection.Close();

            return recipeID;
        }

        public static void AddRecipeInput(int recipeID, string itemName, float quantity)
        {
            DatabaseConnection.Open();
            SqlCommand command = new SqlCommand("AddRecipeInput", DatabaseConnection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@recipeID", recipeID));
            command.Parameters.Add(new SqlParameter("@inputName", itemName));
            command.Parameters.Add(new SqlParameter("@inputQty", quantity));

            command.ExecuteNonQuery();
            DatabaseConnection.Close();
        }

        public static void AddRecipeOutput(int recipeID, string itemName, float quantity)
        {
            DatabaseConnection.Open();
            SqlCommand command = new SqlCommand("AddRecipeOutput", DatabaseConnection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@recipeID", recipeID));
            command.Parameters.Add(new SqlParameter("@outputName", itemName));
            command.Parameters.Add(new SqlParameter("@outputQty", quantity));

            command.ExecuteNonQuery();
            DatabaseConnection.Close();
        }

        public static DataTable GetItemRecipes(int itemID)
        {
            DatabaseConnection.Open();

            SqlCommand command = new SqlCommand("GetItemRecipes", DatabaseConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@itemID", itemID));

            DataTable resultTable = GetQueryData(command);

            DatabaseConnection.Close();
            return resultTable;
        }

        public static DataTable GetRecipeInputs(int recipeID)
        {
            DatabaseConnection.Open();

            SqlCommand command = new SqlCommand("GetRecipeInputs", DatabaseConnection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@recipeID", recipeID));

            DataTable resultTable = GetQueryData(command);

            DatabaseConnection.Close();
            return resultTable;
        }

        public static DataTable ExecuteQuery(string query)
        {
            DatabaseConnection.Open();

            SqlCommand command = new SqlCommand(query, DatabaseConnection);
            command.CommandType = CommandType.Text;

            DataTable resultTable = GetQueryData(command);
           
            DatabaseConnection.Close();
            return resultTable;
        }
    }
}
