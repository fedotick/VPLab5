using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace VPLab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ButtonCircle.Click += ButtonCircle_Click;
            ButtonRectangle.Click += ButtonRectangle_Click;
            ButtonBall.Click += ButtonBall_Click;
            ButtonParallelepiped.Click += ButtonParallelepiped_Click;
            ButtonTriangle.Click += ButtonTriangle_Click;
            ButtonParallelogram.Click += ButtonParallelogram_Click;

            TextBox1.PreviewTextInput += TextBox1_PreviewTextInput;
            TextBox2.PreviewTextInput += TextBox2_PreviewTextInput;
            TextBox3.PreviewTextInput += TextBox3_PreviewTextInput;

            ButtonCalculate.Click += ButtonCalculate_Click;

            VisibilityOfElements(Visibility.Hidden);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;

            string currentDirectory = Directory.GetCurrentDirectory();

            string relativePath = Path.Combine("..", "..", "Resources", "logo-white.ico");

            string fullPath = Path.GetFullPath(Path.Combine(currentDirectory, relativePath));

            Icon = new BitmapImage(new Uri(fullPath, UriKind.RelativeOrAbsolute));
        }

        private void VisibilityOfElements(Visibility visibility)
        {
            VisibilityOfElements(visibility, new int[] { 1, 2, 3, 4, 5, 6 });
        }

        private void VisibilityOfElements(Visibility visibility, int[] elemets)
        {
            foreach (int item in elemets)
            {
                VisibilityOfElements(visibility, item);
            }
        }

        private void VisibilityOfElements(Visibility visibility, int number)
        {
            switch (number)
            {
                case 1:
                    LabelFigure.Visibility = visibility;
                    break;
                case 2:
                    TextBoxFigure.Visibility = visibility;
                    break;
                case 3:
                    TextBox1.Visibility = visibility;
                    break;
                case 4:
                    TextBox2.Visibility = visibility;
                    break;
                case 5:
                    TextBox3.Visibility = visibility;
                    break;
                case 6:
                    ButtonCalculate.Visibility = visibility;
                    break;
            }
        }

        private void Cleaning()
        {
            TextBoxFigure.Text = string.Empty;
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
        }

        private void Quashen(string figure)
        {
            Quashen(figure, new int[] { 1, 2, 3, 4, 5, 6 });
        }

        private void Quashen(string figure, int[] elements)
        {
            Cleaning();
            VisibilityOfElements(Visibility.Hidden);
            LabelFigure.Content = "Figure: " + figure;
            VisibilityOfElements(Visibility.Visible, elements);

            MessageBoxResult messageBoxResult = MessageBox.Show("Would you like to enter a name for the shape?", "", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                VisibilityOfElements(Visibility.Visible, new int[] { 2 });
            }
        }

        private void ButtonCircle_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Radius";
            Label2.Content = "";
            Label3.Content = "";

            Quashen("Circle", new int[] { 1, 3, 6 });
        }

        private void ButtonRectangle_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Width";
            Label2.Content = "Height";
            Label3.Content = "";

            Quashen("Rectangle", new int[] { 1, 3, 4, 6 });
        }

        private void ButtonBall_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Radius";
            Label2.Content = "";
            Label3.Content = "";

            Quashen("Ball", new int[] { 1, 3, 6 });
        }

        private void ButtonParallelepiped_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Width";
            Label2.Content = "Height";
            Label3.Content = "Lenght";

            Quashen("Parallelepiped", new int[] { 1, 3, 4, 5, 6 });
        }

        private void ButtonTriangle_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Side a";
            Label2.Content = "Side b";
            Label3.Content = "Side c";

            Quashen("Triangle", new int[] { 1, 3, 4, 5, 6 });
        }

        private void ButtonParallelogram_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Side a";
            Label2.Content = "Side b";
            Label3.Content = "Angle";

            Quashen("Parallelogram", new int[] { 1, 3, 4, 5, 6 });
        }

        private void Validation(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!char.IsControl(e.Text[0]) && !char.IsDigit(e.Text[0]) && e.Text != ",")
                e.Handled = true;

            if ((e.Text == "," && (textBox.Text.IndexOf(',') > -1 || textBox.SelectionStart == 0 || textBox.Text.StartsWith("-") && textBox.Text.Length == 1)))
                e.Handled = true;

            if (e.Text == "-" && textBox.Text.Length > 0 && textBox.SelectionStart != 0)
                e.Handled = true;
        }

        private void TextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Validation(sender, e);
        }

        private void TextBox2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Validation(sender, e);
        }

        private void TextBox3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Validation(sender, e);
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            string labelFigureContent = LabelFigure.Content.ToString();
            string textBoxFigureText = TextBoxFigure.Text;
            double a, b, c;
            double perimeter, area; 

            if (textBoxFigureText.Equals(""))
            {
                textBoxFigureText = labelFigureContent.Replace("Figure: ", "");
            }

            if (labelFigureContent.Contains("Circle"))
            {
                if (TextBox1.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);

                perimeter = 2 * 3.1415 * a;
                area = 3.1415 * a * a;

                MessageBox.Show($"Perimeter: {perimeter}\r\nArea: {area}", textBoxFigureText);
            }
            else if (labelFigureContent.Contains("Rectangle"))
            {
                if (TextBox1.Text.Equals("") || TextBox2.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);
                b = Convert.ToDouble(TextBox2.Text);

                perimeter = 2 * (a + b);
                area = a * b;

                MessageBox.Show($"Perimeter: {perimeter}\r\nArea: {area}", textBoxFigureText);
            }
            else if (labelFigureContent.Contains("Ball"))
            {
                if (TextBox1.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);

                area = 4 * 3.1415 * a * a;

                MessageBox.Show($"Area: {area}", textBoxFigureText);
            }
            else if (labelFigureContent.Contains("Parallelepiped"))
            {
                if (TextBox1.Text.Equals("") || TextBox2.Text.Equals("") || TextBox3.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);
                b = Convert.ToDouble(TextBox2.Text);
                c = Convert.ToDouble(TextBox3.Text);

                area = 2 * (a * b + b * c + a * c);

                MessageBox.Show($"Area: {area}", textBoxFigureText);
            }
            else if (labelFigureContent.Contains("Triangle"))
            {
                if (TextBox1.Text.Equals("") || TextBox2.Text.Equals("") || TextBox3.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);
                b = Convert.ToDouble(TextBox2.Text);
                c = Convert.ToDouble(TextBox3.Text);

                if (a + b <= c || a + c <= b || b + c <= a)
                {
                    MessageBox.Show("Triangle doesn't exist!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                perimeter = a + b + c;
                double p = (a + b + c) / 2;
                area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

                MessageBox.Show($"Perimeter: {perimeter}\r\nArea: {area}", textBoxFigureText);
            }
            else if (labelFigureContent.Contains("Parallelogram"))
            {
                if (TextBox1.Text.Equals("") || TextBox2.Text.Equals("") || TextBox3.Text.Equals(""))
                {
                    MessageBox.Show("Enter values!", "Attention!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                a = Convert.ToDouble(TextBox1.Text);
                b = Convert.ToDouble(TextBox2.Text);
                c = Convert.ToDouble(TextBox3.Text);

                if (c >= 180)
                {
                    MessageBox.Show("Wrong angle!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                perimeter = 2 * (a + b);
                double angleInRadians = c * Math.PI / 180;
                area = a * b * Math.Sin(angleInRadians);

                MessageBox.Show($"Perimeter: {perimeter}\r\nArea: {area}", textBoxFigureText);
            }
        }
    }
}
