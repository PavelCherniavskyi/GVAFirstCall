using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FirstEvent.View
{
    public interface IHavePassword
    {
        SecureString Password { get; }
    }
    /// <summary>
    /// Interaction logic for WelcomeWind.xaml
    /// </summary>
    public partial class WelcomeWind : Window, IHavePassword
    {
        public WelcomeWind()
        {
            InitializeComponent();
        }

        public SecureString Password => AdminPassword.SecurePassword;
    }
}
