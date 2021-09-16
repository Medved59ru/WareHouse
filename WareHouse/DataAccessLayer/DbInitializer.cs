using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer.Models;
using WareHouse.Models;

namespace WareHouse.DataAccessLayer
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Items.Any())
            {
                return;
            }

            var items = new Item[]
            {
                new Item {Name = "Болт M8", Code = "СЭЗ-8-27"},
                new Item {Name = "Болт М10", Code = "СЭЗ-10-27"},
                new Item {Name = "Болт М12", Code = "СЭЗ-12-27"}
            };
            foreach(Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

           
            var typeOfDocuments = new TypeOfDocument[]
            {
                new TypeOfDocument{Name = "ТТН", Operation = "Приход"},
                new TypeOfDocument{Name = "УПД", Operation = "Приход"},
                new TypeOfDocument{Name = "АКТ", Operation = "Приход"},
                new TypeOfDocument{Name = "ТТН", Operation = "Расход"},
                new TypeOfDocument{Name = "УПД", Operation = "Расход"},
                new TypeOfDocument{Name = "АКТ", Operation = "Расход"}
            };
            foreach (TypeOfDocument t in typeOfDocuments)
            {
                context.TypeOfDocuments.Add(t);
            }
            context.SaveChanges();

            var stores = new Store[]
            {
                new Store { Name = "Основной  склад"},
                new Store { Name = "Сырьевой склад"},
                new Store { Name = "Готовая продукция"}
            };
            foreach (Store s in stores)
            {
                context.Stores.Add(s);
            }
            context.SaveChanges();

            var buyers = new Buyer[]
            {
                new Buyer {Name = "ЧМЗ"},
                new Buyer {Name = "ОГМК"},
                new Buyer {Name = "Третье лицо"}

            };
            foreach (Buyer buyer in buyers)
            {
                context.Buyers.Add(buyer);
            }
            context.SaveChanges();

            var vendors = new Vendor[]
            {
                new Vendor {Name = "ПермМеталл"},
                new Vendor {Name = "ОГМК-Челябинск"},
                new Vendor {Name = "ПЕРММЕТИЗ"}
            };
            foreach(Vendor vendor in vendors)
            {
                context.Vendors.Add(vendor);
            }
            context.SaveChanges();
         
            var storedItems = new StoredItem[]
            {
                new StoredItem { ItemId = 1, Amount = 100,  PlacePosition = "A513", StoreId =1 },
                new StoredItem { ItemId = 1, Amount = 100, PlacePosition = "A513", StoreId =1 },
                new StoredItem { ItemId = 1, Amount = 100, PlacePosition = "A513", StoreId =1 },
                new StoredItem { ItemId = 2, Amount = 100,PlacePosition = "A513", StoreId =2 },
                new StoredItem { ItemId = 2, Amount = 100,  PlacePosition = "A513", StoreId =2 },
                new StoredItem { ItemId = 2, Amount = 100, PlacePosition = "A513", StoreId =2 },
                new StoredItem { ItemId = 3, Amount = 100, PlacePosition = "A513", StoreId =3 },
                new StoredItem { ItemId = 3, Amount = 100,  PlacePosition = "A513", StoreId =3 },
                new StoredItem { ItemId = 3, Amount = 100,  PlacePosition = "A513", StoreId =3 }
            };
            foreach (StoredItem item in storedItems)
            {
                context.StoredItems.Add(item);
            }
            context.SaveChanges();

            var supplies = new Supply[]
            {
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 1, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 1, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 1, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 2, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 2, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 2, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="1-c", Date = DateTime.Now, VendorId = 1, StoreId = 3, Amount = 1, TypeOfDocumentId =1, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId = 1, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId = 1, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId = 1, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId = 2, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId  = 2, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId  = 3, Amount = 1, TypeOfDocumentId =2, ItemId = 1},
                new Supply { Number="2-c", Date = DateTime.Now, VendorId = 2, StoreId  = 3, Amount = 1, TypeOfDocumentId =2, ItemId = 1}
            };
            foreach(Supply s in supplies)
            {
                context.Supplies.Add(s);
            }
            context.SaveChanges();

            var sales = new Sale[]
            {
                new Sale {Number="1-п", Date = DateTime.Now, BuyerId = 1, StoreId = 1, Amount = 1, TypeOfDocumentId =4, ItemId = 1},
                new Sale {Number="1-п", Date = DateTime.Now, BuyerId = 1, StoreId = 1, Amount = 1, TypeOfDocumentId =4, ItemId = 2 },
                new Sale {Number="2-п", Date = DateTime.Now, BuyerId = 2,  StoreId = 2, Amount = 1, TypeOfDocumentId =5, ItemId = 3},
                new Sale {Number="2-п", Date = DateTime.Now, BuyerId = 2,  StoreId = 2, Amount = 1, TypeOfDocumentId =5, ItemId = 2},
                new Sale {Number="3-п", Date = DateTime.Now, BuyerId = 3,  StoreId = 3, Amount = 1, TypeOfDocumentId =6, ItemId = 3},
                new Sale {Number="2-п", Date = DateTime.Now, BuyerId = 3,  StoreId = 3, Amount = 1, TypeOfDocumentId =6, ItemId = 1}
            };
            foreach(Sale s in sales)
            {
                context.Sales.Add(s);
            }
            context.SaveChanges();

        }
    }
}
