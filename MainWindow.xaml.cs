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
using KursachAnna.Models;
using KursachAnna.Context;
using System.ComponentModel;

namespace KursachAnna
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Useraccount _authenticatedUser; //передаем данные об авторизованном пользователе

        private string _username;
        private Visibility _buttonInShow;
        private Visibility _buttonOutShow = Visibility.Collapsed;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public Visibility ButtonInShow
        {
            get { return _buttonInShow; }
            set
            {
                _buttonInShow = value;
                OnPropertyChanged(nameof(ButtonInShow));
            }
        }

        public Visibility ButtonOutShow
        {
            get { return _buttonOutShow; }
            set
            {
                _buttonOutShow = value;
                OnPropertyChanged(nameof(ButtonOutShow));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            OknoToShow.Content = new menu();
            ShowMenu();
            
        }

        public void ShowMenu()
        {
            var menu = new menu();
            OknoToShow.Content = menu;
            if (menu.Authorization != null)
            {
                menu.Authorization.UserAuthenticated += MenuControl_UserAuthenticated;
            }
        }


        public void SetAuthenticatedUser(Useraccount user)
        {
            _authenticatedUser = user;
        }

        private void MenuControl_UserAuthenticated(object sender, UserAuthenticatedEventArgs e)
        {
            _authenticatedUser = e.AuthenticatedUser;
            ShowMenu();
        }

        public void ChangeContentToMenu(Useraccount user)
        {
            Dispatcher.Invoke(() =>
            {
                OknoToShow.Content = new menu();
                _authenticatedUser = user;
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Создаем окно регистрации
            Registrartion registrationWindow = new Registrartion(this);

            // Устанавливаем владельца
            registrationWindow.Owner = this;

            // Делаем окно модальным и отображаем его
            registrationWindow.ShowDialog();
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Код для выхода
        }
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignInButton.Visibility = Visibility.Collapsed;
            SignOutButton.Visibility = Visibility.Visible;
        }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            SignInButton.Visibility = Visibility.Visible;
            SignOutButton.Visibility = Visibility.Collapsed;
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
