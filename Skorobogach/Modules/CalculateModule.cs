using Skorobogach.Interfaces;
using Skorobogach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.DB;
using Skorobogach.Module;
using Skorobogach.Logic;
using Skorobogach.Services;

namespace Skorobogach.Modules
{
    /// <summary>
    /// Модуль обработки счета, в нем описаны методы подсчета и запуска подсчета начислений
    /// </summary>
    internal class CalculateModule
    {
        /// <summary>
        /// Автоматический подсчет начислений за конкретный месяц, запускается подсчет для всех указанных квартир
        /// </summary>
        /// <param name="date">Дата подсчета</param>
        /// <param name="apartmentInformation">Информация о квартире</param>
        /// <param name="DB">База данных</param>
        /// <param name="services">Сервисы</param>
        /// <param name="randomizeCounter">Нужно ли сделать случайные счетчики (чтобы самому не заполнять в рамках этой программы)</param>
        public static void AutomaticCalculateAccrual(DateTime date, List<ApartmentInformation> apartmentInformation, DataBase DB, List<IService> services, bool randomizeCounter = false)
        {
            foreach (ApartmentInformation apartment in apartmentInformation) // запускаем цикл по всем квартирам
            {
                ServiceCounter counter = new ServiceCounter(); // создаем счетчик
                counter.ApartmentInformationId = apartment.Id; // прикрепляем счетчик к квартире

                if (randomizeCounter)
                {
                    Utility.RandomizeCounter(apartment, counter); // если требуется рандомизировать показали 
                }
                else CreateCounterInfo(date, apartment, counter); // вручную вводим показатели

                CalculateApartment(date, apartment, counter, services); // запускаем процесс подсчета
            }
        }
        /// <summary>
        /// Создание счетчика вручную
        /// </summary>
        /// <param name="date">Дата передачи счетчика</param>
        /// <param name="apartmentInformation">Квартира</param>
        private static void CreateCounterInfo(DateTime date, ApartmentInformation apartmentInformation, ServiceCounter counter)
        {
            Console.WriteLine($"Квартира номер {apartmentInformation.Id}"); // показываем айди квартиры

            counter.date = date; //прикрепляем к счетчику дату
            counter.ApartmentInformationId = apartmentInformation.Id; // прикрепляем к счетчику айди квартиры

            // Теперь считываем показатели с консоли и записываем в счетчик

            if (apartmentInformation.CountColdWater)
            {
                double coldWaterBefore = 0, coldWaterCurrent = 0;
                ConsoleModule.GetWaterValues(ref coldWaterBefore, ref coldWaterCurrent);
                counter.ColdWaterCounterBefore = coldWaterBefore;
                counter.ColdWaterCounterCurrent = coldWaterCurrent;
            }
            if (apartmentInformation.CountHotWater)
            {
                double hotWaterBefore = 0, hotWaterCurrent = 0;
                ConsoleModule.GetWaterValues(ref hotWaterBefore, ref hotWaterCurrent);
                counter.HotWaterCounterBefore = hotWaterBefore;
                counter.HotWaterCounterCurrent = hotWaterCurrent;
            }
            if (apartmentInformation.CountElectric)
            {
                double day = 0, night = 0;
                ConsoleModule.GetElectricValues(ref day, ref night);
                counter.ElectricDayCounter = day;
                counter.ElectricNightCounter = night;
            }
        }

        /// <summary>
        /// Подсчет всех зачислений
        /// </summary>
        /// <param name="date">Дата зачисления</param>
        /// <param name="apartmentInformation">Квартира</param>
        /// <param name="counter">Счетчик</param>
        /// <param name="services">Сервисы</param>
        private static void CalculateApartment(DateTime date, ApartmentInformation apartmentInformation, ServiceCounter counter, List<IService> services)
        {
            if (apartmentInformation.MonthlyAccruals != null && 
                apartmentInformation.MonthlyAccruals.Any(m => m.date.Year == date.Year & m.date.Month == date.Month))
            {
                return; // если есть зачисления на этот год и месяц, то пропускаем
            }

            MonthlyAccrual monthlyAccrual = new MonthlyAccrual(); // создаем начисление

            // записываем сервисы, создаем снабжение и передаем в классы, либо с счетчиками, либо без

            ColdWaterService coldWaterService = (ColdWaterService)services.First(service => service is ColdWaterService);
            HotWaterService hotWaterService = (HotWaterService)services.First(service => service is HotWaterService);
            ElectricService electricService = (ElectricService)services.First(service => service is ElectricService);

            ColdWaterSupply coldWaterSupply;
            HotWaterSupply hotWaterSupply;
            ElectricSupply electricSupply;

            coldWaterSupply = apartmentInformation.CountColdWater
                ? new ColdWaterSupply(coldWaterService, apartmentInformation, counter.ColdWaterCounterCurrent, counter.ColdWaterCounterBefore)
                : new ColdWaterSupply(coldWaterService, apartmentInformation);
            hotWaterSupply = apartmentInformation.CountHotWater
                ? new HotWaterSupply(hotWaterService, apartmentInformation, counter.HotWaterCounterCurrent, counter.HotWaterCounterBefore)
                : new HotWaterSupply(hotWaterService, apartmentInformation);
            electricSupply = apartmentInformation.CountElectric
                ? new ElectricSupply(electricService, apartmentInformation, counter.ElectricDayCounter, counter.ElectricNightCounter)
                : new ElectricSupply(electricService, apartmentInformation);

            // определяем одинаковые снабжения в один массив
            ServiceAlgorithm[] algorithm = new ServiceAlgorithm[3]
            {
                coldWaterSupply,
                hotWaterSupply,
                electricSupply
            };

            // запускаем счет снабжения
            foreach (var service in algorithm)
            {
                service.StartCalculate();
            }

            // заполняем информацию по полученным данным

            monthlyAccrual.ApartmentInformationId = apartmentInformation.Id;
            monthlyAccrual.date = date;
            counter.date = date;

            monthlyAccrual.ColdWaterAccrual = Math.Round(coldWaterSupply.GetAccrual(), 2);

            monthlyAccrual.HotWaterAccrual = Math.Round(hotWaterSupply.GetAccrual(), 2);
            monthlyAccrual.HeatEnergyAccrual = Math.Round(hotWaterSupply.GetAccrualEnergy(), 2);

            monthlyAccrual.ElectricAccrual = Math.Round(electricSupply.GetAccrual(), 2);
            monthlyAccrual.ElectricDayAccrual = Math.Round(electricSupply.GetAccrualDay(), 2);
            monthlyAccrual.ElectricNightAccrual = Math.Round(electricSupply.GetAccrualNight(), 2);
            
            // чтобы избежать ошибок с базой данный смотрим и избегаем повторений, либо проверяем на null

            if (apartmentInformation.ServiceCounter == null)
            {
                apartmentInformation.ServiceCounter = new List<ServiceCounter>() { counter };
            }
            else
            {
                apartmentInformation.ServiceCounter.Add(counter);
            }

            if (apartmentInformation.MonthlyAccruals == null)
            {
                apartmentInformation.MonthlyAccruals = new List<MonthlyAccrual>() { monthlyAccrual };
            }
            else
            {
                apartmentInformation.MonthlyAccruals.Add(monthlyAccrual);
            }
        }
    }
}
