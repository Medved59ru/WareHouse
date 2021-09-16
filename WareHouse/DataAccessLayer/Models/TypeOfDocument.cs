using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class TypeOfDocument
    {
        public int Id { get; set; }

        [Display(Name = "Операция")]
        public string Operation { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

    }
}
