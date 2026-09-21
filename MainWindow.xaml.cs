using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultilingualCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentLanguage = "en";
        public MainWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => UpdateLanguage();
        }
    
    private void LanguageComboBox_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (TitleText == null || FirstNumberText == null)
                return;

            switch (LanguageComboBox.SelectedIndex)
            {
                case 0:
                    currentLanguage = "en";
                    break;

                case 1:
                    currentLanguage = "kk";
                    break;

                case 2:
                    currentLanguage = "ru";
                    break;
            }

            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            if (currentLanguage == "en")
            {
                Title = "Multilingual Calculator";

                TitleText.Text = "🧮 Calculator";
                FirstNumberText.Text = "First number";
                SecondNumberText.Text = "Second number";
                ResultTitleText.Text = "Result";
                ClearButton.Content = "Clear";
            }
            else if (currentLanguage == "kk")
            {
                Title = "Көптілді калькулятор";

                TitleText.Text = "🧮 Калькулятор";
                FirstNumberText.Text = "Бірінші сан";
                SecondNumberText.Text = "Екінші сан";
                ResultTitleText.Text = "Нәтиже";
                ClearButton.Content = "Тазалау";
            }
            else
            {
                Title = "Многоязычный калькулятор";

                TitleText.Text = "🧮 Калькулятор";
                FirstNumberText.Text = "Первое число";
                SecondNumberText.Text = "Второе число";
                ResultTitleText.Text = "Результат";
                ClearButton.Content = "Очистить";
            }
        }

        private void OperationButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            bool firstNumberIsValid =
                TryParseNumber(FirstNumberTextBox.Text, out double firstNumber);
            bool secondNumberIsValid =
                TryParseNumber(SecondNumberTextBox.Text, out double secondNumber);

            if (!firstNumberIsValid || !secondNumberIsValid)
            {
                ShowInvalidNumberMessage();
                return;
            }

            Button clickedButton = (Button)sender;

            string operation =
                clickedButton.Content.ToString() ?? "";

            double result;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;


                case "-":
                    result = firstNumber - secondNumber;
                    break;


                case "×":
                    result = firstNumber * secondNumber;
                    break;

                case "÷":

                    if (secondNumber == 0)
                    {
                        ShowDivisionByZeroMessage();
                        return;
                    }

                    result = firstNumber / secondNumber;
                    break;


                default:
                    return;
            }

            ResultText.Text = result.ToString("G15");
        }

        private bool TryParseNumber(
            string text,
            out double number)
        {

            string normalizedText =
                text.Trim().Replace(',', '.');


            return double.TryParse(
                normalizedText,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out number);
        }

        private void ClearButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            // Очищаем оба поля.
            FirstNumberTextBox.Clear();
            SecondNumberTextBox.Clear();

            // Возвращаем результат к нулю.
            ResultText.Text = "0";

            // Переводим курсор обратно
            // в первое поле.
            FirstNumberTextBox.Focus();
        }

        private void ShowInvalidNumberMessage()
        {
            if (currentLanguage == "en")
            {
                MessageBox.Show(
                    "Please enter valid numbers.",
                    "Input error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else if (currentLanguage == "kk")
            {
                MessageBox.Show(
                    "Дұрыс сандарды енгізіңіз.",
                    "Енгізу қатесі",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(
                    "Введите корректные числа.",
                    "Ошибка ввода",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void ShowDivisionByZeroMessage()
        {
            if (currentLanguage == "en")
            {
                MessageBox.Show(
                    "Division by zero is not allowed.",
                    "Calculation error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (currentLanguage == "kk")
            {
                MessageBox.Show(
                    "Нөлге бөлуге болмайды.",
                    "Есептеу қатесі",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(
                    "Деление на ноль невозможно.",
                    "Ошибка вычисления",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}