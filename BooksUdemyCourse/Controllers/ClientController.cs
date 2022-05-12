using BooksUdemyCourse.Models;
using BooksUdemyCourse.Models.ViewModels;
using BooksUdemyCourse.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BooksUdemyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {   
        private readonly IConfiguration _configuration; // traemos variables que tengamos en el archivo de configuracion de la aplicaci{on (appsettings.json)
        public ClientController(IConfiguration configuration) // constructor del controlador que recibe parametros que vayamos a utilizar
        {
            this._configuration = configuration;
        }



        [HttpGet] //Cuando se haga una peticion de tipo Get se reedirecionara a este metodo del controlador Clientes
        public IActionResult GetClients()
        {
            Result _result = new Result(); // capturar el resultado de la peticion

            try
            {
                using (BooksUdemyCourseContext _context = new BooksUdemyCourseContext()) //se crea un objeto del contexto para hacer uso de los modelos de la base de datos en
                                                                                         //un using para liberar conexiones automaticamente,
                                                                                         //hacer dispose y cerrar las conexiones
                {
                    var list = _context.Clients.ToList(); // usamos var para definir el tipo de variable en la ejecución y la igualamos
                                                          // al objeto context volcado a una lista con todos los elementos

                    _result.Res = list; // convierte el resultado a un objeto generico usando una clase get/set
                }
            }

            catch (Exception ex)
            {
                _result.Error = "Se produjo un error al obtener clientes" + ex.Message; // mensaje de error
            }

            return Ok(_result); // retorna el objeto como resultado de la consulta
        }



        [HttpPost]
        public IActionResult AddCliente(ClientViewModel c)
        {
            Result _result = new Result();
            try
            {
                byte[] _keyByte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }; // arreglo con la clave de salto para generar la criptografia del password
                PasswordUtil _util = new PasswordUtil(_keyByte);

                using (BooksUdemyCourseContext _context = new BooksUdemyCourseContext())
                {
                    Client _client = new Client();
                    _client.Name = c.name;
                    _client.Email = c.email;
                    _client.Password = Encoding.ASCII.GetBytes(_util.EncryptPass(c.password, _configuration["KeyPwd"])); // le enviamos al metodo de encriptacion el password convertido a un array de bytes
                    _client.RegisterDate = DateTime.Now;
                    _context.Clients.Add(_client); // agregamos todo el objeto _client para registrarlo
                    _context.SaveChanges(); // confirmamos cambios
                }

            }
            catch(Exception ex)
            {
                _result.Error = "Se produjo un error al registrar cliente" + ex.Message;
            }

            return Ok(_result);
        }



        [HttpPut]
        public IActionResult EditCliente(ClientViewModel c)
        {
            Result _result = new Result();
            try
            {
                byte[] _keyByte = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                PasswordUtil _util = new PasswordUtil(_keyByte);

                using (BooksUdemyCourseContext _context = new BooksUdemyCourseContext())
                {
                    Client _client = _context.Clients.Single(cli => cli.Id == c.id); // busca el cliente en la base de datos que tenga el id seleccionado
                    _client.Name = c.name;
                    _client.Email = c.email;
                    _client.Password = Encoding.ASCII.GetBytes(_util.EncryptPass(c.password, _configuration["KeyPwd"]));
                    _context.Entry(_client).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //sentencia de EntityFramework que modifica los datos seleccionados
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _result.Error = "Se produjo un error al editar cliente" + ex.Message;
            }

            return Ok(_result);
        }



        [HttpDelete("{Id}")]
        public IActionResult DeleteCliente(int id)
        {
            Result _result = new Result();
            try
            {

                using (BooksUdemyCourseContext _context = new BooksUdemyCourseContext())
                {
                    Client _client = _context.Clients.Single(cli => cli.Id == id);
                    _context.Remove(_client);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _result.Error = "Se produjo un error al eliminar cliente" + ex.Message;
            }

            return Ok(_result);
        }


    }
}
