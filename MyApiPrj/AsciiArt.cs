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


}