using SelfFinanceAPI.Core.Models;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFinanceAPI.Core.Repositories.Interfaces
{
    public interface IExpenseTypesRepository
    {
        /// <summary>
        /// Finds all expense type objects in the database
        /// </summary>
        /// <returns>Collection of the expense type objects</returns>
        Task<ICollection<ExpenseType>> GetAll();

        /// <summary>
        /// Finds expense type object in the databse by id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Expense type</returns>
        Task<ExpenseType?> Get(int id);

        /// <summary>
        /// Finds expense type object in the databse by it`s name
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>Expense type</returns>
        Task<ExpenseType?> Get(string name);

        /// <summary>
        /// Checks if does the expense type object with such id exist in the databse
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>If does the object exist</returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Creates new expense type objects in the databse
        /// </summary>
        /// <param name="dto">Dto to fill object with data</param>
        /// <returns>Id of created object if it was added</returns>
        Task<int> Create(ExpenseTypeForCreateDto dto);

        /// <summary>
        /// Updates the object data in the database
        /// </summary>
        /// <param name="dto">Dto with new data</param>
        /// <returns>If was the object updated</returns>
        Task<bool> Update(ExpenseTypeDto dto);

        /// <summary>
        /// Deletes the object data from the database
        /// </summary>
        /// <param name="Id">Id of the object to be deleted</param>
        /// <returns>If was the object deleted</returns>
        Task<bool> Delete(int Id);

        /// <summary>
        /// Saves changes to the databse
        /// </summary>
        /// <returns>If were the changes saved</returns>
        Task<bool> Save();

    }
}
