using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class RequestService
    {
        private readonly UnitOfWork _unitOfWork;

        public RequestService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Requests> GetRequests()
        {
            return _unitOfWork.Requests.GetAll();
        }
        public Requests GetById(int id)
        {
            return _unitOfWork.Requests.GetAll().FirstOrDefault(r => r.Id == id);
        }
        public void Add(Requests request)
        {
            request.CreatedDate = request.CreatedDate.Date;
            request.CompletedDate = request.CompletedDate.Date;
            _unitOfWork.Requests.Add(request);
            _unitOfWork.Complete();
        }
        public void Update(Requests request)
        {
            _unitOfWork.Complete();
        }
        public bool CanDeleteRequest(int requestId)
        {
            if (_unitOfWork.Payments.GetAll().Any(p => p.RequestId == requestId))
            {
                return false;
            }

            return true;
        }
        public void Delete(int requestId)
        {
            if (CanDeleteRequest(requestId))
            {
                var request = GetById(requestId);
                if (request != null)
                {
                    _unitOfWork.Requests.Remove(request);
                    _unitOfWork.Complete();
                    MessageBox.Show("Заявка удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Невозможно удалить заявку из-за зависимых записей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}