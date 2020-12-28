using Microsoft.Win32;

namespace RemoteNotes.UI.Utility
{
    public class ImagePicker
    {
        public string ChooseImage()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            return openFileDialog.ShowDialog().HasValue ? openFileDialog.FileName : null;
        }
    }
}
