// UnitOfWork.cs
using ServiceCentre.Models;
using ServiceCentre.Services;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace ServiceCentre.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ServiceCentreContext _context;
        private readonly LogsService _logsService;

        public UnitOfWork(ServiceCentreContext context)
        {
            _context = context;
            Users = new Repository<Users>(_context);
            Clients = new Repository<Clients>(_context);
            Requests = new Repository<Requests>(_context);
            Parts = new Repository<Parts>(_context);
            Payments = new Repository<Payments>(_context);
            OrderParts = new Repository<OrderParts>(_context);
            RepairLogs = new Repository<RepairLogs>(_context);
            Logs = new Repository<Logs>(_context);
            AccessAdmin = new Repository<AccessAdmin>(_context);
            Sessions = new Repository<SessionTable>(_context);

        }

        public IRepository<Users> Users { get; private set; }
        public IRepository<Clients> Clients { get; private set; }
        public IRepository<Requests> Requests { get; private set; }
        public IRepository<Parts> Parts { get; private set; }
        public IRepository<Payments> Payments { get; private set; }
        public IRepository<OrderParts> OrderParts { get; private set; }
        public IRepository<RepairLogs> RepairLogs { get; private set; }
        public IRepository<Logs> Logs { get; private set; }
        public IRepository<AccessAdmin> AccessAdmin { get; private set; }
        public IRepository<SessionTable> Sessions { get; private set; }


        public void Complete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LogAction(string userName, string action, string description = null)
        {
            _logsService.AddLog(userName, action, description);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}