using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontProjectInAsp.Net.Models
{
    public class Catagory
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu hisseni bos buraxmayin"),MaxLength(50,ErrorMessage ="Uzunluq 50-den cox ola bilmez")]
        public string catagoryName { get; set; }
    }
}
