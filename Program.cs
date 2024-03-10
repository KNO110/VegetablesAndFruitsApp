using System;
using System.Data.SqlClient;

namespace VegetablesAndFruitsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ///// Запомни, ни одна овощная или фруктовая база данных не откроется без этой строки
            string connectionString = "Data Source=(local);Initial Catalog=VegetablesAndFruits;Integrated Security=True";

            // Создание объекта для подключения к базе данных. Наш курьер к овощам и фруктам!
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //// Открытие подключения. Дверь открылась, погружаемся в мир овощей и фруктов!
                    connection.Open();
                    Console.WriteLine("Подключение к базе данных успешно! Добро пожаловать в мир вкусных данных!");

                    /// Отображение всей информации из таблицы. Показываем все, что у нас есть!
                    Show_all(connection);

                    // Отображение всех названий овощей и фруктов.
                    all_name(connection);

                    // Отображение всех цветов. Красный, желтый, зеленый... все цвета радуги! Ну или европейского прайда
                    all_color(connection);

                    // Показать максимальную калорийность
                    max_calories(connection);

                    // Показать минимальную калорийность. В поисках легкого перекуса)
                    min_calories(connection);

                    // Показать среднюю калорийность.
                    avg_calories(connection);

                    /// 4 задание


                    ShowVegetablesCount(connection);

                    // Показать кол-во фруктрв
                    ShowFruits_Count(connection);

                    ShowItemsCountByColor_navernoe(connection, "Красный");

                    // Короче мне лень объяснять дальше что функции делают
                    ShowItemsCount_from_EachColor(connection);

                    ShowItemsBelow_Calories(connection, 50);

                    ShowItemsAbove_Calories(connection, 80);

                    ShowItemsInRange_Calories(connection, 40, 70);

                    ShowItemsBy_Colors(connection, "Желтый", "Красный");
                }
                catch (Exception ex)
                {
                    // В случае ошибки, если съели что-то не так... кажется, база данных не так гладко работает
                    Console.WriteLine($"Ошибка: {ex.Message}. Похоже, овощи и фрукты сегодня на свежие!");
                }
            }
        }

        static void Show_all(SqlConnection connection)
        {
            Console.WriteLine("\nВся информация из таблицы с овощами и фруктами:");
            string query = "SELECT * FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
            }

            reader.Close();
        }

        static void all_name(SqlConnection connection)
        {
            Console.WriteLine("\nВсе названия овощей и фруктов:");
            string query = "SELECT Name FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["Name"]);
            }

            reader.Close();
        }

        static void all_color(SqlConnection connection)
        {
            Console.WriteLine("\nВсе цвета:");
            string query = "SELECT DISTINCT Color FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["Color"]);
            }

            reader.Close();
        }

        static void max_calories(SqlConnection connection)
        {
            string query = "SELECT MAX(Calories) FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            int maxCalories = (int)command.ExecuteScalar();
            Console.WriteLine($"\nМаксимальная калорийность: {maxCalories}");
        }

        static void min_calories(SqlConnection connection)
        {
            string query = "SELECT MIN(Calories) FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            int minCalories = (int)command.ExecuteScalar();
            Console.WriteLine($"Минимальная калорийность: {minCalories}");
        }

        static void avg_calories(SqlConnection connection)
        {
            string query = "SELECT AVG(Calories) FROM Items";
            SqlCommand command = new SqlCommand(query, connection);
            double averageCalories = (int)command.ExecuteScalar();
            Console.WriteLine($"Средняя калорийность: {averageCalories}");
        }


            ///4 задание


            static void ShowVegetablesCount(SqlConnection connection)
            {
                string query = "SELECT COUNT(*) FROM Items WHERE Type = 'овощ'";
                SqlCommand command = new SqlCommand(query, connection);
                int count = (int)command.ExecuteScalar();
                Console.WriteLine($"Количество овощей: {count}");
            }

            static void ShowFruits_Count(SqlConnection connection)
            {
                string query = "SELECT COUNT(*) FROM Items WHERE Type = 'фрукт'";
                SqlCommand command = new SqlCommand(query, connection);
                int count = (int)command.ExecuteScalar();
                Console.WriteLine($"Количество фруктов: {count}");
            }

            static void ShowItemsCountByColor_navernoe(SqlConnection connection, string color)
            {
                string query = $"SELECT COUNT(*) FROM Items WHERE Color = '{color}'";
                SqlCommand command = new SqlCommand(query, connection);
                int count = (int)command.ExecuteScalar();
                Console.WriteLine($"Количество овощей и фруктов цвета '{color}': {count}");
            }

            static void ShowItemsCount_from_EachColor(SqlConnection connection)
            {
                string query = "SELECT Color, COUNT(*) AS Count FROM Items GROUP BY Color";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("\nКоличество овощей и фруктов каждого цвета:");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Color"]}: {reader["Count"]}");
                }

                reader.Close();
            }

            static void ShowItemsBelow_Calories(SqlConnection connection, int calories)
            {
                string query = $"SELECT * FROM Items WHERE Calories < {calories}";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"\nОвощи и фрукты с калорийностью ниже {calories}:");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
                }

                reader.Close();
            }

            static void ShowItemsAbove_Calories(SqlConnection connection, int calories)
            {
                string query = $"SELECT * FROM Items WHERE Calories > {calories}";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"\nОвощи и фрукты с калорийностью выше {calories}:");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
                }

                reader.Close();
            }

            static void ShowItemsInRange_Calories(SqlConnection connection, int minCalories, int maxCalories)
            {
                string query = $"SELECT * FROM Items WHERE Calories BETWEEN {minCalories} AND {maxCalories}";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"\nОвощи и фрукты с калорийностью в диапазоне от {minCalories} до {maxCalories}:");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
                }

                reader.Close();
            }

            static void ShowItemsBy_Colors(SqlConnection connection, params string[] colors)
            {
                string colorsCondition = string.Join("', '", colors);
                string query = $"SELECT * FROM Items WHERE Color IN ('{colorsCondition}')";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("\nОвощи и фрукты с цветом 'желтый' или 'красный':");
                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Тип: {reader["Type"]}, Цвет: {reader["Color"]}, Калорийность: {reader["Calories"]}");
                }

                reader.Close();
            }
        }
    }
    
