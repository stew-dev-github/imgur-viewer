using System;
using System.Collections.Generic;
using System.Text;

namespace imgur_viewer.Services
{
    public class ImgurResponse<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public T Data { get; set; }
    }
}
