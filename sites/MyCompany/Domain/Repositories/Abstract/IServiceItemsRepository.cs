using System;
using System.Linq;
using MyCompany.Domain.Entities;
/// <summary>
/// интерфейс для доменного объекта УСЛУГ который реализует нужное управление для нужного повидения
/// через панель администратора
/// реализация через интерфейс сделана для изминения orm системы
/// </summary>

namespace MyCompany.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        IQueryable<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemById(Guid id);
        void SaveServiceItem(ServiceItem entity);
        void DeleteServiceItem(Guid id);
    }
}
