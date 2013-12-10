using System;

namespace MultiPropertyValidationExample.Model
{
    /// <summary>
    /// Composite of multile items and additional information.
    /// Used to binding and validation in a multi-property example.
    /// </summary>
    public class GroupAdressComposite
    {                
        public string Name { get; set; }

        public GroupAdress ReadAdress { get; set; }
        public GroupAdress WriteAdress { get; set; }


        public static GroupAdressComposite GetDummyItem(int seed)
        {
            var random = new Random(seed);
            return new GroupAdressComposite
                {
                    Name = String.Format("Adresse {0}", random.Next(0, 100)),
                    ReadAdress  = new GroupAdress((ushort) random.Next(0, 100)),
                    WriteAdress = new GroupAdress((ushort) random.Next(0, 100))
                };
        }
    }
}
