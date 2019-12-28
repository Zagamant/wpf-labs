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

namespace Notepad1
{
    partial class MainWindow
    {
        // Объявляем внутреннее событие
        private event EventHandler ChangeModifiedEvent;

        // Упаковываем базовое поле modified в свойство
        private bool IsModified
        {
            get { return modified; }
            set
            {
                if (modified != value)
                {
                    modified = !modified;
                    // Инициируем событие, если есть обработчик
                    if (ChangeModifiedEvent != null)
                        ChangeModifiedEvent(this, EventArgs.Empty);
                }
            }
        }

        // Вызов размещен в конструкторе класса
        void AdditionalHandlers()
        {
            // Начальные запрещения для _Save
            itemSave.IsEnabled = btnSave.IsEnabled = false;
            // Удаляем созданный в CreateGestures() жест _Save
            foreach (KeyGesture gest in gests.Keys)
                if (gests[gest] == SaveOnExecute)
                {
                    gests.Remove(gest);
                    break;
                }

            // Регистрируем обработчик изменения свойства
            this.ChangeModifiedEvent += Window1_ChangeModifiedEvent;
        }

        void Window1_ChangeModifiedEvent(object sender, EventArgs e)
        {
            //MessageBox.Show("Modify");

            // Проверяем состояние любого из источников _Save
            if (btnSave.IsEnabled == false)
                // Добавляем жест _Save
                gests.Add(new KeyGesture(Key.S, ModifierKeys.Control),
                    SaveOnExecute);//_Save
            else
                // Удаляем жест _Save
                foreach (KeyGesture gest in gests.Keys)
                    if (gests[gest] == SaveOnExecute)
                    {
                        gests.Remove(gest);
                        break;
                    }

            // Изменяем состояние интерфейсных элементов _Save
            itemSave.IsEnabled = btnSave.IsEnabled = IsModified;
        }
    }
}
