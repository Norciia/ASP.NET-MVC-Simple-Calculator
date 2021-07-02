using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Calculator.Controllers
{
    public class CalculatorController : Controller
    {
        public static string DisplayedNumber { get; set; }
        private static string LastStoredResult { get; set; }

        private static string NumberOne { get; set; }
        private static string NumberTwo { get; set; }


        private static List<string> Operations = new();
        private static string NameOfOperation { get; set; }

        private static int OperationCounter { get; set; } = 0;

        public IActionResult Calculator()
        {
            if (OperationCounter >= 2)
            {
                foreach (var item in Operations)
                {
                    Operation(item);
                    break;
                }

                Operations.RemoveRange(0, 1);
                OperationCounter = 1;
            }

            return View();
        }

        [HttpPost]
        public IActionResult SetOperation(string? OperationName)
        {
            NameOfOperation = OperationName;
            Operations.Add(OperationName);

            OperationCounter += 1;

            return RedirectToAction("Calculator");
        }

        [HttpPost]
        public IActionResult SetNumber(int? Number)
        {
            if (NameOfOperation == null)
            {
                if (NumberOne == null)
                {
                    NumberOne = Number.ToString();
                    DisplayedNumber = NumberOne;
                }
                else
                {
                    NumberOne += Number.ToString();
                    DisplayedNumber = NumberOne;

                }
            }

            if (NameOfOperation != null && NumberOne != null)
            {
                if (NumberTwo != null)
                {
                    NumberTwo += Number.ToString();
                    DisplayedNumber = NumberTwo;
                }
                else
                {
                    NumberTwo = Number.ToString();
                    DisplayedNumber = NumberTwo;
                }

            }

            if (LastStoredResult != null)
            {
                NumberOne = LastStoredResult;
                NumberTwo = Number.ToString();

                LastStoredResult = null;
                DisplayedNumber = NumberTwo.ToString();
            }

            return RedirectToAction("Calculator");
        }

        public void Operation(string Operation)
        {
            switch (Operation)
            {
                case "Division":
                    if (NumberOne != null && NumberTwo != null)
                    {
                        DisplayedNumber = (float.Parse(NumberOne) / float.Parse(NumberTwo)).ToString();

                        LastStoredResult = DisplayedNumber;
                        NumberTwo = null;
                        NumberOne = LastStoredResult;
                    }
                    break;

                case "Multiplication":
                    if (NumberOne != null && NumberTwo != null)
                    {
                        DisplayedNumber = (float.Parse(NumberOne) * float.Parse(NumberTwo)).ToString();

                        LastStoredResult = DisplayedNumber;
                        NumberTwo = null;
                        NumberOne = LastStoredResult;
                    }
                    break;

                case "Substraction":
                    if (NumberOne != null && NumberTwo != null)
                    {
                        DisplayedNumber = (float.Parse(NumberOne) - float.Parse(NumberTwo)).ToString();

                        LastStoredResult = DisplayedNumber;
                        NumberTwo = null;
                        NumberOne = LastStoredResult;
                    }
                    break;

                case "Add":
                    if (NumberOne != null && NumberTwo != null)
                    {
                        DisplayedNumber = (float.Parse(NumberOne) + float.Parse(NumberTwo)).ToString();

                        LastStoredResult = DisplayedNumber;
                        NumberTwo = null;
                        NumberOne = LastStoredResult;
                    }
                    break;

                default:
                    break;
            }
        }


        public IActionResult Submit()
        {
            Operation(NameOfOperation);

            OperationCounter = 0;
            Operations.Clear();

            return RedirectToAction("Calculator");
        }

        public IActionResult ClearAll()
        {
            OperationCounter = 0;

            NumberOne = null;
            NumberTwo = null;
            DisplayedNumber = null;
            LastStoredResult = null;

            Operations.Clear();
            NameOfOperation = null;

            return RedirectToAction("Calculator");
        }

        public IActionResult PlusMinus()
        {
            if (NumberTwo == null)
            {
                if (NumberOne != null)
                {
                    bool b = NumberOne.Contains("-");
                    if (b == true)
                    {
                        NumberOne = NumberOne.Replace('-', ' ').Trim();

                        DisplayedNumber = NumberOne;
                    }

                    else
                    {
                        NumberOne = "-" + NumberOne;
                        DisplayedNumber = NumberOne;
                    }
                }
            }

            else
            {
                bool b = NumberTwo.Contains("-");
                if (b == true)
                {
                    NumberTwo = NumberTwo.Replace('-', ' ').Trim();
                    DisplayedNumber = NumberTwo;
                }

                else
                {
                    NumberTwo = "-" + NumberTwo;
                    DisplayedNumber = NumberTwo;
                }
            }

            return RedirectToAction("Calculator");
        }

        public IActionResult Percentage()
        {
            if (NumberTwo == null)
            {
                if (NumberOne != null)
                {
                    NumberOne = (float.Parse(NumberOne) / 100).ToString();
                    DisplayedNumber = NumberOne;
                }
            }

            else
            {
                NumberTwo = (float.Parse(NumberTwo) / 100).ToString();
                DisplayedNumber = NumberTwo;
            }

            return RedirectToAction("Calculator");
        }

        public IActionResult ToFloat()
        {
            if (NumberTwo == null)
            {
                if (NumberOne != null)
                {
                    if (!NumberOne.Contains(","))
                    {
                        NumberOne = NumberOne + ",";
                        DisplayedNumber = NumberOne;
                    }
                }
            }

            else
            {
                if (!NumberTwo.Contains(","))
                {
                    NumberTwo = NumberTwo + ",";
                    DisplayedNumber = NumberTwo;
                }
            }
            return RedirectToAction("Calculator");
        }
    }
}