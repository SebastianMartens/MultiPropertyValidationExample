using System;
using System.Globalization;
using System.Windows;
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
            return value!=null ? value.ToString() : String.Empty;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // -----------
            // Converting valid values is easy.
            // But what about unconvertable values?? => see different examples below.
            // -----------
            if (value == null || value.Equals("0") || String.IsNullOrEmpty(value.ToString()))
                return null;
            GroupAdress result;

            // Option 1: do not convert, rely on data binding for error handling:
            // Use DependencyProperty.UnsetValue to indicate that the property exists, but does not have its value set by the property system
            // Use Binding.DoNothing to instruct the binding engine not to transfer a value to the binding target, not to move to the next Binding in a PriorityBinding, or not to use the FallBackValue or default value            
            //
            // Pro: very simple, works "out of the box"
            // Con: no place to set any custom error message as the input string is not converted to any object that implements INotifyDataErrorInfo.
            // -----------            
            //return GroupAdress.TryParse((string)value, out result) ? result : DependencyProperty.UnsetValue;
            

            // Option 2: Return converted object with validation errors.
            // Suboption 2.1: store wrong value (usually as string) in the entity. Write validator to set errors based on "ErrorsAwareDomainObject".            
            // Suboption 2.2: don't store the wrong value but set ErrorInfo of the entity (invalid value is cleared from UI).
            // Here I show option 2.2:
            // -----------            
            var canConvert = GroupAdress.TryParse((string)value, out result);
            if (!canConvert)
            {
                result = new GroupAdress("0"); // the instance is not validated inside and everytime in a consistent state!

                // Error is set on the whole entity instead of an entity member (the member is not known)!
                result.SetError(String.Format("Error: Provided GroupAdress value '{0}' is invalid!", value)); // TODO: localize                
            }
            return result;
                       

            // Option 3: Exceptions
            // You should NEVER throw exceptions from inside of converters! They would not be catched by the environment and may popup as runtimer errors!
            // -----------


            // Option 4: You can also return the original value in some cases (depends on data types and binding. To be evaluated...).            
            // -----------
        }
    }
}
