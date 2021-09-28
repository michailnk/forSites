using MyCompany.Domain.Repositories.Abstract;
/// <summary>
/// класс помошник - "точка входа" для контектса бд.
/// централизованое управление репозиторием. При передачи контроллеру через свойства DataManager
/// будем через реализованые интерфесы управлять соответсвующими сущностями
/// </summary>
namespace MyCompany.Domain.Repositories.EntityFrameworke {
    public class DataManager {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager(ITextFieldsRepository textFields, IServiceItemsRepository serviceItems) {
            TextFields = textFields;
            ServiceItems = serviceItems;
            }
        }
    }
