using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{

    public interface IManageCategoryRepository
    {
        Task<bool> AddCategoryAsync(string categoryName);
        Task<bool> EditCategoryAsync(int? categoryId, string newCategoryName);
        Task<bool> DeleteCategoryAsync(int? categoryId);
        Task<bool> CategoryAlreadyExistsAsync(string categoryName);
        Task<List<CategoryModel>> GetAllCategoriesAsync();
    }

    public class ManageCategoryRepository : IManageCategoryRepository
    {
        public async Task<bool> AddCategoryAsync(string categoryName)
        {
            string query = "INSERT INTO ItemCategories (Category) VALUES (@CategoryName)";

            if (string.IsNullOrWhiteSpace(categoryName)) return false;

            bool categoryExists = await CategoryAlreadyExistsAsync(categoryName);

            if (categoryExists) return false;

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> CategoryAlreadyExistsAsync(string categoryName)
        {
            string query = "SELECT COUNT(1) FROM ItemCategories WHERE Category = @CategoryName";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@CategoryName", categoryName);
            await conn.OpenAsync();
            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        public async Task<bool> EditCategoryAsync(int? categoryId, string editedCategory) {

            if (categoryId == null || categoryId == 0
                || string.IsNullOrEmpty(editedCategory))
                return false;

            bool isDuplicate = await CategoryAlreadyExistsAsync(editedCategory);

            if (isDuplicate) return false;


            string query = "UPDATE ItemCategories SET Category = @EditedCategory WHERE CategoryId = @ID";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EditedCategory", editedCategory);
            cmd.Parameters.AddWithValue("@ID", categoryId);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();   
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int? categoryId) {

            if (categoryId == null || categoryId == 0) return false;

            string query = "DELETE FROM ItemCategories WHERE CategoryId = @ID";

            using SqlConnection  conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", categoryId);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync() { 
            var list = new List<CategoryModel>();
            string query = "SELECT CategoryID, Category FROM ItemCategories";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new CategoryModel
                {
                    CategoryId = reader.GetInt32(0),
                    Category = reader.GetString(1)
                });
            }
            return list;
        }
    }
    
}
