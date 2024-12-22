using ServiceCentre.DataAccess;
using ServiceCentre.Helpers;
using ServiceCentre.Models;
using ServiceCentre.Services;
using SetriceCenter.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SetriceCenter.Views;
using System.Linq.Dynamic.Core;
using System.ComponentModel;
using System.Data.Entity;
using SetviceCenter.Views;

namespace SetriceCenter
{
    public partial class MainForm : Form
    {
        private UnitOfWork _unitOfWork;
        private EntityDefinition _currentEntityDefinition;
        private string _currentEntityName;
        private List<Parts> _currentPartsList;

        private int _currentPage = 1;
        private int _itemsPerPage = 3;

        private static readonly Dictionary<string, Type> _entityTypes = new Dictionary<string, Type>
        {
            { "Parts", typeof(ServiceCentre.Models.Parts) },
            { "Requests", typeof(ServiceCentre.Models.Requests) },
            { "OrderParts", typeof(ServiceCentre.Models.OrderParts) },
            { "Clients", typeof(ServiceCentre.Models.Clients) },
            { "Users", typeof(ServiceCentre.Models.Users) },
            { "Payments", typeof(ServiceCentre.Models.Payments) },
            { "RepairLogs", typeof(ServiceCentre.Models.RepairLogs) },
            { "Logs", typeof(ServiceCentre.Models.Logs) }
        };
        private static readonly Dictionary<string, Func<UnitOfWork, object>> _serviceFactories = new Dictionary<string, Func<UnitOfWork, object>>
        {
            { "Parts", uow => new PartService(uow) },
            { "Requests", uow => new RequestService(uow) },
            { "OrderParts", uow => new OrderPartsService(uow) },
            { "Clients", uow => new ClientService(uow) },
            { "Users", uow => new UserService(uow) },
            { "Payments", uow => new PaymentService(uow) },
            { "RepairLogs", uow => new RepairLogsService(uow) },
            { "Logs", uow => new LogsService(uow) },
        };

        private Control CurrentControl
        {
            get
            {
                switch (_currentEntityName)
                {
                    case "Parts":
                        return PartsFlowLayoutPanel; // Панель с UserControl'ами
                    case "Requests":
                        return RequestsDataGridView;
                    case "Clients":
                        return ClientsDataGridView;
                    case "OrderParts":
                        return OrderPartsDataGridView;
                    case "Payments":
                        return PaymentsDataGridView;
                    case "RepairLogs":
                        return RepairsLogsDataGridView;
                    case "Users":
                        return UsersDataGridView;
                    case "Logs":
                        return LogsDataGridView;
                    default:
                        return null;
                }
            }
        }

        public MainForm()
        {
            InitializeComponent();

            var context = new ServiceCentreContext();
            _unitOfWork = new UnitOfWork(context);

            UpdateUserInfo();
            ConfigureTabsByRole();

            ServiceTabControl.SelectedIndexChanged += ServiceTabControl_SelectedIndexChanged;
            FilterTypeComboBox.SelectedIndexChanged += (s, e) => UpdateFilterValuesAndLoadData();
            FilterValueComboBox.SelectedIndexChanged += (s, e) => LoadCurrentTabData();
            SearchTextBox.TextChanged += (s, e) => LoadCurrentTabData();
            ResetFiltersButton.Click += (s, e) => ResetCurrentFilters();

            ServiceTabControl_SelectedIndexChanged(null, null);
        }


