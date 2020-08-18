using System;
using System.Collections.Generic;
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

namespace Valutaomregner
{
    /// <summary>
    /// Interaction logic for Converter.xaml
    /// </summary>
    public partial class Converter : Page
    {
        public Converter()
        {
            InitializeComponent();
        }

        public string fromCurrency;
        public string toCurrency;
        public int amount = 1;

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            fromCurrency = ((ComboBoxItem)Combo1.SelectedItem).Content.ToString();
            toCurrency = ((ComboBoxItem)Combo2.SelectedItem).Content.ToString();
            amount = int.Parse(s: txt1.Text);
            float exchangeRate = CurrencyConverter.ConvertValuta(fromCurrency, toCurrency, amount);

            lbl1.Content = exchangeRate;
        }
    }
}
