using Microsoft.AspNetCore.Mvc;
using MiniProjet.IServices;
using MiniProjet.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiniProjet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _oUserServices;
        public UserController (IUserServices oUserServices)
        {
            _oUserServices = oUserServices;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _oUserServices.getAll();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _oUserServices.getById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public User Post([FromBody] User oUser)
        {
            if(ModelState.IsValid) 
                return _oUserServices.save(oUser);
            return null;
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return _oUserServices.delete(id);
        }
    }
}
