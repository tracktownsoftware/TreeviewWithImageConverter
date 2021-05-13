using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeviewWithImageConverter
{
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If image files Build Action is None and copy always then use this approach to load images
            //      string filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + "\\images\\";
            //      uri = new Uri(filePath + "FolderOpened-32.png", UriKind.RelativeOrAbsolute);

            // Code below uses image files Build Action is Resource and copy always
            if (value is TreeViewModel)
            {
                TreeViewModel tvm = (TreeViewModel)value;

                Uri uri = null;
                switch (tvm.TreeItemType)
                {
                    case TreeItemType.Folder:
                        if (tvm.IsNodeExpanded)
                            uri = new Uri("/images/FolderOpened-32.png", UriKind.Relative);
                        else
                            uri = new Uri("/images/FolderClosed-32.png", UriKind.Relative);
                        break;
                    case TreeItemType.EngineCode:
                        uri = new Uri("/images/EngineCode.png", UriKind.Relative);
                        break;
                    case TreeItemType.WindowsCode:
                        uri = new Uri("/images/WindowsCode.png", UriKind.Relative);
                        break;
                }
                BitmapImage source = new BitmapImage(uri);
                return source;
             }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
            //throw new NotSupportedException("Cannot convert back");
        }
    }
}
