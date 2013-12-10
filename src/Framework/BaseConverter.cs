using System;
using System.Windows.Markup;

namespace MultiPropertyValidationExample.Framework
{
    /// <summary>
    /// --- Cool Trick from Christian Moser: http://wpftutorial.net/ValueConverters.html ---
    ///
    /// Simplify the usage of ValueConvers
    /// If you want to use a normal ValueConverter in XAML, you have to add an instance of it to the resources and reference it by using a key. 
    /// This is cumbersome, because and the key is typically just the name of the converter.
    /// A simple and cool trick is to derive value converters from MarkupExtension. This way you can create and use it in the binding like this:
    /// Text={Binding Time, Converter={x:MyConverter}}, and that is quite cool!
    /// 
    /// 
    /// </summary>
    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
