﻿namespace Final;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
//Финальное задание
// Проект: Учет финансов пользователей (Personal Finance Management Console Application)
// Цель этого проекта - создать консольное приложение для учета финансов пользователей. Приложение позволит пользователям отслеживать свои доходы, расходы и состояние финансов в целом.ersonal Finance Management Console Application).
// Функциональные требования:
// 1. Добавление транзакций: Пользователь может добавлять новые транзакции, указывая тип (доход или расход), сумму и описание.
// 2. Просмотр списка транзакций: Пользователь может просматривать список всех транзакций с указанием их типа, суммы и даты.
// 3. Расчет общего дохода и расхода: Пользователь может просматривать общую сумму доходов и расходов за определенный период времени.
// 4. Фильтрация транзакций: Пользователь может фильтровать транзакции по типу (доход или расход) и периоду времени.
// 5. Отчет о состоянии финансов: Пользователь может получать отчет о текущем состоянии своих финансов, включая общий доход, расход, баланс и статистику по категориям транзакций.
// 6. Хранение данных в базе данных: Вся информация о транзакциях должна храниться в базе данных с использованием Entity Framework Core.
// Технические детали:
// 1. Определите модель данных для пользователя и транзакций, включая классы для представления типа транзакции, суммы, описания и даты. Ваша база данных должна разделять финансовые данные между пользователями.
// 2. В ходе разработки проекта, обязательно используйте:
// 1) Миграции и файл appsettings.json (для хранения строки подключения к базе данных).
// 2) Запросы LINQ to Entities.
// 3) Набор методов Fluent API.
// 4) Отношения между сущностями:
// 	a) Пользователь и Настройки (один к одному). 
// 	b) Пользователь и Транзакции (один ко многим).
// 	c) Транзакция и Категория (один ко многим).
// 5) Прямое использование команд Sql и обращение к Хранимым процедурам.