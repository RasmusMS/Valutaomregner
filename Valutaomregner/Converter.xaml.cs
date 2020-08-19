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
using System.Xml;

namespace Valutaomregner
{
    /// <summary>
    /// Interaction logic for Converter.xaml
    /// </summary>
    public partial class Converter : Page
    {
        public Converter()
        {
            //populateCombobox();
            InitializeComponent();
        }

        private void populateCombobox()
        {
            //ObservableCollection<string> list = new ObservableCollection<string>();
            ComboBoxItem item = new ComboBoxItem();

            XmlDocument doc = new XmlDocument();
            doc.Load("https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da");
            XmlNodeList currencyList = doc.SelectNodes("exchangerates/dailyrates/currency/@code");
            foreach (XmlNode currency in currencyList)
            {
                //list.Add(currency.Attributes["code"].Value.ToString());
                item.Content = currency.Value.ToString();

                ((ComboBox)Combo1).Items.Add(item);
                ((ComboBox)Combo2).Items.Add(item);
            }

            //Combo1.SelectedIndex = 0;
            //Combo2.SelectedIndex = 1;
            //Combo1.ItemsSource = list;
        }

        public string fromCurrency;
        public string toCurrency;
        public int amount = 1;

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            fromCurrency = ((ComboBoxItem)Combo1.SelectedItem).Content.ToString();
            toCurrency = ((ComboBoxItem)Combo2.SelectedItem).Content.ToString();
            try
            {
                amount = int.Parse(s: txt1.Text);
            } catch
            {
                amount = 1;
            }
            float exchangeRate = CurrencyConverter.ConvertValuta(fromCurrency, toCurrency, amount);

            lbl1.Content = exchangeRate;
        }
    }
}
