using System.Data;
using System.Data.SQLite;
using static RHGMTool.Helper.EnumMapper;

namespace RHGMTool.Data
{
    public class DataTableManager
    {
        private static DataTable? cachedDataTable;

        public static DataTable? GetCachedDataTable()
        {
            if (cachedDataTable == null)
            {
                InitializeCachedDataTable();
            }

            return cachedDataTable;
        }

        private static void InitializeCachedDataTable()
        {
            cachedDataTable = new DataTable();

            // Fetch data from each table and merge into the cachedDataTable
            AddDataToCachedDataTable(ItemType.Item, "itemlist");
            AddDataToCachedDataTable(ItemType.Costume, "itemlist_costume");
            AddDataToCachedDataTable(ItemType.Armor, "itemlist_armor");
            AddDataToCachedDataTable(ItemType.Weapon, "itemlist_weapon");
        }

        private static void AddDataToCachedDataTable(ItemType itemType, string itemTableName)
        {
            using SQLiteConnection? connection = SQLiteDBConnection.OpenDatabaseConnection();
            if (connection == null)
            {
                return;
            }

            string query = $@"
            SELECT i.nID, i.nWeaponID00, i.szIconName, i.nCategory, i.nSubCategory, i.nBranch, i.nSocketCountMin, i.nSocketCountMax, i.nReconstructionMax, i.nJobClass, i.nLevelLimit, 
                i.nItemTrade, i.nOverlapCnt, i.nDurability, i.nDefense, i.nMagicDefense, i.nWeight, i.nSellPrice, i.nOptionCountMin, i.nOptionCountMax, i.nSetId, i.nFixOption00, i.nFixOptionValue00, i.nFixOption01, i.nFixOptionValue01, i.nPetEatGroup,
                s.wszDesc, s.wszItemDescription
            FROM {itemTableName} i
            LEFT JOIN {itemTableName}_string s ON i.nID = s.nID";

            using SQLiteDataAdapter adapter = new(query, connection);
            DataTable itemDataTable = new();
            adapter.Fill(itemDataTable);

            // Add a column for ItemType and set its value
            itemDataTable.Columns.Add("ItemType", typeof(ItemType));
            foreach (DataRow row in itemDataTable.Rows)
            {
                row["ItemType"] = itemType;
            }

            // Merge the current itemDataTable into the cachedDataTable
            cachedDataTable?.Merge(itemDataTable);
        }


    }
}
