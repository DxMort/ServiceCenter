using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class ClientService
    {
        private readonly UnitOfWork _unitOfWork;

        public ClientService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Clients> GetClients()
        {
            return _unitOfWork.Clients.GetAll();
        }
        public Clients GetById(int id)
        {
            return _unitOfWork.Clients.GetAll().FirstOrDefault(c => c.Id == id);
        }
        public void Add(Clients client)
        {
            client.CreatedDate = client.CreatedDate.Date;
            _unitOfWork.Clients.Add(client);
            _unitOfWork.Complete();
        }
        public void Update(Clients client)
        {
            _unitOfWork.Complete();
        }
        public bool CanDeleteClient(int clientId)
        {
            if (_unitOfWork.Requests.GetAll().Any(r => r.ClientId == clientId))
            {
                return false;
            }

            return true;
        }
        public void Delete(int clientId)
        {
            if (CanDeleteClient(clientId))
            {
                var client = GetById(clientId);
                if (client != null)
                {
                    _unitOfWork.Clients.Remove(client);
                    _unitOfWork.Complete();
                    MessageBox.Show("Клиент успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Невозможно удалить клиента из-за связанных данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}