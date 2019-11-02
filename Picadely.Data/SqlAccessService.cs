using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Picadely.Services
{
    public class SqlAccessService
    {
        private readonly string _connectionString;
        private string _dataTableName;

        public SqlAccessService()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Picadely;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public DataTable SelectDatas(string tableName, List<string> selectColumns)
        {
            _dataTableName = tableName;
            return SelectData(null, selectColumns);
        }
        public DataTable SelectData(string tableName, List<Parameter> parameters)
        {
            _dataTableName = tableName;
            return SelectData(parameters, null);
        }

        public DataTable SelectData(string query)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;

        }

        public DataTable SelectDataIn(Parameters propertyToCheckIn, Parameters parametersForPutIn, List<string> selectColumns)
        {
            string query = "SELECT ";

            if (selectColumns != null)
                query = string.Concat(query, string.Join(",", selectColumns), " ");
            else
                query = string.Concat(query, "*", " ");

            query = string.Concat(query, $"FROM {_dataTableName}", " ");

            query = string.Concat(query, " WHERE ", string.Join(propertyToCheckIn.Send().First().ColumnName, " IN", "(", parametersForPutIn.Send().Select(value =>
            {
                return $" {value.Value},";
            }).ToList()));
            query = query.Remove(query.Length - 1);
            query = String.Concat(query, ")");
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;

        }

        public DataTable SelectData(List<Parameter> parameters, List<string> selectColumns)
        {
            string query = "SELECT ";

            if (selectColumns != null)
                query = string.Concat(query, string.Join(",", selectColumns), " ");
            else
                query = string.Concat(query, "*", " ");

            query = string.Concat(query, $"FROM {_dataTableName}", " ");

            if (parameters != null)
            {
                query = string.Concat(query, " WHERE ", string.Join(" AND ", parameters.Select(value =>
                {
                    return $" {value.ColumnName}=@{value.ColumnName}";
                }).ToList()));
            }

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(query, conn);
            if (parameters != null)
                command.Parameters.AddRange(parameters.Select(parameter =>
                {
                    return new SqlParameter($"@{parameter.ColumnName}", parameter.Value);
                }).ToArray());
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return dataTable;


        }
        public int InsertDataAsync(string tableName, Parameters parameters)
        {
            _dataTableName = tableName;
            return InsertDataAsync(parameters.Send());
        }
        private int InsertDataAsync(List<Parameter> parameters, string tableName = null)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                tableName = _dataTableName;
            return ExecuteScalarAsync($"INSERT INTO dbo.{tableName} ({string.Join(",", parameters.Select(x => x.ColumnName).ToList())}) " +
                          $"VALUES ({string.Join(",", parameters.Select(key => $"@{key.ColumnName}").ToList())}) ; SELECT SCOPE_IDENTITY()", parameters);
        }

        public void DeleteAsync(Parameters where = default)
        {

            string query = $"DELETE  FROM dbo.{_dataTableName}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Send().Select(x => $"{x.ColumnName}=@{x.ColumnName}")));

            var parametersToAdd = new List<Parameter>();
            if (where != null)
                parametersToAdd.AddRange(where.Send());
            ExcecuteQueryAsync(query, parametersToAdd);
        }
        public void UpdateAsync(Parameters parameters, Parameters where = default)
        {
            var parametersToAdd = parameters.Send();
            string query = $"UPDATE  dbo.{_dataTableName} SET {string.Join(",", parametersToAdd.Select(value => $"{value.ColumnName} = @{value.ColumnName}").ToList())}";

            if (where != null)
                query = string.Concat(query, " WHERE ", string.Join(" AND ", where.Send().Select(x => $"{x.ColumnName}=@{x.ColumnName}")));
            query = string.Concat(query, ";");
            if (where != null)
                parametersToAdd.AddRange(where.Send());
            ExcecuteQueryAsync(query, parametersToAdd);
        }

        private int ExecuteScalarAsync(string query, List<Parameter> parameters)
        {
            var identity = 0;
            ExecuteCommandAsync(query, parameters, cmd => identity = decimal.ToInt32((decimal)(cmd.ExecuteScalar())));
            return identity;
        }

        private void ExecuteCommandAsync(string query, List<Parameter> parameters, Action<SqlCommand> executeFunction)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.AddRange(parameters.Select(parameter =>
                {
                    if (parameter.Value == "NULL")
                        return new SqlParameter($"@{parameter.ColumnName}", DBNull.Value)
                        {
                            SqlDbType = parameter.Type
                        };

                    return new SqlParameter($"@{parameter.ColumnName}", parameter.Value)
                    {
                        SqlDbType = parameter.Type
                    };
                }).ToArray());

                cn.Open();
                executeFunction(cmd);
                cn.Close();
            }

        }
        public void Backup()
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("Backup database Picadely to disk='c:/Backup/Picadely.bak'", cn))
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
        public void Restore()
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("ALTER DATABASE Picadely  SET OFFLINE WITH ROLLBACK IMMEDIATE RESTORE DATABASE Picadely FROM DISK = 'c:/Backup/Picadely.bak' WITH REPLACE ALTER DATABASE Picadely SET ONLINE", cn))
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        public void ExcecuteQueryAsync(string query, List<Parameter> parameters) =>
             ExecuteCommandAsync(query, parameters, cmd => cmd.ExecuteNonQueryAsync());
    }
}
