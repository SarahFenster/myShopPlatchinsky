﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace myShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserServices userServices ;
        public UsersController(IUserServices _userServices)
        {
            userServices = _userServices;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Nechami", "Platchinsky" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await userServices.getUserById(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User newUser)
        {
            int num = userServices.checkPassword(newUser.Password);
            if (num >= 2)
            {
                await userServices.addUser(newUser);
                return CreatedAtAction(nameof(Get), new { id = newUser.UserId }, newUser);
            }
            else
            {
                return BadRequest();
            }


        }
        //iii
        //iiii
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult <User>> PostLogin([FromQuery] string Email,string Password)
        {
            User user =await userServices.getUserToLogin(Email, Password);
            if(user!=null)
                return Ok(user);
            return NoContent();
        }
        [HttpPost]
        [Route("password")]
        public ActionResult<int> PostPassword([FromBody] string Password)
        {
            int res = userServices.checkPassword(Password);
            return Ok(res);
        }
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] User updateUser)
        {
            
                
            int num = userServices.checkPassword(updateUser.Password);
            if (num >= 2|| updateUser.Password == "")
            {
                userServices.updateUser(id, updateUser);
                return Ok();
            }
            else
            {
                 return BadRequest("קוד לא תקין");
            }

            


        }

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
