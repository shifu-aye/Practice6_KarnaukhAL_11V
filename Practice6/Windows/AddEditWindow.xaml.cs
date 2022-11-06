using Practice6.Models;
using System;
using System.Text;
using System.Windows;

namespace Practice6.Windows
{
    public partial class AddEditWindow : Window
    {
        private Correspondents _correspondent = new Correspondents();

        /// <summary>
        /// Конструктор окна добавления и изменения данных
        /// </summary>
        /// <param name="selectedCorrespondent">объект корреспондента из предыдущего окна</param>
        public AddEditWindow(Correspondents selectedCorrespondent)
        {
            InitializeComponent();
            if (selectedCorrespondent != null)
            {
                _correspondent = selectedCorrespondent;
            }
            DataContext = _correspondent;
        }

        /// <summary>
        /// Обработчик событий для кнопки "Принять"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrEmpty(_correspondent.SurnameNameLastname))
                errors.AppendLine("Введите ФИО!");
            if (string.IsNullOrEmpty(_correspondent.Position))
                errors.AppendLine("Введите должность!");
            if (string.IsNullOrEmpty(_correspondent.Division))
                errors.AppendLine("Введите отдел");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_correspondent.Id == 0)
                {
                    UchPracticeDBEntities.GetContext().Correspondents.Add(_correspondent);
                }
                
                UchPracticeDBEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                new MainWindow().Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Кнопка возвращения на предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
