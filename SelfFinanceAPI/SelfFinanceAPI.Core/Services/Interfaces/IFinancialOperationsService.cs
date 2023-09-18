using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFinanceAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfFinanceAPI.Core.Services.Interfaces
{
    public interface IFinancialOperationsService
    {
        /// <summary>
        /// Finds all financial operation objects in the database
        /// </summary>
        /// <returns>Collection of the financial operation objects</returns>
        Task<ICollection<FinancialOperation>> GetAll();

        /// <summary>
        /// Finds financial operation object in the databse by id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>Financial operation</returns>
        Task<FinancialOperation?> Get(int id);

        /// <summary>
        /// Finds financial operation objects in the databse by the date of it
        /// </summary>
        /// <param name="dateTime">The date</param>
        /// <returns>Financial operations list</returns>
        Task<ICollection<FinancialOperation>> Get(DateTime dateTime);

        /// <summary>
        /// Finds financial operation objects in the databse by the date of it
        /// </summary>
        /// <param name="startDate">The starting date</param>
        /// <param name="endDate">The ending date</param>
        /// <returns>Financial operations list</returns>
        Task<ICollection<FinancialOperation>> Get(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Checks if does the financial operation object with such id exist in the databse
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>If does the object exist</returns>
        Task<bool> Exists(int id);

        /// <summary>
        /// Creates new financial operation objects in the databse
        /// </summary>
        /// <param name="dto">Dto to fill object with data</param>
        /// <returns>Id of created object if it was added</returns>
        Task<int> Create(FinancialOperationForCreateDto dto);

        /// <summary>
        /// Updates the object data in the database
        /// </summary>
        /// <param name="dto">Dto with new data</param>
        /// <returns>If was the object updated</returns>
        Task<bool> Update(FinancialOperationDto dto);

        /// <summary>
        /// Deletes the object data from the database
        /// </summary>
        /// <param name="id">Id of the object to be deleted</param>
        /// <returns>If was the object deleted</returns>
        Task<bool> Delete(int id);

        /// <summary>
        /// Saves changes to the databse
        /// </summary>
        /// <returns>If were the changes saved</returns>
        Task<bool> Save();
    }
}
