namespace Config
{
    public class SqlConnectionString : BaseConfig<string>
    {
        private SqlConnectionString(string value) : base(value)
        {
        }

        public static implicit operator string(SqlConnectionString obj)
        {
            return obj.Value;
        }

        public static implicit operator SqlConnectionString(string value)
        {
            return new SqlConnectionString(value);
        }
    }
}
