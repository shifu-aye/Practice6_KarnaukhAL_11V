using System.Linq;
using System.Text;
using System.Windows;
using Practice6.Models;

namespace Practice6.Windows
{
    public partial class AuthorisationWindow : Window
    {
        public AuthorisationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик событий кнопки "Войти"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            var stringBuilder = new StringBuilder();
            var name = NameTb.Text.Trim();
            var position = PositionTb.Text.Trim();
            var division = DivisionTb.Text.Trim();
            if (string.IsNullOrEmpty(name))
                stringBuilder.AppendLine("Введите ФИО!");
            else if (string.IsNullOrWhiteSpace(name))
                stringBuilder.AppendLine("Проверьте правильность поля ФИО! Возможен пробел");
            if (string.IsNullOrEmpty(position))
                stringBuilder.AppendLine("Введите должность!");
            else if (string.IsNullOrWhiteSpace(position))
                stringBuilder.AppendLine("Проверьте правильность поля Должность! Возможен пробел");
            if (string.IsNullOrEmpty(division))
                stringBuilder.AppendLine("Введите отдел");
            else if (string.IsNullOrWhiteSpace(division))
                stringBuilder.AppendLine("Проверьте правильность поля Отдел! Возможен пробел");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Authorisation(name, position, division);
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <param name="name">Имя корреспондента</param>
        /// <param name="position">Должность корреспондента</param>
        /// <param name="division">Отдел, в котором работает корреспондент</param>
        private void Authorisation(string name, string position, string division)
        {
            var correspondent = UchPracticeDBEntities.GetContext().Correspondents.FirstOrDefault(
                x => x.SurnameNameLastname == name &&
                     x.Position == position &&
                     x.Division == division);

            if (correspondent != null)
            {
                MessageBox.Show($"Авторизация успешна!, Вы вошли как {correspondent.Position}", "Успех!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow window = new MainWindow();
                window.Show();
                Close();
            }

            else
                MessageBox.Show("Где-то была допущена ошибка :( Проверьте правильность введенных данных," +
                                " а также обратите внимание на регистр." +
                                " Программа к этому чувствительна", "Ошибка",
                                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
