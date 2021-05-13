using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TreeviewWithImageConverter
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeViewModel)
            {
                TreeViewModel tvm = (TreeViewModel)value;

                string filePath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + "\\images\\";

                Uri uri = null;
                switch (tvm.TreeItemType)
                {
                    case TreeItemType.Folder:
                        if (tvm.IsNodeExpanded)
                            uri = new Uri(filePath + "FolderOpened-32.png", UriKind.RelativeOrAbsolute);
                        else
                            uri = new Uri(filePath + "FolderClosed-32.png", UriKind.RelativeOrAbsolute);
                        break;
                    case TreeItemType.EngineCode:
                        uri = new Uri(filePath + "EngineCode.png", UriKind.RelativeOrAbsolute);
                        break;
                    case TreeItemType.WindowsCode:
                        uri = new Uri(filePath + "WindowsCode.png", UriKind.RelativeOrAbsolute);
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
