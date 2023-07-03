using Skorobogach.Interfaces;
using Skorobogach.Models;
using Skorobogach.Services;

namespace Skorobogach.Logic;
/// <summary>
/// Электроснабжение, наследуемся от общего алгоритма сервиса.
/// В виду добавления новых параметров и чуть другой логики, мы переопределили соответствующие методы и добавили новые свойства
/// </summary>
internal class ElectricSupply : ServiceAlgorithm
{
    /// <summary>
    /// Счетчик электричества за день
    /// </summary>
    protected double ElectricDay { get; }
    /// <summary>
    /// Начисление за день
    /// </summary>
    protected double AccrualDay { get; set; }
    /// <summary>
    /// Счетчик электричества за ночь
    /// </summary>
    protected double ElectricNight { get; }
    /// <summary>
    /// Начисление за ночь
    /// </summary>
    protected double AccrualNight { get; set; }
    /// <summary>
    /// Сервис электроэнергии
    /// </summary>
    private new ElectricService Service { get; }

    /// <summary>
    /// Конструктор класса электроснабжения, из нового - получение счетчиков за день и ночь 
    /// </summary>
    /// <param name="service">Сервис электричества</param>
    /// <param name="apartmentInformation">Квартира</param>
    /// <param name="electricDay">Счетчик за день</param>
    /// <param name="electricNight">Счетчик за ночь</param>
    public ElectricSupply(ElectricService service, ApartmentInformation apartmentInformation, double electricDay = 0,
        double electricNight = 0) : base(service, apartmentInformation, electricDay, electricNight)
    {
        ElectricDay = electricDay;
        ElectricNight = electricNight;

        Service = (ElectricService)base.Service;
    }
    /// <summary>
    /// Конструктор класса электроснабжения.
    /// </summary>
    /// <param name="service">Сервис электричества</param>
    /// <param name="apartmentInformation">Квартира</param>
    public ElectricSupply(ElectricService service, ApartmentInformation apartmentInformation) : base(service, apartmentInformation)
    {
        Service = (ElectricService)base.Service;
    }
    /// <summary>
    /// Подсчет электроэнергии в зависимости от наличия счетчика
    /// </summary>
    protected override void CalculateVolume()
    {
        if (HasCounter)
        {
            AccrualDay = ElectricDay * Service.TariffDay; // начисление за день зависит от счетчика за день * на тариф за день
            AccrualNight = ElectricNight * Service.TariffNight; // начисление за ночь зависит от счетчика за ночь * на тариф за ночь
        }
        else
        {
            Volume = ApartmentInformation.Count * Service.Norm; // если счетчиков нет, то просто по количество людей * на норму
        }
    }
    /// <summary>
    /// Получить счетчик за день
    /// </summary>
    /// <returns>Счетчик за день</returns>
    public double GetElectricDay() => ElectricDay;
    /// <summary>
    /// Получить начисления за день
    /// </summary>
    /// <returns>Начисления за день</returns>
    public double GetAccrualDay() => AccrualDay;
    /// <summary>
    /// Получить счетчик за ночь
    /// </summary>
    /// <returns>Счетчик за ночь</returns>
    public double GetElectricNight() => ElectricNight;
    /// <summary>
    /// Получить начисления за ночь
    /// </summary>
    /// <returns>Начисления за ночь</returns>
    public double GetAccrualNight() => AccrualNight;
    /// <summary>
    /// Получить сервис электроэнергии
    /// </summary>
    /// <returns>Сервис электроэнергии</returns>
    public override ElectricService GetService() => Service;

}