using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace KursachAnna
{
    public partial class menu : UserControl
    {
        public Registrartion Authorization { get; private set; }
        public menu()
        {
            InitializeComponent();
            Authorization = new Registrartion();
        }

        //запуск списка товаров для всадника
        private void OpenVsadnikTovarsList(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            int category_id = 1;
            mainWindow.OknoToShow.Content = new ForVsadnikTovarsList(category_id);
            //товары для всаднкиа
        }

        private void Authorization_UserAuthenticated(object sender, UserAuthenticatedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.SetAuthenticatedUser(e.AuthenticatedUser);
                menu mainMenu = new menu();
                mainWindow.OknoToShow.Content = mainMenu;
            }
        }

        private void OpenHorseTovarsList(object sender, RoutedEventArgs e) //товары для лошади
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            int category_id = 2;
            mainWindow.OknoToShow.Content = new ForHorseTovarsList(category_id);
        }
    }
}
