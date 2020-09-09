using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Task1.Pages;

namespace Task1
{
    internal class CreateBrushes : NavigationWindow
    {
        public CreateBrushes()
        {
            Width = 300;
            Height = 300;
            Title = "Каркас приложения Упражнения 1";
            //this.ShowsNavigationUI = false;// Скрыть навигационный интерфейс

            var page1 = new Page1();
            Content = page1; // Поместили страницу в каркас
        }
    }
}