using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace Task1
{
	public class FontPropertyLists
    {
	    public static ICollection<FontFamily> FontFaces { get; set; }
	    public static Collection<FontWeight> FontWeights { get; set; }
	    public static ICollection<double> FontSizes { get; set; }

	    static readonly int maxFontSizes = 36;

        static FontPropertyLists()
        {
	        if (FontFaces == null)
	        {
		        FontFaces = new Collection<FontFamily>();

		        foreach (var fontFamily in Fonts.SystemFontFamilies)
		        {
			        FontFaces.Add(fontFamily);
		        }
	        }
			

	        if (FontWeights == null)
	        {
		        FontWeights = new Collection<FontWeight>();

		        Unique(System.Windows.FontWeights.Thin);
		        Unique(System.Windows.FontWeights.Light);
		        Unique(System.Windows.FontWeights.Regular);
		        Unique(System.Windows.FontWeights.Normal);
		        Unique(System.Windows.FontWeights.Medium);
		        Unique(System.Windows.FontWeights.Heavy);
		        Unique(System.Windows.FontWeights.SemiBold);
		        Unique(System.Windows.FontWeights.DemiBold);
		        Unique(System.Windows.FontWeights.Bold);
		        Unique(System.Windows.FontWeights.Black);
		        Unique(System.Windows.FontWeights.ExtraLight);
		        Unique(System.Windows.FontWeights.ExtraBold);
		        Unique(System.Windows.FontWeights.ExtraBlack);
		        Unique(System.Windows.FontWeights.UltraLight);
		        Unique(System.Windows.FontWeights.UltraBold);
		        Unique(System.Windows.FontWeights.UltraBlack);

	        }

	        if (FontSizes == null)
	        {
		        FontSizes = new Collection<double>();

		        for (double i = 8; i <= maxFontSizes; i++)
		        {
			        FontSizes.Add(i);
		        }
	        }
        }

        private static void Unique(FontWeight fontWeight)
        {
            if (FontWeights.IndexOf(fontWeight) == -1)
                FontWeights.Add(fontWeight);
        }


        public static bool CanParseFontWeight(string fontWeightName)
        {
            try
            {
                var converter = new FontWeightConverter();
                converter.ConvertFromString(fontWeightName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static FontWeight ParseFontWeight(string fontWeightName)
        {
            var converter = new FontWeightConverter();

            return (FontWeight)converter.ConvertFromString(fontWeightName);
        }
    }
}
