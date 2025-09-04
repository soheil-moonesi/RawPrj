using Figgle;
using System.Reflection;

namespace AsciiArtSvc;

public static class AsciiArt
{
    public static string Write(string text, string? fontName)
    {
        FiggleFont? font = null;
        if (!string.IsNullOrWhiteSpace(fontName))
        {

            font = typeof(FiggleFonts).
            //Searches for a property with the name
            GetProperty(fontName,
             //Property must be public and static.
             BindingFlags.Static | BindingFlags.Public)
            //Gets the value of the property if found
            ?.GetValue(null)
            //convert result of above to FiggleFont
            as FiggleFont;
        }
        //Uses standard if no property is found
        font ??= FiggleFonts.Standard;
        //Returns as string instead of writing to console
        return font.Render(text);
    }
    //Lazy static property
    //Lazy means that the property’s value isn’t established until its first use.
    public static Lazy<IEnumerable<string>> AllFonts = new(
        //Anonymous method to initialize lazy property
        () =>
        //Uses LINQ to query the data
        from p in typeof(FiggleFonts).
        //GetProperties returns an array of PropertyInfo objects, one for each FiggleFont property
        GetProperties(BindingFlags.Static | BindingFlags.Public)
        //Projects the name of the font
              select p.Name);
              
//     Lazy-initialized properties are useful when it’s expensive to build something and/or
    // the values created take up memory and you want to pay that cost only if those values
    // are used. A list of font names isn’t a substantial use of memory, and reflection is rela-
    // tively fast, so it’s not necessary to use Lazy in this situation, but it doesn’t hurt.


}