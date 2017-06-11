using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CurrencyCalculator.Models
{
    public class DataToConvert
    {
        public static List<string> CharCodes { get; } = new List<string> { "USD", "EUR", "GBP", "CNY", "JPY", "UAH" };
        public static SelectList SelectList { get; } = CreateList();

        [Display(Name = "Валюта")]
        public string Currency { get; set; }

        [Display(Name = "Сумма в рублях")]
        public float SummInRub { get; set; }

        [Display(Name = "Сумма в валюте")]
        public float SummInVal { get; set; }

        static SelectList CreateList()
        {
            var charCodeList = CharCodes.Select(x => new SelectListItem { Value = x, Text = x });
            return new SelectList(charCodeList, "Value", "Text", "USD");
        }
    }
}