using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CurrencyCalculator.Models;

namespace CurrencyCalculator.Controllers
{
    public class HomeController : Controller
    {
        float GetCourse(string currency)
        {
            WebRequest request = WebRequest.Create("http://www.cbr.ru/scripts/XML_daily.asp");
            WebResponse response = request.GetResponse();
            XDocument xDoc = XDocument.Load(response.GetResponseStream());
            var currencyNode = (from node in xDoc.Element("ValCurs").Elements("Valute")
                                where node.Element("CharCode").Value == currency
                                select node).First();
            int nominal = int.Parse(currencyNode.Element("Nominal").Value);
            float value = float.Parse(currencyNode.Element("Value").Value);
            return value / nominal;
        }

        public ActionResult Index()
        {
            ViewBag.currencyList = DataToConvert.SelectList;
            return View();
        }

        public ActionResult RubToVal(DataToConvert dataInput)
        {
            try
            {
                ViewBag.result = (dataInput.SummInRub / GetCourse(dataInput.Currency)).ToString();
                ViewBag.currencyList = DataToConvert.SelectList;
                ViewBag.currency = dataInput.Currency;
                return View("Result", dataInput);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ValToRub(DataToConvert dataInput)
        {
            try
            {
                ViewBag.result = (dataInput.SummInVal * GetCourse(dataInput.Currency)).ToString();
                ViewBag.currencyList = DataToConvert.SelectList;
                ViewBag.currency = "RUB";
                return View("Result", dataInput);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}