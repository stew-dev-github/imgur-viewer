using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace imgur_viewer
{
    public class JsonResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("animated")]
        public bool Animated { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }

    public class JsonWrapper
    {
        [JsonPropertyName("data")]
        public JsonResult Data { get; set; }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var client = new Services.ImgurService();

        //    var result = await client.GetImageAsync(txtBox.Text);

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };

        //    var jsonResult = JsonSerializer.Deserialize<JsonWrapper>(result, options);
       
        //    if(jsonResult.Data.Animated || jsonResult.Data.Type.ToLower().Contains("gif"))
        //    {
        //        // DANGER
        //    }
        //    else
        //    {
        //        var fileUri = new Uri(jsonResult.Data.Link);
        //        this.imgControl.Source = new BitmapImage(fileUri);
        //    }
        //}
    }
}
