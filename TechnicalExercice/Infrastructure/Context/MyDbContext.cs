using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicalExercice.Domain.Models;

namespace TechnicalExercice.Infrastructure.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingBasket> ShoppingBaskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasDiscriminator<ClientType>("ClientType")
                .HasValue<Individual>(ClientType.Individual)
                .HasValue<Professional>(ClientType.Professional);
            
            modelBuilder.Entity<Client>()
                .HasMany(c => c.ShoppingBaskets)
                .WithOne()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);
                

            modelBuilder.Entity<ShoppingBasket>()
                .HasMany(b => b.Products)
                .WithMany()
                .UsingEntity(j => j.ToTable("ShoppingBasketProducts"));

            // modelBuilder.Entity<Product>()
            //     .Property(p => p.Price)
            //     .HasConversion(
            //         v => string.Join(';', v.Select(kv => $"{kv.Key}:{kv.Value}")),
            //         v => v.Split(';', StringSplitOptions.RemoveEmptyEntries)
            //               .Select(s => s.Split(':'))
            //               .ToDictionary(kv => (ClientType)Enum.Parse(typeof(ClientType), kv[0]), kv => decimal.Parse(kv[1]))
            //     );
            var jsonOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() } // si vous voulez serialiser les enum comme chaine
            };        

           modelBuilder.Entity<Product>()
                    .Property(p => p.Price)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<Dictionary<ClientType, decimal>>(v, jsonOptions) ?? new Dictionary<ClientType, decimal>())
                    .HasColumnType("text");     
        }
        
    }
}