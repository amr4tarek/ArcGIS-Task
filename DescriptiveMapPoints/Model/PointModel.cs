using ArcGIS.Core.Geometry;
using System;
using System.ComponentModel;

namespace DescriptiveMapPoints.Model
{
    public class PointModel : INotifyPropertyChanged
    {
        private string _description;
        private MapPoint _location;
        private IDisposable _mapOverlay;

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public MapPoint Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                    OnPropertyChanged(nameof(Latitude));
                    OnPropertyChanged(nameof(Longitude));
                }
            }
        }

        public double Latitude => Location?.Y ?? 0;
        public double Longitude => Location?.X ?? 0;

        public IDisposable MapOverlay
        {
            get => _mapOverlay;
            set
            {
                if (_mapOverlay != value)
                {
                    _mapOverlay = value;
                    OnPropertyChanged(nameof(MapOverlay));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
