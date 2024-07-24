using Microsoft.Data.SqlClient;

namespace WebApi.Data.Common.Converters;

public static class ReaderConvert
{
    public static long ToLong(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt64(dr[name]) : 0;
    }

    public static short ToShort(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt16(dr[name]) : (short)0;
    }

    public static short? ToShortNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt16(dr[name]) : null;
    }

    public static int ToInt32(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt32(dr[name]) : 0;
    }

    public static int? ToInt32Null(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt32(dr[name]) : null;
    }

    public static long? ToLongNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToInt64(dr[name]) : null;
    }

    public static string ToString(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? (string)dr[name] : "";
    }

    public static string ToStringTrim(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? ((string)dr[name]).Trim() : "";
    }

    public static string ToStringNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? (string)dr[name] : null!;
    }

    public static bool ToBool(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) && Convert.ToBoolean(dr[name]);
    }

    public static bool? ToBoolNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToBoolean(dr[name]) : default;
    }

    public static decimal ToDecimal(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDecimal(dr[name]) : 0.00M;
    }

    public static decimal? ToDecimalNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDecimal(dr[name]) : null;
    }

    public static DateTime ToDateTime(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDateTime(dr[name]) : DateTime.UtcNow;
    }

    public static DateTime? ToDateTimeNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDateTime(dr[name]) : null;
    }

    public static DateOnly ToDateOnly(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? DateOnly.FromDateTime(Convert.ToDateTime(dr[name])) : DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public static double ToDouble(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDouble(dr[name]) : 0;
    }

    public static double? ToDoubleNull(SqlDataReader dr, string name)
    {
        return ColumnExists(dr, name) ? Convert.ToDouble(dr[name]) : null;
    }

    private static bool ColumnExists(SqlDataReader dr, string columnName)
    {
        try
        {
            return !dr.IsDBNull(dr.GetOrdinal(columnName));
        }catch (IndexOutOfRangeException)
        {
            return false;
        }
        
    }
}