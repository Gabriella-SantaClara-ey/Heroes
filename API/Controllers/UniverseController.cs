using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API.Controllers;
    [ApiController]
    [Route("[controller]")]
    public class UniverseController : ControllerBase
    {


    private readonly IConfiguration _configuration;

        public UniverseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


     [HttpGet(Name = "Universe")]

        public JsonResult Get()
        {
             string query = @"select * from universos";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HeroCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query,myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

    }