        private void UpdateUserInfo()
        {
            UsernameLabel.Text = Session.Username;
            UserroleLabel.Text = Session.Role;
        }
        private void ConfigureTabsByRole()
        {
            var userRole = Session.GetUserRole();

            void SetTabVisibility(TabPage tabPage, bool visible)
            {
                if (visible)
                {
                    if (!ServiceTabControl.TabPages.Contains(tabPage))
                        ServiceTabControl.TabPages.Add(tabPage);
                }
                else
                {
                    if (ServiceTabControl.TabPages.Contains(tabPage))
                        ServiceTabControl.TabPages.Remove(tabPage);
                }
            }

            PartsTabPage.Tag = "Parts";
            RequestsTabPage.Tag = "Requests";
            ClientsTabPage.Tag = "Clients";
            OrderPartsTabPage.Tag = "OrderParts";
            PaymentsTabPage.Tag = "Payments";
            LogsTabPage.Tag = "Logs";
            RepairsLogsTabPage.Tag = "RepairLogs";
            UsersTabPage.Tag = "Users";
            // Аналогично для других вкладок (Payments, OrderParts, Users, Logs и т.д.)

            // "Запчасти" доступно только мастерам и админам
            SetTabVisibility(PartsTabPage, userRole == "master" || userRole == "admin" || userRole == "tempadmin");

            // "Заявки на запчасти" доступно только мастерам и админам
            SetTabVisibility(OrderPartsTabPage, userRole == "master" || userRole == "admin" || userRole == "tempadmin");

            // "Оплаты" доступно только менеджерам и админам
            SetTabVisibility(PaymentsTabPage, userRole == "manager" || userRole == "admin" || userRole == "tempadmin");

            // "Клиенты" доступно только менеджерам и админам
            SetTabVisibility(ClientsTabPage, userRole == "manager" || userRole == "admin" || userRole == "tempadmin");

            // "Логи ремонта" доступно только мастерам и админам
            SetTabVisibility(RepairsLogsTabPage, userRole == "master" || userRole == "admin" || userRole == "tempadmin");

            // "Работники" доступно только админам
            SetTabVisibility(UsersTabPage, userRole == "admin");

            // "Логи" доступно только админам
            SetTabVisibility(LogsTabPage, userRole == "admin" || userRole == "tempadmin");
        }


        private void ClearFilterControls()
        {
            FilterTypeComboBox.DataSource = null;
            FilterValueComboBox.DataSource = null;
            SearchTextBox.Text = string.Empty;
        }
        private void ResetCurrentFilters()
        {
            if (_currentEntityDefinition == null) return;

            var filterableColumns = _currentEntityDefinition.Columns
                .Where(c => c.IsFilterable)
                .Select(c => c.DisplayName)
                .ToList();

            FilterTypeComboBox.DataSource = filterableColumns;
            FilterTypeComboBox.SelectedIndex = filterableColumns.Count > 0 ? 0 : -1;

            UpdateFilterValuesAndLoadData();
        }
        private void UpdateFilterValuesAndLoadData()
        {
            if (_currentEntityDefinition == null) return;

            var selectedDisplayName = FilterTypeComboBox.SelectedItem?.ToString();
            FilterValueComboBox.DataSource = null;

            if (!string.IsNullOrEmpty(selectedDisplayName))
            {
                var column = _currentEntityDefinition.Columns.FirstOrDefault(c => c.DisplayName == selectedDisplayName);
                if (column != null)
                {
                    var values = GetDistinctColumnValues(column.PropertyName);
                    FilterValueComboBox.DataSource = values;
                    FilterValueComboBox.SelectedIndex = -1;
                }
            }

            LoadCurrentTabData();
        }
        private List<string> GetDistinctColumnValues(string propertyName)
        {
            if (_currentEntityName == "Parts")
            {
                var partsService = new PartService(_unitOfWork);
                var query = partsService.GetParts().ToList(); // в память

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "Requests")
            {
                var requestService = new RequestService(_unitOfWork);
                var query = requestService.GetRequests().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "Clients")
            {
                var clientService = new ClientService(_unitOfWork);
                var query = clientService.GetClients().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "OrderParts")
            {
                var orderPartsService = new OrderPartsService(_unitOfWork);
                var query = orderPartsService.GetOrderParts().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "Payments")
            {
                var paymentService = new PaymentService(_unitOfWork);
                var query = paymentService.GetPayments().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "RepairLogs")
            {
                var repairLogsService = new RepairLogsService(_unitOfWork);
                var query = repairLogsService.GetRepairLogs().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "Users")
            {
                var userService = new UserService(_unitOfWork);
                var query = userService.GetUsers().ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }
            else if (_currentEntityName == "Logs")
            {
                var logsService = new LogsService(_unitOfWork);
                var query = logsService.GetLogs()
                .Select(log => new
                {
                    log.UserName,
                    log.Action,
                    log.Date,
                    log.Description
                }).ToList();

                var values = query
                    .AsQueryable()
                    .Select(propertyName)
                    .Distinct()
                    .Cast<object>()
                    .Where(x => x != null)
                    .Select(x => x.ToString())
                    .ToList();

                return values;
            }

            return new List<string>();
        }


