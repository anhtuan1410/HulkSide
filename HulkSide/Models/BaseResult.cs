using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HulkSide.Models
{
    public class BaseResult
    {
        public bool status { get; set; }
        public object data { get; set; }
        public ErrorResult error { get; set; }
        public Panigator panigator { get; set; }
    }

    public class ErrorResult
    {
        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
    }

    public class Panigator
    {
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public int RowsPerPage { get; set; }
    }

}
