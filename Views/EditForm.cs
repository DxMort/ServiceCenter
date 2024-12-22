using ServiceCentre.DataAccess;
using SetriceCenter.Metadata;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SetriceCenter.Views
{
    public partial class EditForm : Form
    {
        public event Action<object> EntityUpdated; // Универсальное событие для обновления
        private readonly object _entity;
        private readonly object _service;
        private readonly EntityDefinition _definition;
        private readonly bool _isNew;
        private string _selectedImagePath; // Поле для хранения выбранного пути к изображению


        public EditForm(object entity, UnitOfWork unitOfWork, bool isNew = false)
        {
            InitializeComponent();

            _entity = entity;
            _isNew = isNew;
            if (_isNew == true) this.Text = "Добавление";
            var entityType = entity.GetType();
            _definition = MetadataProvider.GetDefinition(entityType.Name);
            _service = MetadataProvider.GetService(unitOfWork, entityType.Name);

            GenerateFields();
        }

        private void GenerateFields()
        {
            int y = 20;
            int formHeight = 0; // Переменная для динамической высоты формы

            foreach (var column in _definition.Columns)
            {
                // Заголовок
                var label = new Label
                {
                    Text = column.DisplayName,
                    Location = new Point(20, y),
                    AutoSize = true
                };
                Controls.Add(label);

                // Если поле - путь к изображению
                if (column.PropertyName == "ImagePath")
                {
                    var button = new Button
                    {
                        Text = "Выберите изображение",
                        Name = $"btn_{column.PropertyName}",
                        Location = new Point(190, y),
                        Width = 200
                    };

                    button.Click += (s, e) => SelectImageFile(column.PropertyName);
                    Controls.Add(button);
                }
                else if (column.IsEditable) // Для редактируемых полей
                {
                    var textBox = new TextBox
                    {
                        Name = $"tb_{column.PropertyName}",
                        Location = new Point(190, y),
                        Width = 200,
                        Text = GetNestedPropertyValue(_entity, column.PropertyName)?.ToString() ?? ""
                    };
                    Controls.Add(textBox);
                }
                else // Для нередактируемых полей
                {
                    var valueLabel = new Label
                    {
                        Name = $"lbl_{column.PropertyName}",
                        Location = new Point(190, y),
                        AutoSize = true,
                        Text = GetNestedPropertyValue(_entity, column.PropertyName)?.ToString() ?? "Не указано"
                    };
                    Controls.Add(valueLabel);
                }

                y += 30; // Расстояние между полями
                formHeight = y; // Обновляем высоту
            }

            SaveButton.Location = new Point(150, y + 10);
            formHeight += 100; // Увеличиваем высоту формы
            this.Height = formHeight; // Устанавливаем высоту формы
        }



        private void SelectImageFile(string propertyName)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                openFileDialog.Title = "Выберите изображение";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = openFileDialog.FileName;
                    MessageBox.Show($"Файл загружен: {_selectedImagePath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Устанавливаем значение в объект
                    var prop = _entity.GetType().GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_entity, _selectedImagePath);
                    }
                }
            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Проверка, что все обязательные текстовые поля заполнены
            foreach (var column in _definition.Columns)
            {
                if (column.IsPrimaryKey) continue; // Пропускаем проверку для первичных ключей

                if (column.IsEditable) // Проверяем только редактируемые поля
                {
                    var textBox = Controls[$"tb_{column.PropertyName}"] as TextBox;
                    if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageBox.Show($"Поле \"{column.DisplayName}\" должно быть заполнено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox.Focus(); // Устанавливаем фокус на незаполненное поле
                        return; // Прекращаем выполнение метода
                    }
                }
            }

            foreach (var column in _definition.Columns)
            {
                if (column.IsPrimaryKey) continue; // Не устанавливаем значение Id, так как оно будет автоматически назначаться в базе данных

                var textBox = Controls[$"tb_{column.PropertyName}"] as TextBox;
                var prop = _entity.GetType().GetProperty(column.PropertyName);

                if (prop != null)
                {
                    // Проверяем значение через GetNestedPropertyValue
                    var currentValue = GetNestedPropertyValue(_entity, column.PropertyName);

                    // Если TextBox не найден, сохраняем текущее значение без изменений
                    if (textBox == null)
                    {
                        prop.SetValue(_entity, currentValue);
                        continue;
                    }

                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    var newValue = string.IsNullOrWhiteSpace(textBox.Text)
                        ? null
                        : Convert.ChangeType(textBox.Text, targetType);

                    // Если это поле "PasswordHash", выполняем хэширование
                    if (column.PropertyName == "PasswordHash" && !string.IsNullOrEmpty(textBox.Text))
                    {
                        newValue = ComputeHash(textBox.Text); // Хэшируем значение
                    }

                    if (targetType == typeof(DateTime) && newValue is DateTime dateTimeValue)
                    {
                        if (dateTimeValue < new DateTime(1753, 1, 1))
                        {
                            newValue = new DateTime(2000, 1, 1); // Минимально допустимая дата в SQL Server
                        }
                    }

                    if (!Equals(currentValue, newValue))
                    {
                        prop.SetValue(_entity, newValue);
                    }
                }
            }

            // Сохранение через сервис
            if (_isNew)
            {
                _service.GetType().GetMethod("Add").Invoke(_service, new[] { _entity });
            }
            else
            {
                _service.GetType().GetMethod("Update").Invoke(_service, new[] { _entity });
            }

            EntityUpdated?.Invoke(_entity);

            MessageBox.Show("Запись сохранена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }


        private object GetNestedPropertyValue(object obj, string propertyPath)
        {
            foreach (var propertyName in propertyPath.Split('.'))
            {
                if (obj == null) return null;

                var property = obj.GetType().GetProperty(propertyName,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (property == null) return null;
                obj = property.GetValue(obj);

                if ((property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?)))
                {
                    return DateTime.Now; // Возвращаем текущую дату
                }
            }
            return obj;
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