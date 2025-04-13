using Re_Backend.Common.enumscommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common
{
    public class Result<T> 
    {
        public ResultEnum Code { get; set; }
        public T? Data { get; set; }
    }
}
