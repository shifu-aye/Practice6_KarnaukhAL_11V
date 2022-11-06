using Practice6.Models;
using System.Linq;
using System.Windows;

namespace Practice6.Windows
{
    public partial class DocumentsWindow : Window
    {
        public DocumentsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Переход на окно мероприятий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrdersBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new OrdersWindow().Show();
            Close();
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
        /// Обновление таблицы с данными документов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                UchPracticeDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataBaseDG.ItemsSource = UchPracticeDBEntities.GetContext().DocTable.ToList();
            }
        }
    }
}
