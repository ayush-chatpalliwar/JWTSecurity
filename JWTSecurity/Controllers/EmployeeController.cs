using JWTSecurity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTSecurity.Controllers   
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public SecurityDBContext db;
        public EmployeeController()
        {
            db = new SecurityDBContext();
        }  


        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[Route("api/[controller]")]
        public IActionResult GetAdminEmployee()
        {
            return Ok(db.Employees.Where(x => x.Gender=="Male"));
        }

        //[HttpGet]
        //[Authorize(Roles = "Manager")]
        //public IActionResult GetManagerEmployee()
        //{
        //    return Ok(db.Employees.Where(x => x.Gender == "Female"));
        //}


    }
}
