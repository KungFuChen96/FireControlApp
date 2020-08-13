namespace Tools
{
    public static class Tools
    {
        public static string SqlSafe(string sql)
        {
            sql = sql.Replace("'", string.Empty);
            return sql;
        }
    }
}