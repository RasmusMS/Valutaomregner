using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Valutaomregner
{
    class Program
    {
        static void Main(string[] args)
        {
            string fromCurrency = "EUR";
            string toCurrency = "USD";
            int amount = 1;

            //
            // STEP 1 : Add all availabe currencies to the ComboBox
            //

            // Get all available currency tags
            string[] availableCurrency = CurrencyConverter.GetCurrencyTags();
            foreach (string item in availableCurrency)
            {
                ComboBox1.items.Add(item);
            }

            //
            // STEP 2 : Register which currencies the user want to convert from and to
            //



            //
            // STEP 3 : Calculate and display the currency exchange rate
            //

            // Calls a method to get the exchange rate between 2 currencies
            float exchangeRate = CurrencyConverter.GetExchangeRate(fromCurrency, toCurrency, amount);
            // Print result of currency exchange
            


        }
    }
}
