using imgur_viewer.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace imgur_viewer.ViewModels
{
    public class ViewerViewModel : BindableBase
    {
        private class ExecuteCommand : ICommand
        {
            private readonly Services.IImgurService _service;

            private readonly ViewerViewModel _viewModel;
            public event EventHandler CanExecuteChanged;

            public ExecuteCommand(ViewerViewModel vm, Services.IImgurService service)
            {
                _service = service;
                _viewModel = vm;

                vm.PropertyChanged += (src, args) =>
                {
                    if (args.PropertyName == nameof(vm.Url))
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public bool CanExecute(object _) => !string.IsNullOrWhiteSpace(_viewModel.Url);

            public async void Execute(object parameter)
            {
                try
                {
                    var identifiedType = _service.GetTypeForUrl(_viewModel.Url);

                    switch(identifiedType)
                    {
                        case Models.Type.Gallery:
                        case Models.Type.Album:
                            var albumHashcode = _service.GetHashcodeForUrl(_viewModel.Url);
                            var albumImages = await _service.GetAlbumImagesAsync(albumHashcode);

                            if(albumImages.Data != null && albumImages.Data.Images != null && albumImages.Data.Images.Any())
                                DisplayImage(albumImages.Data.Images[0].Id);

                            break;
                        case Models.Type.Image:
                            var imageHashcode = _service.GetHashcodeForUrl(_viewModel.Url);
                            DisplayImage(imageHashcode);
                            break;

                        default:
                            throw new NotSupportedException($"Unsupported imgur object {identifiedType}");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            private async void DisplayImage(string hashcode)
            {
                var response = await _service.GetImageAsync(hashcode);

                if (response != null && response.Data != null)
                {
                    if (response.Data.Animated || (!string.IsNullOrWhiteSpace(response.Data.Type) && response.Data.Type.ToLower().Contains("gif")))
                    {
                        MessageBox.Show("Animated Image Detected!\r\nIt will not be shown.");
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(response.Data.Link))
                        _viewModel.Image = new BitmapImage(new Uri(response.Data.Link));
                }
            }
        }

        private string _url;
        private BitmapImage _image;

        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public BitmapImage Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public ICommand GoCommand { get; }

        public ViewerViewModel(Services.IImgurService service)
        {
            GoCommand = new ViewerViewModel.ExecuteCommand(this, service);
        }

        public ViewerViewModel()
            : this(new Services.ImgurService())
        { }
    }
}
