using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public interface IManageUnitRepository
    {
        Task<bool> AddUnitAsync(string unitName);
        Task<bool> EditUnitAsync(int? unitId, string newUnitName);
        Task<bool> DeleteUnitAsync(int? unitId);
        Task<bool> UnitAlreadyExistsAsync(string unitName);
        Task<List<UnitModel>> GetAllUnitsAsync();
    }

    public class ManageUnitRepository : IManageUnitRepository
    {
        public async Task<bool> AddUnitAsync(string unitName)
        {
            string query = "INSERT INTO ItemUnit (Unit) VALUES (@UnitName)";

            if (string.IsNullOrWhiteSpace(unitName)) return false;
            bool unitExists = await UnitAlreadyExistsAsync(unitName);

            if (unitExists) return false;

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UnitName", unitName);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> UnitAlreadyExistsAsync(string unitName)
        {
            string query = "SELECT COUNT(1) FROM ItemUnit WHERE Unit = @UnitName";
            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@UnitName", unitName);
            await conn.OpenAsync();
            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count > 0;
        }

        public async Task<bool> EditUnitAsync(int? unitId, string editedUnitName)
        {

            if (unitId == null || unitId == 0
                || string.IsNullOrEmpty(editedUnitName))
                return false;

            bool isDuplicate = await UnitAlreadyExistsAsync(editedUnitName);

            if (isDuplicate) return false;


            string query = "UPDATE ItemUnit SET Unit = @EditedUnitName WHERE UnitID = @ID";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EditedUnitName", editedUnitName);
            cmd.Parameters.AddWithValue("@ID", unitId);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteUnitAsync(int? unitId)
        {

            if (unitId == null || unitId == 0) return false;

            string query = "DELETE FROM ItemUnit WHERE UnitID = @ID";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", unitId);
            await conn.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<List<UnitModel>> GetAllUnitsAsync()
        {
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
    }
}