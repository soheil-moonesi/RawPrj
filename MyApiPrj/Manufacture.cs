public class Manufacture
{
    public int ManufacturerIdentifier { get; set; }
    public string ManufactureName { get; set; }
    public string ManufactureCountry { get; set; }

public virtual ICollection<Product> ManufactureProducts { get; set; }

}