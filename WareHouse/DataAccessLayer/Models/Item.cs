using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Display(Name = "Наименование изделия")]
        public string Name { get; set; }

        [Display(Name = "Код изделия")]
        public string Code { get; set; }

    }
}
