namespace SetriceCenter
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FullAccessButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.UserroleLabel = new System.Windows.Forms.Label();
            this.ServiceTabControl = new System.Windows.Forms.TabControl();
            this.RequestsTabPage = new System.Windows.Forms.TabPage();
            this.RequestsDataGridView = new System.Windows.Forms.DataGridView();
            this.PartsTabPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.NextPageButton = new System.Windows.Forms.Button();
            this.PreviousPageButton = new System.Windows.Forms.Button();
            this.PartsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OrderPartsTabPage = new System.Windows.Forms.TabPage();
            this.OrderPartsDataGridView = new System.Windows.Forms.DataGridView();
            this.PaymentsTabPage = new System.Windows.Forms.TabPage();
            this.PaymentsDataGridView = new System.Windows.Forms.DataGridView();
            this.ClientsTabPage = new System.Windows.Forms.TabPage();
            this.ClientsDataGridView = new System.Windows.Forms.DataGridView();
            this.RepairsLogsTabPage = new System.Windows.Forms.TabPage();
            this.RepairsLogsDataGridView = new System.Windows.Forms.DataGridView();
            this.UsersTabPage = new System.Windows.Forms.TabPage();
            this.UsersDataGridView = new System.Windows.Forms.DataGridView();
            this.LogsTabPage = new System.Windows.Forms.TabPage();
            this.LogsDataGridView = new System.Windows.Forms.DataGridView();
            this.ResetFiltersButton = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.FilterValueComboBox = new System.Windows.Forms.ComboBox();
            this.FilterTypeComboBox = new System.Windows.Forms.ComboBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddingButton = new System.Windows.Forms.Button();
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.ServiceTabControl.SuspendLayout();
            this.RequestsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RequestsDataGridView)).BeginInit();
            this.PartsTabPage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.OrderPartsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderPartsDataGridView)).BeginInit();
            this.PaymentsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentsDataGridView)).BeginInit();
            this.ClientsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientsDataGridView)).BeginInit();
            this.RepairsLogsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepairsLogsDataGridView)).BeginInit();
            this.UsersTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsersDataGridView)).BeginInit();
            this.LogsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FullAccessButton
            // 
            this.FullAccessButton.Location = new System.Drawing.Point(825, 10);
            this.FullAccessButton.Name = "FullAccessButton";
            this.FullAccessButton.Size = new System.Drawing.Size(100, 25);
            this.FullAccessButton.TabIndex = 0;
            this.FullAccessButton.Text = "Полный доступ";
            this.FullAccessButton.UseVisualStyleBackColor = true;
            this.FullAccessButton.Click += new System.EventHandler(this.FullAccessButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(931, 10);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(100, 25);
            this.LogoutButton.TabIndex = 1;
            this.LogoutButton.Text = "Выход";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(1037, 16);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(103, 13);
            this.UsernameLabel.TabIndex = 2;
            this.UsernameLabel.Text = "Имя пользователя";
            this.UsernameLabel.Click += new System.EventHandler(this.UsernameLabel_Click);
            // 
            // UserroleLabel
            // 
            this.UserroleLabel.AutoSize = true;
            this.UserroleLabel.Location = new System.Drawing.Point(1146, 16);
            this.UserroleLabel.Name = "UserroleLabel";
            this.UserroleLabel.Size = new System.Drawing.Size(106, 13);
            this.UserroleLabel.TabIndex = 3;
            this.UserroleLabel.Text = "Роль пользователя";
            // 
            // ServiceTabControl
            // 
            this.ServiceTabControl.Controls.Add(this.RequestsTabPage);
            this.ServiceTabControl.Controls.Add(this.PartsTabPage);
            this.ServiceTabControl.Controls.Add(this.OrderPartsTabPage);
            this.ServiceTabControl.Controls.Add(this.PaymentsTabPage);
            this.ServiceTabControl.Controls.Add(this.ClientsTabPage);
            this.ServiceTabControl.Controls.Add(this.RepairsLogsTabPage);
            this.ServiceTabControl.Controls.Add(this.UsersTabPage);
            this.ServiceTabControl.Controls.Add(this.LogsTabPage);
            this.ServiceTabControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ServiceTabControl.Location = new System.Drawing.Point(0, 34);
            this.ServiceTabControl.Name = "ServiceTabControl";
            this.ServiceTabControl.SelectedIndex = 0;
            this.ServiceTabControl.Size = new System.Drawing.Size(1264, 517);
            this.ServiceTabControl.TabIndex = 6;
            // 
            // RequestsTabPage
            // 
            this.RequestsTabPage.Controls.Add(this.RequestsDataGridView);
            this.RequestsTabPage.Location = new System.Drawing.Point(4, 22);
            this.RequestsTabPage.Name = "RequestsTabPage";
            this.RequestsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.RequestsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.RequestsTabPage.TabIndex = 0;
            this.RequestsTabPage.Text = "Заявки на ремонт";
            this.RequestsTabPage.UseVisualStyleBackColor = true;
            // 
            // RequestsDataGridView
            // 
            this.RequestsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RequestsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RequestsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.RequestsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.RequestsDataGridView.Name = "RequestsDataGridView";
            this.RequestsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.RequestsDataGridView.TabIndex = 3;
            // 
            // PartsTabPage
            // 
            this.PartsTabPage.Controls.Add(this.flowLayoutPanel1);
            this.PartsTabPage.Controls.Add(this.PartsFlowLayoutPanel);
            this.PartsTabPage.Location = new System.Drawing.Point(4, 22);
            this.PartsTabPage.Name = "PartsTabPage";
            this.PartsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PartsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.PartsTabPage.TabIndex = 1;
            this.PartsTabPage.Text = "Запчасти";
            this.PartsTabPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.NextPageButton);
            this.flowLayoutPanel1.Controls.Add(this.PreviousPageButton);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 459);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1250, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // NextPageButton
            // 
            this.NextPageButton.Location = new System.Drawing.Point(1172, 3);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(75, 23);
            this.NextPageButton.TabIndex = 1;
            this.NextPageButton.Text = "→";
            this.NextPageButton.UseVisualStyleBackColor = true;
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // PreviousPageButton
            // 
            this.PreviousPageButton.Location = new System.Drawing.Point(1091, 3);
            this.PreviousPageButton.Name = "PreviousPageButton";
            this.PreviousPageButton.Size = new System.Drawing.Size(75, 23);
            this.PreviousPageButton.TabIndex = 0;
            this.PreviousPageButton.Text = "←";
            this.PreviousPageButton.UseVisualStyleBackColor = true;
            this.PreviousPageButton.Click += new System.EventHandler(this.PreviousPageButton_Click);
            // 
            // PartsFlowLayoutPanel
            // 
            this.PartsFlowLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.PartsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.PartsFlowLayoutPanel.Name = "PartsFlowLayoutPanel";
            this.PartsFlowLayoutPanel.Size = new System.Drawing.Size(1250, 456);
            this.PartsFlowLayoutPanel.TabIndex = 0;
            // 
            // OrderPartsTabPage
            // 
            this.OrderPartsTabPage.Controls.Add(this.OrderPartsDataGridView);
            this.OrderPartsTabPage.Location = new System.Drawing.Point(4, 22);
            this.OrderPartsTabPage.Name = "OrderPartsTabPage";
            this.OrderPartsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.OrderPartsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.OrderPartsTabPage.TabIndex = 2;
            this.OrderPartsTabPage.Text = "Заявки на запчасти";
            this.OrderPartsTabPage.UseVisualStyleBackColor = true;
            // 
            // OrderPartsDataGridView
            // 
            this.OrderPartsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrderPartsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OrderPartsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.OrderPartsDataGridView.Name = "OrderPartsDataGridView";
            this.OrderPartsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.OrderPartsDataGridView.TabIndex = 9;
            // 
            // PaymentsTabPage
            // 
            this.PaymentsTabPage.Controls.Add(this.PaymentsDataGridView);
            this.PaymentsTabPage.Location = new System.Drawing.Point(4, 22);
            this.PaymentsTabPage.Name = "PaymentsTabPage";
            this.PaymentsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PaymentsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.PaymentsTabPage.TabIndex = 3;
            this.PaymentsTabPage.Text = "Оплаты";
            this.PaymentsTabPage.UseVisualStyleBackColor = true;
            // 
            // PaymentsDataGridView
            // 
            this.PaymentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PaymentsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaymentsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.PaymentsDataGridView.Name = "PaymentsDataGridView";
            this.PaymentsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.PaymentsDataGridView.TabIndex = 9;
            // 
            // ClientsTabPage
            // 
            this.ClientsTabPage.Controls.Add(this.ClientsDataGridView);
            this.ClientsTabPage.Location = new System.Drawing.Point(4, 22);
            this.ClientsTabPage.Name = "ClientsTabPage";
            this.ClientsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ClientsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.ClientsTabPage.TabIndex = 4;
            this.ClientsTabPage.Text = "Клиенты";
            this.ClientsTabPage.UseVisualStyleBackColor = true;
            // 
            // ClientsDataGridView
            // 
            this.ClientsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.ClientsDataGridView.Name = "ClientsDataGridView";
            this.ClientsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.ClientsDataGridView.TabIndex = 9;
            // 
            // RepairsLogsTabPage
            // 
            this.RepairsLogsTabPage.Controls.Add(this.RepairsLogsDataGridView);
            this.RepairsLogsTabPage.Location = new System.Drawing.Point(4, 22);
            this.RepairsLogsTabPage.Name = "RepairsLogsTabPage";
            this.RepairsLogsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.RepairsLogsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.RepairsLogsTabPage.TabIndex = 5;
            this.RepairsLogsTabPage.Text = "Логи ремонтов";
            this.RepairsLogsTabPage.UseVisualStyleBackColor = true;
            // 
            // RepairsLogsDataGridView
            // 
            this.RepairsLogsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RepairsLogsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RepairsLogsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.RepairsLogsDataGridView.Name = "RepairsLogsDataGridView";
            this.RepairsLogsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.RepairsLogsDataGridView.TabIndex = 9;
            // 
            // UsersTabPage
            // 
            this.UsersTabPage.Controls.Add(this.UsersDataGridView);
            this.UsersTabPage.Location = new System.Drawing.Point(4, 22);
            this.UsersTabPage.Name = "UsersTabPage";
            this.UsersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.UsersTabPage.Size = new System.Drawing.Size(1256, 491);
            this.UsersTabPage.TabIndex = 6;
            this.UsersTabPage.Text = "Сотруднки";
            this.UsersTabPage.UseVisualStyleBackColor = true;
            // 
            // UsersDataGridView
            // 
            this.UsersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UsersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersDataGridView.Location = new System.Drawing.Point(3, 3);
            this.UsersDataGridView.Name = "UsersDataGridView";
            this.UsersDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.UsersDataGridView.TabIndex = 9;
            // 
            // LogsTabPage
            // 
            this.LogsTabPage.Controls.Add(this.LogsDataGridView);
            this.LogsTabPage.Location = new System.Drawing.Point(4, 22);
            this.LogsTabPage.Name = "LogsTabPage";
            this.LogsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogsTabPage.Size = new System.Drawing.Size(1256, 491);
            this.LogsTabPage.TabIndex = 7;
            this.LogsTabPage.Text = "Логи";
            this.LogsTabPage.UseVisualStyleBackColor = true;
            // 
            // LogsDataGridView
            // 
            this.LogsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogsDataGridView.Location = new System.Drawing.Point(3, 3);
            this.LogsDataGridView.Name = "LogsDataGridView";
            this.LogsDataGridView.Size = new System.Drawing.Size(1250, 485);
            this.LogsDataGridView.TabIndex = 9;
            // 
            // ResetFiltersButton
            // 
            this.ResetFiltersButton.Location = new System.Drawing.Point(367, 8);
            this.ResetFiltersButton.Name = "ResetFiltersButton";
            this.ResetFiltersButton.Size = new System.Drawing.Size(75, 23);
            this.ResetFiltersButton.TabIndex = 2;
            this.ResetFiltersButton.Text = "Сбросить";
            this.ResetFiltersButton.UseVisualStyleBackColor = true;
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(261, 8);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 20);
            this.SearchTextBox.TabIndex = 1;
            // 
            // FilterValueComboBox
            // 
            this.FilterValueComboBox.FormattingEnabled = true;
            this.FilterValueComboBox.Location = new System.Drawing.Point(134, 8);
            this.FilterValueComboBox.Name = "FilterValueComboBox";
            this.FilterValueComboBox.Size = new System.Drawing.Size(121, 21);
            this.FilterValueComboBox.TabIndex = 0;
            // 
            // FilterTypeComboBox
            // 
            this.FilterTypeComboBox.FormattingEnabled = true;
            this.FilterTypeComboBox.Location = new System.Drawing.Point(7, 8);
            this.FilterTypeComboBox.Name = "FilterTypeComboBox";
            this.FilterTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.FilterTypeComboBox.TabIndex = 0;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(448, 8);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 7;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddingButton
            // 
            this.AddingButton.Location = new System.Drawing.Point(529, 8);
            this.AddingButton.Name = "AddingButton";
            this.AddingButton.Size = new System.Drawing.Size(75, 23);
            this.AddingButton.TabIndex = 8;
            this.AddingButton.Text = "Добавить";
            this.AddingButton.UseVisualStyleBackColor = true;
            this.AddingButton.Click += new System.EventHandler(this.AddingButton_Click);
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(610, 8);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(75, 23);
            this.SaveChangesButton.TabIndex = 9;
            this.SaveChangesButton.Text = "Сохранить";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 551);
            this.Controls.Add(this.SaveChangesButton);
            this.Controls.Add(this.AddingButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ServiceTabControl);
            this.Controls.Add(this.ResetFiltersButton);
            this.Controls.Add(this.UserroleLabel);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.FilterValueComboBox);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.FilterTypeComboBox);
            this.Controls.Add(this.FullAccessButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сервисный центр";
            this.ServiceTabControl.ResumeLayout(false);
            this.RequestsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RequestsDataGridView)).EndInit();
            this.PartsTabPage.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.OrderPartsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OrderPartsDataGridView)).EndInit();
            this.PaymentsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PaymentsDataGridView)).EndInit();
            this.ClientsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClientsDataGridView)).EndInit();
            this.RepairsLogsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RepairsLogsDataGridView)).EndInit();
            this.UsersTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UsersDataGridView)).EndInit();
            this.LogsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FullAccessButton;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label UserroleLabel;
        private System.Windows.Forms.TabControl ServiceTabControl;
        private System.Windows.Forms.TabPage RequestsTabPage;
        private System.Windows.Forms.TabPage PartsTabPage;
        private System.Windows.Forms.TabPage OrderPartsTabPage;
        private System.Windows.Forms.TabPage PaymentsTabPage;
        private System.Windows.Forms.TabPage ClientsTabPage;
        private System.Windows.Forms.TabPage RepairsLogsTabPage;
        private System.Windows.Forms.TabPage UsersTabPage;
        private System.Windows.Forms.TabPage LogsTabPage;
        private System.Windows.Forms.Button ResetFiltersButton;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.ComboBox FilterValueComboBox;
        private System.Windows.Forms.ComboBox FilterTypeComboBox;
        private System.Windows.Forms.DataGridView RequestsDataGridView;
        private System.Windows.Forms.DataGridView OrderPartsDataGridView;
        private System.Windows.Forms.DataGridView PaymentsDataGridView;
        private System.Windows.Forms.DataGridView ClientsDataGridView;
        private System.Windows.Forms.DataGridView RepairsLogsDataGridView;
        private System.Windows.Forms.DataGridView UsersDataGridView;
        private System.Windows.Forms.DataGridView LogsDataGridView;
        private System.Windows.Forms.FlowLayoutPanel PartsFlowLayoutPanel;
        private System.Windows.Forms.Button PreviousPageButton;
        private System.Windows.Forms.Button NextPageButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddingButton;
        private System.Windows.Forms.Button SaveChangesButton;
    }
}

