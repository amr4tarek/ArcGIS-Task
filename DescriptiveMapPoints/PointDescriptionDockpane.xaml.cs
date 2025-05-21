using ArcGIS.Desktop.Framework;
using DescriptiveMapPoints.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace DescriptiveMapPoints
{
    public partial class PointDescriptionDockpaneView : UserControl
    {
        public PointDescriptionDockpaneView()
        {
            InitializeComponent();

        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is PointModel point)
            {
                var viewModel = DataContext as PointDescriptionDockpaneViewModel;
                viewModel?.ZoomToPointCommand.Execute(point);
            }
        }
    }
}
