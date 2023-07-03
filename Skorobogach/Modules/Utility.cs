using Skorobogach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.DB;
using Skorobogach.Interfaces;
using Skorobogach.Logic;
using Skorobogach.Modules;
using Skorobogach.Services;

namespace Skorobogach.Module
{
    /// <summary>
    /// Полезные методы которые непонятно куда определить
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Автоматическая генерация квартир и наличие их счетчиков и добавление в бд
        /// </summary>
        /// <param name="DB">База данных</param>
        public static void AutomaticGeneration(DataBase DB)
        {
            Random rand = new Random();

            Console.WriteLine("Выберите кол-во квартир для генерации");
            var ans = Console.ReadLine();

            List<ApartmentInformation> tenants = new List<ApartmentInformation>();

            for (int i = 0; i < Convert.ToInt32(ans); i++)
            {
                int j = rand.Next(2), k = rand.Next(2), l = rand.Next(2);

                ApartmentInformation apartmentInformation = new ApartmentInformation()
                {
                    Count = rand.Next(1, 6),
                    CountColdWater = j == 1,
                    CountHotWater = k == 1,
                    CountElectric = l == 1
                };

                tenants.Add(apartmentInformation);
            }

            DB.ApartmentInformations.AddRange(tenants);
            DB.SaveChanges();
        }

        /// <summary>
        /// Случайные числа для счетчиков
        /// </summary>
        /// <param name="apartment">Квартира</param>
        /// <param name="counter">Счетчик</param>
        public static void RandomizeCounter(ApartmentInformation apartment, ServiceCounter counter)
        {
            Random rand = new Random();
            counter.ColdWaterCounterBefore = apartment.CountColdWater ? (double)rand.Next(1000) / 10 : 0;
            counter.ColdWaterCounterCurrent = apartment.CountColdWater ? GetRandomNumber(rand, counter.ColdWaterCounterBefore) : 0;

            counter.HotWaterCounterBefore = apartment.CountHotWater ? (double)rand.Next(1000) / 10 : 0;
            counter.HotWaterCounterCurrent = apartment.CountHotWater ? GetRandomNumber(rand, counter.HotWaterCounterBefore) : 0;

            counter.ElectricDayCounter = apartment.CountElectric ? (double)rand.Next(1000) / 10 : 0;
            counter.ElectricNightCounter = apartment.CountElectric ? (double)rand.Next(1000) / 10 : 0;
        }
        /// <summary>
        /// Получить случайное число такое, которое больше чем переданное
        /// </summary>
        /// <param name="rand">Класс рандома</param>
        /// <param name="before">Предыдущее число</param>
        /// <returns>Текущее число больше чем предыдущее</returns>
        private static double GetRandomNumber(Random rand, double before)
        {
            double current = 0;
            while (current < before)
            {
                current = (double)rand.Next(1000) / 10;
            }

            return current;
        }

        

    }
}

