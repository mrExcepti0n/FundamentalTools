# FundamentalTools
Набор вспомогательных инструментов (авторизация, логирование, поставщик служб wcf)

### Блок безопасность
IIdentityProvider абстракция над ClaimsPrincipal, которая позволяет хранить расширенную информацию для пользователей различных организаций.

Авторизация на основе CodeAccessSecurityAttribute, разделение объект/действие. Работает только для .net framework.

### Блок сервисная инфраструктура

Хост службы WCF с глобальным обработчиком ошибок и логированием запрос/ответ.

SAML и JWT поставщик службы WCF для коммуникации между WCF сервисами.

### Блок логирование
Регистрация NLog.ILogger для текущего класса с помощью Autofac.Module.

### Примеры
Для примеров используется тестовое приложение WCF, а также представлен набор тестов. 
