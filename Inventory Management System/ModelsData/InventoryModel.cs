using Inventory_Management_System.ModelsData;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Shapes;

namespace Inventory_Management_System.Models
{
    // facing db interactions

    public class InventoryModel
    {

        public int ItemId { get; set; }

        public string? Category { get; set; }

        public string? Item { get; set; }

        public int InStock { get; set; }

        public string? ItemUnit { get; set; }

        public int MinimumStock { get; set; }

        public bool IsSelected { get; set; }

        public int CategoryId { get; set; }
        public int UnitId { get; set; }
    }

    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? Category { get; set; }
    }

    public class UnitModel
    {
        public int UnitId { get; set; }
        public string? Unit { get; set; }
    }

    public interface IInventoryRepository {

        Task<bool> AddAsync(InventoryModel item);
        Task<bool> UpdateAsync(InventoryModel item);
        Task<bool> DeleteAsync(List<int> itemId);
        Task<bool> DoesItemExistsAsync(string item, int itemId);
        Task<List<InventoryModel>> GetAllInventoriesAsync();
        Task<List<InventoryModel>> SearchInventoryAsync(string searchTerm);
        Task<int> CountLowStockAsync();
        Task<int> CountOutOfStockAsync();
        Task<int> CountAllStocksAsync();
        Task<List<InventoryModel>> GetLowStockAsync();
        Task<List<InventoryModel>> GetOutOfStockAsync();
        Task<List<CategoryModel>> GetCategoriesAsync();
        Task<List<UnitModel>> GetUnitsAsync();
        Task<bool> LogInventoryActivity(string userFullName, string action);
    }

    public class InventoriesRepository : IInventoryRepository
    {
        public async Task<bool> AddAsync(InventoryModel item) {
            string query = "INSERT INTO Inventory (Category, Item, InStock, Unit, MinimumStock) VALUES (@Category, @Item, @InStock, @Unit, @MinimumStock)";

            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();

            int rowsAffected = 0;
            using (SqlCommand cmd = new(query, conn))
            {
                cmd.Parameters.AddWithValue("@Category", item.CategoryId);
                cmd.Parameters.AddWithValue("@Item", item.Item);
                cmd.Parameters.AddWithValue("@InStock", item.InStock);
                cmd.Parameters.AddWithValue("@Unit", item.UnitId);
                cmd.Parameters.AddWithValue("@MinimumStock", item.MinimumStock);
                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateAsync(InventoryModel item)
        {
            string query = "UPDATE Inventory SET Category = @Category, Item = @Item, InStock = @InStock, Unit = @Unit, MinimumStock = @MinimumStock WHERE ItemID = @ID";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);

            cmd.Parameters.AddWithValue("@ID", item.ItemId);
            cmd.Parameters.AddWithValue("@Category", item.CategoryId);
            cmd.Parameters.AddWithValue("@Item", item.Item);
            cmd.Parameters.AddWithValue("@InStock", item.InStock);
            cmd.Parameters.AddWithValue("@Unit", item.UnitId);
            cmd.Parameters.AddWithValue("@MinimumStock", item.MinimumStock);

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(List<int> itemIds) {

            var paramNames = itemIds.Select((id, i) => $"@ID{i}").ToList();
            string query = $"DELETE FROM Inventory WHERE ItemID IN ({string.Join(", ", paramNames)})";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);

            for (int i = 0; i < itemIds.Count; i++)
                cmd.Parameters.Add($"@ID{i}", SqlDbType.Int).Value = itemIds[i];

            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DoesItemExistsAsync(string item, int itemId) {
            string query = "SELECT COUNT(1) FROM Inventory WHERE LOWER(Item) = LOWER(@Item) AND ItemID != @ItemID";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@Item", item.Trim());
            cmd.Parameters.Add("@ItemID", System.Data.SqlDbType.Int).Value = itemId;

            await conn.OpenAsync();
            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

            // if >1: duplicate exists, else none
            return count > 0;
        }

        public async Task<List<InventoryModel>> GetAllInventoriesAsync() {
            var list = new List<InventoryModel>();

            string query = @"
                SELECT 
                    i.ItemID,
                    i.Category AS CategoryId,
                    ISNULL(c.Category, 'Uncategorized') AS Category,
                    i.Item,
                    i.InStock,
                    i.Unit AS UnitId,
                    ISNULL(u.Unit, 'N/A') AS Unit,
                    i.MinimumStock
                FROM Inventory i
                LEFT JOIN ItemCategories c ON i.Category = c.CategoryID
                LEFT JOIN ItemUnit u ON i.Unit = u.UnitID";

            using (SqlConnection conn = ApplicationDB.GetConnection())
            using (SqlCommand cmd = new(query, conn))
            {
                await conn.OpenAsync();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new InventoryModel
                    {
                        ItemId = reader.GetInt32(reader.GetOrdinal("ItemID")),
                        CategoryId = reader.IsDBNull(reader.GetOrdinal("CategoryId"))
                                     ? 0
                                     : reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        Category = reader.GetString(reader.GetOrdinal("Category")),
                        Item = reader.GetString(reader.GetOrdinal("Item")),
                        InStock = reader.GetInt32(reader.GetOrdinal("InStock")),
                        UnitId = reader.IsDBNull(reader.GetOrdinal("UnitId"))
                                     ? 0
                                     : reader.GetInt32(reader.GetOrdinal("UnitId")),
                        ItemUnit = reader.GetString(reader.GetOrdinal("Unit")),
                        MinimumStock = reader.GetInt32(reader.GetOrdinal("MinimumStock"))
                    });
                }
            }

            return list;
        }

        public async Task<List<InventoryModel>> SearchInventoryAsync(string searchTerm)
        { 
            List<InventoryModel> filteredList = new List<InventoryModel>();

            string query = @"SELECT 
                                i.ItemID, 
                                i.Category AS CategoryId,
                                ISNULL(c.Category, 'Uncategorized') AS Category,
                                i.Item, 
                                i.InStock, 
                                i.Unit AS UnitId,
                                ISNULL(u.Unit, 'N/A') AS Unit, 
                                i.MinimumStock
                            FROM Inventory i
                            LEFT JOIN ItemCategories c ON i.Category = c.CategoryID
                            LEFT JOIN ItemUnit u ON i.Unit = u.UnitID
                            WHERE i.Item LIKE @Search 
                               OR ISNULL(c.Category, 'Uncategorized') LIKE @Search
                               OR ISNULL(u.Unit, 'N/A') LIKE @Search";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using (SqlCommand cmd = new(query, conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%");
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    filteredList.Add(new InventoryModel
                    {
                        ItemId = reader.GetInt32(reader.GetOrdinal("ItemID")),
                        CategoryId = reader.IsDBNull(reader.GetOrdinal("CategoryId")) ? 0 : reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        Category = reader.GetString(reader.GetOrdinal("Category")),
                        Item = reader.GetString(reader.GetOrdinal("Item")),
                        InStock = reader.GetInt32(reader.GetOrdinal("InStock")),
                        UnitId = reader.IsDBNull(reader.GetOrdinal("UnitId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UnitId")),
                        ItemUnit = reader.GetString(reader.GetOrdinal("Unit")),
                        MinimumStock = reader.GetInt32(reader.GetOrdinal("MinimumStock"))
                    });
                }
            }

            return filteredList;
        }

        public async Task<int> CountLowStockAsync()
        {
            return (await GetAllInventoriesAsync()).Count(i => i.InStock <= i.MinimumStock && i.InStock > 0);
        }

        public async Task<int> CountOutOfStockAsync()
        {
            return (await GetAllInventoriesAsync()).Count(i => i.InStock == 0);
        }

        public async Task<int> CountAllStocksAsync()
        {
            return (await GetAllInventoriesAsync()).Sum(i => i.InStock);
        }

        public async Task<List<InventoryModel>> GetLowStockAsync()
        {
            return (await GetAllInventoriesAsync()).Where(i => i.InStock <= i.MinimumStock && i.InStock > 0).ToList();
        }

        public async Task<List<InventoryModel>> GetOutOfStockAsync()
        {
            return (await GetAllInventoriesAsync()).Where(i => i.InStock == 0).ToList();
        }

        public async Task<List<CategoryModel>> GetCategoriesAsync() {
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
        public async Task<List<UnitModel>> GetUnitsAsync() {
            var list = new List<UnitModel>();
            string query = "SELECT UnitID, Unit FROM ItemUnit";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new UnitModel
                {
                    UnitId = reader.GetInt32(0),
                    Unit = reader.GetString(1)
                });
            }
            return list;
        }

        public async Task<bool> LogInventoryActivity(string userFullName, string action)
        {
            string query = "INSERT INTO InventoryLogs (Fullname, ActionDateTime, ActionDescription) VALUES (@Fullname, @ActionDateTime, @ActionDescription)";

            DateTime now = DateTime.Now;

            using SqlConnection conn = ApplicationDB.GetConnection();
            await conn.OpenAsync();

            int rowsAffected = 0;
            using (SqlCommand cmd = new(query, conn))
            {
                cmd.Parameters.AddWithValue("@Fullname", userFullName);
                cmd.Parameters.AddWithValue("@ActionDateTime", now);
                cmd.Parameters.AddWithValue("@ActionDescription", action);
                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }

            return rowsAffected > 0;
        }
    }
}
