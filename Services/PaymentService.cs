using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class PaymentService
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Payments> GetPayments()
        {
            return _unitOfWork.Payments.GetAll();
        }
        public Payments GetById(int id)
        {
            return _unitOfWork.Payments.GetAll().FirstOrDefault(p => p.Id == id);
        }
        public void Add(Payments payment)
        {
            payment.PaymentDate = payment.PaymentDate.Date;
            _unitOfWork.Payments.Add(payment);
            _unitOfWork.Complete();
        }
        public void Update(Payments payment)
        {
            _unitOfWork.Complete();
        }
        public void Delete(int id)
        {
            var payment = GetById(id);
            if (payment != null)
            {
                _unitOfWork.Payments.Remove(payment);
                _unitOfWork.Complete();
                MessageBox.Show("Платеж удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}