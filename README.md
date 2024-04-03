# oblakoteka-test-task
Тестовое задание в Облакотека

1. Напишите SQL-скрипт, который будет выполнять следующие действия:
  1) Создайте БД с именем TestDb
  2) В БД TestDb создайте таблицу для сущностей «Изделия» с именем Product со следующими полями:
    • ID – уникальное обозначение изделия, тип данных – Guid, поле обязательное для заполнения (первичный ключ);
    • Name – наименование изделия, тип данных - строка 255 символов, поле обязательное для заполнения;
    • Description – описание изделия, тип - строка с максимально возможным кол-вом символов, поле НЕ обязательное для заполнения.

Необходимо обеспечить контроль уникальности значения поля Name.

Необходимо создать некластеризованный индекс для поля Name с разрешенной блокировкой страниц и строк.

  3) В БД TestDb создайте таблицу для сущностей «Версия изделия» с именем ProductVersion со следующими полями:
    • ID – уникальное обозначение версии изделия, тип данных – Guid, поле обязательное для заполнения (первичный ключ);
    • ProductID – уникальное обозначение изделия к которому относится версия (ссылка на изделие), тип данных – Guid, поле обязательное для заполнения;
    • Name – наименование версии изделия, тип данных - строка 255 символов, поле обязательное для заполнения;
    • Description – описание версии изделия, тип - строка с максимально возможным кол-вом символов, поле НЕ обязательное для заполнения; 
    • CreatingDate – дата создания версии изделия, тип – дата, поле обязательное для заполнения, умолчаемое значение – текущая дата и время;
    • Width – габаритная ширина изделия в миллиметрах, тип – вещественное положительное число, поле обязательное для заполнения;
    • Height – габаритная высота изделия в миллиметрах, тип – вещественное положительное число, поле обязательное для заполнения;
    • Length – габаритная длина изделия в миллиметрах, тип – вещественное положительное число, поле обязательное для заполнения.

Необходимо создать некластеризованный индекс для полей Name, CreatingDate, Width, Height и Length с разрешенной блокировкой страниц и строк (для каждого поля отдельный индекс).

Таблица ProductVersion связана вторичным ключом с таблицей Product (полями ProductID и ID соответственно). При удалении записи из таблицы Product должны удаляться все связанные с ней записи из таблицы ProductVersion.

  4) В БД TestDb создайте таблицу для сущностей «Журнал событий» с именем EventLog со следующими полями:
    • ID – уникальное обозначение записи журнала событий, тип данных – Guid, поле обязательное для заполнения (первичный ключ);
    • EventDate – дата совершения события зафиксированного текущей записью, тип – дата, поле обязательное для заполнения, умолчаемое значение – текущая дата и время;
    • Description – описание версии изделия, тип - строка с максимально возможным кол-вом символов, поле НЕ обязательное для заполнения.

Необходимо создать некластеризованный индекс для поля EventDate с разрешенной блокировкой страниц и строк.

  5) В таблицах Product и ProductVersion создайте триггера, обеспечивающие фиксирование событий по созданию/редактирования/удалению изделия/версии изделия в журнал событий (таблица EventLog).

  6) В БД TestDb создайте хранимую функцию поиска версий изделия по следующим параметрам:
    • наименование изделия (вхождение указанного значения как подстроки в строку наименования изделия);
    • наименование версии изделия (вхождение указанного значения как подстроки в строку наименования версии изделия);
    • минимальный габаритный объем изделия;
    • максимальный габаритный объем изделия. 

Функция должна возвращать данные в табличном виде со следующими колонками:
    • ID версии изделия;
    • наименование изделия;
    • наименование версии изделия;
    • габаритная ширина изделия;
    • габаритная длинна изделия;
    • габаритная высота изделия.

  7) Заполнить таблицы Product и ProductVersion тестовыми данными

2. Реализуйте REST Web API сервис (ASP.NET Core) для возможности управления данными в БД в таблице Product. В сервисе должны присутствовать методы, реализующие:
        ◦ Получение списка изделий по переданному фильтру (по наименованию изделия). 
        ◦ Добавление нового изделия
        ◦ Редактирование существующего изделия
        ◦ Удаление существующего изделия по указанному идентификатору изделия

Доступ к БД осуществлять посредством использования EntityFramework.

3. Необходимо создать WEB-приложение ASP.NET CORE 7.0 MVC для управления данными об изделиях по средством сервиса, созданного в пункте №2.
Основные требования:
        ◦ Приложение должно получать данные об изделии через сервис (смотри пункт 2). Все "общение" с сервисом ведется только на серверной стороне приложения (политика безопасности).
        ◦ В приложении должны присутствовать страница-перечень изделий.
        ◦ На списковой странице необходимо реализовать фильтрацию по наименованию изделия (не клиентская фильтрация данных).
        ◦ Добавление и редактирование изделий реализовать в виде модальных окон.

## Запуск
### Локальный запуск
Установить переменную среды / параметр в appsettings.json **DB_CONNECTION_STRING** для OblakotekaServer - строка подключения к MSSQL для EF в формате `Server=<SERVER>,1433;Database=TestDb;User Id=<USER>;Password=<PASSWORD>;TrustServerCertificate=true`

Установить переменную среды / параметр в appsettings.json **SERVER_URL** для OblakotekaClient - базовый url к OblakotekaServer

### Запуск через docker-compose
Прописать в корень директории .env с параметрами **DB_CONNECTION_STRING** для OblakotekaServer в виде `Server=db,1433;Database=TestDb;User Id=sa;Password=<PASSWORD>;TrustServerCertificate=true`

Для использования образа MSSQL достаточно задать пароль администратора (**sa**) - `MSSQL_SA_PASSWORD=<PASSWORD>`

```
docker-compose -f docker-compose.yml up -d --build
```


