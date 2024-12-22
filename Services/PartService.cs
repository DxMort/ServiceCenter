using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class PartService
    {
        private readonly UnitOfWork _unitOfWork;

        public PartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Parts> GetParts()
        {
            return _unitOfWork.Parts.GetAll();
        }
        public Parts GetById(int id)
        {
            return _unitOfWork.Parts.GetAll().FirstOrDefault(p => p.Id == id);
        }
        public void Add(Parts part)
        {
            part.LastUpdateDate = part.LastUpdateDate.Date;
            _unitOfWork.Parts.Add(part);
            _unitOfWork.Complete();
        }
        public void Update(Parts part)
        {
            _unitOfWork.Complete();
        }
        public bool CanDeletePart(int partId)
        {
            // Проверяем наличие зависимых записей в OrderParts
            if (_unitOfWork.OrderParts.GetAll().Any(op => op.PartId == partId))
            {
                return false;
            }

            return true;
        }

        public void Delete(int partId)
        {
            if (CanDeletePart(partId))
            {
                var part = GetById(partId);
                if (part != null)
                {
                    _unitOfWork.Parts.Remove(part);
                    _unitOfWork.Complete();
                    MessageBox.Show("Запчасть удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Невозможно удалить запчасть из-за зависимых записей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}