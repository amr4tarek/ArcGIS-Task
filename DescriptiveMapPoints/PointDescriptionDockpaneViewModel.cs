using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using DescriptiveMapPoints.Helpers;
using DescriptiveMapPoints.MapTools;
using DescriptiveMapPoints.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DescriptiveMapPoints
{
    internal class PointDescriptionDockpaneViewModel : DockPane
    {
        private const string _dockPaneID = "DescriptiveMapPoints_PointDescriptionDockpane";

        private string _pendingDescription;
        private string _heading = "Descriptive Map Points";
        private string _descriptionText;

        public string PendingDescription
        {
            get => _pendingDescription;
            set => SetProperty(ref _pendingDescription, value, () => PendingDescription);
        }

        public string Heading
        {
            get => _heading;
            set => SetProperty(ref _heading, value, () => Heading);
        }

        public string DescriptionText
        {
            get => _descriptionText;
            set => SetProperty(ref _descriptionText, value, () => DescriptionText);
        }

        public ObservableCollection<PointModel> Points { get; }

        public PointModel SelectedPoint { get; set; }

        public ICommand AddPointCommand { get; }
        public ICommand EditPointCommand { get; }
        public ICommand DeletePointCommand { get; }
        public ICommand ZoomToPointCommand { get; }

        public PointDescriptionDockpaneViewModel()
        {
            Points = new ObservableCollection<PointModel>();

            AddPointCommand = new RelayCommand(OnAddPoint);
            EditPointCommand = new RelayCommand(OnEditPoint);
            DeletePointCommand = new RelayCommand(OnDeletePoint);
            ZoomToPointCommand = new RelayCommand(OnZoomToPoint);
        }


        private async void OnZoomToPoint(object obj)
        {
            if (obj is PointModel point && point.Location != null)
            {
                await QueuedTask.Run(() =>
                {
                    var envelope = point.Location.Extent;
                    var buffered = envelope.Expand(1.5, 1.5, true);
                    MapView.Active.ZoomTo(buffered, new TimeSpan(0, 0, 1));
                });
            }
        }

        internal static void Show()
        {
            var pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            pane?.Activate();
        }

        private async void OnAddPoint()
        {
            if (string.IsNullOrWhiteSpace(DescriptionText))
            {
                MessageBoxHelper.ShowError("Please enter a description before adding a point.");
                return;
            }

            if (Points.Any(p => p.Description.Equals(DescriptionText, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBoxHelper.ShowError("A point with this description already exists. Please provide a unique description.");
                return;
            }

            PendingDescription = DescriptionText;
            DescriptionText = string.Empty;

            await FrameworkApplication.SetCurrentToolAsync("DescriptiveMapPoints_AddPointMapTool");
        }

        private async void OnEditPoint(object obj)
        {
            if (obj is PointModel point)
            {
                EditPointMapTool.PointToEdit = point;
                await FrameworkApplication.SetCurrentToolAsync("DescriptiveMapPoints_EditPointMapTool");
            }
        }

        private void OnDeletePoint(object obj)
        {
            if (obj is PointModel point)
            {
                var confirm = MessageBoxHelper.ShowConfirmation("Are you sure you want to delete this point?", "Confirm Delete");
                if (!confirm)
                    return;

                QueuedTask.Run(() => point.MapOverlay?.Dispose());

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    Points.Remove(point);
                });
            }
        }
    }

    internal class PointDescriptionDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            PointDescriptionDockpaneViewModel.Show();
        }
    }
}
