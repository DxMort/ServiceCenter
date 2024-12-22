using ServiceCentre.DataAccess;
using ServiceCentre.Models;
using ServiceCentre.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace SetriceCenter.Metadata
{
    public static class MetadataProvider
    {
        private static Dictionary<string, EntityDefinition> _definitions;
        private static readonly Dictionary<string, Type> EntityTypes = new Dictionary<string, Type>
        {
            { "Parts", typeof(Parts) },
            { "Requests", typeof(Requests) },
            { "OrderParts", typeof(OrderParts) },
            { "Payments", typeof(Payments) },
            { "Clients", typeof(Clients) },
            { "RepairLogs", typeof(RepairLogs) },
            { "Users", typeof(Users) },
            { "Logs", typeof(Logs) }
        };

        private static readonly Dictionary<string, Func<UnitOfWork, object>> ServiceFactories = new Dictionary<string, Func<UnitOfWork, object>>
        {
            { "Parts", uow => new PartService(uow) },
            { "Requests", uow => new RequestService(uow) },
            { "OrderParts", uow => new OrderPartsService(uow) },
            { "Payments", uow => new PaymentService(uow) },
            { "Clients", uow => new ClientService(uow) },
            { "RepairLogs", uow => new RepairLogsService(uow) },
            { "Users", uow => new UserService(uow) },
            { "Logs", uow => new LogsService(uow) }
        };

        static MetadataProvider()
        {
            _definitions = new Dictionary<string, EntityDefinition>();

            // Requests
            _definitions["Requests"] = new EntityDefinition
            {
                EntityName = "Requests",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsFilterable = true, IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Клиент", PropertyName = "ClientId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Мастер", PropertyName = "CurrentMasterId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Статус", PropertyName = "Status", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Общая стоимость", PropertyName = "TotalCost", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Выполнено", PropertyName = "IsCompleted", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата создания", PropertyName = "CreatedDate", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата завершения", PropertyName = "CompletedDate", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Описание", PropertyName = "Description", IsFilterable = false, IsEditable = true }
                }
            };

            // Parts
            _definitions["Parts"] = new EntityDefinition
            {
                EntityName = "Parts",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsFilterable = true, IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Название", PropertyName = "PartName", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Количество", PropertyName = "Quantity", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Цена", PropertyName = "Price", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата последнего обновления", PropertyName = "LastUpdateDate", IsFilterable = false, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Описание", PropertyName = "Description", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Производитель", PropertyName = "Manufacturer", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Путь к изображению", PropertyName = "ImagePath", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Поддерживаемые модели", PropertyName = "SupportedModels", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Номер ячейки", PropertyName = "CellNumber", IsFilterable = true, IsEditable = true }
                }
            };

            // OrderParts
            _definitions["OrderParts"] = new EntityDefinition
            {
                EntityName = "OrderParts",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsFilterable = true, IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "ID запчасти", PropertyName = "PartId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Количество", PropertyName = "Quantity", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Статус", PropertyName = "Status", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата заказа", PropertyName = "OrderDate", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Сумма", PropertyName = "TotalCost", IsFilterable = false, IsEditable = true }
                }
            };

            // Payments
            _definitions["Payments"] = new EntityDefinition
            {
                EntityName = "Payments",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsFilterable = true, IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Метод оплаты", PropertyName = "PaymentMethod", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "ID заявки", PropertyName = "RequestId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Сумма", PropertyName = "Amount", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата оплаты", PropertyName = "PaymentDate", IsFilterable = false, IsEditable = true },
                }
            };

            // Clients
            _definitions["Clients"] = new EntityDefinition
            {
                EntityName = "Clients",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsFilterable = true, IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Имя клиента", PropertyName = "Name", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Email", PropertyName = "Email", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Телефон", PropertyName = "Phone", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата создания", PropertyName = "CreatedDate", IsFilterable = false, IsEditable = false },
                }
            };

            // RepairLogs
            _definitions["RepairLogs"] = new EntityDefinition
            {
                EntityName = "RepairLogs",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "ID заявки", PropertyName = "RequestId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Описание работ", PropertyName = "WorkDescription", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата работ", PropertyName = "WorkDate", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "ID запчасти", PropertyName = "PartId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "ID мастера", PropertyName = "UserId", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Доп. стоимость", PropertyName = "AddedCost", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Описание заявки", PropertyName = "Request.Description", IsFilterable = false, IsEditable = false }
                }
            };

            // Users
            _definitions["Users"] = new EntityDefinition
            {
                EntityName = "Users",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Имя пользователя", PropertyName = "Username", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Роль", PropertyName = "Role", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Пароль", PropertyName = "PasswordHash", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Email", PropertyName = "Email", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Телефон", PropertyName = "Phone", IsFilterable = false, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата создания", PropertyName = "CreatedDate", IsFilterable = false, IsEditable = true }
                }
            };

            // Logs
            _definitions["Logs"] = new EntityDefinition
            {
                EntityName = "Logs",
                Columns = new List<ColumnDefinition>
                {
                    new ColumnDefinition { DisplayName = "ID", PropertyName = "Id", IsPrimaryKey = true, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Действие", PropertyName = "Action", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Дата", PropertyName = "Date", IsFilterable = false, IsEditable = false },
                    new ColumnDefinition { DisplayName = "Описание", PropertyName = "Description", IsFilterable = true, IsEditable = true },
                    new ColumnDefinition { DisplayName = "Пользователь", PropertyName = "UserName", IsFilterable = true, IsEditable = true }
                }
            };
        }

        public static EntityDefinition GetDefinition(string entityName)
        {
            return _definitions.ContainsKey(entityName) ? _definitions[entityName] : null;
        }
        public static Type GetEntityType(string entityName)
        {
            return EntityTypes.TryGetValue(entityName, out var type) ? type : null;
        }
        public static object GetService(UnitOfWork unitOfWork, string entityName)
        {
            return ServiceFactories.TryGetValue(entityName, out var factory) ? factory(unitOfWork) : null;
        }
    }
}