namespace Common.Enums
{
    public enum RolePermissions
    {
        // Reserved value
        None = 0,

        /// <summary>
        /// Просмотр заказов
        /// </summary>
        OrdersView = 1,

        /// <summary>
        /// Создание заказов
        /// </summary>
        OrdersCreate = 2,

        /// <summary>
        /// Просмотр и прикрепление документов к заказу
        /// </summary>
        OrdersViewAndAttachDocument = 4,

        /// <summary>
        /// Редактирование и удаление документов из заказа
        /// </summary>
        OrdersEditAndDeleteDocument = 5,

        /// <summary>
        /// Просмотр истории заказов
        /// </summary>
        OrdersViewHistory = 6,

        /// <summary>
        /// Просмотр перевозок
        /// </summary>
        ShippingsView = 7,
        
        /// <summary>
        /// Создание перевозок
        /// </summary>
        ShippingsCreate = 8,
        
       /// <summary>
        /// Просмотр и прикрепление документов к перевозке
        /// </summary>
       ShippingsViewAndAttachDocument = 10,

        /// <summary>
        /// Редактирование и удаление документов из перевозки
        /// </summary>
        ShippingsEditAndDeleteDocument = 11,

        /// <summary>
        /// Просмотр истории перевозок
        /// </summary>
        ShippingsViewHistory = 12,

        /// <summary>
        /// Просмотр тарифов
        /// </summary>
        //[OrderNumber(9)]
        TariffsView = 13,

        /// <summary>
        /// Редактирование тарифов
        /// </summary>
        //[OrderNumber(10)]
        TariffsEdit = 14,

        /// <summary>
        /// Редактирование складов доставки
        /// </summary>
        WarehousesEdit = 15,

        /// <summary>
        /// Редактирование артикулов
        /// </summary>
        //[OrderNumber(13)]
        //ArticlesEdit = 16,

        /// <summary>
        /// Редактирование типов комплектаций
        /// </summary>
        PickingTypesEdit = 17,

        /// <summary>
        /// Редактирование транспортных компаний
        /// </summary>
        TransportCompaniesEdit = 18,

        /// <summary>
        /// Редактирование типов ТС
        /// </summary>
        VehicleTypesEdit = 19,

        /// <summary>
        /// Редактирование типов документов
        /// </summary>
        DocumentTypesEdit = 20,

        /// <summary>
        /// Редактирование ролей
        /// </summary>
        RolesEdit = 21,

        /// <summary>
        /// Редактирование пользователей
        /// </summary>
        UsersEdit = 22,

        /// <summary>
        /// Настройка полей
        /// </summary>
        FieldsSettings = 23,

        /// <summary>
        /// Редактирование складов отгрузки
        /// </summary>
        ShippingWarehousesEdit = 24,

        /// <summary>
        /// Редактирование клиентов
        /// </summary>
        ClientsEdit = 25,

        /// <summary>
        /// Редактирование поставщиков
        /// </summary>
        ProvidersEdit = 26,

        /// <summary>
        /// Редактирование типов продукта
        /// </summary>
        ProductTypesEdit = 27,

        /// <summary>
        /// Отчёты
        /// </summary>
        Report = 28,
    }
}
