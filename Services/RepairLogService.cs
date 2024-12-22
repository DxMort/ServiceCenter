using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class RepairLogsService
    {
        private readonly UnitOfWork _unitOfWork;

        public RepairLogsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<RepairLogs> GetRepairLogs()
        {
            return _unitOfWork.RepairLogs
                              .GetAll()
                              .Include(r => r.Part)
                              .Include(r => r.RepairRequest);
        }
        public RepairLogs GetById(int id)
        {
            var result = _unitOfWork.RepairLogs
                                    .GetAll()
                                    .Include(r => r.Part) 
                                    .FirstOrDefault(rl => rl.Id == id);

            return result;
        }
        public void Add(RepairLogs log)
        {
            log.WorkDate = log.WorkDate.Date;
            _unitOfWork.RepairLogs.Add(log);
            _unitOfWork.Complete();
        }
        public void Update(RepairLogs log)
        {
            _unitOfWork.Complete();
        }
        public void Delete(int id)
        {
            var log = GetById(id);
            if (log != null)
            {
                _unitOfWork.RepairLogs.Remove(log);
                _unitOfWork.Complete();
                MessageBox.Show("Запись о ремонте удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}