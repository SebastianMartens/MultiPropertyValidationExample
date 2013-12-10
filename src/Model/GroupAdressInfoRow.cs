namespace MultiPropertyValidationExample.Model
{
    /// <summary>
    /// Composite of two read-GA, write-GA and additional information
    /// </summary>
    public class GroupAdressInfoRow
    {
        public string Name { get; set; }
        public GroupAdress ReadAdress { get; set; }
        public GroupAdress WriteAdress { get; set; }
    }
}
