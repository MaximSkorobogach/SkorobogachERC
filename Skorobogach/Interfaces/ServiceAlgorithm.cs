using Skorobogach.Logic;
using Skorobogach.Models;

namespace Skorobogach.Interfaces;
/// <summary>
/// Описывает общую логику снабжения сервисом
/// Абстрактный класс описывающий свойства и принцип работы базового алгоритма начисления. Если сервис требует другую логику, то мы переопределяем методы
/// </summary>
public abstract class ServiceAlgorithm
{
    /// <summary>
    /// Сервис с которым работаем
    /// </summary>
    protected IService Service { get; set; }
    /// <summary>
    /// Информация о квартире
    /// </summary>
    protected ApartmentInformation ApartmentInformation { get; set; }
    /// <summary>
    /// Есть ли счетчик
    /// </summary>
    public bool HasCounter { get; } = false;
    /// <summary>
    /// Начисление, формула P = V * T, Начисление = Объем * Тариф
    /// </summary>
    protected double Accrual { get; set; }
    /// <summary>
    /// Объем
    /// </summary>
    protected double Volume { get; set; }
    /// <summary>
    /// Текущие показатели счетчика (если есть)
    /// </summary>
    protected double Current { get; }
    /// <summary>
    /// Предыдущие показатели счетчика (если есть)
    /// </summary>
    protected double Before { get; }

    /// <summary>
    /// Конструктор класса если есть счетчики
    /// </summary>
    /// <param name="service">Сервис подсчета</param>
    /// <param name="apartmentInformation">Информация о квартире</param>
    /// <param name="current">Текущий счетчик</param>
    /// <param name="before">Предыдущий счетчик</param>
    protected ServiceAlgorithm(IService service, ApartmentInformation apartmentInformation, double current, double before = 0)
    {
        Service = service;
        ApartmentInformation = apartmentInformation;
        Current = current;
        Before = before;
        HasCounter = true;
    }
    /// <summary>
    /// Конструктор класса если счетчика нет
    /// </summary>
    /// <param name="service">Сервис подсчета</param>
    /// <param name="apartmentInformation">Информация о квартире</param>
    protected ServiceAlgorithm(IService service, ApartmentInformation apartmentInformation)
    {
        Service = service;
        ApartmentInformation = apartmentInformation;
    }
    /// <summary>
    /// Запустить подсчет
    /// </summary>
    public void StartCalculate()
    {
        CalculateVolume();
        Calculate();
    }
    /// <summary>
    /// Подсчитать объем в зависимости от наличия счетчика
    /// </summary>
    protected virtual void CalculateVolume()
    {
        if (HasCounter)
        {
            Volume = Current - Before;
        }
        else
        {
            Volume = ApartmentInformation.Count * Service.Norm;
        }
    }
    /// <summary>
    /// Подсчет начисления
    /// </summary>
    protected virtual void Calculate() => Accrual = Volume * Service.Tariff;
    /// <summary>
    /// Получить объем
    /// </summary>
    /// <returns>объем</returns>
    public virtual double GetVolume() => Volume;
    /// <summary>
    /// Получить начисление
    /// </summary>
    /// <returns>Начисление</returns>
    public virtual double GetAccrual() => Accrual;
    /// <summary>
    /// Получить используемый сервис
    /// </summary>
    /// <returns>Сервис</returns>
    public virtual IService GetService() => Service;
    /// <summary>
    /// Получить информацию о квартире
    /// </summary>
    /// <returns>Квартира</returns>
    public virtual ApartmentInformation GetApartmentInformation() => ApartmentInformation;
}