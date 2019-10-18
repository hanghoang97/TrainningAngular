using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamplUserAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamplUserAPI.DataContext
{
    public class DataDbContext:DbContext
    {
        public static int SEQ;
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            GetData(this);
        }
        public DbSet<User> Users { get; set; }
        public void GetData(DataDbContext context)
        {
            if(context.Users.Any()) return;
            var items = new List<User>();
            for (int i = 0; i < 30; i++)
            {
                items.Add(new User
                {
                    Id = i + 1,
                    UserName = "hthang" +i,
                    FullName = "HoangHang" +i,
                    Gender ="Nữ",
                    Age  = 10 + i,
                    Email ="hthang@cmc.com.vn"
                });
                SEQ++;
            }
            context.Users.AddRange(items);
            context.SaveChanges();
        }
    }
}
