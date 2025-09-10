//manufacure is "one" side 
//The relationship is: One Manufacturer can make Many Products.

public class Manufacture
{
    public int ManufacturerIdentifier { get; set; }
    public string ManufactureName { get; set; }
    public string ManufactureCountry { get; set; }
    //This is a collection navigation property. 
    // It holds a list of all the Product entities that belong to this single Manufacture. 
    //This represents the "Many" part of the relationship from the parent's perspective.
    public virtual ICollection<Product> ManufactureProducts { get; set; }

}