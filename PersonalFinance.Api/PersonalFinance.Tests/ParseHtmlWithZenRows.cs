using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using AngleSharp;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;

namespace PersonalFinance.Tests
{
    internal class ParseHtmlWithZenRows
    {
        public static List<string> ParseHtmlWithZenRowsMethod()
        {
            var url = "https://api.zenrows.com/v1/?apikey=7f3c4d26c5e8255284727f605759bf049b10ff97&url=https%3A%2F%2Fwww.iban.ru%2Fcurrency-codes";
            var request = WebRequest.Create(url);
            request.Method = "GET";
            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();
            return ParseHtml(webStream);
        }
        private static List<string> ParseHtml(Stream html)
        {
            var doc = new HtmlDocument();
            doc.Load(html);

            HtmlNodeCollection currency = doc.DocumentNode.SelectNodes("//td");
            List<string> currencyList = new List<string>();
            string pattern = @"[A-Z]{3}";

            for (int i = 0; i < currency.Count; i++)
            {
                if (Regex.IsMatch(currency[i].InnerText, pattern) && currency[i].InnerText.Length == 3)
                {
                    currencyList.Add(currency[i].InnerText);
                }
                
            }
            return currencyList;
        }
    }
}
