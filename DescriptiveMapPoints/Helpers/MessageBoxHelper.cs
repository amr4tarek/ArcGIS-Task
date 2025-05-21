using ArcGIS.Desktop.Framework.Dialogs;

namespace DescriptiveMapPoints.Helpers
{
    public static class MessageBoxHelper
    {
        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }

        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        public static bool ShowConfirmation(string message, string title = "Confirm")
        {
            var result = MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question);
            return result == System.Windows.MessageBoxResult.Yes;
        }
    }
}
