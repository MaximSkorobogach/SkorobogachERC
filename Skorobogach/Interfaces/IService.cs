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
        public string Name { get; set; }
        public double Tariff { get; set; }
        public double Norm { get; set; }
        public string Unit { get; set; }
    }
}
