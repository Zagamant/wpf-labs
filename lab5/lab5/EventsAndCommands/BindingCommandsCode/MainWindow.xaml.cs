using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BindingCommandsCode
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Регистрация обработчика
            this.Loaded += new RoutedEventHandler(Window1_Loaded);
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            // Привязка команд в коде
            CommandBinding binding = new CommandBinding();
            binding.Command = ApplicationCommands.Open;
            binding.Executed += new ExecutedRoutedEventHandler(OpenCommand_Executed);
            this.CommandBindings.Add(binding);

            binding = new CommandBinding();
            binding.Command = ApplicationCommands.Save;
            binding.Executed += new ExecutedRoutedEventHandler(SaveCommand_Executed);
            this.CommandBindings.Add(binding);

            // Очистка коллекций прежних жестов команд
            ApplicationCommands.Open.InputGestures.Clear();
            ApplicationCommands.Save.InputGestures.Clear();

            // Добавление новых жестов клавиатуры Alt+O и Alt+S
            InputGesture key = new KeyGesture(Key.O, ModifierKeys.Alt, "Alt+O");
            ApplicationCommands.Open.InputGestures.Add(key);
            //
            KeyGesture keyGesture = new KeyGesture(Key.S, ModifierKeys.Alt, "Alt+S");
            ApplicationCommands.Save.InputGestures.Add(keyGesture);

            // Добавление новых жестов мыши Ctrl+LeftClick и Ctrl+RightClick
            InputGesture mouse = new MouseGesture(MouseAction.LeftClick, ModifierKeys.Control);
            ApplicationCommands.Open.InputGestures.Add(mouse);
            //
            MouseGesture mouseGesture = new MouseGesture();
            mouseGesture.MouseAction = MouseAction.RightClick;
            mouseGesture.Modifiers = ModifierKeys.Control;
            ApplicationCommands.Save.InputGestures.Add(mouseGesture);
        }

        void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Выполнена команда Open");
        }

        void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Выполнена команда Save");
        }
    }

}
