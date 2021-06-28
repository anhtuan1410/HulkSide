using HulkSide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.Controllers
{
    [Route("usergroups")]
    public class UserGroupsController : ControllerBase
    {
        [HttpPost("getlist")]
        public BaseResult GetList()
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    var t = db.Usergroups
                        .OrderBy(p => p.CreatedDate)
                        .Select(k => new
                        {
                            iduser = k.IdUserGroup,
                            groupname = k.Groupname
                        }).ToList();
                    return new BaseResult
                    {
                        status = true,
                        data = t
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResult
                {
                    status = false,
                    error = new ErrorResult
                    {
                        ErrCode = 224,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// Get detail of user-groups by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public BaseResult GetDetail(int id)
        {
            try
            {
                var _k = Request.Headers;

                using (var db = new SampleDatabaseContext())
                {
                    var t = db.Usergroups
                        .Where(x => x.IdUserGroup.Equals(id))
                        .Select(k => new
                        {
                            iduser = k.IdUserGroup,
                            groupname = k.Groupname
                        }).SingleOrDefault();
                    return new BaseResult
                    {
                        status = true,
                        data = t
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResult
                {
                    status = false,
                    error = new ErrorResult
                    {
                        ErrCode = 224,
                        ErrMsg = ex.Message
                    }
                };
            }
        }


    }
}
