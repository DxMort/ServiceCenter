using System;
using System.Drawing;
using System.Windows.Forms;
using ServiceCentre.Models;
using ServiceCentre.DataAccess;
using System.Reflection.Emit;

namespace SetriceCenter.Views
{
    public partial class PartsUserControl : UserControl
    {
        private bool _isSelected;
        private readonly UnitOfWork _unitOfWork;
        public Parts PartData { get; private set; }
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                UpdateSelectionStyle();
            }
        }
        public int PartId { get; set; }

        public PartsUserControl(UnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;

            // Подключаем обработчики событий
            this.Click += PartsUserControl_Click;
            this.MouseDoubleClick += PartsUserControl_MouseDoubleClick;

            // Для всех вложенных контролов
            foreach (Control control in Controls)
            {
                // При клике на любой внутренний элемент выделяем UserControl
                control.Click += (s, e) => OnClick(EventArgs.Empty);

                // Для текстбоксов добавляем отдельный обработчик
                if (control is TextBox textBox)
                {
                    textBox.Click += (s, e) => textBox.SelectAll(); // Выделение текста только для текстбокса
                    textBox.Enter += (s, e) => textBox.SelectAll(); // Выделение текста при получении фокуса
                }
            }
        }



        private void PartsUserControl_Click(object sender, EventArgs e)
        {
            IsSelected = true; // Устанавливаем подсветку для текущего контрола
            if (Parent != null)
            {
                foreach (Control ctrl in Parent.Controls)
                {
                    if (ctrl is PartsUserControl partCtrl && partCtrl != this)
                    {
                        partCtrl.IsSelected = false; // Снимаем подсветку с других контролов
                    }
                }
            }
        }


        private void PartsUserControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var part = PartData;  // Получаем текущую запчасть

            // Если запчасть существует, открываем форму редактирования
            if (part != null)
            {
                var editForm = new EditForm(part, _unitOfWork);
                editForm.EntityUpdated += entity =>
                {
                    if (entity is Parts updatedPart)
                    {
                        SetData(updatedPart); // Обновляем данные
                    }
                };
                editForm.ShowDialog();
            }
        }


        private void EditForm_PartUpdated(Parts updatedPart)
        {
            SetData(updatedPart);
        }

        private void UpdateSelectionStyle()
        {
            this.BackColor = IsSelected ? Color.LightBlue : SystemColors.Control;
        }

        public Parts GetPartData()
        {
            // Обработка даты без Nullable
            DateTime parsedDate = DateTime.TryParse(LastUpdateDateLabel.Text, out var lastUpdateDate)
                ? lastUpdateDate
                : new DateTime(2000, 1, 1); // Используем дефолтное значение вместо null

            return new Parts
            {
                Id = GetPartId(),
                PartName = NameLabel.Text,
                Manufacturer = ManufacturerLabel.Text,
                Description = DescriptionTextBox.Text,
                Quantity = int.TryParse(QuantityLabel.Text, out var quantity) ? quantity : 0,
                Price = decimal.TryParse(PriceLabel.Text, out var price) ? price : 0,
                LastUpdateDate = parsedDate, // Передаем дату как значение DateTime
                SupportedModels = SupportedModelsTextBox.Text
            };
        }

        private int GetPartId()
        {
            int.TryParse(IdLabel.Text, out var id);
            return id;
        }

        public void SetData(Parts part)
        {
            PartData = part;
            IdLabel.Text = part.Id.ToString();
            NameLabel.Text = part.PartName;
            ManufacturerLabel.Text = part.Manufacturer;
            QuantityLabel.Text = part.Quantity.ToString();
            PriceLabel.Text = part.Price.ToString("C");

            // Дата всегда имеет значение, убираем проверки на null
            LastUpdateDateLabel.Text = part.LastUpdateDate.ToString("g");

            CellNumberLabel.Text = part.CellNumber ?? "Не указано";
            DescriptionTextBox.Text = part.Description;
            SupportedModelsTextBox.Text = part.SupportedModels;

            // Устанавливаем изображение в PictureBox
            string defaultImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Parts", "default.png");

            PartImagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Добавьте эту строку для подгонки изображения

            if (!string.IsNullOrWhiteSpace(part.ImagePath) && System.IO.File.Exists(part.ImagePath))
            {
                try
                {
                    PartImagePictureBox.Image = Image.FromFile(part.ImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PartImagePictureBox.Image = Image.FromFile(defaultImagePath); // Устанавливаем дефолтное изображение
                }
            }
            else
            {
                // Если путь пустой или файл отсутствует, устанавливаем дефолтное изображение
                if (System.IO.File.Exists(defaultImagePath))
                {
                    PartImagePictureBox.Image = Image.FromFile(defaultImagePath);
                }
                else
                {
                    MessageBox.Show("Дефолтное изображение отсутствует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PartImagePictureBox.Image = null; // Устанавливаем пустое изображение
                }
            }
        }



    }
}
