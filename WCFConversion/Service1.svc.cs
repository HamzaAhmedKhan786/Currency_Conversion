using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFConversion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        static String[] units = { "zero",  "one",   "two",  "three", "four",   "five",
            "six", "seven", "eight", "nine", "ten",
            "eleven", "twelve", "thirteen","fourteen", "fifteen",
            "sixteen", "seventeen", "eighteen", "nineteen" };

        static String[] tens = { "twenty", "thirty", "forty", "fifty", "sixty",
            "seventy", "eighty", "ninety" };

        static String[] hundreds = { "", "thousand", "million", "billion", "trillion",
            "quadrillion" };

        //convertCurrencyToStringOneToNinetyNine : This method convert currency numbers in words.
        //Can take inputs from 00.00$ to 99.99$, in return convert into words form.
        string conversion1;
        string conversion2;
        string amount_in_Words = "";
        string amount_in_Words_after_Decimal = "";
        string all_amount_in_Words = "";
        string all_amount_in_WordsafterDecimal = "";

        public Tuple<int, int> SplittingMethod(string Invalue)
        {
            int breakAmount1;
            int breakAmount2;

            if (Invalue.Contains(".") == true)
            {
                string[] test = Invalue.Split('.').ToArray();
                string split1 = Convert.ToString(test[1]);
                if (split1.Length == 2)
                {
                    split1 = Convert.ToString(split1[0]) + Convert.ToString(split1[1]);
                    string split2 = Convert.ToString(test[0]);
                    breakAmount1 = Convert.ToInt32(split2);
                    breakAmount2 = Convert.ToInt32(split1);

                    return Tuple.Create(breakAmount1, breakAmount2);
                }

                else
                {
                    return Tuple.Create(0, 0); ;
                }


            }
            else
            {
                breakAmount1 = Convert.ToInt32(Invalue);
                return Tuple.Create(breakAmount1, 0);
            }
        }
        public string ConvertCurrency(string value)
        {

            double parsedValue = double.Parse(value);
            string output = "";
            if (parsedValue < 100)
            {
                output = ConvertCurrencyToStringOneToNinetyNine(value);
                return output;
            }
            else if (parsedValue > 100 && parsedValue < 2000)
            {
                output = ConvertCurrencyToStringHundredToNineteenHundredNinetyNine(value);
                return output;
            }
            else if (parsedValue > 1999 && parsedValue < 999999999.99)
            {
                output = ConvertCurrencyToStringAllAboveNineteenHundredNinetyNine(value);
                return output;
            }
            else
            {
                output = "Entered value should not more than million values!";
            }
            return output;
        }
        //23.00
        //1.00
        //1.0023
        public string ConvertCurrencyToStringOneToNinetyNine(string value)
        {


            int breakAmount1 = SplittingMethod(value).Item1;
            int breakAmount2 = SplittingMethod(value).Item2;


            if (breakAmount1 == 0 && breakAmount2 == 0)

            {
                string Massage = "Invalid Value! After decimal there should 2 Values. ";
                return Massage;
            }
            else
            {
                if (breakAmount1 < 20 && breakAmount2 < 20)
                {
                    if (breakAmount1 == 1)
                    {
                        conversion1 = units[breakAmount1];
                        amount_in_Words = conversion1 + " Dollar ";
                        return amount_in_Words;
                    }

                    if (breakAmount2 == 01)
                    {
                        conversion1 = units[breakAmount1];
                        conversion2 = units[breakAmount2];
                        amount_in_Words = conversion1 + " Dollars " + conversion2 + " Cent Only";
                        return amount_in_Words;
                    }
                    conversion1 = units[breakAmount1];
                    conversion2 = units[breakAmount2];
                    amount_in_Words = conversion1 + " Dollars " + conversion2 + " Cents Only";
                    return amount_in_Words;
                }
                else if (breakAmount1 < 20 && breakAmount2 > 20)
                {
                    for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                    {
                        conversion1 = units[breakAmount1];
                        conversion2 = tens[valuesCount];
                        int doubleValue = 20 + 10 * valuesCount;
                        if (doubleValue + 10 > breakAmount2)
                        {
                            if ((breakAmount2 % 10) != 0)
                            {
                                amount_in_Words = conversion1 + " Dollars and " + conversion2 + "-" + units[(breakAmount2 % 10)] + " Cents Only";
                                return amount_in_Words;
                            }
                            else
                            {
                                return conversion1 + " Dollars and " + conversion2 + " Cents Only";
                            }
                        }
                    }
                }
                else if (breakAmount1 > 20 && breakAmount2 == 0)
                {
                    for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                    {
                        amount_in_Words = tens[valuesCount];
                        int doubleValue = 20 + 10 * valuesCount;
                        if (doubleValue + 10 > breakAmount1)
                        {
                            if ((breakAmount1 % 10) != 0)
                                all_amount_in_Words = amount_in_Words + "-" + units[(breakAmount1 % 10)] + " Dollars";
                            return all_amount_in_Words;
                        }

                    }
                }
                else if (breakAmount1 > 20)
                {
                    for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                    {
                        amount_in_Words = tens[valuesCount];

                        int doubleValue = 20 + 10 * valuesCount;
                        if (doubleValue + 10 > breakAmount1)
                        {

                            if ((breakAmount1 % 10) != 0)
                                all_amount_in_Words = amount_in_Words + "-" + units[(breakAmount1 % 10)] + " Dollars  and ";


                            if (breakAmount2 > 20)
                            {
                                for (int valuesCount1 = 0; valuesCount1 < tens.Length; valuesCount1++)
                                {

                                    amount_in_Words_after_Decimal = tens[valuesCount1];
                                    int doubleValue2 = 20 + 10 * valuesCount1;
                                    if (doubleValue2 + 10 > breakAmount2)
                                    {
                                        if ((breakAmount2 % 10) != 0)
                                            all_amount_in_WordsafterDecimal = amount_in_Words_after_Decimal + "-" + units[(breakAmount2 % 10)];
                                        if ((breakAmount2 % 10) == 0)
                                            all_amount_in_WordsafterDecimal = amount_in_Words_after_Decimal;
                                        return all_amount_in_Words + all_amount_in_WordsafterDecimal + " Cents Only";
                                    }
                                }
                            }
                            if (breakAmount2 < 20)
                            {
                                for (int valuesCount3 = 0; valuesCount3 < tens.Length; valuesCount3++)
                                {
                                    conversion1 = units[breakAmount2];

                                    int doubleValue3 = 20 + 10 * valuesCount3;
                                    if (doubleValue3 + 10 > breakAmount2)
                                    {
                                        if ((breakAmount2 % 10) != 0)
                                        {

                                            all_amount_in_WordsafterDecimal = conversion1;
                                            return all_amount_in_Words + all_amount_in_WordsafterDecimal + " Cents Only";
                                        }
                                        else
                                        {
                                            return conversion2;
                                        }
                                    }
                                }
                            }
                        }
                    }




                }
                else
                {
                    throw new Exception("Not a valid value");
                }


                // amount_in_Words = SmallerMethod(value);

            }
            return amount_in_Words;
        }

        //convertCurrencyToStringHundredToNinetyNineThousandNineHundredNinetyNine : This method convert
        //currency numbers in words.
        //Can take inputs from 100.00$ to 999.99$, in return convert into words form.
        public string ConvertCurrencyToStringHundredToNineteenHundredNinetyNine(string value1)
        {


            int breakAmount1 = SplittingMethod(value1).Item1;
            int breakAmount2 = SplittingMethod(value1).Item2;

            int remainder = (breakAmount1 / 100);
            int modulus = (breakAmount1 % 100);
            if (remainder > 0)
            {
                all_amount_in_Words = units[remainder] + " hundred";
                if (modulus > 0)
                {
                    all_amount_in_Words = all_amount_in_Words + " ";
                }
            }
            if (modulus > 0 && breakAmount2 > 0)
            {
                string modPlusBreakAmount2 = Convert.ToString(modulus) + "." + Convert.ToString(breakAmount2);

                all_amount_in_Words = all_amount_in_Words + ConvertCurrencyToStringOneToNinetyNine(modPlusBreakAmount2);
            }
            else if (modulus > 0)
            {
                string modPlusBreakAmount2 = Convert.ToString(modulus);

                all_amount_in_Words = all_amount_in_Words + ConvertCurrencyToStringOneToNinetyNine(modPlusBreakAmount2);
            }

            return all_amount_in_Words;
        }
        public string ConvertCurrencyToStringAllAboveNineteenHundredNinetyNine(string value)
        {
            string returnValue = "";
            int breakAmount1 = SplittingMethod(value).Item1;
            int breakAmount2 = SplittingMethod(value).Item2;


            if (breakAmount1 < 100)
            {
                return SmallerMethod(value);
            }
            if (breakAmount1 < 2000)
            {
                return BiggerMethod(value);
            }

            for (int v = 0; v < hundreds.Length; v++)
            {

                int val1;
                int modulus;
                int val2 = v - 1;
                val1 = Convert.ToInt32((Math.Pow(1000, v)));
                if (val1 > breakAmount1 && breakAmount2 == 0)
                {
                    modulus = Convert.ToInt32((Math.Pow(1000, val2)));
                    int l = (breakAmount1 / modulus);
                    int r = (breakAmount1 - (l * modulus));

                    returnValue = BiggerMethod(Convert.ToString(l)) + " " + hundreds[val2];
                    if (r > 0)
                    {
                        returnValue = returnValue + ", " + ConvertCurrencyToStringAllAboveNineteenHundredNinetyNine(Convert.ToString(r));
                    }
                    return returnValue;
                }
                else if (val1 > breakAmount1 && breakAmount2 != 0)
                {
                    modulus = Convert.ToInt32((Math.Pow(1000, val2)));
                    int l = (breakAmount1 / modulus);
                    int r = (breakAmount1 - (l * modulus));

                    returnValue = BiggerMethod(Convert.ToString(l)) + " " + hundreds[val2];
                    if (r > 0)
                    {
                        returnValue = returnValue + ", " + ConvertCurrencyToStringAllAboveNineteenHundredNinetyNine(Convert.ToString(r) + "." + Convert.ToString(breakAmount2));
                    }
                    return returnValue;
                }

            }

            return returnValue;
        }
        public string BiggerMethod(string retValue)
        {

            int breakAmount1 = SplittingMethod(retValue).Item1;
            int breakAmount2 = SplittingMethod(retValue).Item2;

            int remainder = (breakAmount1 / 100);
            int modulus = (breakAmount1 % 100);
            if (remainder > 0)
            {
                all_amount_in_Words = units[remainder] + " hundred";
                if (modulus > 0)
                {
                    all_amount_in_Words = all_amount_in_Words + " ";
                }
            }
            if (modulus > 0 && breakAmount2 > 0)
            {
                string modPlusBreakAmount2 = Convert.ToString(modulus) + "." + Convert.ToString(breakAmount2);

                all_amount_in_Words = all_amount_in_Words + SmallerMethod(modPlusBreakAmount2);
            }
            else if (modulus > 0)
            {
                string modPlusBreakAmount2 = Convert.ToString(modulus);

                all_amount_in_Words = all_amount_in_Words + SmallerMethod(modPlusBreakAmount2);
            }


            return all_amount_in_Words;
        }
        public string SmallerMethod(string retValue2)
        {
            int breakAmount1 = SplittingMethod(retValue2).Item1;
            int breakAmount2 = SplittingMethod(retValue2).Item2;

            if (breakAmount1 < 20 && breakAmount2 < 20)
            {

                conversion1 = units[breakAmount1];
                conversion2 = units[breakAmount2];
                if (conversion2 == "zero")
                {
                    amount_in_Words = conversion1;
                }
                else
                {
                    amount_in_Words = conversion1 + conversion2;
                }

                return amount_in_Words;
            }
            else if (breakAmount1 < 20 && breakAmount2 >= 20)
            {
                for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                {
                    conversion1 = units[breakAmount1];
                    conversion2 = tens[valuesCount];
                    int doubleValue = 20 + 10 * valuesCount;
                    if (doubleValue + 10 > breakAmount2)
                    {
                        if ((breakAmount2 % 10) != 0)
                        {
                            amount_in_Words = conversion1 + " " + conversion2 + "-" + units[(breakAmount2 % 10)] +" Cents";
                            return amount_in_Words ;
                        }
                        else
                        {
                            return conversion2;
                        }
                    }
                }
            }
            else if (breakAmount1 > 20 && breakAmount2 == 0)
            {
                for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                {
                    amount_in_Words = tens[valuesCount];
                    int doubleValue = 20 + 10 * valuesCount;
                    if (doubleValue + 10 > breakAmount1)
                    {
                        if ((breakAmount1 % 10) != 0)
                            all_amount_in_Words = amount_in_Words + "-" + units[(breakAmount1 % 10)];
                        return all_amount_in_Words +" Dollars";
                    }

                }
            }
            else if (breakAmount1 > 20 && breakAmount2 >= 20)
            {
                for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                {
                    amount_in_Words = tens[valuesCount];

                    int doubleValue = 20 + 10 * valuesCount;
                    if (doubleValue + 10 > breakAmount1)
                    {

                        if ((breakAmount1 % 10) != 0)
                            all_amount_in_Words = amount_in_Words + "-" + units[(breakAmount1 % 10)];


                        for (int valuesCount1 = 0; valuesCount1 < tens.Length; valuesCount1++)
                        {

                            amount_in_Words_after_Decimal = tens[valuesCount1];
                            int doubleValue2 = 20 + 10 * valuesCount1;
                            if (doubleValue2 + 10 > breakAmount2)
                            {
                                if ((breakAmount2 % 10) != 0)
                                {
                                    all_amount_in_WordsafterDecimal = amount_in_Words_after_Decimal + "-" + units[(breakAmount2 % 10)];
                                    return all_amount_in_Words + " Dollars and " + all_amount_in_WordsafterDecimal + " Cents Only";
                                }
                                if ((breakAmount2 % 10) == 0)
                                {
                                    all_amount_in_WordsafterDecimal = amount_in_Words_after_Decimal;

                                    return all_amount_in_Words + " Dollars and " + all_amount_in_WordsafterDecimal + " Cents Only";
                                }

                            }
                        }
                    }

                }

            }

            else if (breakAmount1 >= 20 && breakAmount2 < 20)
            {
                for (int valuesCount = 0; valuesCount < tens.Length; valuesCount++)
                {
                    amount_in_Words = tens[valuesCount];

                    int doubleValue = 20 + 10 * valuesCount;
                    if (doubleValue + 10 > breakAmount1)
                    {

                        if ((breakAmount1 % 10) != 0)
                            all_amount_in_Words = amount_in_Words + "-" + units[(breakAmount1 % 10)];

                        for (int valuesCount3 = 0; valuesCount3 < tens.Length; valuesCount3++)
                        {
                            conversion1 = units[breakAmount2];
                            conversion2 = tens[valuesCount];
                            int doubleValue3 = 20 + 10 * valuesCount3;
                            if (doubleValue3 + 10 > breakAmount2)
                            {
                                if ((breakAmount2 % 10) != 0)
                                {

                                    all_amount_in_WordsafterDecimal = conversion1;
                                    return all_amount_in_Words + " Dollars and " + all_amount_in_WordsafterDecimal + " Cents Only";
                                }
                                else
                                {
                                    return conversion2;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                throw new Exception("Not a valid value");
            }
            return amount_in_Words;
        }
    }
}
