using System.ComponentModel;

namespace Domain.Error
{
    public enum ErrorCodes
    {
        /// <summary>
        /// Во время сохранения произошла ошибка. Пожалуйста, обратитесь к администратору.
        /// </summary>
        [Description("Во время сохранения произошла ошибка. Пожалуйста, обратитесь к администратору.")]
        NotPreserved = 0,
        
        /// <summary>
        /// Пользователь с таким логином уже существует
        /// </summary>
        [Description("Пользователь с таким логином уже существует")]
        UserNameAlreadyExists = 100,
        
        /// <summary>
        /// Объекта не существует
        /// </summary>
        [Description("Объекта не существует")]
        NotExist = 101,
        /// <summary>
        /// Файла не существует
        /// </summary>
        [Description("Файла не существует")]
        NotFileExist = 102,
        /// <summary>
        /// Не указан Id объекта
        /// </summary>
        [Description("Не указан Id объекта")]
        IdNotProvided = 103,
        /// <summary>
        /// Элемент справочника не найден
        /// </summary>
        [Description("Элемент справочника не найден")]
        DictionaryValueMissing = 104,
        /// <summary>
        /// Не указан тип справочника
        /// </summary>
        [Description("Не указан тип справочника")]
        TypeNotProvided = 105,
        /// <summary>
        /// Неверный код смс
        /// </summary>
        [Description("Неверный код смс")]
        InvalidCodeSMS = 106,
        /// <summary>
        /// Точка организации с таким наименованием уже существует
        /// </summary>
        [Description("Точка организации уже существует")]
        OrganizationPointAlreadyExists = 107,
        /// <summary>
        /// Пользователь не может быть разблокирован, т.к. точка организации заблокирована
        /// </summary>
        [Description("Пользователь не может быть разблокирован, т.к. точка организации заблокирована")]
        UserCanNotBeUnlocked = 108,
        /// <summary>
        /// Пользователь не может быть разблокирован, т.к. точка организации заблокирована
        /// </summary>
        [Description("Точка организации не может быть заблокирована, т.к. в ней присутствуют не заблокированные пользователи")]
        OrganizationPointCanNotBeBlocked = 109,
        /// <summary>
        /// Руководитель уже существует
        /// </summary>
        [Description("Руководитель уже существует")]
        ManagerAlreadyExists = 110,
        /// <summary>
        /// Пользователь уже залогинен
        /// </summary>
        [Description("Пользователь уже залогинен")]
        AlreadyLoggedIn = 112,
        /// <summary>
        /// Пользователь уже залогинен
        /// </summary>
        [Description("Неверный логин/пароль")]
        InvalidLoginPassword = 114,
        /// <summary>
        /// Редактирование с текущим статусом запрещено
        /// </summary>
        [Description("Настройка кредитного калькулятора с заданной датой активности уже существует")]
        AlreadyExistsCalcSettings = 113,
        
        /// <summary>
        /// Заказ не добавлен
        /// </summary>
        [Description("Заказ не добавлен")]
        OrderCreateError = 114,
        
        /// <summary>
        /// Пользователь с таким email уже существует
        /// </summary>
        [Description("Пользователь с таким email уже существует")]
        UserEmailAlreadyExists = 115,
        
        /// <summary>
        /// Элемент не найден
        /// </summary>
        [Description("Элемент не найден")]
        ElementNotFound = 116,

        /// <summary>
        /// Исключение
        /// </summary>
        [Description("Исключение")]
        ExceptionWasThroun = 117,
    }
}
