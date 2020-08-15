using Microsoft.EntityFrameworkCore;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Contexts{
    public partial class SmartHomeDbContext : DbContext{
        public SmartHomeDbContext() { }

        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> contextOptions) : base(contextOptions) { }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Particulates> Particulates { get; set; }
        public virtual DbSet<Temperature> Temperature { get; set; }
    }
}
