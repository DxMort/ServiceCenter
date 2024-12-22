using ServiceCentre.Models;
namespace ServiceCentre.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Users> Users { get; }
        IRepository<Clients> Clients { get; }
        IRepository<Requests> Requests { get; }
        IRepository<Parts> Parts { get; }
        IRepository<Payments> Payments { get; }
        IRepository<OrderParts> OrderParts { get; }
        IRepository<RepairLogs> RepairLogs { get; }
        IRepository<Logs> Logs { get; }

        void Complete();
    }
}
