using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Skorobogach.Interfaces;
using Skorobogach.Models;
using Skorobogach.Services;

namespace Skorobogach.DB
{
    /// <summary>
    /// База данных и ее контекст, выполняет непосредственную роль связи между Entity Framework и SQLite. Выполняет роль ORM
    /// </summary>
    public class DataBase : DbContext
    {
        /// <summary>
        /// Таблица сервисов
        /// </summary>
        public DbSet<Service> Services => Set<Service>();
        /// <summary>
        /// Таблица об квартирах
        /// </summary>
        public DbSet<ApartmentInformation> ApartmentInformations => Set<ApartmentInformation>();
        /// <summary>
        /// Таблица о месячном начислении
        /// </summary>
        public DbSet<MonthlyAccrual> MonthlyAccruals => Set<MonthlyAccrual>();
        /// <summary>
        /// Таблица о переданных счетчиках
        /// </summary>
        public DbSet<ServiceCounter> ServiceCounters => Set<ServiceCounter>();

        /// <summary>
        /// Если бд нет, то создается начальное состояние о сервисах (можно откуда нибудь подтянуть, но в данном случае статичная информация)
        /// </summary>
        public DataBase()
        {
            if (Database.EnsureCreated())
            {
                Services.AddRange(new List<Service>()
                 {
                     new() { Name = "ХВС", Tariff = 35.78, Norm = 4.85, Unit = "м3" },
                     new() { Name = "ЭЭ", Tariff = 4.28, Norm = 164, Unit = "кВт.ч" },
                     new() { Name = "ЭЭ день", Tariff = 4.9, Norm = 0, Unit = "кВт.ч" },
                     new() { Name = "ЭЭ ночь", Tariff = 2.31, Norm = 0, Unit = "кВт.ч" },
                     new() { Name = "ГВС Теплоноситель", Tariff = 35.78, Norm = 4.01, Unit = "м3" },
                     new() { Name = "ГВС Тепловая энергия", Tariff = 998.69, Norm = 0.05349, Unit = "Гкал" },
                 });
                SaveChanges();
            }
        }
        /// <summary>
        /// Строка подключения
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=../../../DB/DataBase.db");
        }
        /// <summary>
        /// Получить информацию о сервисах и запихать в удобный презентабельный вид. На данном случае поиск простейший по совпадению слов
        /// </summary>
        public List<IService> GetActualInfromation()
        {
            List<IService> services = new List<IService>();

            Service coldWaterService = Services.First(service => service.Name.Contains("ХВС"));

            services.Add(new ColdWaterService()
            {
                Name = coldWaterService.Name,
                Norm = coldWaterService.Norm,
                Tariff = coldWaterService.Tariff,
                Unit = coldWaterService.Unit
            });

            Service heatTransferService = Services.First(service => service.Name.Contains("Теплоноситель"));
            Service heatEnergyService = Services.First(service => service.Name.Contains("Тепловая энергия"));

            services.Add(new HotWaterService()
            {
                Norm = heatTransferService.Norm,
                NormEnergy = heatEnergyService.Norm,
                Tariff = heatTransferService.Tariff,
                TariffEnergy = heatEnergyService.Tariff,
                Unit = heatTransferService.Unit
            });

            Service electricService = Services.First(service => service.Name.Contains("ЭЭ"));
            Service electricDayService = Services.First(service => service.Name.Contains("ЭЭ день"));
            Service electricNightService = Services.First(service => service.Name.Contains("ЭЭ ночь"));

            services.Add(new ElectricService()
            {
                Norm = electricService.Norm,
                Tariff = electricService.Tariff,
                TariffDay = electricDayService.Tariff,
                TariffNight = electricNightService.Tariff,
                Unit = electricService.Unit
            });

            return services;
        }
        /// <summary>
        /// Автоматическая генерация квартир и наличие их счетчиков и добавление в бд
        /// </summary>
        /// <param name="DB">База данных</param>
        public void AutomaticGeneration(int count)
        {
            Random rand = new Random();

            List<ApartmentInformation> tenants = new List<ApartmentInformation>();

            for (int i = 0; i < Convert.ToInt32(count); i++)
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

            ApartmentInformations.AddRange(tenants);
            SaveChanges();
        }

        /// <summary>
        /// Удалить всю информацию со всех таблиц
        /// </summary>
        public void ClearAboutTenants()
        {
            ApartmentInformations.RemoveRange(ApartmentInformations);
            MonthlyAccruals.RemoveRange(MonthlyAccruals);
            ServiceCounters.RemoveRange(ServiceCounters);

            SaveChanges();
        }
        /// <summary>
        /// Удалить информацию о начислениях
        /// </summary>
        public void ClearAboutAccrual()
        {
            MonthlyAccruals.RemoveRange(MonthlyAccruals);
            ServiceCounters.RemoveRange(ServiceCounters);

            SaveChanges();
        }
        /// <summary>
        /// Получить актуальные данные по квартирам
        /// </summary>
        public List<ApartmentInformation> GetApartmentInfo() => ApartmentInformations
                .Include(information => information.MonthlyAccruals)
                .Include(information => information.ServiceCounter)
                .ToList();

    }
}
