using System;
using System.Globalization;
using System.Windows.Data;
using MultiPropertyValidationExample.Framework;
using MultiPropertyValidationExample.Model;

namespace MultiPropertyValidationExample.UI.Converters
{
    /// <summary>
    /// Converter is useful here, as the conversion logic will be complex.
    /// The converter will mostly be used in the WPF binding but is not limited to it => could be part of the domain model!?
    /// (Some converters will only be used in the UI and are UI specific (e.g. convert to "visibility") - those remain to the UI namespace....)
    /// 
    /// TODO: make example of how to use a custom converter/ parameter with custom styles (here: "0/0/n" vs. "0 0 n")
    /// </summary>
    [ValueConversion(typeof(GroupAdress), typeof(string))]  
    public class GroupAdressToStringConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GroupAdress.Parse((string)value);
        }
    }
}
