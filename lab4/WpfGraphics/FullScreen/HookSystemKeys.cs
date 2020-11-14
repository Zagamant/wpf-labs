using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms; // Для перечисления Keys 

namespace Task2
{
	internal class HookSystemKeys
	{
		private const int WhKeyboardLl = 13;
		private const int WmKeyup = 257; // Отпускание любой клавиши

		private static readonly LowLevelKeyboardProc Proc = HookCallback;
		private static IntPtr _hookId = IntPtr.Zero;

		// Поддержка флагов состояния системных клавиш 
		private static bool _ctrlKey, _altKey, _winKey;

		private static void StateKey(int vkCode, IntPtr wParam)
		{
			switch ((Keys) vkCode)
			{
				case Keys.LControlKey:
				case Keys.RControlKey:
					_ctrlKey = wParam != (IntPtr) WmKeyup;
					break;
				case Keys.LMenu:
				case Keys.RMenu:
					_altKey = wParam != (IntPtr) WmKeyup;
					break;
				case Keys.LWin:
				case Keys.RWin:
					_winKey = wParam != (IntPtr) WmKeyup;
					break;
			}
		}

		// Общедоступная упаковка оригинала
		public static void FunHook()
		{
			_hookId = SetHook(Proc);
		}

		public static void FunUnHook()
		{
			UnhookWindowsHookEx(_hookId);
		}

		private static IntPtr SetHook(LowLevelKeyboardProc proc)
		{
			using (var curProcess = Process.GetCurrentProcess())
			using (var curModule = curProcess.MainModule)
			{
				//Устанавливает подключаемую процедуру, которая отслеживает низкоуровневые события ввода с клавиатуры.
				return SetWindowsHookEx(WhKeyboardLl, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			var vkCode = Marshal.ReadInt32(lParam);
			StateKey(vkCode, wParam);
			if (_winKey // Системная
			    || _ctrlKey && (Keys) vkCode == Keys.Escape // Ctrl+Esc
			    || _altKey && (Keys) vkCode == Keys.Escape // Alt+Esc
			    || _altKey && (Keys) vkCode == Keys.Tab) // Alt+Tab
				return (IntPtr) 1;
			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		}

		//установить подключаемую процедуру для мониторинга системы для определенных типов событий
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		//удаляет процедуру фильтра (hook), установленную в цепочке фильтров функцией SetWindowsHookEx.
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		//Функция CallNextHookEx переправляет информацию о фильтре (hook)  в следующий фильтр (hook)  в текущей цепочке фильтров событий.
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		//извлекает дескриптор указанного модуля, если файл был отображен в адресном пространстве вызывающего процесса.
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
	}
}