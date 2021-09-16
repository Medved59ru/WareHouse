using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class StoredItem
    {
        public int Id { get; set; }

        [Display(Name = "Изделие")]
        public int? ItemId { get; set; }
        public Item Item { get; set; }

        [Display(Name = "Склад")]
        public int? StoreId { get; set; }
        public Store Store { get; set; }

        [Display(Name = "Место хранения")]
        public string PlacePosition { get; set; }

        [Display(Name = "Количество")]
        [Required]
        [Range(0, 100000000000, ErrorMessage = "Не верное количество")]
        public int Amount { get; set; }

    }
}
