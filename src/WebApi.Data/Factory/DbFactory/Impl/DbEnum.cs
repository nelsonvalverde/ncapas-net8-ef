namespace WebApi.Data.Factory.DbFactory.Impl;

public static class DbSchema
{
    public static string Select(Schema schema)
    {
        return schema switch
        {
            Schema.dbo => "dbo",
            Schema.log => "log",
            Schema.aud => "aud",
            _ => "dbo"
        };
    }

    public enum Schema
    {
        dbo,
        log,
        aud
    }
}