using System.Data;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API.Controllers;
    [ApiController]
    [Route("[controller]")]
    public class PowerController : ControllerBase
    {


    private readonly IConfiguration _configuration;

        public PowerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


     [HttpGet(Name = "Power")]

        public JsonResult Get()
        {
             string query = @"select * from superpoder";

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
