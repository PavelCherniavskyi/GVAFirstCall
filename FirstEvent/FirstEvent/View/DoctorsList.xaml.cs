using System;
using System.Windows;
using System.Windows.Input;
using FirstEvent.Model;

namespace FirstEvent.View
{
    /// <summary>
    /// Interaction logic for DoctorsList.xaml
    /// </summary>
    public partial class DoctorsList
    {
        private const int GwlStyle = -16;
        private const int WsSysmenu = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        public DoctorsList()
        {
            InitializeComponent();

            Loaded += ToolWindow_Loaded;
        }

        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GwlStyle, GetWindowLong(hwnd, GwlStyle) & ~WsSysmenu);
        }

    }
}
