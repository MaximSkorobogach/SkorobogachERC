using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.Interfaces;

namespace Skorobogach.Services
{
    /// <summary>
    /// Класс описывающий сервис холодной воды, наследуется от IService
    /// </summary>
    public class ColdWaterService : IService
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Name { get; set; } = "Холодное водоснабжение (ХВС)";
        /// <summary>
        /// Тариф
        /// </summary>
        public double Tariff { get; set; }
        /// <summary>
        /// Норматив
        /// </summary>
        public double Norm { get; set; }
        /// <summary>
        /// Названиеи единицы
        /// </summary>
        public string Unit { get; set; } = "м3";
    }
}
