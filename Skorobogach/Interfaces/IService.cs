using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skorobogach.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса, его будем реализовать при создании сервиса (либо поверх дополнять если требуется еще информация)
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тариф сервиса
        /// </summary>
        public double Tariff { get; set; }
        /// <summary>
        /// Норматив сервиса
        /// </summary>
        public double Norm { get; set; }
        /// <summary>
        /// Название единицы сервиса
        /// </summary>
        public string Unit { get; set; }
    }
}
