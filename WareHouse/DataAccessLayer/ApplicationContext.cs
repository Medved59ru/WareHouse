using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.DataAccessLayer
{
    public class ApplicationContext:DbContext
    {
       
        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoredItem> StoredItems { get; set; }
        public DbSet<TypeOfDocument> TypeOfDocuments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Sale> Sales { get; set; }
    
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}
