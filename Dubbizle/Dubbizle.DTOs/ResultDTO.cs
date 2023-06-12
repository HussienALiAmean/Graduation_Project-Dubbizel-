using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class ResultDTO
    {
        public int StatusCode { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }
    }
}
