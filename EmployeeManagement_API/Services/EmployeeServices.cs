using EmployeeManagement_API.Config;
using EmployeeManagement_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement_API.Services
{
    public class EmployeeServices
    {
        private Response response;

        public Response insertEmployee(Employee employee)
        {
            response = new Response();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectDB.getConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_Employee_Insert";

                        command.Parameters.AddWithValue("@firstName", employee.firstName);
                        command.Parameters.AddWithValue("@lastName", employee.lastName);
                        command.Parameters.AddWithValue("@emailId", employee.emailId);

                        int rows = command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();

                        if (rows == -1)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Insert Failed";
                        }
                        else if (rows < 0)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Insert Failed";
                        }
                        else if (rows > 0)
                        {
                            response.status = "Success";
                            response.content = employee;
                            response.message = "Successfully Inserted";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.status = "Fail";
                response.content = null;
                response.message = ex.Message;
            }
            return response;
        }

        public Response updateEmployee(Employee employee)
        {
            response = new Response();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectDB.getConnectionString()))
                {
                    connection.Open();

                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_Employee_Update";

                        command.Parameters.AddWithValue("@id", employee.id);
                        command.Parameters.AddWithValue("@firstName", employee.firstName);
                        command.Parameters.AddWithValue("@lastName", employee.lastName);
                        command.Parameters.AddWithValue("@emailId", employee.emailId);

                        int rows = command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();

                        if (rows == -1)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Update Failed";
                        }
                        else if(rows < 0)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Update Failed";
                        }
                        else if(rows > 0)
                        {
                            response.status = "Success";
                            response.content = employee;
                            response.message = "Successfully Updated";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.status = "Fail";
                response.content = null;
                response.message = ex.Message;
            }
            return response;
        }

        public Response deleteEmployee(int id)
        {
            response = new Response();

            try
            {
                using(SqlConnection connection = new SqlConnection(ConnectDB.getConnectionString()))
                {
                    connection.Open();

                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_Employee_Delete";

                        command.Parameters.AddWithValue("@id", id);

                        int rows = command.ExecuteNonQuery();
                        command.Dispose();
                        connection.Close();

                        if(rows == -1)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Delete Failed";
                        }
                        else if(rows < 0)
                        {
                            response.status = "Fail";
                            response.content = null;
                            response.message = "Delete Failed";
                        }
                        else if(rows > 0)
                        {
                            response.status = "Success";
                            response.content = null;
                            response.message = "Successfully Deleted";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.status = "Fail";
                response.content = null;
                response.message = ex.Message;
            }
            return response;
        }

        public Response getAllEmployees()
        {
            response = new Response();

            try
            {
                List<Employee> employeeList = new List<Employee>();

                using(SqlConnection connection = new SqlConnection(ConnectDB.getConnectionString()))
                {
                    connection.Open();

                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_Employee_ViewAll";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    Employee employee = new Employee();

                                    employee.id = Convert.ToInt32(reader["id"]);
                                    employee.firstName = reader["firstName"].ToString();
                                    employee.lastName = reader["lastName"].ToString();
                                    employee.emailId = reader["emailId"].ToString();

                                    employeeList.Add(employee);
                                }
                                response.status = "Success";
                                response.content = employeeList;
                                response.message = "Sample Data";
                            }
                            else
                            {
                                response.status = "Success";
                                response.content = employeeList;
                                response.message = "Employees not found";
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.status = "Fail";
                response.content = null;
                response.message = ex.Message;
            }
            return response;
        }

        public Response getEmployeesById(int id)
        {
            response = new Response();

            try
            {
                Employee employee = new Employee();

                using (SqlConnection connection = new SqlConnection(ConnectDB.getConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "sp_Employee_ViewById";

                        command.Parameters.AddWithValue("@id", id);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    employee.id = Convert.ToInt32(reader["id"]);
                                    employee.firstName = reader["firstName"].ToString();
                                    employee.lastName = reader["lastName"].ToString();
                                    employee.emailId = reader["emailId"].ToString();
                                }
                                response.status = "Success";
                                response.content = employee;
                                response.message = "Sample Data";
                            }
                            else
                            {
                                response.status = "Success";
                                response.content = null;
                                response.message = "Employees not found";
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.status = "Fail";
                response.content = null;
                response.message = ex.Message;
            }
            return response;
        }
    }
}
