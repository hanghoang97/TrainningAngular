using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace ExamplUserAPI.Models
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int? Id { get; set; }
        public string UserName { get; set; }
        public  string FullName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

    }
}
