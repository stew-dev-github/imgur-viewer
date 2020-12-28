using imgur_viewer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace imgur_viewer.Views
{
    /// <summary>
    /// Interaction logic for ViewerView.xaml
    /// </summary>
    public partial class ViewerView : UserControl
    {
        public ViewerView()
        {
            DataContext = new ViewerViewModel();
            InitializeComponent();
        }
    }
}
