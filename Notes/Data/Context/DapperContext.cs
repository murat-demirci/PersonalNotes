using Data.Utilities;
using System.Data;
using System.Data.SqlClient;

namespace Data.Context
{
    public class DapperContext : IDisposable
    {

        private static DapperContext _customDbFactory;
        private static readonly object _lock = new object();
        private string connection { get { return ConnectionStringHelper.GetConncetion(); } }
        public static DapperContext Singleton
        {
            get
            {
                if (_customDbFactory == null)
                {
                    lock (_lock)
                    {
                        if (_customDbFactory == null)
                        {
                            _customDbFactory = new DapperContext();
                        }
                    }
                }
                return _customDbFactory;
            }
        }

        public IDbConnection CreateConnection()
        {

            return new SqlConnection(connection);
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
