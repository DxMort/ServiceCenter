using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class OrderPartsService
    {
        private readonly UnitOfWork _unitOfWork;

        public OrderPartsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<OrderParts> GetOrderParts()
        {
            return _unitOfWork.OrderParts.GetAll();
        }
        public OrderParts GetById(int id)
        {
            return _unitOfWork.OrderParts.GetAll().FirstOrDefault(o => o.Id == id);
        }
        public void Add(OrderParts orderPart)
        {
            orderPart.OrderDate = orderPart.OrderDate.Date;
            _unitOfWork.OrderParts.Add(orderPart);
            _unitOfWork.Complete();
        }
        public void Update(OrderParts orderPart)
        {
            _unitOfWork.Complete();
        }
        public void Delete(int id)
        {
            var orderPart = GetById(id);
            if (orderPart != null)
            {
                _unitOfWork.OrderParts.Remove(orderPart);
                _unitOfWork.Complete();
                MessageBox.Show("Заявка на запчасть удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}