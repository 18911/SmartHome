using Microsoft.EntityFrameworkCore;
using SmartHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Contexts{
    public partial class SmartHomeDbContext : DbContext {
        public virtual DbSet<Client> client { get; set; }
        public virtual DbSet<Device> device { get; set; }
        public virtual DbSet<Room> room { get; set; }
        public virtual DbSet<Particulates> particulates { get; set; }
        public virtual DbSet<Temperature> temperature { get; set; }

        public SmartHomeDbContext() { }

        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }
    }
}
