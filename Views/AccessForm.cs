using ServiceCentre.Helpers;
using ServiceCentre.Models;
using ServiceCentre.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetviceCenter.Views
{
    public partial class AccessForm : Form
    {
        private readonly AccessService _accessService;
        private readonly string _userRole; // Роль текущего пользователя
        private AccessAdmin _currentAccess; // Текущий объект Access
        public AccessForm(AccessService accessService, string userRole)
        {
            InitializeComponent();
            _accessService = accessService;
            _userRole = userRole;

            LoadAccessKey();
            ActionButton.DialogResult = DialogResult.OK;
        }
        private void LoadAccessKey()
        {
            _currentAccess = _accessService.GetAccessKeys().FirstOrDefault();

            if (_currentAccess != null && _userRole == "admin")
            {
                // Админ видит текущий пароль и может его редактировать
                PasswordTextBox.Text = _currentAccess.AccessKey;
                PasswordTextBox.ReadOnly = false;
                ActionButton.Text = "Сохранить"; // Кнопка для изменения пароля
            }
            else if (_userRole == "manager" || _userRole == "master" || _userRole == "tempadmin")
            {
                // Мастера и менеджеры могут вводить пароль для проверки
                PasswordTextBox.Text = string.Empty;
                PasswordTextBox.ReadOnly = false; // Разрешаем ввод
                ActionButton.Text = "Авторизоваться"; // Кнопка для проверки пароля
            }
        }

        private void ActionButton_Click(object sender, EventArgs e)
        {
            if (_userRole == "admin")
            {
                // Сохранение пароля
                if (_currentAccess == null)
                {
                    _currentAccess = new AccessAdmin { AccessKey = PasswordTextBox.Text };
                    _accessService.AddAccessKey(_currentAccess);
                }
                else
                {
                    _currentAccess.AccessKey = PasswordTextBox.Text;
                    _accessService.UpdateAccessKey(_currentAccess);
                }

                MessageBox.Show("Пароль успешно сохранён.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Устанавливаем результат
                this.Close();
            }
            else if (_userRole == "manager" || _userRole == "master" || _userRole == "tempadmin")
            {
                // Проверка пароля
                if (_currentAccess != null && PasswordTextBox.Text == _currentAccess.AccessKey)
                {
                    Session.Role = "tempadmin";
                    MessageBox.Show("Доступ предоставлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Устанавливаем результат
                }
                else
                {
                    MessageBox.Show("Неверный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}