namespace RHGMTool.Helper
{
    public class EnumMapper
    {
        public enum JobClass
        {
            All = 0,
            Frantz = 1,
            Angela = 2,
            Tude = 3,
            Natasha = 4,
            Roselle = 101,
            Leila = 102,
            Edgar = 201,
            Meilin = 301,
            Ian = 401,
        }

        public static int GetJobClassValue(string? jobClassName)
        {
            return jobClassName switch
            {
                nameof(JobClass.All) => (int)JobClass.All,
                nameof(JobClass.Frantz) => (int)JobClass.Frantz,
                nameof(JobClass.Angela) => (int)JobClass.Angela,
                nameof(JobClass.Tude) => (int)JobClass.Tude,
                nameof(JobClass.Natasha) => (int)JobClass.Natasha,
                nameof(JobClass.Roselle) => (int)JobClass.Roselle,
                nameof(JobClass.Leila) => (int)JobClass.Leila,
                nameof(JobClass.Edgar) => (int)JobClass.Edgar,
                nameof(JobClass.Meilin) => (int)JobClass.Meilin,
                nameof(JobClass.Ian) => (int)JobClass.Ian,
                _ => 0,
            };
        }

        public static string GetJobClassName(int jobClassCode)
        {
            return jobClassCode switch
            {
                (int)JobClass.Frantz => nameof(JobClass.Frantz),
                (int)JobClass.Angela => nameof(JobClass.Angela),
                (int)JobClass.Tude => nameof(JobClass.Tude),
                (int)JobClass.Natasha => nameof(JobClass.Natasha),
                (int)JobClass.Roselle => nameof(JobClass.Roselle),
                (int)JobClass.Leila => nameof(JobClass.Leila),
                (int)JobClass.Edgar => nameof(JobClass.Edgar),
                (int)JobClass.Meilin => nameof(JobClass.Meilin),
                (int)JobClass.Ian => nameof(JobClass.Ian),
                _ => string.Empty,
            };
        }

        public static IEnumerable<int> MapBranchIndexToValues(int branchIndex)
        {
            return branchIndex switch
            {
                0 => new List<int> { 0 }, // All branches
                1 => new List<int> { 1 },
                2 => new List<int> { 2 },
                3 => new List<int> { 4 },
                4 => new List<int> { 5 },
                5 => new List<int> { 6 },
                6 => new List<int> { 5 },
                _ => new List<int> { 0 },
            };
        }

        public enum SocketColor
        {
            None = 0,
            Red = 1,
            Blue = 2,
            Yellow = 3,
            Green = 4,
            Colorless = 5,
            Gray = 6
        }

        public static readonly Dictionary<string, int> socketColorIdMap = new()
        {
            { SocketColor.None.ToString(), (int)SocketColor.None },
            { SocketColor.Red.ToString(), (int)SocketColor.Red },
            { SocketColor.Blue.ToString(), (int)SocketColor.Blue },
            { SocketColor.Yellow.ToString(), (int)SocketColor.Yellow },
            { SocketColor.Green.ToString(), (int)SocketColor.Green },
            { SocketColor.Colorless.ToString(), (int)SocketColor.Colorless },
            { SocketColor.Gray.ToString(), (int)SocketColor.Gray }
        };

        public enum ItemType
        {
            All,
            Item,
            Costume,
            Armor,
            Weapon,
            Invalid
        }

    }


}
