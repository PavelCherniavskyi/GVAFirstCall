using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using FirstEvent.Model;
using FirstEvent.View;
using FirstEvent.View.MainWindows;
using GalaSoft.MvvmLight.Command;

namespace FirstEvent.ViewModel
{
    public class WelcomeViewModel
    {
        private readonly Task _task = new Task(DataBaseManager.Initialize);

        public WelcomeViewModel()
        {
            AdminLoginCommand = new RelayCommand<Window>(AdminLoginExecute);
            UserLoginCommand = new RelayCommand<Window>(UserLoginExecute);
            _task.Start();
        }

        public string UserName { get; set; } = string.Empty;

        public bool IsExam { get; set; }

        public RelayCommand<Window> AdminLoginCommand { get; set; }

        public RelayCommand<Window> UserLoginCommand { get; set; }

        public string Password { get; set; }

        private void AdminLoginExecute(Window obj)
        {
            if (obj == null)
                return;
            var havePassword = obj as IHavePassword;
            if (havePassword != null)
            {
                var secureString = havePassword.Password;
                Password = ConvertToUnsecureString(secureString);
            }

            _task.Wait();

            if (DataBaseManager.GetPassByName("Admin").Pass != Password)
                new WarningWindow("Wrong password").ShowDialog();
            else
            {
                new FEList().Show();
                obj.Close();
            }
        }

        private void UserLoginExecute(Window wind)
        {
            if (UserName == string.Empty)
            {
                new WarningWindow("Please enter user name.").ShowDialog();
            }
            else
            {
                _task.Wait();

                if (IsExam)
                    new MainExamWindow(UserName).Show();
                else
                    new MainTestWindow(UserName).Show();
                wind.Close();
            }
            
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
