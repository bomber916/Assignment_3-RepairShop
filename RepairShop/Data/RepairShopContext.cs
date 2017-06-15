using Microsoft.EntityFrameworkCore;
using RepairShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Data
{
    public class RepairShopContext : DbContext
    {
        public RepairShopContext(DbContextOptions<RepairShopContext> options) : base(options)
        {

        }

        public DbSet<Service> Services { get; set; }
        public DbSet<DeviceService> DeviceServices { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
