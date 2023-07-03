using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.Interfaces;

namespace Skorobogach.Services
{
    /// <summary>
    /// Класс описывающий сервис электричества, наследуется от IService
    /// </summary>
    internal class ElectricService : IService
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Name { get; set; } = "Электроэнергия (ЭЭ).";
        /// <summary>
        /// Тариф
        /// </summary>
        public double Tariff { get; set; }
        /// <summary>
        /// Тариф за день
        /// </summary>
        public double TariffDay { get; set; }
        /// <summary>
        /// Тариф за ночь
        /// </summary>
        public double TariffNight { get; set; }
        /// <summary>
        /// Норматив
        /// </summary>
        public double Norm { get; set; }
        /// <summary>
        /// Название единицы
        /// </summary>
        public string Unit { get; set; } = "Квт.ч";

    }
}
