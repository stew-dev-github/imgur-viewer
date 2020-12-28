using System;
using System.Collections.Generic;
using System.Text;

namespace imgur_viewer.Services
{
    public class Image
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public bool Animated { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
