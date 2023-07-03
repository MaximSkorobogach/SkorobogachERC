using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skorobogach.Models
{
    /// <summary>
    /// Модель показателя счетчика
    /// </summary>
    public class ServiceCounter
    {
        /// <summary>
        /// Уникальный айди
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уникальный айди квартиры, к которому относится данный счетчик
        /// </summary>
        public int ApartmentInformationId { get; set; }
        /// <summary>
        /// Дата передачи счетчика
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Счетчик за холодную воду текущий
        /// </summary>
        public double ColdWaterCounterCurrent { get; set; }
        /// <summary>
        /// Счетчик за холодную воду предыдущий
        /// </summary>
        public double ColdWaterCounterBefore { get; set; }
        /// <summary>
        /// Счетчик за горячую воду текущий
        /// </summary>
        public double HotWaterCounterCurrent { get; set; }
        /// <summary>
        /// Счетчик за горячую воду предыдущий
        /// </summary>
        public double HotWaterCounterBefore { get; set; }
        /// <summary>
        /// Счетчик за электроэнергию за день
        /// </summary>
        public double ElectricDayCounter { get; set; }
        /// <summary>
        /// Счетчик за электроэнергию зза ночь
        /// </summary>
        public double ElectricNightCounter { get; set; }
    }
}
