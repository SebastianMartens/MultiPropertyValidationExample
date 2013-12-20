using System;
using System.Text.RegularExpressions;
using MultiPropertyValidationExample.Framework;

namespace MultiPropertyValidationExample.Model
{
    /// <summary>
    /// Grou Adress is a value object, so it has no identity ("Id")!
    /// Additionally we can make it immutable in this case.
    /// </summary>
    public class GroupAdress: ErrorsAwareDomainObject
    {
        /// <summary>
        /// Backing field for storing the value.
        ///
        /// Outside of this class we work with different string representations but the value is
        /// stored as ushort behind the scenes (every GroupAdress can be represented as ushort).
        /// 
        /// As the GroupAdress class is an immutable ValueObject, this propertyName is readonly!
        /// </summary>
        public readonly ushort UShortValue;

        /// <summary>
        /// constructor for safe initialisation.
        /// </summary>
        /// <param name="value"></param>
        public GroupAdress(string value)
        {            
            // regex can be improved and has to be tested!
            // These are supported matches for "42" now:
            // 42
            // 0 0 42
            // 0/0/42
            // 0/0 42            
            // 55 666 7/88 42

            var regex = new Regex(@"^(?:[0-9]*\s*[0-9]*\s*[/ ])*([0-9]+$)");
            var match = regex.Match(value);
            if (match.Success)
            {
                // expression groups: 
                // (?:) => uncaptured group (ignored)
                // () captured group is available by match.Groups[1] as match.Groups[0] contains complete matching string.
                UShortValue = ToUshort(match.Groups[1].Value);
            }
            else
            {
                throw new FormatException("The input string in in an unsupported format.");
            }
        }

        /// <summary>
        /// 2nd constructor for safe initialisation.
        /// </summary>
        /// <param name="value"></param>
        public GroupAdress(ushort value)
        {
            UShortValue = value;
        }


        #region toString and parse methods

        /// <summary>        
        /// formated version of group adress value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // this is the "save" version of getting enum values from our "object"-based setting service (could be improved :-))
            // simpler would be: var style = (GaStylesEnum) SettingsService.GetSetting("CurrentGaStyle")
            var style = SettingsService.GetSetting("CurrentGaStyle") is GaStylesEnum ? (GaStylesEnum) SettingsService.GetSetting("CurrentGaStyle") : GaStylesEnum.Slashed; 

            return ToString(style);
        }

        /// <summary>
        /// Here we can implement different styles to fomat the value.
        /// If there are more than a few styles with high complexity, then mention the "open closed principle" and
        /// implement a converter for each style!
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public string ToString(GaStylesEnum style)
        {
            switch (style) // in real app "style" is an enum...
            {
                case GaStylesEnum.Slashed:
                    return String.Format("0/0/{0}", UShortValue);
                case GaStylesEnum.Spaced:
                    return String.Format("0 0 {0}", UShortValue);
                default:
                    return UShortValue.ToString();
            }            
        }

        /// <summary>
        /// Parse a string to a Group Adress.
        /// The parse method of any class should return a new class instance.
        /// Compare to "ToUshort()" method...
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static GroupAdress Parse(string value)
        {            
            return new GroupAdress(value);
        }
        
        /// <summary>
        /// TODO: we can easily implement more logic here, e.g. parsing other styles as "0/0/n" or "0 0 n"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static ushort ToUshort(string value)
        {
            return ushort.Parse(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParse(string value, out GroupAdress val)
        {
            try
            {
                val = Parse(value);
                return true;
            }
            catch (Exception)
            {
                val = null;
                return false;
            }            
        }

        #endregion

       
    }
}
