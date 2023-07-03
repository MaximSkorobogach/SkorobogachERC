using Skorobogach.Interfaces;
using Skorobogach.Models;
using Skorobogach.Services;

namespace Skorobogach.Logic;
/// <summary>
/// Холодное водоснабжение, наследуемся от общего алгоритма сервиса. Так как это сервис в котором нет ничего нового, то ничего не переопределяется
/// </summary>
public class ColdWaterSupply : ServiceAlgorithm
{
    /// <summary>
    /// Указываем что наш сервис - это сервис холодной воды, то есть класс сервиса реализующий интерфейс IService
    /// </summary>
    private new ColdWaterService Service { get; set; }
    /// <summary>
    /// Конструктор класса, передается не какой то там IService, а именно ColdWaterService, наследуемый от него
    /// </summary>
    /// <param name="service">Сервис холодной воды</param>
    /// <param name="apartmentInformation">Квартира</param>
    /// <param name="current">Счетчик текущий</param>
    /// <param name="before">Счетчик предыдущий</param>
    public ColdWaterSupply(ColdWaterService service, ApartmentInformation apartmentInformation, double current,
        double before = 0) : base(service, apartmentInformation, current, before)
    {
        Service = (ColdWaterService)base.Service;
    }
    /// <summary>
    /// Конструктор класса, передается не какой то там IService, а именно ColdWaterService, наследуемый от него
    /// </summary>
    /// <param name="service">Сервис</param>
    /// <param name="apartmentInformation">Квартира</param>
    public ColdWaterSupply(IService service, ApartmentInformation apartmentInformation) : base(service, apartmentInformation)
    {
        Service = (ColdWaterService)base.Service;
    }
}