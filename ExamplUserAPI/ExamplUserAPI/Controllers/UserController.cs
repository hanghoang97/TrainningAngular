using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using ExamplUserAPI.DataContext;
using ExamplUserAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

namespace ExamplUserAPI.Controllers
{
    [Route("api/[controller]/{id?}")]
    [ApiController]
    [EnableCors]
    public class UserController : Controller
    {
        private readonly DataDbContext _context;

        public UserController(DataDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public ActionResult<dynamic> Search([FromQuery]string strsearch ,[FromQuery]int? id)
        {
            if (id != null) return _context.Users.FirstOrDefault(s => s.Id == id);
            if (!string.IsNullOrEmpty(strsearch))
            {
                return _context.Users.Where(s => s.UserName.Contains(strsearch)
                                                 || s.FullName.Contains(strsearch)
                                                 || s.Gender.Contains(strsearch)
                                                 || s.Age.ToString().Contains(strsearch)
                                                 || s.Email.Contains(strsearch)).ToList();
            }
            return _context.Users.ToList();


        }

        [HttpPost]
        public ActionResult<dynamic> Add(User item)
        {
            item.Id = ++DataDbContext.SEQ;
            _context.Users.Add(item);
           return _context.SaveChanges();
        }

        [HttpPut]
        public ActionResult<bool> Update(User item)
        {
            var currentitem = _context.Users.Find(item.Id);
            if (currentitem == null) return NotFound();
            currentitem.UserName = item.UserName;
            currentitem.FullName = item.FullName;
            currentitem.Gender = item.Gender;
            currentitem.Age = item.Age;
            currentitem.Email = item.Email;
            _context.Users.Update(currentitem);
            return _context.SaveChanges() > 0;
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
           var item = _context.Users.FirstOrDefault(s => s.Id == id);
           if (item == null) return NotFound();
           _context.Users.Remove(item);
           return _context.SaveChanges() > 0;
        }
    }
}