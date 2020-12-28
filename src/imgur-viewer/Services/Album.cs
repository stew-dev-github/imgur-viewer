using System;
using System.Collections.Generic;
using System.Text;

namespace imgur_viewer.Services
{
    public class Album
    {
        public string Id { get; set; }
        public string Link { get; set; }

        public List<Image> Images { get; set; }
    }
}
