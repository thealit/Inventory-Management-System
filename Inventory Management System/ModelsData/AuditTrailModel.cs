using Inventory_Management_System.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.ModelsData
{
    public class AuditTrailModel
    {
        public int TrailId { get; set; }
        public string Fullname { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string ActionDescription { get; set; }
    }

    public interface IAuditTrailRepository
    {
        Task<List<AuditTrailModel>> GetAllInventoryLogsAsync();
        Task<List<AuditTrailModel>> SearchInventoryLogsAsync(string searchTerm);
    }

    public class AuditTrailRepository : IAuditTrailRepository
    {
        public async Task<List<AuditTrailModel>> GetAllInventoryLogsAsync()
        { 
            var list = new List<AuditTrailModel>();
            string query = @"
                SELECT
                    TrailId,
                    Fullname,
                    ActionDateTime,
                    ActionDescription
                FROM [dbo].[InventoryLogs]
                ORDER BY ActionDateTime DESC";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            
            await conn.OpenAsync();
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new AuditTrailModel
                {
                    TrailId = reader.GetInt32(reader.GetOrdinal("TrailId")),
                    Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                    ActionDateTime = reader.GetDateTime(reader.GetOrdinal("ActionDateTime")),
                    ActionDescription = reader.GetString(reader.GetOrdinal("ActionDescription"))
                });
            }
            
            return list;
        }

        // search by user, description
        public async Task<List<AuditTrailModel>> SearchInventoryLogsAsync(string searchTerm)
        { 
            var filteredList = new List<AuditTrailModel>();
            string query = @"
                SELECT
                    TrailId,
                    Fullname,
                    ActionDateTime,
                    ActionDescription
                FROM [dbo].[InventoryLogs]
                WHERE Fullname LIKE @Search
                    OR ActionDescription LIKE @Search
                ORDER BY ActionDateTime DESC";

            using SqlConnection conn = ApplicationDB.GetConnection();
            using SqlCommand cmd = new(query, conn);
            
            await conn.OpenAsync();
            cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = $"%{searchTerm}%";

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                filteredList.Add(new AuditTrailModel
                {
                    TrailId = reader.GetInt32(reader.GetOrdinal("TrailId")),
                    Fullname = reader.GetString(reader.GetOrdinal("Fullname")),
                    ActionDateTime = reader.GetDateTime(reader.GetOrdinal("ActionDateTime")),
                    ActionDescription = reader.GetString(reader.GetOrdinal("ActionDescription"))
                });
            }
            
            return filteredList;
        }

        
    }
}
