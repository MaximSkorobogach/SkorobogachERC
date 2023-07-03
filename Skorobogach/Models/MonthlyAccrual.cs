using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skorobogach.Models
{
    /// <summary>
    /// Модель описывающая месячное начисление по всем сервисам
    /// </summary>
    public class MonthlyAccrual
    {
        /// <summary>
        /// Уникальный айди
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уникальный айди квартиры, к которому относится данное начисление
        /// </summary>
        public int ApartmentInformationId { get; set; }
        /// <summary>
        /// Дата начисления
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Начисление за воду
        /// </summary>
        public double ColdWaterAccrual { get; set; }
        /// <summary>
        /// Начисление за теплоэнергию
        /// </summary>
        public double HeatEnergyAccrual { get; set; }
        /// <summary>
        /// Начисление за теплоноситель
        /// </summary>
        public double HotWaterAccrual { get; set; }
        /// <summary>
        /// Начисление за электричество (если нет счетчиков)
        /// </summary>
        public double ElectricAccrual { get; set; }
        /// <summary>
        /// Начисление за электроэнергию за день
        /// </summary>
        public double ElectricDayAccrual { get; set; }
        /// <summary>
        /// Начисление за электроэнергию за ночь
        /// </summary>
        public double ElectricNightAccrual { get; set; }
    }
}
