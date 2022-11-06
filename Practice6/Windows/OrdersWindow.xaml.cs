using System.Linq;
using System.Windows;
using Practice6.Models;

namespace Practice6.Windows
{
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переход на окно корреспондентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CorrespondentsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        /// <summary>
        /// Переход на окно документов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new DocumentsWindow().Show();
            Close();
        }

        /// <summary>
        /// Обновление таблицы с данными мероприятий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                UchPracticeDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataBaseDG.ItemsSource = UchPracticeDBEntities.GetContext().Orders.ToList();
            }
        }
    }
}
