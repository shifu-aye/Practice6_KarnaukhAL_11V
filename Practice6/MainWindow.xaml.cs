using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Practice6.Models;
using Practice6.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace Practice6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Изменить данные выбранного корреспондента из таблицы корреспондентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new AddEditWindow((sender as Button).DataContext as Correspondents).Show();
            Close();
        }

        /// <summary>
        /// Удаление выбранных корреспондентов из таблицы корреспондентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var menuForRemoving = DataBaseDG.SelectedItems.Cast<Correspondents>().ToList();

            if (MessageBox.Show($"Выбранные элементы будут безвозвратно удалены!" +
                                $" Вы точно хотите удалить {menuForRemoving.Count()} элементов?", "Внимание!",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    UchPracticeDBEntities.GetContext().Correspondents.RemoveRange(menuForRemoving);
                    UchPracticeDBEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!", "Успех!", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    DataBaseDG.ItemsSource = UchPracticeDBEntities.GetContext().Correspondents.ToList();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Переход на окно добавления данных в таблицу корреспондентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new AddEditWindow(null).Show();
            Close();
        }

        /// <summary>
        /// Обработчик событий для экспорта в .pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PdfBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var correspondents = UchPracticeDBEntities.GetContext().Correspondents.OrderBy(x => x.Id).ToList();
            var app = new Word.Application();
            Word.Document document = app.Documents.Add();
            int startRowIndex = 1;
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;
            range.Text = "Список корреспондентов.";
            paragraph.set_Style("Заголовок 1");
            range.InsertParagraphAfter();
            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table table = document.Tables.Add(tableRange, correspondents.Count() + 1, 4);
            Word.Range cellRange;
            cellRange = table.Cell(1, 1).Range;
            cellRange.Text = "Уникальный номер";
            cellRange = table.Cell(1, 2).Range;
            cellRange.Text = "Фамилия, имя и отчество";
            cellRange = table.Cell(1, 3).Range;
            cellRange.Text = "Должность";
            cellRange = table.Cell(1, 4).Range;
            cellRange.Text = "Отдел";
            startRowIndex++;
            foreach (var item in correspondents)
            {
                cellRange = table.Cell(startRowIndex, 1).Range;
                cellRange.Text = Convert.ToString(item.Id);
                cellRange = table.Cell(startRowIndex, 2).Range;
                cellRange.Text = (item.SurnameNameLastname);
                cellRange = table.Cell(startRowIndex, 3).Range;
                cellRange.Text = item.Position;
                cellRange = table.Cell(startRowIndex, 4).Range;
                cellRange.Text = item.Division;
                startRowIndex++;
            }
            app.Visible = true;
            document.SaveAs2(@"D:\ExportCorrespondentsDocument.docx");
            document.SaveAs2(@"D:\ExportCorrespondents.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        /// <summary>
        /// Обработчик событий для выхода из учетной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("При согласии, вы покинете учетную запись!" +
                                " Вы уверены, что хотите это сделать?",
                    "Предупреждение", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                new AuthorisationWindow().Show();
                Close();
            }
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
        /// Обновление таблицы с данными корреспондентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                UchPracticeDBEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataBaseDG.ItemsSource = UchPracticeDBEntities.GetContext().Correspondents.ToList();
            }
            
        }
    }
}
