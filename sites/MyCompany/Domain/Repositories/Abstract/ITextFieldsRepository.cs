using System;
using System.Linq;
using MyCompany.Domain.Entities;
/// <summary>
/// интерфейс для доменного объекта текст который реализует нужное управление для нужного повидения
/// через панель администратора
/// </summary>
namespace MyCompany.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id);
    }
}
