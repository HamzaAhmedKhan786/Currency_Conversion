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
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace WPFConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            using (ServiceHost host = new ServiceHost(typeof(WPFConversion.ConversionController)))

            {
                host.Open();

            }
            InitializeComponent();
        }
        public string Input()
        {

            WCFServiceReference.Service1Client client = new WCFServiceReference.Service1Client();

            client.Open();


            string output = "";
            string enteredValue = textBox.Text;
            string pattern = @"^\d*\.\d{2}$";
            string param1 = "it's a wrong type,please input a decimal type number" +
                            " Please enter number with decimal format for example" +
                            "23.00, 23.34";
            Match m = Regex.Match(enteredValue, pattern);

            if (!m.Success)
            {
                this.textArea.Text = param1;
                return output;
            }

            if (m.Success)
            {
                string value = enteredValue;
                output = client.ConvertCurrency(value);
                textArea.Text = output;

            }
        
           // double parsedValue = double.Parse(enteredValue);

            //if (parsedValue < 100)
            //{
            //    output = client.ConvertCurrencyToStringOneToNinetyNine(enteredValue);
            //    textArea.Text = output;

            //}
            //else if (parsedValue > 100 && parsedValue < 2000)
            //{

            //    output = client.ConvertCurrencyToStringHundredToNineteenHundredNinetyNine(enteredValue);
            //    textArea.Text = output;
            //}
            //else if (parsedValue > 1999 && parsedValue < 999999999.99)
            //{
            //    output = client.ConvertCurrencyToStringAllAboveNineteenHundredNinetyNine(enteredValue);
            //    textArea.Text = output;
            //}
            else
            {
                output = "Entered value should not more than million values!";
                textArea.Text = output;

            }

            return output;
            //client.Close();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {

            Input();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
