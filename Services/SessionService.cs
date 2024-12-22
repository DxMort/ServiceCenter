using System.Linq;
using ServiceCentre.DataAccess;
using ServiceCentre.Models;

namespace ServiceCentre.Services
{
    public class SessionService
    {
        private readonly UnitOfWork _unitOfWork;

        public SessionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddSession(string userName)
        {
            var session = new SessionTable
            {
                UserName = userName
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Complete();
        }
        public SessionTable GetCurrentSession()
        {
            return _unitOfWork.Sessions.GetAll().LastOrDefault();
        }
        public void UpdateSession(string userName)
        {
            var currentSession = _unitOfWork.Sessions.GetAll().FirstOrDefault();

            if (currentSession == null)
            {
                // Если записи нет, создаем новую
                currentSession = new SessionTable
                {
                    UserName = userName
                };
                _unitOfWork.Sessions.Add(currentSession);
            }
            else
            {
                // Если запись существует, обновляем имя пользователя
                currentSession.UserName = userName;
                _unitOfWork.Sessions.Update(currentSession);
            }

            _unitOfWork.Complete();
        }

        public void ClearSessions()
        {
            var sessions = _unitOfWork.Sessions.GetAll().ToList();
            foreach (var session in sessions)
            {
                _unitOfWork.Sessions.Remove(session);
            }

            _unitOfWork.Complete();
        }
    }
}
