using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ToDoListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]

        public IActionResult Registration(register user)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO user_login (user_id, password, full_name) VALUES (@userId, @password, @fullName)", con))
                        {
                            cmd.Parameters.AddWithValue("@userId", user.user_id);
                            cmd.Parameters.AddWithValue("@password", user.password);
                            cmd.Parameters.AddWithValue("@fullName", user.full_name);

                            cmd.Transaction = trans;
                            int rowsAffected = cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return Ok( new {message = "Berhasil Registrasi"});
                    }
                    catch (MySqlException ex)
                    {
                        trans.Rollback();

                        if (ex.Number == 1062)
                        {
                            return Ok(new { message = "User ID telah terpakai. Silakan pilih User ID lain." });
                        }
                        return Ok(new { message = "Registrasi gagal: " + ex.Message });
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Ok(new { message = "Registrasi gagal: " + ex.Message });
                    }
                }
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] user_login user)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            try
            {
                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM user_login WHERE user_id = @user_id AND password = @password", con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", user.user_id);
                        cmd.Parameters.AddWithValue("@password", user.password);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt.Rows.Count == 0)
                            {
                                
                                return Ok(new object[] { new {
                                    user_id = "",
                                    password = "",
                                    full_name = "",
                                    message = "User ID atau Password yang anda masukkan tidak sesuai"
                                }});
                            }

                            var users = dt.AsEnumerable().Select(row => new register
                            {
                                user_id = row.Field<string>("user_id"),
                                password = row.Field<string>("password"),
                                full_name = row.Field<string>("full_name"),
                                message = "Berhasil"
                            }).ToList();

                            return Ok(users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(500, new { message = "Terjadi kesalahan di server" });
            }
        }



        [HttpPost]
        [Route("GetListTodo")]

        public IActionResult GetListTodo(todo_list list)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM todolist WHERE user_id = @user_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user_id", list.user_id);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            if (dt == null || dt.Rows.Count == 0)
                            {
                                return Ok(new object[] { new {
                                    user_id = "",
                                    password = "",
                                    full_name = "",
                                    message = "Data Kosong"
                                }});
                            }

                            List<todo_list> todoLists = new List<todo_list>();

                            foreach (DataRow row in dt.Rows)
                            {
                                todo_list todo = new todo_list
                                {
                                    user_id = row["user_id"].ToString(),
                                    ID_Show = row["ID_Show"].ToString(),
                                    Subject = row["Subject"].ToString(),
                                    Description = row["Description"].ToString(),
                                    Status = row["Status"].ToString(),
                                    Mark = row["Mark"].ToString(),
                                    message = "Berhasil",
                                };
                                todoLists.Add(todo);
                            }

                            return Ok(todoLists);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return StatusCode(500, "Error saat mengambil data: " + ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Terjadi kesalahan: " + ex.Message);
                }
            }
        }

        [HttpPost]
        [Route("InsertList")]

        public IActionResult InsertList(todo_list list)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("InsertList", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("p_userId", list.user_id);
                            cmd.Parameters.AddWithValue("p_Subject", list.Subject);
                            cmd.Parameters.AddWithValue("p_Description", list.Description);

                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return Ok(new { message = "Berhasil Menambahkan Data" });
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Ok(new { message = "An error occurred: " + ex.Message });
                    }
                }
            }
        }

        [HttpPost]
        [Route("UpdateList")]

        public IActionResult UpdateList(todo_list list)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("UpdateList", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("p_ID_Show", list.ID_Show);
                            cmd.Parameters.AddWithValue("p_Subject", list.Subject);
                            cmd.Parameters.AddWithValue("p_Description", list.Description);
                            cmd.Parameters.AddWithValue("p_Status", list.Status);
                            cmd.Parameters.AddWithValue("p_Mark", list.Mark);

                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return Ok(new { message = "Berhasil Mengubah Data" });
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Ok(new { message = "An error occurred: " + ex.Message });
                    }
                }
            }
        }

        [HttpPost]
        [Route("DeleteList")]

        public IActionResult DeleteList(todo_list list)
        {
            string connectionString = _configuration.GetConnectionString("StoreCon");

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand("DeleteList", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("p_ID_Show", list.ID_Show);

                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return Ok(new { message = "Berhasil Menghapus Data" });
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return Ok(new { message = "An error occurred: " + ex.Message });
                    }
                }
            }
        }
    }
}
