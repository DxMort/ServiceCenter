using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class LogsService
    {
        private readonly UnitOfWork _unitOfWork;

        public LogsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Logs> GetLogs()
        {
            return _unitOfWork.Logs.GetAll(); // Работает только с UserName
        }

        public Logs GetById(int id)
        {
            return _unitOfWork.Logs.GetAll().FirstOrDefault(l => l.Id == id);
        }
        public void AddLog(string userName, string action, string description = null)
        {
            var log = new Logs
            {
                UserName = userName,
                Action = action,
                Date = DateTime.Now.Date,
                Description = description
            };

            _unitOfWork.Logs.Add(log);
            _unitOfWork.Complete();
        }
        public void Update(Logs log)
        {
            _unitOfWork.Complete();
        }
        public void Delete(int id)
        {
            var log = GetById(id);
            if (log != null)
            {
                _unitOfWork.Logs.Remove(log);
                _unitOfWork.Complete();
                MessageBox.Show("Лог удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}