using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.DI
{
    public interface IMarvel
    {
        public string insert();
        public string update();
        public string delete();
    }

    public class PostgreSQL : IMarvel
    {
        string gPostgre = "Postgre";
        public string delete()
        {
            return gPostgre + " delete";
        }

        public string insert()
        {
            return gPostgre + " insert";
        }

        public string update()
        {
            return gPostgre + " update";
        }
    }

    public class SQLSequel : IMarvel
    {
        string gPostgre = "SQL Server";
        public string delete()
        {
            return gPostgre + " delete";
        }

        public string insert()
        {
            return gPostgre + " insert";
        }

        public string update()
        {
            return gPostgre + " update";
        }
    }


    public class OracleDB : IMarvel
    {
        string gPostgre = "Oracle";
        public string delete()
        {
            return gPostgre + " delete";
        }

        public string insert()
        {
            return gPostgre + " insert";
        }

        public string update()
        {
            return gPostgre + " update";
        }
    }


}
