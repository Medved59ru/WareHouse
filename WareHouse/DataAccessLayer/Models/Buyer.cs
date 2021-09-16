using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class Buyer
    {
        public int Id { get; set; }

        [Display(Name = "Покупатель")]
        public string Name { get; set; }
    }
}
