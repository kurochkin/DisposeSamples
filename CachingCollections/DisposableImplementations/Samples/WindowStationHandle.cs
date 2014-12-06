using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DisposableImplementations.Samples
{
    // Пример корректной реализации IDisposable.
    // В идеале нужно еще наследоваться от SafeHandle
    public sealed class WindowStationHandle : IDisposable
    {
        public WindowStationHandle(IntPtr handle)
        {
            this.Handle = handle;
        }

        public WindowStationHandle()
            : this(IntPtr.Zero)
        {
        }

        public bool IsInvalid
        {
            get { return (this.Handle == IntPtr.Zero); }
        }

        public IntPtr Handle { get; set; }

        private void CloseHandle()
        {
            // Если хэндл нулевой, ничего не делаем
            if (this.IsInvalid)
            {
                return;
            }

            // Закрытие хэндла, запись ошибок
            if (!NativeMethods.CloseWindowStation(this.Handle))
            {
                Trace.WriteLine("CloseWindowStation: " + new Win32Exception().Message);
            }

            // Установка хэндлу нулевого значения
            this.Handle = IntPtr.Zero;
        }

        public void Dispose()
        {
            this.CloseHandle();
            GC.SuppressFinalize(this);
        }

        ~WindowStationHandle()
        {
            this.CloseHandle();
        }
    }

    internal static partial class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseWindowStation(IntPtr hWinSta);
    }

}
