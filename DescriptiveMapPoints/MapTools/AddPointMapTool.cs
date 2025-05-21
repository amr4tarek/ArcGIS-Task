using ArcGIS.Core.CIM;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using DescriptiveMapPoints.Helpers;
using DescriptiveMapPoints.Model;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace DescriptiveMapPoints.MapTools
{
    internal class AddPointMapTool : MapTool
    {
        public AddPointMapTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override async Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            try
            {
                if (!(geometry is MapPoint mapPoint))
                {
                    MessageBoxHelper.ShowError("Invalid geometry selected.");
                    return false;
                }

                var pane = FrameworkApplication.DockPaneManager.Find("DescriptiveMapPoints_PointDescriptionDockpane")
                           as PointDescriptionDockpaneViewModel;
                if (pane == null)
                {
                    MessageBoxHelper.ShowError("DockPane not available.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(pane.PendingDescription))
                {
                    MessageBoxHelper.ShowError("Description cannot be empty.");
                    return false;
                }

                IDisposable overlay = null;
                await QueuedTask.Run(() =>
                {
                    var symbol = SymbolFactory.Instance.ConstructPointSymbol(
                        ColorFactory.Instance.CreateRGBColor(255, 0, 0),
                        10, SimpleMarkerStyle.Circle);

                    overlay = MapView.Active.AddOverlay(mapPoint, symbol.MakeSymbolReference());

                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    pane.Points.Add(new PointModel
                    {
                        Description = pane.PendingDescription,
                        Location = mapPoint,
                        MapOverlay = overlay
                    });
                    pane.PendingDescription = null;
                });
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"Error adding point: {ex.Message}");
            }
            finally
            {
                 FrameworkApplication.SetCurrentToolAsync(null);
            }

            return true;
        }
    }
}
