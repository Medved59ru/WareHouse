using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Display(Name = "Название склада")]
        public  string Name { get; set; }

    }
}
