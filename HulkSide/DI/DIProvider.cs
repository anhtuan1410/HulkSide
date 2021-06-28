using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.DI
{
    public class FactoryMarvel
    {
        public static IMarvel getMarvel(string _d)
        {
            IMarvel service = null;
            try
            {
                String database = _d;
                switch (database)
                {
                    case "1":
                        service = new PostgreSQL();
                        break;
                    case "2":
                        service = new OracleDB();
                        break;
                    case "3":
                        service = new SQLSequel();
                        break;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return service;
        }
    }

    public class DIProvider
    {
        IMarvel marvel;
        public int startDB { get; set; }
        public DIProvider()
        {
            marvel = FactoryMarvel.getMarvel(this.startDB.ToString());
        }

        public DIProvider(int _startDB)
        {
            this.startDB = _startDB;
            marvel = FactoryMarvel.getMarvel(this.startDB.ToString());
        }
        public IMarvel getDao()
        {
            return marvel;
        }
        public void setDao(IMarvel dao)
        {
            this.marvel = dao;
        }

        public List<string> execute()
        {
            return new List<string> 
            {
                { marvel.insert() },
                { marvel.update() },
                { marvel.delete() },
            };
        }
    }
}
