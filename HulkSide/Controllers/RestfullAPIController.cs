using HulkSide.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HulkSide.Controllers
{
    [Route("restful")]
    [ApiController]
    public class RestfullAPIController : ControllerBase
    {
        /// <summary>
        /// response full data, select query
        /// </summary>
        /// <returns></returns>
        [HttpGet("getmethod")]
        public BaseResult GetMethod()
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    var t = db.Usergroups
                        .OrderBy(x => x.CreatedDate)
                        .Select(k => new
                        {
                            idusergroup = k.IdUserGroup,
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }


        /// <summary>
        /// just response header
        /// </summary>
        /// <returns></returns>
        [HttpHead("headmethod")]
        public BaseResult HeadMethod()
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    var t = db.Usergroups
                        .OrderBy(x => x.CreatedDate)
                        .Select(k => new
                        {
                            idusergroup = k.IdUserGroup,
                            groupname = k.Groupname
                        }).ToList();
                    Response.Headers.Add("TokenSign", Guid.NewGuid().ToString());
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// create data
        /// </summary>
        /// <returns></returns>
        [HttpPost("postmethod")]
        public BaseResult PostMethod([FromBody] Usergroup p)
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    if(p != null)
                    {
                        if(string.IsNullOrEmpty(p.Groupname))
                        {
                            return new BaseResult
                            {
                                status = false,
                                error = new ErrorResult
                                {
                                    ErrCode = 2254,
                                    ErrMsg = "Empty group name"
                                }
                            };
                        }

                        p.Groupname = Regex.Replace(p.Groupname.Trim(), "[ ]{2,}", "");

                        if(db.Usergroups.Count(k => k.Groupname.Equals(p.Groupname)) > 0)
                        {
                            return new BaseResult
                            {
                                status = false,
                                error = new ErrorResult
                                {
                                    ErrCode = 2255,
                                    ErrMsg = "Duplicate group name"
                                }
                            };
                        }

                        Usergroup g = new Usergroup();
                        g.Groupname = p.Groupname;
                        g.CreatedBy = 0;
                        g.CreatedDate = DateTime.Now;
                        db.Usergroups.Add(g);
                        db.SaveChanges();

                        return new BaseResult
                        {
                            status = true,
                            data = "Insert success"
                        };
                    }
                    else
                    {
                        return new BaseResult
                        {
                            status = false,
                            error = new ErrorResult
                            {
                                ErrCode = 2234,
                                ErrMsg = "Input is empty"
                            }
                        };
                    }

                    var t = db.Usergroups
                        .OrderBy(x => x.CreatedDate)
                        .Select(k => new
                        {
                            idusergroup = k.IdUserGroup,
                            groupname = k.Groupname
                        }).ToList();
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// put is update all field follow id of object single
        /// </summary>
        /// <returns></returns>
        [HttpPut("putmethod/{keyid}")]
        public BaseResult PutMethod(long keyid, [FromBody] Usergroup p)
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    if (p != null)
                    {
                        if (string.IsNullOrEmpty(p.Groupname))
                        {
                            return new BaseResult
                            {
                                status = false,
                                error = new ErrorResult
                                {
                                    ErrCode = 2254,
                                    ErrMsg = "Empty group name"
                                }
                            };
                        }

                        p.Groupname = Regex.Replace(p.Groupname.Trim(), "[ ]{2,}", "");

                        if (db.Usergroups.Count(k => k.IdUserGroup.Equals(keyid)) == 0)
                        {
                            return new BaseResult
                            {
                                status = false,
                                error = new ErrorResult
                                {
                                    ErrCode = 2251,
                                    ErrMsg = "Undefined group name"
                                }
                            };
                        }

                        if (db.Usergroups.Count(k => k.Groupname.Equals(p.Groupname)
                        && !k.IdUserGroup.Equals(keyid)
                        ) > 0)
                        {
                            return new BaseResult
                            {
                                status = false,
                                error = new ErrorResult
                                {
                                    ErrCode = 2255,
                                    ErrMsg = "Duplicate group name"
                                }
                            };
                        }
                        
                        Usergroup _d = db.Usergroups.Find(keyid);
                        _d.Groupname = p.Groupname;
                        
                        db.SaveChanges();

                        return new BaseResult
                        {
                            status = true,
                            data = "Update success"
                        };
                    }
                    else
                    {
                        return new BaseResult
                        {
                            status = false,
                            error = new ErrorResult
                            {
                                ErrCode = 2234,
                                ErrMsg = "Input is empty"
                            }
                        };
                    }

                    var t = db.Usergroups
                        .OrderBy(x => x.CreatedDate)
                        .Select(k => new
                        {
                            idusergroup = k.IdUserGroup,
                            groupname = k.Groupname
                        }).ToList();
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// put is update a field in one of follow id of object single
        /// </summary>
        /// <returns></returns>
        [HttpPatch("patchmethod/{keyid}")]
        public BaseResult PatchMethod(long keyid, [FromBody] Usergroup p)
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    if (p != null)
                    {                        
                        Usergroup _d = db.Usergroups.Find(keyid);
                        _d.Note = p.Note;
                        db.SaveChanges();

                        return new BaseResult
                        {
                            status = true,
                            data = "Update success by patch"
                        };
                    }
                    else
                    {
                        return new BaseResult
                        {
                            status = false,
                            error = new ErrorResult
                            {
                                ErrCode = 2234,
                                ErrMsg = "Input is empty"
                            }
                        };
                    }

                    var t = db.Usergroups
                        .OrderBy(x => x.CreatedDate)
                        .Select(k => new
                        {
                            idusergroup = k.IdUserGroup,
                            groupname = k.Groupname
                        }).ToList();
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// delete is remove data on server
        /// </summary>
        /// <returns></returns>
        [HttpDelete("deletemethod/{keyid}")]
        public BaseResult DeleteMethod(long keyid)
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    return new BaseResult
                    {
                        status = true,
                        error = new ErrorResult
                        {
                            ErrCode = 99,
                            ErrMsg = "Delete action"
                        }
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// don't understand
        /// </summary>
        /// <returns></returns>
        [HttpOptions("optionmethod")]
        public BaseResult OptionMethod([FromBody] Usergroup p)
        {
            try
            {
                using (var db = new SampleDatabaseContext())
                {
                    return new BaseResult
                    {
                        status = true,
                        data = p,
                        error = new ErrorResult
                        {
                            ErrCode = 99,
                            ErrMsg = "Delete action"
                        }
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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// don't understand
        /// </summary>
        /// <returns></returns>
        [HttpGet("getdatabystore")]
        public BaseResult GetDataByStore()
        {
            try
            {
                
                {

                    var t = StoreDB.GetListUserGroup();

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
                        ErrCode = 1,
                        ErrMsg = ex.Message
                    }
                };
            }
        }



    }
}
