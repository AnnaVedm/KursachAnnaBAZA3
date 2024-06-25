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
using System.Windows.Shapes;
using KursachAnna.Models;
using KursachAnna.Context;
using System.Net;

namespace KursachAnna
{
    public partial class Registrartion : Window
    {
        public static bool isAuthorized;
        public event EventHandler<UserAuthenticatedEventArgs> UserAuthenticated;

        private MainWindow _mainWindow;

        public Registrartion(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        public Registrartion()
        {
            InitializeComponent();
        }

        private void VoitiVAkkaynt_Click(object sender, RoutedEventArgs e)
        {
            string username = NicknamePolzovatel.Text;
            string passwordUser = PasswordPolzovatel.Text;

            using (var db = new User1000Context())
            {
                var user = db.Useraccounts.FirstOrDefault(u => u.Username == username && u.Userpassword == passwordUser);
                if (user != null)
                {
                    _mainWindow.ChangeContentToMenu(user);

                    _mainWindow.Username = user.Username;
                    _mainWindow.ButtonInShow = Visibility.Collapsed;
                    _mainWindow.ButtonOutShow = Visibility.Visible;
                    // Закрываем окно регистрации

                    // Вызываем событие, чтобы сообщить MainWindow об авторизации пользователя
                    OnUserAuthenticated(new UserAuthenticatedEventArgs(user));
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверные данные для авторизации!");
                }
            }
        }


        protected virtual void OnUserAuthenticated(UserAuthenticatedEventArgs e)
        {
            UserAuthenticated?.Invoke(this, e);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class UserAuthenticatedEventArgs : EventArgs
    {
        public Useraccount AuthenticatedUser { get; }

        public UserAuthenticatedEventArgs(Useraccount user)
        {
            AuthenticatedUser = user;
        }
    }
}
