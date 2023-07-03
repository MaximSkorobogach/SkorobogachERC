using Skorobogach.Interfaces;
using Skorobogach.Models;
using Skorobogach.Services;

namespace Skorobogach.Logic;
/// <summary>
/// Горячее водоснабжение, наследуемся от общего алгоритма сервиса.
/// В виду добавления новых параметров и чуть другой логики, мы переопределили соответствующие методы и добавили новые свойства
/// </summary>
internal class HotWaterSupply : ServiceAlgorithm
{
    /// <summary>
    /// Показатель теплоэнергии
    /// </summary>
    private double VolumeHeatEnergy { get; set; }
    /// <summary>
    /// Начисление за электроэнергию
    /// </summary>
    private double AccrualHeatEnergy { get; set; }
    /// <summary>
    /// Сервис горячей воды
    /// </summary>
    private new HotWaterService Service { get; set; }
    /// <summary>
    /// Конструктор класса горячего водоснабжения
    /// </summary>
    /// <param name="service">Сервис горячей воды</param>
    /// <param name="apartmentInformation">Квартира</param>
    /// <param name="current">Текущий счетчик</param>
    /// <param name="before">Предыдущий счетчик</param>
    public HotWaterSupply(HotWaterService service, ApartmentInformation apartmentInformation, double current,
        double before = 0) : base(service, apartmentInformation, current, before)
    {
        Service = (HotWaterService)base.Service;
    }
    /// <summary>
    /// Конструктор класса горячего водоснабжения
    /// </summary>
    /// <param name="service">Сервис горячей воды</param>
    /// <param name="apartmentInformation">Квартира</param>
    public HotWaterSupply(HotWaterService service, ApartmentInformation apartmentInformation) : base(service, apartmentInformation)
    {
        Service = (HotWaterService)base.Service;
    }
    /// <summary>
    /// Выполнить начисление (расчет объема используется в базовом классе и не переопределяется)
    /// </summary>
    protected override void Calculate()
    {
        base.Calculate();
        
        VolumeHeatEnergy = Volume * Service.NormEnergy; // объем теплоэнергии зависит от объема воды (которую следует разогреть) умноженной на норму энергии

        AccrualHeatEnergy = VolumeHeatEnergy * Service.TariffEnergy; // полученный объем умножаем на тариф и получаем начисление за потраченную теплоэнергию
    }
    /// <summary>
    /// Получить объем теплоэнергии
    /// </summary>
    /// <returns>Объем теплоэнергии</returns>
    public double GetVolumeHeatEnergy() => VolumeHeatEnergy;
    /// <summary>
    /// Получить начисление за теплоэнергию
    /// </summary>
    /// <returns>Начисление за теплоэнергию</returns>
    public double GetAccrualEnergy() => AccrualHeatEnergy;
    /// <summary>
    /// Получить сервис горячей воды
    /// </summary>
    /// <returns>Сервис горячей воды</returns>
    public override HotWaterService GetService() => Service;
}