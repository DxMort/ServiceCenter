using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;

namespace ServiceCentre.Services
{
    public class AccessService
    {
        private readonly UnitOfWork _unitOfWork;

        public AccessService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<AccessAdmin> GetAccessKeys()
        {
            return _unitOfWork.AccessAdmin.GetAll();
        }

        public AccessAdmin GetAccessKeyById(int id)
        {
            return _unitOfWork.AccessAdmin.GetAll().FirstOrDefault(a => a.Id == id);
        }

        public void AddAccessKey(AccessAdmin access)
        {
            _unitOfWork.AccessAdmin.Add(access);
            _unitOfWork.Complete();
        }

        public void UpdateAccessKey(AccessAdmin access)
        {
            _unitOfWork.Complete();
        }

        public void DeleteAccessKey(int id)
        {
            var access = GetAccessKeyById(id);
            if (access != null)
            {
                _unitOfWork.AccessAdmin.Remove(access);
                _unitOfWork.Complete();
            }
        }
    }
}