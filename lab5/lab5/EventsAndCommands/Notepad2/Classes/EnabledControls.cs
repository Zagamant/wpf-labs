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

using System.Windows.Controls.Primitives;// Для ButtonBase

namespace Notepad2
{
    partial class MainWindow
    {
        // Объявляем и инициализируем поля команд 
        // Их обязательно нужно объявлять статическими, чтобы
        // размещались в объекте-типе и уже были созданы перед
        // созданием элементов, к которым присоединяются
        // Последний параметр означает жесты
        public static RoutedCommand SaveCommand = ApplicationCommands.Save;
        public static RoutedCommand PageSetupCommand =
            new RoutedCommand("PageSetup", typeof(MainWindow), null);// Без жеста
        public static RoutedCommand UndoCommand = ApplicationCommands.Undo;
        public static RoutedCommand RedoCommand = ApplicationCommands.Redo;
        public static RoutedCommand CutCommand = ApplicationCommands.Cut;
        public static RoutedCommand CopyCommand = ApplicationCommands.Copy;
        public static RoutedCommand PasteCommand = ApplicationCommands.Paste;
        public static RoutedCommand DeleteCommand = ApplicationCommands.Delete;
        public static RoutedCommand FindNextCommand;// Определим в ст. конструкторе
        public static RoutedCommand ReplaceCommand = ApplicationCommands.Replace;
        public static RoutedCommand GoToCommand;// Определим в ст. конструкторе
        public static RoutedCommand SelectAllCommand = ApplicationCommands.SelectAll;
        // Статический конструктор
        static MainWindow()
        {
                // Определяем с добавлением жестов
                InputGestureCollection coll = new InputGestureCollection();
                coll.Add(new KeyGesture(Key.F3, ModifierKeys.None, "F3"));
                FindNextCommand = new RoutedCommand("FindNext", typeof(MainWindow), coll);
                coll = new InputGestureCollection();
                coll.Add(new KeyGesture(Key.G, ModifierKeys.Control, "Ctrl+G"));
                GoToCommand = new RoutedCommand("GoTo", typeof(MainWindow), coll);

                // Заменяем библиотечные команды на свои для правильной работы логики
                coll = new InputGestureCollection();
                coll.Add(new KeyGesture(Key.Z, ModifierKeys.Control, "Ctrl+Z"));
                UndoCommand = new RoutedCommand("Undo", typeof(MainWindow), coll);
                coll = new InputGestureCollection();
                coll.Add(new KeyGesture(Key.Y, ModifierKeys.Control, "Ctrl+Y"));
                RedoCommand = new RoutedCommand("Redo", typeof(MainWindow), coll);
                coll = new InputGestureCollection();
                coll.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));
                SelectAllCommand = new RoutedCommand("SelectAll", typeof(MainWindow), coll);
        }
        // Вызов размещен в конструкторе класса
        void AdditionalHandlers()
        {
            Clipboard.Clear();// Временно, чтобы испытать начальное состояние
            // Присоединяем команды к источникам, жесты уже встроены в команды
            //SaveCommand       - присоединим в разметке
            //PageSetupCommand  - присоединим в разметке
            //UndoCommand       - присоединим в разметке
            //RedoCommand       - присоединим в разметке
            //CutCommand        - присоединим в разметке
            //CopyCommand       - присоединим в разметке
            btnPaste.Command = itemPaste.Command = contextPaste.Command = PasteCommand;
            btnDelete.Command = itemDelete.Command = contextDelete.Command = DeleteCommand;
            itemFindNext.Command = FindNextCommand;
            itemReplace.Command = ReplaceCommand;
            itemGoTo.Command = GoToCommand;
            itemSelectAll.Command = SelectAllCommand;

            // Привязка части команд к объекту окна в коде
            // 0)
            this.CommandBindings.Add(new CommandBinding(RedoCommand, RedoOnExecute, RedoCanExecute));
            this.CommandBindings.Add(new CommandBinding(CutCommand, CutOnExecute, CutCanExecute));
            this.CommandBindings.Add(new CommandBinding(CopyCommand, CopyOnExecute, CopyCanExecute));
            // Теперь чуть подлиннее: создаем, настраиваем, привязываем!
            // 1)
            CommandBinding binding = new CommandBinding();
            binding.Command = ReplaceCommand;
            binding.Executed += ReplaceOnExecute;
            binding.CanExecute += ReplaceCanExecute;
            this.CommandBindings.Add(binding);
            // 2)
            binding = new CommandBinding(GoToCommand);
            binding.Executed += GoToOnExecute;
            this.CommandBindings.Add(binding);
            // 3)
            binding = new CommandBinding(SelectAllCommand, SelectAllOnExecute);
            binding.CanExecute += SelectAllCanExecute;
            this.CommandBindings.Add(binding);
        }


        // Обработчики события CanExecute команд
        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если есть несохраненные изменения
            e.CanExecute = IsModified;
        }
        /*
        private void PageSetupCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Источник не регулируется
        }
        */
        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если есть что откатывать
            e.CanExecute = IsModified;
        }
        private void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если очередь отмены есть и стоим не самые первые
            e.CanExecute = txtBox1.CanRedo;
        }
        private void CutCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если есть выделенный текст
            e.CanExecute = txtBox1.SelectionLength > 0;
        }
        private void CopyCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если есть выделенный текст
            e.CanExecute = txtBox1.SelectionLength > 0;
        }
        private void PasteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если буфер обмена непустой и там содержится текстовый формат
            e.CanExecute = Clipboard.ContainsText();
        }
        private void DeleteCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Позволить, если есть выделенный текст
            e.CanExecute = txtBox1.SelectionLength > 0;
        }
        private void FindNextCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //!!!!! От фонаря - не хочется думать !!!!!
            e.CanExecute = txtBox1.CaretIndex < txtBox1.Text.Length;
        }
        private void ReplaceCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //!!!!! От фонаря - не хочется думать !!!!!
            e.CanExecute = txtBox1.CaretIndex < txtBox1.Text.Length;
        }
        /*
        private void GoToCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Источник не регулируется
        }
        */
        private void SelectAllCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Заблокировать, если нечего выделять или уже все выделено
            if (txtBox1.Text == String.Empty ||
                txtBox1.SelectionLength == txtBox1.Text.Length)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }
    }

}