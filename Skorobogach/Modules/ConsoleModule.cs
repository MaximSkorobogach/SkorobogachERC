using Skorobogach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skorobogach.Modules
{
    /// <summary>
    /// Модуль описывающий взаимодействие с консолью (отображение информации)
    /// </summary>
    internal class ConsoleModule
    {
        /// <summary>
        /// Получить информацию
        /// </summary>
        /// <param name="message">Вопрос</param>
        /// <returns>Ответ, true - счетчик есть, false - счетчика нет</returns>
        public static bool GetInfo(string message)
        {
            Console.WriteLine(message + " | y (есть) / n (нет)");
            return Console.ReadLine() == "y";
        }
        /// <summary>
        /// У холодного и горячего водоснабжения есть явные счетчики за текущий и предыдущий период, спрашиваем и получаем их
        /// </summary>
        /// <param name="before">Предыдущий</param>
        /// <param name="current">Текущий</param>
        public static void GetWaterValues(ref double before, ref double current)
        {
            Console.WriteLine("Показание за предыдущий месяц: ");
            before = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Показание за текущий месяц: ");
            current = Convert.ToDouble(Console.ReadLine());

            while (current < before)
            {
                Console.WriteLine("Показание за текущий месяц не может быть ниже предыдущего");
                Console.WriteLine("Показание за текущий месяц: ");
                current = Convert.ToDouble(Console.ReadLine());
            }
        }
        /// <summary>
        /// Электричество делится на ночь и день
        /// </summary>
        /// <param name="day">Дневной счетчик</param>
        /// <param name="night">Ночной счетчик</param>
        public static void GetElectricValues(ref double day, ref double night)
        {
            Console.WriteLine("Показание за день: ");
            day = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Показание за ночь: ");
            night = Convert.ToDouble(Console.ReadLine());
        }
        /// <summary>
        /// Отчет по квартире и ее счетам
        /// </summary>
        /// <param name="apartmentInformation"> Квартира</param>
        public static void Print(ApartmentInformation apartmentInformation)
        {
            Console.WriteLine($"{"Уникальный номер квартиры",-45} {apartmentInformation.Id}");
            Console.WriteLine(new string('-', 90)); // Разделитель

            for (int i = 0; i < apartmentInformation.MonthlyAccruals.Count; i++)
            {
                Console.WriteLine($"{"Дата:",-45} {apartmentInformation.MonthlyAccruals[i].date}");
                Console.WriteLine($"{"Счетчик за Холодное водоснабжение:",-45} {( apartmentInformation.CountColdWater ? "поставлен" : "нет" )}");
                if (apartmentInformation.CountColdWater)
                {
                    Console.WriteLine($"{"По показаниям ХВС -",-45} было:{apartmentInformation.ServiceCounter[i].ColdWaterCounterBefore}; сейчас: {apartmentInformation.ServiceCounter[i].ColdWaterCounterCurrent}");
                    Console.WriteLine($"{"Цена за услугу ХВС составляет:",-45} {apartmentInformation.MonthlyAccruals[i].ColdWaterAccrual}");
                }
                else
                {
                    Console.WriteLine($"{"По нормативу на",-45} {apartmentInformation.Count} человек");
                    Console.WriteLine($"{"Цена за услугу ХВС составляет:",-45} {apartmentInformation.MonthlyAccruals[i].ColdWaterAccrual}");
                }

                Console.WriteLine($"{"Счетчик за Горячее водоснабжение:",-45} {( apartmentInformation.CountHotWater ? "поставлен" : "нет" )}");

                if (apartmentInformation.CountHotWater)
                {
                    Console.WriteLine($"{"По показаниям ГВС -",-45} было: {apartmentInformation.ServiceCounter[i].HotWaterCounterBefore}; сейчас: {apartmentInformation.ServiceCounter[i].HotWaterCounterCurrent}");
                    Console.WriteLine($"{"Цена за услугу ГВС Теплоноситель составляет:",-45} {apartmentInformation.MonthlyAccruals[i].HotWaterAccrual}");
                    Console.WriteLine($"{"Цена за услугу ГВС Тепловая энергия Теплоноситель составляет:",-45} {apartmentInformation.MonthlyAccruals[i].HeatEnergyAccrual}");
                    Console.WriteLine($"{"Итог ГВС:",-45} {apartmentInformation.MonthlyAccruals[i].HeatEnergyAccrual + apartmentInformation.MonthlyAccruals[i].HotWaterAccrual}");
                }
                else
                {
                    Console.WriteLine($"{"По нормативу на",-45} {apartmentInformation.Count} человек");
                    Console.WriteLine($"{"Цена за услугу ГВС Теплоноситель составляет:",-45} {apartmentInformation.MonthlyAccruals[i].HotWaterAccrual}");
                    Console.WriteLine($"{"Цена за услугу ГВС Тепловая энергия Теплоноситель составляет:",-45} {apartmentInformation.MonthlyAccruals[i].HeatEnergyAccrual}");
                    Console.WriteLine($"{"Итог ГВС:",-45} {apartmentInformation.MonthlyAccruals[i].HeatEnergyAccrual + apartmentInformation.MonthlyAccruals[i].HotWaterAccrual}");
                }

                Console.WriteLine($"{"Счетчик за Электроснабжение:",-45} {( apartmentInformation.CountElectric ? "поставлен" : "нет" )}");

                if (apartmentInformation.CountElectric)
                {
                    Console.WriteLine($"{"По показаниям ЭЭ -",-45} день: {apartmentInformation.ServiceCounter[i].ElectricDayCounter}; ночь: {apartmentInformation.ServiceCounter[i].ElectricNightCounter}");
                    Console.WriteLine($"{"Цена за услугу ЭЭ день составляет:",-45} {apartmentInformation.MonthlyAccruals[i].ElectricDayAccrual}");
                    Console.WriteLine($"{"Цена за услугу ЭЭ ночь составляет:",-45} {apartmentInformation.MonthlyAccruals[i].ElectricNightAccrual}");
                    Console.WriteLine($"{"Итог ЭЭ:",-45} {apartmentInformation.MonthlyAccruals[i].ElectricNightAccrual + apartmentInformation.MonthlyAccruals[i].ElectricDayAccrual}");
                }
                else
                {
                    Console.WriteLine($"{"По нормативу на",-45} {apartmentInformation.Count} человек");
                    Console.WriteLine($"{"Цена за услугу ЭЭ составляет:",-45} {apartmentInformation.MonthlyAccruals[i].ElectricAccrual}");
                }

                Console.WriteLine(new string('-', 90)); // Разделитель
                Console.WriteLine($@"Общая цена за все услуги в этом месяце: {
                    Math.Round(apartmentInformation.MonthlyAccruals[i].ColdWaterAccrual +
                               apartmentInformation.MonthlyAccruals[i].HotWaterAccrual +
                               apartmentInformation.MonthlyAccruals[i].HeatEnergyAccrual +
                               apartmentInformation.MonthlyAccruals[i].ElectricAccrual +
                               apartmentInformation.MonthlyAccruals[i].ElectricDayAccrual +
                               apartmentInformation.MonthlyAccruals[i].ElectricNightAccrual, 2),-45}");
                Console.WriteLine(new string('-', 90)); // Разделитель
            }
        }
    }
}
