using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.DataAccessLayer.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Display(Name = "Номер документа")]
        public string Number { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Документ")]
        public int? TypeOfDocumentId { get; set; }
        public TypeOfDocument TypeOfDocument { get; set; }


        [Display(Name = "Покупатель")]
        public int? BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        [Display(Name = "Изделие")]
        public int? ItemId { get; set; }
        public Item Item { get; set; }

        [Display(Name = "Склад")]
        public int? StoreId { get; set; }
        public Store Store { get; set; }


        [Display(Name = "Количество")]
        [Required]
        [Range(0, 100000000000, ErrorMessage = "Не верное количество")]
        public int Amount { get; set; }
    }
}
