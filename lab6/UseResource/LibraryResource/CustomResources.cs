using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

