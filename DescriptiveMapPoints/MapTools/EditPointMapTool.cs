using ArcGIS.Core.Geometry;
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
    internal class EditPointMapTool : MapTool
    {
        public static PointModel PointToEdit { get; set; }

        public EditPointMapTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Point;
            SketchOutputMode = SketchOutputMode.Map;
        }

        protected override async Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            try
            {
                if (!(geometry is MapPoint newPoint) || PointToEdit == null)
                    return false;

                await QueuedTask.Run(() =>
                {
                    PointToEdit.MapOverlay?.Dispose();

                    var symbol = SymbolFactory.Instance.ConstructPointSymbol(
                        ColorFactory.Instance.CreateRGBColor(0, 0, 255),
                        10, SimpleMarkerStyle.Circle);

                    var newOverlay = MapView.Active.AddOverlay(newPoint, symbol.MakeSymbolReference());

                    PointToEdit.Location = newPoint;
                    PointToEdit.MapOverlay = newOverlay;
                });
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowError($"Error editing point: {ex.Message}");
            }
            finally
            {
                PointToEdit = null;
                 FrameworkApplication.SetCurrentToolAsync(null);
            }
            return true;
        }
    }
}
