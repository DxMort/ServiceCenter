namespace SetriceCenter.Views
{
    partial class PartsUserControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PartImagePictureBox = new System.Windows.Forms.PictureBox();
            this.IdLabel = new System.Windows.Forms.Label();
            this.CellNumberLabel = new System.Windows.Forms.Label();
            this.LastUpdateDateLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.ManufacturerLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.DescriptionTextBox = new System.Windows.Forms.TextBox();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.SupportedModelsTextBox = new System.Windows.Forms.TextBox();
            this.SupportedModelsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PartImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PartImagePictureBox
            // 
            this.PartImagePictureBox.Location = new System.Drawing.Point(3, 3);
            this.PartImagePictureBox.Name = "PartImagePictureBox";
            this.PartImagePictureBox.Size = new System.Drawing.Size(135, 135);
            this.PartImagePictureBox.TabIndex = 0;
            this.PartImagePictureBox.TabStop = false;
            // 
            // IdLabel
            // 
            this.IdLabel.AutoSize = true;
            this.IdLabel.Location = new System.Drawing.Point(468, 3);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(35, 13);
            this.IdLabel.TabIndex = 6;
            this.IdLabel.Text = "label1";
            // 
            // CellNumberLabel
            // 
            this.CellNumberLabel.AutoSize = true;
            this.CellNumberLabel.Location = new System.Drawing.Point(153, 116);
            this.CellNumberLabel.Name = "CellNumberLabel";
            this.CellNumberLabel.Size = new System.Drawing.Size(85, 13);
            this.CellNumberLabel.TabIndex = 5;
            this.CellNumberLabel.Text = "Номер ячейки: ";
            // 
            // LastUpdateDateLabel
            // 
            this.LastUpdateDateLabel.AutoSize = true;
            this.LastUpdateDateLabel.Location = new System.Drawing.Point(153, 98);
            this.LastUpdateDateLabel.Name = "LastUpdateDateLabel";
            this.LastUpdateDateLabel.Size = new System.Drawing.Size(69, 13);
            this.LastUpdateDateLabel.TabIndex = 4;
            this.LastUpdateDateLabel.Text = "Обновлено: ";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(154, 60);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(68, 13);
            this.PriceLabel.TabIndex = 3;
            this.PriceLabel.Text = "Стоимость: ";
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.AutoSize = true;
            this.QuantityLabel.Location = new System.Drawing.Point(153, 42);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(69, 13);
            this.QuantityLabel.TabIndex = 2;
            this.QuantityLabel.Text = "Количество:";
            // 
            // ManufacturerLabel
            // 
            this.ManufacturerLabel.AutoSize = true;
            this.ManufacturerLabel.Location = new System.Drawing.Point(153, 21);
            this.ManufacturerLabel.Name = "ManufacturerLabel";
            this.ManufacturerLabel.Size = new System.Drawing.Size(92, 13);
            this.ManufacturerLabel.TabIndex = 1;
            this.ManufacturerLabel.Text = "Производитель: ";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(153, 3);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(63, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Название: ";
            // 
            // DescriptionTextBox
            // 
            this.DescriptionTextBox.Location = new System.Drawing.Point(509, 19);
            this.DescriptionTextBox.Multiline = true;
            this.DescriptionTextBox.Name = "DescriptionTextBox";
            this.DescriptionTextBox.Size = new System.Drawing.Size(362, 114);
            this.DescriptionTextBox.TabIndex = 1;
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(509, 3);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "Описание: ";
            // 
            // SupportedModelsTextBox
            // 
            this.SupportedModelsTextBox.Location = new System.Drawing.Point(877, 19);
            this.SupportedModelsTextBox.Multiline = true;
            this.SupportedModelsTextBox.Name = "SupportedModelsTextBox";
            this.SupportedModelsTextBox.Size = new System.Drawing.Size(368, 114);
            this.SupportedModelsTextBox.TabIndex = 2;
            // 
            // SupportedModelsLabel
            // 
            this.SupportedModelsLabel.AutoSize = true;
            this.SupportedModelsLabel.Location = new System.Drawing.Point(874, 3);
            this.SupportedModelsLabel.Name = "SupportedModelsLabel";
            this.SupportedModelsLabel.Size = new System.Drawing.Size(146, 13);
            this.SupportedModelsLabel.TabIndex = 0;
            this.SupportedModelsLabel.Text = "Поддерживаемые модели: ";
            // 
            // PartsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CellNumberLabel);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.LastUpdateDateLabel);
            this.Controls.Add(this.SupportedModelsLabel);
            this.Controls.Add(this.SupportedModelsTextBox);
            this.Controls.Add(this.DescriptionTextBox);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.QuantityLabel);
            this.Controls.Add(this.ManufacturerLabel);
            this.Controls.Add(this.PartImagePictureBox);
            this.Controls.Add(this.NameLabel);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Name = "PartsUserControl";
            this.Size = new System.Drawing.Size(1248, 140);
            this.DoubleClick += new System.EventHandler(this.PartsUserControl_Click);
            ((System.ComponentModel.ISupportInitialize)(this.PartImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PartImagePictureBox;
        private System.Windows.Forms.Label QuantityLabel;
        private System.Windows.Forms.Label ManufacturerLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label LastUpdateDateLabel;
        private System.Windows.Forms.Label CellNumberLabel;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.Label SupportedModelsLabel;
        private System.Windows.Forms.TextBox DescriptionTextBox;
        private System.Windows.Forms.TextBox SupportedModelsTextBox;
        private System.Windows.Forms.Label IdLabel;
    }
}
