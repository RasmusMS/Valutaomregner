using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows;

namespace Valutaomregner
{
    public class CurrencyConverter
    {

        /// Gets all available currency tags
        public static string[] GetCurrencyTags()
        {
            string[] currencies = { "Vælg" };
            int i = 0;

            string xmlUrl = "https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da";
            XmlTextReader reader = new XmlTextReader(xmlUrl);

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.LocalName == "currency")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "code")
                            {
                                currencies[i++] = reader.Value;
                            }
                        }
                    }
                }
            }

            // Hardcoded currency tags neccesairy to parse the ecb xml's
            return currencies;
        }

        /// <summary>
        /// Get currency exchange rate in euro's 
        /// </summary>
        /*public static float GetCurrencyRateInEuro(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
            if (currency.ToLower() == "eur")
                throw new ArgumentException("Invalid Argument! Cannot get exchange rate from EURO to EURO");

            try
            {
                // Create with currency parameter, a valid RSS url to Nationalbanken
                string xmlUrl = string.Concat("https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da", currency.ToLower());

                // Create & Load New Xml Document
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                // Create XmlNamespaceManager for handling XML namespaces.
                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                // Get list of daily currency exchange rate between selected "currency" and the EURO
                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                // Loop Through all XMLNODES with daily exchange rates
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    try
                    {
                        // Get currency exchange rate with EURO from XMLNODE
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any,
                            ci);

                        return exchangeRate;
                    }
                    catch { return 101; }
                }

                // currency not parsed!! 
                // return default value
                return 0;
            }
            catch
            {
                // currency not parsed!! 
                // return default value
                return 0;
            }
        }


        /// Get The Exchange Rate Between 2 Currencies
        public static float GetExchangeRate(string from, string to, float amount = 1)
        {
            // If currency's are empty abort
            if (from == null || to == null) 
            {
                return 0;
            }

            // Convert Euro to Euro
            if (from.ToLower() == "eur" && to.ToLower() == "eur")
            {
                return amount;
            }

            try
            {
                // First Get the exchange rate of both currencies in euro
                float toRate = GetCurrencyRateInEuro(to);
                float fromRate = GetCurrencyRateInEuro(from);

                // Convert Between Euro to Other Currency
                if (from.ToLower() == "eur")
                {
                    return (amount * toRate);
                }
                else if (to.ToLower() == "eur")
                {
                    return (amount / fromRate);
                }
                else
                {
                    // Calculate non EURO exchange rates From A to B
                    return (fromRate / toRate) * amount;
                }
            }
            catch { return GetCurrencyRateInEuro(to); }
        }*/

        public static float GetRate(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
            if (currency.ToLower() == "dkk")
                throw new ArgumentException("Invalid Argument! Cannot get exchange rate from DKK to DKK");

            try
            {
                // XML URL
                string xmlUrl = "https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da";
                XmlTextReader reader = new XmlTextReader(xmlUrl);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if(reader.LocalName == "currency")
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Value == currency)
                                {
                                    if (reader.Name == "rate")
                                    {
                                        return float.Parse(reader.Value);
                                    }
                                    else
                                    {
                                        return 101;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // currency not parsed!! 
                // return default value
                return 1;
            }

            return 2;
        }

        public static float ConvertValuta(string from, string to, float amount = 1)
        {
            // If input is missing
            if (from == null || to == null)
            {
                return 3;
            }

            // If currency is being converted to same currency
            if (from.ToLower() == to.ToLower())
            {
                return amount;
            }

            try
            {
                float fromRate = 0;
                float toRate = 0;

                if (from != "dkk")
                {
                    fromRate = GetRate(from);
                }

                if (to != "dkk")
                {
                    toRate = GetRate(to);
                }

                if (from == "dkk")
                {
                    return (amount * toRate);
                } 
                else if (to == "dkk")
                {
                    return (amount / fromRate);
                } 
                else
                {
                    return fromRate;
                }
            } catch { return 4; }
        }
    }
}
