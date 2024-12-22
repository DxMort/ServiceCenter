using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ServiceCentre.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Users> GetUsers()
        {
            return _unitOfWork.Users.GetAll();
        }
        public Users GetById(int id)
        {
            return _unitOfWork.Users.GetAll().FirstOrDefault(u => u.Id == id);
        }
        public void Add(Users user)
        {
            user.CreatedDate = user.CreatedDate.Date;
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }
        public void Update(Users user)
        {
            _unitOfWork.Complete();
        }
        public bool CanDeleteUser(int userId)
        {
            if (_unitOfWork.Requests.GetAll().Any(r => r.CurrentMasterId == userId))
            {
                return false;
            }

            return true;
        }
        public Users Authenticate(string username, string password)
        {
            // Хэшируем введённый пароль
            string hashedPassword = ComputeHash(password);

            // Проверяем пользователя в базе данных
            var user = _unitOfWork.Users.GetAll()
                .SingleOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            return user;
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public void Delete(int userId)
        {
            if (CanDeleteUser(userId))
            {
                var user = GetById(userId);
                if (user != null)
                {
                    _unitOfWork.Users.Remove(user);
                    _unitOfWork.Complete();
                    MessageBox.Show("Пользователь удалён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Невозможно удалить пользователя из-за зависимых записей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}