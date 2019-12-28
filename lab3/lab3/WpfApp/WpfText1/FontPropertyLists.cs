using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace WpfText1
{
    /// <summary>
    /// Код класса из примера MSDN
    /// </summary>
    public class FontPropertyLists
    {
        static Collection<FontFamily> fontFaces;
        static Collection<FontWeight> fontWeights;
        static Collection<double> fontSizes;
        static int maxFontSizes = 36;

        /// <summary>
        /// Заполняет коллекцию доступных системе FontFamily
        /// </summary>
        public static ICollection<FontFamily> FontFaces
        {
            get
            {
                if (fontFaces == null) fontFaces = new Collection<FontFamily>();
                foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
                {
                    fontFaces.Add(fontFamily);
                }
                return fontFaces;
            }
        }

        /// <summary>
        /// Заполняет коллекцию доступных FontWeight
        /// </summary>
        public static ICollection FontWeights
        {
            get
            {
                if (fontWeights == null)
                {
                    fontWeights = new Collection<FontWeight>();

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
                return fontWeights;
            }
        }

        static void Unique(FontWeight fontWeight)
        {
            if (fontWeights.IndexOf(fontWeight) == -1)
                fontWeights.Add(fontWeight);
        }

        /// <summary>
        /// Заполняет коллекцию доступных FontSizes 
        /// </summary>
        public static Collection<double> FontSizes
        {
            get
            {
                if (fontSizes == null)
                {
                    fontSizes = new Collection<double>();
                    for (double i = 8; i <= maxFontSizes; i++) fontSizes.Add(i);
                }
                return fontSizes;
            }
        }

        public static bool CanParseFontWeight(string fontWeightName)
        {
            try
            {
                FontWeightConverter converter = new FontWeightConverter();
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
            FontWeightConverter converter = new FontWeightConverter();
            return (FontWeight)converter.ConvertFromString(fontWeightName);
        }
    }
}
