using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PassListToSP.Entities;
using System.Data;

namespace PassListToSP.Controllers
{
    [ApiController]
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private string connectionString;

        public ValuesController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Value>> Get([FromQuery] int[] ids)
        {
            var result = new List<Value>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("Values_GetValues", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    var dt = new DataTable();
                    dt.Columns.Add("Id", typeof(int));

                    foreach (var id in ids)
                    {
                        dt.Rows.Add(id);
                    }

                    var parameter = command.Parameters.AddWithValue("ListIds", dt);
                    parameter.SqlDbType = SqlDbType.Structured;

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(new Value()
                        {
                            Id = int.Parse(reader["Id"].ToString()!),
                            Text = reader["Value"].ToString()!
                        });
                    }
                }
            }

            return result;
        }
    }
}