        private void ServiceTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServiceTabControl.SelectedTab != null && ServiceTabControl.SelectedTab.Tag is string entityName)
            {
                _currentEntityName = entityName;
                _currentEntityDefinition = MetadataProvider.GetDefinition(_currentEntityName);
                ResetCurrentFilters();
            }
            else
            {
                _currentEntityName = null;
                _currentEntityDefinition = null;
                ClearFilterControls();
            }
        }
        private void LoadCurrentTabData()
        {
            if (_currentEntityDefinition == null || _currentEntityName == null)
                return;

            var filterTypeDisplayName = FilterTypeComboBox.SelectedItem?.ToString();
            var filterValue = FilterValueComboBox.SelectedItem?.ToString();
            var searchText = SearchTextBox.Text;

            if (_currentEntityName == "Parts")
            {
                LoadPartsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "Requests")
            {
                LoadRequestsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "Clients")
            {
                LoadClientsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "OrderParts")
            {
                LoadOrderPartsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "Users")
            {
                LoadUsersData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "Payments")
            {
                LoadPaymentsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "RepairLogs")
            {
                LoadRepairLogsData(filterTypeDisplayName, filterValue, searchText);
            }
            else if (_currentEntityName == "Logs")
            {
                LoadLogsData(filterTypeDisplayName, filterValue, searchText);
            }
        }
        private void LoadPartsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var partsService = new PartService(_unitOfWork);
            var query = partsService.GetParts();

            // Фильтрация
            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns
                    .FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);

                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            // Поиск по полям PartName, Manufacturer, Description (пример)
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(PartName.ToLower().Contains(@0) or Manufacturer.ToLower().Contains(@0) or Description.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var partsList = query.ToList();
            _currentPartsList = partsList;

            var totalItems = _currentPartsList.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)_itemsPerPage);

            if (_currentPage > totalPages) _currentPage = totalPages == 0 ? 1 : totalPages;

            var paginatedParts = _currentPartsList
                .Skip((_currentPage - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            PartsFlowLayoutPanel.Controls.Clear();
            foreach (var p in paginatedParts)
            {
                var partControl = new PartsUserControl(_unitOfWork);
                partControl.SetData(p);

                PartsFlowLayoutPanel.Controls.Add(partControl);
            }
        }
        private void LoadRequestsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var requestService = new RequestService(_unitOfWork);

            var query = requestService.GetRequests()
                .Include(r => r.Client)
                .Include(r => r.CurrentMaster);

            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns.FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);
                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(Description.ToLower().Contains(@0) or Status.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var requestsList = query.ToList();

            var bindingList = new SortableBindingList<Requests>(requestsList);
            RequestsDataGridView.DataSource = bindingList;

            RequestsDataGridView.AutoGenerateColumns = false;
            RequestsDataGridView.Columns.Clear();

            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ClientId",
                HeaderText = "ClientId",
                Name = "ClientIdColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CurrentMasterId",
                HeaderText = "CurrentMasterId",
                Name = "CurrentMasterIdColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Name = "StatusColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalCost",
                HeaderText = "TotalCost",
                Name = "TotalCostColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsCompleted",
                HeaderText = "IsCompleted",
                Name = "IsCompletedColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDate",
                HeaderText = "CreatedDate",
                Name = "CreatedDateColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CompletedDate",
                HeaderText = "CompletedDate",
                Name = "CompletedDateColumn"
            });
            RequestsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                Name = "DescriptionColumn"
            });
        }
        private void LoadOrderPartsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var orderPartsService = new OrderPartsService(_unitOfWork);

            var query = orderPartsService.GetOrderParts().Include(op => op.Part);

            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns.FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);
                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(Part.PartName.ToLower().Contains(@0) or Status.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var orderPartsList = query.ToList();

            var bindingList = new SortableBindingList<OrderParts>(orderPartsList);
            OrderPartsDataGridView.DataSource = bindingList;

            OrderPartsDataGridView.AutoGenerateColumns = false;
            OrderPartsDataGridView.Columns.Clear();

            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });
            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PartId",
                HeaderText = "PartId",
                Name = "PartIdColumn"
            });
            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Quantity",
                Name = "QuantityColumn"
            });
            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderDate",
                HeaderText = "OrderDate",
                Name = "OrderDateColumn"
            });
            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Name = "StatusColumn"
            });
            OrderPartsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalCost",
                HeaderText = "TotalCost",
                Name = "TotalCostColumn"
            });
        }
        private void LoadPaymentsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var paymentService = new PaymentService(_unitOfWork);

            var query = paymentService.GetPayments().Include(p => p.RepairRequest);

            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns.FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);
                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(PaymentMethod.ToLower().Contains(@0) or RepairRequest.Description.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var paymentsList = query.ToList();

            var bindingList = new SortableBindingList<Payments>(paymentsList);
            PaymentsDataGridView.DataSource = bindingList;

            PaymentsDataGridView.AutoGenerateColumns = false;
            PaymentsDataGridView.Columns.Clear();

            PaymentsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });
            PaymentsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RequestId",
                HeaderText = "RequestId",
                Name = "RequestIdColumn"
            });
            PaymentsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Name = "AmountColumn"
            });
            PaymentsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "PaymentDate",
                Name = "PaymentDateColumn"
            });
            PaymentsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMethod",
                HeaderText = "PaymentMethod",
                Name = "PaymentMethodColumn"
            });
        }
        private void LoadClientsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var clientService = new ClientService(_unitOfWork);
            var query = clientService.GetClients();

            // Фильтрация
            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns
                    .FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);

                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            // Поиск (например по Name, Email)
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(Name.ToLower().Contains(@0) or Email.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var clientsList = query.ToList();

            // Используем SortableBindingList для сортировки
            var bindingList = new SortableBindingList<Clients>(clientsList);
            ClientsDataGridView.DataSource = bindingList;

            ClientsDataGridView.AutoGenerateColumns = false;
            ClientsDataGridView.Columns.Clear();

            ClientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });

            ClientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Name = "NameColumn"
            });

            ClientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Name = "PhoneColumn"
            });

            ClientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "EmailColumn"
            });

            ClientsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDate",
                HeaderText = "CreatedDate",
                Name = "CreatedDateColumn"
            });
        }
        private void LoadRepairLogsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var repairLogsService = new RepairLogsService(_unitOfWork);

            var query = repairLogsService.GetRepairLogs()
                .Include(rl => rl.RepairRequest)
                .Include(rl => rl.User)
                .Include(rl => rl.Part);

            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns
                    .FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);

                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(WorkDescription.ToLower().Contains(@0) or Part.PartName.ToLower().Contains(@0) or RepairRequest.Description.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var repairLogsList = query.ToList();

            // Используем SortableBindingList для сортировки
            var bindingList = new SortableBindingList<RepairLogs>(repairLogsList);
            RepairsLogsDataGridView.DataSource = bindingList;

            RepairsLogsDataGridView.AutoGenerateColumns = false;
            RepairsLogsDataGridView.Columns.Clear();

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RequestId",
                HeaderText = "RequestId",
                Name = "RequestIdColumn"
            });

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserId",
                HeaderText = "UserId",
                Name = "UserIdColumn"
            });

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WorkDescription",
                HeaderText = "WorkDescription",
                Name = "WorkDescriptionColumn"
            });

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WorkDate",
                HeaderText = "WorkDate",
                Name = "WorkDateColumn"
            });

            RepairsLogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AddedCost",
                HeaderText = "AddedCost",
                Name = "AddedCostColumn"
            });
        }
        private void LoadUsersData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var userService = new UserService(_unitOfWork);
            var query = userService.GetUsers();

            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns
                    .FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);

                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                string searchCondition = "(Username.ToLower().Contains(@0) or Role.ToLower().Contains(@0))";
                query = query.Where(searchCondition, searchText);
            }

            var usersList = query.ToList();

            // Используем SortableBindingList для сортировки
            var bindingList = new SortableBindingList<Users>(usersList);
            UsersDataGridView.DataSource = bindingList;

            UsersDataGridView.AutoGenerateColumns = false;
            UsersDataGridView.Columns.Clear();

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn"
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Username",
                Name = "UsernameColumn"
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Role",
                Name = "RoleColumn"
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "EmailColumn"
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Name = "PhoneColumn"
            });

            UsersDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedDate",
                HeaderText = "CreatedDate",
                Name = "CreatedDateColumn"
            });
        }
        private void LoadLogsData(string filterTypeDisplayName, string filterValue, string searchText)
        {
            var logsService = new LogsService(_unitOfWork);
            var query = logsService.GetLogs();

            // Фильтрация по столбцу
            if (!string.IsNullOrEmpty(filterTypeDisplayName))
            {
                var column = _currentEntityDefinition.Columns
                    .FirstOrDefault(c => c.DisplayName == filterTypeDisplayName);

                if (column != null && !string.IsNullOrEmpty(filterValue))
                {
                    query = query.Where($"{column.PropertyName} == @0", filterValue);
                }
            }

            // Поиск по Action, Description
            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();
                query = query.Where(l => l.Action.ToLower().Contains(searchText) ||
                                         l.Description.ToLower().Contains(searchText));
            }

            var logsList = query.ToList();

            // Используем SortableBindingList
            var bindingList = new SortableBindingList<Logs>(logsList);
            LogsDataGridView.DataSource = bindingList;

            LogsDataGridView.AutoGenerateColumns = false;
            LogsDataGridView.Columns.Clear();

            // Ручное добавление столбцов
            LogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Id",
                Name = "IdColumn",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            LogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserName",
                HeaderText = "UserName",
                Name = "UserNameColumn",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            LogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Action",
                HeaderText = "Action",
                Name = "ActionColumn",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            LogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "Date",
                Name = "DateColumn",
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            LogsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Description",
                HeaderText = "Description",
                Name = "DescriptionColumn",
                Width = 400,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
        }


        private void DeleteSelectedRecord()
        {
            // Проверяем, если это работа с запчастями (через UserControl)
            if (_currentEntityName == "Parts")
            {
                // Обработка удаления для запчастей через UserControl
                if (PartsFlowLayoutPanel.Controls.Count == 0) return;

                var selectedControl = PartsFlowLayoutPanel.Controls
                    .OfType<PartsUserControl>()
                    .FirstOrDefault(ctrl => ctrl.IsSelected);

                if (selectedControl == null)
                {
                    MessageBox.Show("Выберите элемент для удаления.");
                    return;
                }

                // Получаем объект Part из selectedControl
                var part = selectedControl.GetPartData();  // Assuming GetPartData() returns a 'Parts' object

                // Проверяем, что у объекта Part есть свойство Id
                var idProperty = part.GetType().GetProperty("Id");
                if (idProperty == null)
                {
                    MessageBox.Show("У сущности нет свойства Id для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var idValue = idProperty.GetValue(part);

                // Получаем сервис для запчастей
                var service = _serviceFactories[_currentEntityName](_unitOfWork);

                // Удаляем запчасть через сервис
                var deleteMethod = service.GetType().GetMethod("Delete");
                deleteMethod.Invoke(service, new object[] { idValue });

                // Обновляем интерфейс
                PartsFlowLayoutPanel.Controls.Remove(selectedControl); // Удаляем контрол из панели
            }

            else if (CurrentControl is DataGridView grid && grid.CurrentRow != null)
            {


                // Получаем DataSource из DataGridView
                var dataSource = grid.DataSource;
                if (dataSource == null)
                {
                    MessageBox.Show("Нет данных для удаления.");
                    return;
                }

                // Проверяем, что DataSource является BindingList<T>
                var bindingListType = typeof(BindingList<>).MakeGenericType(dataSource.GetType().GetGenericArguments()[0]);
                if (!bindingListType.IsAssignableFrom(dataSource.GetType()))
                {
                    MessageBox.Show("Источник данных не является BindingList сущностей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Приводим к IList и ищем выбранный элемент
                var bindingList = dataSource as System.Collections.IList;
                if (bindingList == null)
                {
                    MessageBox.Show("Ошибка при обработке данных.");
                    return;
                }

                var selectedItem = grid.CurrentRow.DataBoundItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Невозможно получить выбранный элемент.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получаем Id выбранного элемента с помощью рефлексии
                var idProperty = selectedItem.GetType().GetProperty("Id");
                if (idProperty == null)
                {
                    MessageBox.Show("У сущности нет свойства Id для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var idValue = idProperty.GetValue(selectedItem);

                // Получаем сервис для удаления элемента
                var service = _serviceFactories[_currentEntityName](_unitOfWork);
                var deleteMethod = service.GetType().GetMethod("Delete");

                // Вызываем метод Delete для удаления элемента из базы данных, передавая только Id
                deleteMethod.Invoke(service, new object[] { idValue });

                // Удаляем элемент из BindingList
                bindingList.Remove(selectedItem);

                // Обновляем DataGridView
                grid.Refresh();
            }

            // Перезагружаем данные для отображения актуальной информации
            LoadCurrentTabData();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteSelectedRecord();
        }
        private void AddingButton_Click(object sender, EventArgs e)
        {
            // Определяем, какая вкладка активна
            var activeTab = _currentEntityName;

            object newEntity = null; // Новый объект для добавления
            Type entityType = null;

            switch (activeTab)
            {
                case "Requests":
                    newEntity = new Requests(); // Создаем новую заявку
                    entityType = typeof(Requests);
                    break;

                case "Parts":
                    newEntity = new Parts(); // Создаем новую запчасть
                    entityType = typeof(Parts);
                    break;

                case "OrderParts":
                    newEntity = new OrderParts(); // Создаем новую запчасть
                    entityType = typeof(OrderParts);
                    break;

                case "Payments":
                    newEntity = new Payments(); // Создаем новый платеж
                    entityType = typeof(Payments);
                    break;

                case "Clients":
                    newEntity = new Clients(); // Создаем новый платеж
                    entityType = typeof(Clients);
                    break;

                case "RepairLogs":
                    newEntity = new RepairLogs(); // Создаем новый лог ремонта
                    entityType = typeof(RepairLogs);
                    break;

                case "Logs":
                    MessageBox.Show("Добавление записей в таблицу логов запрещено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case "Users":
                    newEntity = new Users(); // Создаем новый лог
                    entityType = typeof(Users);
                    break;

                // Добавьте обработку для других вкладок, если нужно
                default:
                    MessageBox.Show("Добавление для данной вкладки не поддерживается.");
                    return;
            }

            if (newEntity != null && entityType != null)
            {
                // Открываем форму добавления
                var addForm = new EditForm(newEntity, _unitOfWork, true);
                addForm.ShowDialog();
                LoadCurrentTabData(); // Подписываемся на событие обновления
            }
        }
        private void SaveChangesButton_Click(object sender, EventArgs e)
        {
            if (CurrentControl is DataGridView grid)
            {
                var dataSource = grid.DataSource;
                if (dataSource == null)
                {
                    MessageBox.Show("Нет данных для сохранения.");
                    return;
                }

                // Получаем тип сущности
                if (!_entityTypes.TryGetValue(_currentEntityName, out var entityType))
                {
                    MessageBox.Show("Неизвестный тип сущности для сохранения.");
                    return;
                }

                // Приводим к BindingList, чтобы получить доступ к элементам
                // Предполагается, что в Load...Data мы установили DataSource как BindingList<T>.
                var listType = typeof(BindingList<>).MakeGenericType(entityType);
                if (dataSource.GetType() != listType)
                {
                    MessageBox.Show("Источник данных не является BindingList сущностей.");
                    return;
                }

                var service = _serviceFactories[_currentEntityName](_unitOfWork);

                // Перебираем элементы BindingList
                var bindingList = (System.Collections.IList)dataSource;
                foreach (var item in bindingList)
                {
                    // Предполагаем, что сущность имеет свойство int Id
                    var idProperty = item.GetType().GetProperty("Id");
                    if (idProperty == null)
                    {
                        MessageBox.Show("У сущности нет свойства Id для определения, новая она или нет.");
                        return;
                    }

                    int idValue = (int)idProperty.GetValue(item);
                    if (idValue == 0)
                    {
                        // Новая сущность
                        var addMethod = service.GetType().GetMethod("Add");
                        addMethod.Invoke(service, new[] { item });
                    }
                    else
                    {
                        // Существующая сущность
                        var updateMethod = service.GetType().GetMethod("Update");
                        updateMethod.Invoke(service, new[] { item });
                    }
                }

                // Сохраняем изменения в БД
                _unitOfWork.Complete();
                MessageBox.Show("Изменения успешно сохранены.");
                LoadCurrentTabData(); // Перезагрузка данных после сохранения
            }
            else
            {
                MessageBox.Show("Нет данных для сохранения.");
            }
        }


        private void PreviousPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadCurrentTabData();
            }
        }
        private void NextPageButton_Click(object sender, EventArgs e)
        {
            var totalItems = _currentPartsList?.Count ?? 0;
            var totalPages = (int)Math.Ceiling(totalItems / (double)_itemsPerPage);

            if (_currentPage < totalPages)
            {
                _currentPage++;
                LoadCurrentTabData();
            }
        }


        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            IsLoggingOut = true;
            this.Close();
        }
        private void FullAccessButton_Click(object sender, EventArgs e)
        {
            var accessService = new AccessService(_unitOfWork);

            // Открытие формы для пароля
            var accessForm = new AccessForm(accessService, Session.Role);
            if (accessForm.ShowDialog() == DialogResult.OK)
            {
                ConfigureTabsByRole();
                UserroleLabel.Text = Session.Role;
            }
        }
        public bool IsLoggingOut { get; private set; } = false;
        private void UsernameLabel_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр UserService
            var userService = new UserService(_unitOfWork);

            // Получаем текущего пользователя из сессии
            var currentUser = userService.GetById(Session.UserId);

            if (currentUser != null)
            {
                // Открываем форму редактирования
                var editForm = new EditForm(currentUser, _unitOfWork, isNew: false);

                // Подписываемся на событие обновления данных
                editForm.EntityUpdated += entity =>
                {
                    if (entity is Users updatedUser)
                    {
                        // Обновляем информацию на главной форме
                        UsernameLabel.Text = updatedUser.Username;
                        Session.Username = updatedUser.Username; // Обновляем сессию
                    }
                };

                editForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}