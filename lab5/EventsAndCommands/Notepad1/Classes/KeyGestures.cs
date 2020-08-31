using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;// Для Dictionary<TKey, TValue>

namespace Notepad1
{
    partial class MainWindow
    {   // Еще один вариант в Петцольд, WPF, с.316 !!!
        // Определяем ассоциативный словарь
        Dictionary<KeyGesture, RoutedEventHandler> gests =
            new Dictionary<KeyGesture, RoutedEventHandler>();

        void CreateGestures()
        {
            // File
            gests.Add(new KeyGesture(Key.N, ModifierKeys.Control), NewOnExecute);//_New
            gests.Add(new KeyGesture(Key.O, ModifierKeys.Control), OpenOnExecute);//_Open...
            gests.Add(new KeyGesture(Key.S, ModifierKeys.Control), SaveOnExecute);//_Save
            gests.Add(new KeyGesture(Key.F2, ModifierKeys.Control), PrintPreviewOnExecute);//P_rint Preview
            gests.Add(new KeyGesture(Key.P, ModifierKeys.Control), PrintOnExecute);//_Print...

            // Edit
            gests.Add(new KeyGesture(Key.Z, ModifierKeys.Control), UndoOnExecute);//_Undo
            gests.Add(new KeyGesture(Key.Y, ModifierKeys.Control), RedoOnExecute);//_Redo
            gests.Add(new KeyGesture(Key.X, ModifierKeys.Control), CutOnExecute);//Cu_t
            gests.Add(new KeyGesture(Key.C, ModifierKeys.Control), CopyOnExecute);//_Copy
            gests.Add(new KeyGesture(Key.V, ModifierKeys.Control), PasteOnExecute);//_Paste
            gests.Add(new KeyGesture(Key.Delete, ModifierKeys.None), DeleteOnExecute);//De_lete
            gests.Add(new KeyGesture(Key.F, ModifierKeys.Control), FindOnExecute);//_Find...
            gests.Add(new KeyGesture(Key.F3, ModifierKeys.None), FindNextOnExecute);//Find _Next
            gests.Add(new KeyGesture(Key.H, ModifierKeys.Control), ReplaceOnExecute);//_Replace...
            gests.Add(new KeyGesture(Key.G, ModifierKeys.Control), GoToOnExecute);//_Go To...
            gests.Add(new KeyGesture(Key.A, ModifierKeys.Control), SelectAllOnExecute);//Select _All

            // Format
            gests.Add(new KeyGesture(Key.W, ModifierKeys.Control), WordWrapOnExecute);//_Word Wrap
        }

        // Перекрываем стандартный обработчик
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // Ищем жест, останавливаем событие и исполняем обработчик
            foreach (KeyGesture gest in gests.Keys)
                if (gest.Matches(null, e))  // Сравниваем перехваченный жест с заданным в объекте
                {
                    gests[gest](this, e);   // Вызываем обработчик через словарь
                    e.Handled = true;       // Останавливаем событие
                    break;                  // Прерываем цикл
                }
        }
    }
}
