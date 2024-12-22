using System.Data.Entity;
using ServiceCentre.Models;

namespace ServiceCentre.DataAccess
{
    public class ServiceCentreContext : DbContext
    {
        public ServiceCentreContext() : base("name=SC") { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<Parts> Parts { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<OrderParts> OrderParts { get; set; }
        public DbSet<RepairLogs> RepairLogs { get; set; }
        public DbSet<AccessAdmin> AccessAdmin { get; set; }
        public DbSet<SessionTable> SessionTable { get; set; }
        // Настройки для связей и дополнительной конфигурации
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связь Requests с Clients (клиент)
            modelBuilder.Entity<Requests>()
                .HasRequired(r => r.Client) // Каждая заявка относится к одному клиенту
                .WithMany() // Один клиент может иметь несколько заявок
                .HasForeignKey(r => r.ClientId)
                .WillCascadeOnDelete(false);

            // Связь Requests с Users (мастер)
            modelBuilder.Entity<Requests>()
                .HasOptional(r => r.CurrentMaster) // Заявка может быть связана с мастером
                .WithMany() // Один мастер может работать над несколькими заявками
                .HasForeignKey(r => r.CurrentMasterId)
                .WillCascadeOnDelete(false);

            // Связь RepairLogs с Requests (заявка на ремонт)
            modelBuilder.Entity<RepairLogs>()
                .HasRequired(rl => rl.RepairRequest) // Один лог ремонта относится к одной заявке
                .WithMany() // Одна заявка может иметь несколько логов
                .HasForeignKey(rl => rl.RequestId)
                .WillCascadeOnDelete(false);

            // Связь RepairLogs с Users (мастер)
            modelBuilder.Entity<RepairLogs>()
                .HasRequired(rl => rl.User) // Один лог ремонта относится к одному мастеру
                .WithMany() // Один мастер может иметь несколько логов ремонта
                .HasForeignKey(rl => rl.UserId)
                .WillCascadeOnDelete(false);

            // Связь RepairLogs с Parts (запчасть)
            modelBuilder.Entity<RepairLogs>()
                .HasOptional(rl => rl.Part) // Лог ремонта может быть связан с одной запчастью
                .WithMany() // Одна запчасть может использоваться в нескольких ремонтах
                .HasForeignKey(rl => rl.PartId)
                .WillCascadeOnDelete(false);

            // Связь OrderParts с Parts (запчасти)
            modelBuilder.Entity<OrderParts>()
                .HasRequired(op => op.Part) // Каждый заказ связан с одной запчастью
                .WithMany() // Одна запчасть может быть заказана в нескольких заказах
                .HasForeignKey(op => op.PartId)
                .WillCascadeOnDelete(false);

            // Связь Payments с Requests (заявки)
            modelBuilder.Entity<Payments>()
                .HasRequired(p => p.RepairRequest) // Платеж связан с одной заявкой
                .WithMany() // Одна заявка может иметь несколько платежей
                .HasForeignKey(p => p.RequestId)
                .WillCascadeOnDelete(false);

            // Пример дополнительных настроек свойств
            modelBuilder.Entity<Users>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Parts>()
                .Property(p => p.PartName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}