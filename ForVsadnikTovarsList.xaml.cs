using KursachAnna.Context;
using KursachAnna.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KursachAnna
{
    public partial class ForVsadnikTovarsList : UserControl, INotifyPropertyChanged
    {

        public ObservableCollection<Product> ForVsadnikList { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> filterVsadnikList { get; set; } = new ObservableCollection<Product>();

        private Visibility _kategoriavisibility = Visibility.Collapsed;
        public Visibility KategoriaVisibility
        {
            get { return _kategoriavisibility; }
            set
            {
                if (_kategoriavisibility != value)
                {
                    _kategoriavisibility = value;
                    OnPropertyChanged(nameof(KategoriaVisibility));
                }
            }
        }

        private int _categoryid;
        public ForVsadnikTovarsList()
        {
            InitializeComponent();
        }
        public ForVsadnikTovarsList(int categoryId) //выводим список товаров
        {
            InitializeComponent();
            DataContext = this;
            _categoryid = categoryId;

            using (var context = new User1000Context())
            {
                var appointmentList = context.Products
                    .Include(m => m.Category)
                    .Include(p => p.Categoriesid2Navigation)
                    .Where(m => m.Categoryid == _categoryid) // Исправлено на UserId
                    .ToList();

                ForVsadnikList = new ObservableCollection<Product>(appointmentList);
                filterVsadnikList = new ObservableCollection<Product>(appointmentList);
            }
            //здесь будет использоваться контекст базы данных чтобы отобразить информацию о товаре
        }

        private void OpenSideMenu()
        {
            // Создание и запуск анимации открытия бокового меню
            DoubleAnimation animation = new DoubleAnimation(200, TimeSpan.FromSeconds(0.2));
            KategoriaMenu.BeginAnimation(Grid.WidthProperty, animation);
            KategoriaVisibility = Visibility.Visible;
        }

        private void CloseSideMenu()
        {
            // Создание и запуск анимации закрытия бокового меню
            DoubleAnimation animation = new DoubleAnimation(40, TimeSpan.FromSeconds(0.2));
            KategoriaMenu.BeginAnimation(Grid.WidthProperty, animation);
            KategoriaVisibility = Visibility.Collapsed;
        }


        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            // Переключение состояния бокового меню (открыто/закрыто)
            if (KategoriaMenu.ActualWidth > 40)
                CloseSideMenu();
            else
                OpenSideMenu();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PereituVmenu(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = Window.GetWindow(this) as MainWindow;
            mainwindow.OknoToShow.Content = new menu();
        }

        private void ApplyPriceSort_Click(object sender, RoutedEventArgs e)
        {
            bool isPriceFromValid = decimal.TryParse(PriceOt.Text, out decimal priceFrom);
            bool isPriceToValid = decimal.TryParse(PriceDo.Text, out decimal priceTo);

            // Если поля пустые или введены некорректные данные, отображаем полный список
            if (!isPriceFromValid || !isPriceToValid)
            {
                filterVsadnikList.Clear();
                foreach (var product in ForVsadnikList)
                {
                    filterVsadnikList.Add(product);
                }
                return;
            }

            // Если введены корректные данные, применяем фильтр
            filterVsadnikList.Clear();
            foreach (var product in ForVsadnikList)
            {
                if (product.Price >= priceFrom && product.Price <= priceTo)
                {
                    filterVsadnikList.Add(product);
                }
            }
        }
                // Обработка некорректного ввода пользователя

    }
}
