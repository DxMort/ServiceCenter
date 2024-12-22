using ServiceCentre.DataAccess;
using ServiceCentre.Helpers;
using ServiceCentre.Models;
using ServiceCentre.Services;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SetriceCenter
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            PasswordTextBox.UseSystemPasswordChar = true;
        }


        private void LoginButton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string username1 = "111";
            //    string password1 = "111";
            //    string role = "admin";

            //    using (var context = new ServiceCentreContext())
            //    {
            //        // Создаем нового пользователя
            //        var newUser = new Users
            //        {
            //            Username = username1,
            //            PasswordHash = ComputeHash(password1), // Хэшируем пароль
            //            Role = role,
            //            Phone = "111", // Заполняем обязательное поле Phone
            //            Email = "111@example.com", // Заполняем Email
            //            CreatedDate = DateTime.Now // Устанавливаем текущую дату
            //        };

            //        // Добавляем пользователя в базу данных
            //        context.Users.Add(newUser);
            //        context.SaveChanges();

            //        MessageBox.Show("Пользователь с именем '111' и ролью 'admin' успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}











            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var context = new ServiceCentreContext())
                {
                    using (var unitOfWork = new UnitOfWork(context))
                    {
                        var userService = new UserService(unitOfWork);
                        var sessionService = new SessionService(unitOfWork);

                        // Проверка авторизации через UserService
                        var user = userService.Authenticate(username, password);

                        if (user == null)
                        {
                            MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Сохранение данных о сессии
                        Session.UserId = user.Id;
                        Session.Username = user.Username;
                        Session.Role = user.Role;

                        // Обновление записи в таблице сессии через SessionService
                        sessionService.UpdateSession(Session.Username);

                        // Открытие главного окна
                        var mainWindow = new MainForm();
                        mainWindow.FormClosed += (s, args) =>
                        {
                            var main = (MainForm)s;
                            if (main.IsLoggingOut)
                            {
                                this.Show();
                            }
                            else
                            {
                                Application.Exit();
                            }
                        };

                        this.Hide();
                        mainWindow.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
