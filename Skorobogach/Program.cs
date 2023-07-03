using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Skorobogach.DB;
using Skorobogach.Interfaces;
using Skorobogach.Logic;
using Skorobogach.Models;
using Skorobogach.Module;
using Skorobogach.Modules;
using Skorobogach.Services;

namespace Skorobogach
{
    /// <summary>
    /// Здесь запускаем всю систему, спрашиваем нужную информацию
    /// </summary>
    internal class Program
    {
        public static DataBase DB { get; set; } = new DataBase();
        public static List<IService> Services { get; set; }

        static void Main(string[] args)
        {
            // получение информации о сервисах
            Services = DB.GetActualInfromation();

            // чистка базы данных если требуется

            Console.WriteLine("Очистить базу данных полностью? [y]es/[n]o Если требуется очистить только историю начислений [a]ccrual");
            var clr = Console.ReadLine();
            if (clr == "y") DB.ClearAboutTenants();
            if (clr == "a") DB.ClearAboutAccrual();

            DateTime date = DateTime.Now;

            // выбор и реализация способа заполнения данных о квартирах

            Console.WriteLine("Выберите способ заполнения данных об квартирах \r\n(0 - автоматически заполнить таблицу случайными данными, 1 - вручную) ");
            var ans = Console.ReadLine();

            switch (ans)
            {
                // автоматическая
                case "0":
                    {
                        Utility.AutomaticGeneration(DB); // запуск автоматической генерации квартир

                        //цикл, в котором по кол-ву месяцев запускаем расчет

                        Console.WriteLine("Сколько месяцев симулировать начисление?");
                        var d = Console.ReadLine();
                        for (int i = 0; i < Convert.ToInt32(d); i++)
                        {
                            CalculateModule.AutomaticCalculateAccrual(date, DB.GetApartmentInfo(), DB, Services, true);
                            date = date.AddMonths(1);
                        }

                        DB.SaveChanges();
                        break;
                    }
                // вручную
                case "1":
                    {
                        // спрашиваем кол во квартир
                        Console.WriteLine("Сколько квартир создаете? (если требуется только провести зачисления к уже имеющимся квартирам то вводите 0)");
                        var n = Console.ReadLine();

                        // создаем лист с таким кол-вом
                        List<ApartmentInformation> newApartmentInformations = new List<ApartmentInformation>(Convert.ToInt32(n));

                        // добавляем в этот лист квартиру, с указанием сколько там проживают
                        for (int i = 0; i < Convert.ToInt32(n); i++)
                        {

                            Console.WriteLine("Количество проживающих?");
                            var count = Console.ReadLine();

                            ApartmentInformation apartmentInformation = new ApartmentInformation()
                            {
                                Count = Convert.ToInt32(count),
                            };
                            apartmentInformation.CountColdWater = ConsoleModule.GetInfo("Счетчик холодного водоснабжения");
                            apartmentInformation.CountHotWater = ConsoleModule.GetInfo("Счетчик горячего водоснабжения");
                            apartmentInformation.CountElectric = ConsoleModule.GetInfo("Счетчик электроснабжения");

                            newApartmentInformations.Add(apartmentInformation);
                        }

                        // записываем в бд
                        
                        DB.ApartmentInformations.AddRange(newApartmentInformations);
                        DB.SaveChanges();

                        // проводит по каждому месяцу зачисление

                        Console.WriteLine("Сколько месяцев симулировать начисление?");
                        var d = Console.ReadLine();
                        for (int i = 0; i < Convert.ToInt32(d); i++)
                        {
                            CalculateModule.AutomaticCalculateAccrual(date, DB.GetApartmentInfo(), DB, Services);

                            date = date.AddMonths(1);
                        }

                        DB.SaveChanges();
                        break;
                    }
            }

            foreach (var apartmentInformation in DB.GetApartmentInfo())
            {
                ConsoleModule.Print(apartmentInformation);
            }
        }
    }
}