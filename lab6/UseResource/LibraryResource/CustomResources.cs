using System.Windows;
using System.Windows.Controls;

namespace LibraryResource
{
    public class CustomResources : Control
    {
        // Статические поля (для разнообразия)
        public static readonly ComponentResourceKey Title1Key =
            new ComponentResourceKey(typeof(CustomResources), "Title1");
        public static readonly ComponentResourceKey Title2Key =
            new ComponentResourceKey(typeof(CustomResources), "Title2");

        // Добавили флаг для регулирования извлечения в элементах разметки
        public static bool changeResourceKeyFlag = true;

        // Добавляем статические свойства
        public static ComponentResourceKey ForegroundBrushKey
        {
            get
            {
                if (changeResourceKeyFlag)
                    return new ComponentResourceKey(
                        typeof(CustomResources), "ForegroundBrush1");
                else
                    return new ComponentResourceKey(
                        typeof(CustomResources), "ForegroundBrush2");
            }
        }

        public static ComponentResourceKey BackgroundBrushKey
        {
            get
            {
                if (changeResourceKeyFlag)
                    return new ComponentResourceKey(
                        typeof(CustomResources), "BackgroundBrush1");
                else
                    return new ComponentResourceKey(
                        typeof(CustomResources), "BackgroundBrush2");
            }
        }
    }
}

