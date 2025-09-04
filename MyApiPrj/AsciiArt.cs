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
            GetProperty(fontName, BindingFlags.Static | BindingFlags.Public)
            ?.GetValue(null)
            as FiggleFont;
        }
        font ??= FiggleFonts.Standard;
        return font.Render(text);  
    }


}