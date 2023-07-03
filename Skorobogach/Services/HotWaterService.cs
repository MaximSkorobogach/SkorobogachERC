using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.Interfaces;

namespace Skorobogach.Services
{
    /// <summary>
    /// Класс описывающий сервис горячей воды, наследуется от IService
    /// </summary>
    public class HotWaterService : IService
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Name { get; set; } = "Горячее водоснабжение (ГВС)";
        /// <summary>
        /// Тариф за теплоноситель (вода)
        /// </summary>
        public double Tariff { get; set; }
        /// <summary>
        /// Тариф за теплоэнергию
        /// </summary>
        public double TariffEnergy { get; set; }
        /// <summary>
        /// Норматив теплоносителя (воды)
        /// </summary>
        public double Norm { get; set; }
        /// <summary>
        /// Норматив теплоэнергии
        /// </summary>
        public double NormEnergy { get; set; }
        /// <summary>
        /// Название единицы теплоносителя
        /// </summary>
        public string Unit { get; set; } = "м3";
    }
}
