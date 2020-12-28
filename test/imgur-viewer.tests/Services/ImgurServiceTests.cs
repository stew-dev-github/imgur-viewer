using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace imgur_viewer.tests.Services
{
    public class ImgurServiceTests
    {
        private readonly imgur_viewer.Services.ImgurService _service;

        public ImgurServiceTests()
        {
            _service = new imgur_viewer.Services.ImgurService();
        }

        [Theory]
        [InlineData("https://www.imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]
        [InlineData("https://imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]
        [InlineData("http://www.imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]
        [InlineData("http://imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]
        [InlineData("www.imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]
        [InlineData("imgur.com/gallery/6XQGzz7", Models.Type.Gallery)]

        [InlineData("https://www.imgur.com/a/6XQGzz7", Models.Type.Album)]
        [InlineData("https://imgur.com/a/6XQGzz7", Models.Type.Album)]
        [InlineData("http://www.imgur.com/a/6XQGzz7", Models.Type.Album)]
        [InlineData("http://imgur.com/a/6XQGzz7", Models.Type.Album)]
        [InlineData("www.imgur.com/a/6XQGzz7", Models.Type.Album)]
        [InlineData("imgur.com/a/6XQGzz7", Models.Type.Album)]

        [InlineData("https://www.imgur.com/DN4eDeg", Models.Type.Image)]
        [InlineData("https://imgur.com/DN4eDeg", Models.Type.Image)]
        [InlineData("http://www.imgur.com/DN4eDeg", Models.Type.Image)]
        [InlineData("http://imgur.com/DN4eDeg", Models.Type.Image)]
        [InlineData("www.imgur.com/DN4eDeg", Models.Type.Image)]
        [InlineData("imgur.com/DN4eDeg", Models.Type.Image)]

        [InlineData("https://www.i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("https://i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("https://imgur.com/DN4eDeg.jpg", Models.Type.Image)]

        [InlineData("http://www.i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("http://i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("http://imgur.com/DN4eDeg.jpg", Models.Type.Image)]

        [InlineData("www.i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("i.imgur.com/DN4eDeg.jpg", Models.Type.Image)]
        [InlineData("imgur.com/DN4eDeg.jpg", Models.Type.Image)]

        [InlineData("www.i.imgur.com/DN4eDeg.png", Models.Type.Image)]
        [InlineData("i.imgur.com/DN4eDeg.gif", Models.Type.Image)]
        public void GetTypeForUrl_ProducesExpectedType(string url, Models.Type expectedType)
        {
            var type = _service.GetTypeForUrl(url);

            Assert.Equal(expectedType, type);
        }

        [Theory]
        [InlineData("https://www.imgur.com/gallery/6XQGzz7", "6XQGzz7")]
        [InlineData("https://imgur.com/gallery/6XQGzz7", "6XQGzz7")]
        [InlineData("http://www.imgur.com/gallery/6XQGzz7", "6XQGzz7")]
        [InlineData("http://imgur.com/gallery/6XQGzz7", "6XQGzz7")]
        [InlineData("www.imgur.com/gallery/6XQGzz7", "6XQGzz7")]
        [InlineData("imgur.com/gallery/6XQGzz7", "6XQGzz7")]

        [InlineData("https://www.imgur.com/a/6XQGzz7", "6XQGzz7")]
        [InlineData("https://imgur.com/a/6XQGzz7", "6XQGzz7")]
        [InlineData("http://www.imgur.com/a/6XQGzz7", "6XQGzz7")]
        [InlineData("http://imgur.com/a/6XQGzz7", "6XQGzz7")]
        [InlineData("www.imgur.com/a/6XQGzz7", "6XQGzz7")]
        [InlineData("imgur.com/a/6XQGzz7", "6XQGzz7")]

        [InlineData("https://www.imgur.com/DN4eDeg", "DN4eDeg")]
        [InlineData("https://imgur.com/DN4eDeg", "DN4eDeg")]
        [InlineData("http://www.imgur.com/DN4eDeg", "DN4eDeg")]
        [InlineData("http://imgur.com/DN4eDeg", "DN4eDeg")]
        [InlineData("www.imgur.com/DN4eDeg", "DN4eDeg")]
        [InlineData("imgur.com/DN4eDeg", "DN4eDeg")]

        [InlineData("https://www.i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("https://i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("https://imgur.com/DN4eDeg.jpg", "DN4eDeg")]

        [InlineData("http://www.i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("http://i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("http://imgur.com/DN4eDeg.jpg", "DN4eDeg")]

        [InlineData("www.i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("i.imgur.com/DN4eDeg.jpg", "DN4eDeg")]
        [InlineData("imgur.com/DN4eDeg.jpg", "DN4eDeg")]

        [InlineData("www.i.imgur.com/DN4eDeg.png", "DN4eDeg")]
        [InlineData("i.imgur.com/DN4eDeg.gif", "DN4eDeg")]
        public void GetHashcodeForUrl_ProducesExpectedHashcode(string url, string expectedHashcode)
        {
            var hashcode = _service.GetHashcodeForUrl(url);

            Assert.Equal(expectedHashcode, hashcode);
        }

        [Theory]
        [InlineData("https://www.imgur.com/b/6XQGzz7")]
        [InlineData("https://www.imgur.com/b/6XQGzz7.jpg")]
        [InlineData("ftp://www.imgur.com/6XQGzz7.jpg")]
        public void GetTypeForUrl_ThrowsArgumentException_WhenInvalidUrlFormat(string url) 
            => Assert.Throws<ArgumentException>(() => _service.GetTypeForUrl(url));

        [Theory]
        [InlineData("https://www.imgur.com/b/6XQGzz7")]
        [InlineData("https://www.imgur.com/b/6XQGzz7.jpg")]
        [InlineData("ftp://www.imgur.com/6XQGzz7.jpg")]
        public void GetHashcodeForUrl_ThrowsArgumentException_WhenInvalidUrlFormat(string url) 
            => Assert.Throws<ArgumentException>(() => _service.GetHashcodeForUrl(url));
    }
}
