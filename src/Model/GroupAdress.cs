using System;

namespace MultiPropertyValidationExample.Model
{
    /// <summary>
    /// Grou Adress is a value object, so it has no identity ("Id")!
    /// Additionally we can make it immutable in this case.
    /// </summary>
    public class GroupAdress
    {
        /// <summary>
        /// Backing field for storing the value.
        ///
        /// Outside of this class we work with different string representations but the value is
        /// stored as ushort behind the scenes (every GroupAdress can be represented as ushort).
        /// 
        /// As the GroupAdress class is an immutable ValueObject, this property is readonly!
        /// </summary>
        public readonly ushort UShortValue;

        /// <summary>
        /// constructor for safe initialisation.
        /// </summary>
        /// <param name="value"></param>
        public GroupAdress(string value)
        {
            UShortValue = ToUshort(value);
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
        /// TODO: get default display options from some kind of application settings and return 
        /// formated version of group adress value.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return UShortValue.ToString();
        }

        /// <summary>
        /// Here we can implement different styles to fomat the value.
        /// If there are more than a few styles with high complexity, then mention the "open closed principle" and
        /// implement a converter for each style!
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public string ToString(string style)
        {
            switch (style) // in real app "style" is an enum...
            {
                case "full":
                    return String.Format("0/0/{0}", UShortValue);
            }
            return null;
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
            return new GroupAdress(value); // consider to create constructor for init
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

        #endregion
    }
}
