using HulkSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HulkSide.Controllers
{
    public static class StoreDB
    {
        public static object GetListUserGroup()
        {
            using (var db  = new SampleDatabaseContext())
            {
                //var _q = db.Usergroups.FromSqlRaw<object>("EXEC sp_Get_List_UserGroup ''").ToList();
                var _k = db.Report_2256s.FromSqlRaw("EXEC sp_Report_2256 ''").ToList();
                 //           var _d = db.Users
                 //.FromSqlRaw("EXEC sp_Get_List_UserGroup ''")
                 //.ToList();
                //db.Database.ExecuteSqlRaw("EXEC sp_Get_List_UserGroup ''", null);
                //db.Database.SqlQuery<Report_2256>("Select x, y, z FROM tbl FOR JSON AUTO").First();
                //db.Database.com
                return _k;
            }
        }
    }
}
