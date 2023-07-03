using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skorobogach.Models
{
    /// <summary>
    /// Модель квартиры, является фундаментом в базе данных, так как к ней прикрепляется месячное начисление и переданные счетчики
    /// </summary>
    public class ApartmentInformation
    {
        /// <summary>
        /// Уникальный айди
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Количество проживающих
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Есть ли счетчик за холодную воду
        /// </summary>
        public bool CountColdWater { get; set; } = false;
        /// <summary>
        /// Есть ли счетчик за горячую воду
        /// </summary>
        public bool CountHotWater { get; set; } = false;
        /// <summary>
        /// Есть ли счетчик за электроэнергию
        /// </summary>
        public bool CountElectric { get; set; } = false;
        /// <summary>
        /// Все месячные начисления
        /// </summary>
        public List<MonthlyAccrual> MonthlyAccruals { get; set; }
        /// <summary>
        /// Все переданные данные по счетчикам
        /// </summary>
        public List<ServiceCounter> ServiceCounter { get; set; }
    }
}
