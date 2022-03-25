using System.Data;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence;

namespace API.Controllers
{
    public class HerosController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        public HerosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly DataContext _context;
        
        [HttpGet]

        public JsonResult Get()
        {
             string query = @"select * from super_heroi";

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

       



        [HttpPost]

        public JsonResult Post(Hero hero)
        {
             string query = @"insert into super_heroi(heroi,poder,universo,data_reg,exibir,imagem) values (@name,@power,@universe,@dat_reg,@exibir,@imagem)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HeroCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@name",hero.heroi);
                    myCommand.Parameters.AddWithValue("@power",hero.poder);
                    myCommand.Parameters.AddWithValue("@universe",hero.universo);
                    myCommand.Parameters.AddWithValue("@dat_reg",hero.dat_reg);
                    myCommand.Parameters.AddWithValue("@exibir",hero.exibir);
                     myCommand.Parameters.AddWithValue("@imagem",hero.imagem);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("ok");
        }

        [HttpGet("{id}")]

                public JsonResult GetId(int id)
        {
             string query = @"select * from super_heroi where heroi_id = @id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HeroCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query,myCon))
                {
                    myCommand.Parameters.AddWithValue("@id",id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        
        [HttpPut("{id}")]

                public JsonResult Del(int id)
        {
             string query = @"UPDATE super_heroi set exibir = @exibir where heroi_id = @id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("HeroCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon=new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query,myCon))
                {
                     myCommand.Parameters.AddWithValue("@id",id);
                    myCommand.Parameters.AddWithValue("@exibir",false);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("ok");
        }
        
        
  
    }
}