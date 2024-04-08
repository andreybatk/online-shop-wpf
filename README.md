# Online Shop WPF
Практическая работа 16-17. Магазин

## Технологии
- WPF
- ADO.NET
- Entity Framework Core
- MSSQL
- MS ACCESS

## Задания
Задание 1. Подключение двух разных источников данных в проекте:
- Разработайте приложение, в котором будет подключено два разных источника данных: MSSQLLocalDB и MS Access.
- Организуйте подключение, выведите строку подключения и статус подключения к этим источникам данных. 
- Вывод данных выполните в графическом интерфейсе. Также необходимо учесть, что должна быть авторизация по логину и паролю для источника данных.

Задание 2. Создание таблиц с данными:
- Предположим, что в разных источниках данных храниться информация из двух систем, информацию из них необходимо сверять между собой, чтобы находить и выводить недостающую. Создайте и заполните таблицы произвольными данными для решения задачи. 

Задание 3. Разработка программы для онлайн-магазина, создайте запросы SQL:
- Select — для выборки данных о покупках по клиенту.
- Insert — вставка во вторую таблицу по совершенной покупке, а также добавление новых клиентов в первую таблицу.
- Update — обновление данных по клиенту из первой таблицы.
- Delete — очистка таблиц.

После чего, используя запросы SQL и компоненты WPF, разработайте программу для сотрудников магазина.

Задание 4. Доработайте предыдущее задание, добавив в проект Entity Framework. При выполнении задания используйте один из двух подходов:
- Database First.
- Code First.
- Избавьтесь от использования ADO.NET.
