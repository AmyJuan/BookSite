using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Dao.Mapping
{
    internal static class Constants
    {
        public static string SCHEMA = "dbo";

        public static string SHORT_NVAR = "nvarchar(127)";

        public static string MID_NVAR = "nvarchar(255)";

        public static string LONG_NVAR = "nvarchar(1023)";

        public static string Max_NVAR = "nvarchar(MAX)";

        public static string BIGINT = nameof(SqlDbType.BigInt);//"bigint";

        public static string TINYINT = nameof(SqlDbType.TinyInt);//"tinyint";

        public static string INT = nameof(SqlDbType.Int);//"int";

        public static string UNIQUEIDENTIFIER = nameof(SqlDbType.UniqueIdentifier);// "UNIQUEIDENTIFIER";

        public static int LONGCHARLENGTH = 1023;

        public static int SHORTCHARLENGTH = 127;

        public static int MIDCHARLENGTH = 255;

        public static string DATETIME2 = nameof(SqlDbType.DateTime2);
    }
}
