using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skorobogach.Interfaces;

namespace Skorobogach.Models
{
    /// <summary>
    /// Модель описывающая сервис (нужен только для базы данных)
    /// </summary>
    public class Service : IService
    {
        /// <summary>
        /// Уникальный айди сервиса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тариф сервиса
        /// </summary>
        public double Tariff { get; set; }
        /// <summary>
        /// Норма сервиса
        /// </summary>
        public double Norm { get; set; }
        /// <summary>
        /// Названия единицы сервиса
        /// </summary>
        public string Unit { get; set; }
    }
}
