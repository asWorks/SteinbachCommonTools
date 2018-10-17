using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace DAL.Tools
{
    public class ParseFormula
    {


        public static decimal GetCalculation(decimal Start, int Theme)
        {
           decimal buffer = Start;
            using (var db = new SteinbachEntities())
            {

                var fo = db.SI_FormelnPositionen.Where(n => n.id_thema == Theme).OrderBy(n=>n.idx);
                if (fo != null)
                {

                    foreach (var item in fo)
                    {
                        switch (item.Operator.ToLower())
                        {
                            case "+":
                                {
                                   // buffer = ParseFormula.CalcStep(buffer,(decimal)item.Value,(S,R)=>S+R);
                                    buffer += (decimal)item.Value;
                                    break;
                                }

                            case "-":
                                {
                                    //buffer = ParseFormula.CalcStep(buffer, (decimal)item.Value, (S, R) => S - R);
                                    buffer -= (decimal)item.Value;
                                    break;
                                }

                            case "*":
                                {
                                    //buffer = ParseFormula.CalcStep(buffer, (decimal)item.Value, (S, R) => S * R);
                                    buffer *= (decimal)item.Value;
                                    break;
                                }

                            case "x":
                                {
                                    //buffer = ParseFormula.CalcStep(buffer, (decimal)item.Value, (S, R) => S * R);
                                    buffer *= (decimal)item.Value;
                                    break;
                                }

                            case "/":
                                {
                                    //buffer = ParseFormula.CalcStep(buffer, (decimal)item.Value, (S, R) => S / R);
                                    buffer /= (decimal)item.Value;
                                    break;
                                }
                            case ":":
                                {
                                    //buffer = ParseFormula.CalcStep(buffer, (decimal)item.Value, (S, R) => S / R);
                                    buffer /= (decimal)item.Value;
                                    break;
                                }


                            default:
                                break;
                        }
                    }


                }
            }

            return buffer;

        }


        private static decimal CalcStep(decimal StartVal, decimal value, Func<decimal, decimal ,decimal > Calculate)
    {

        return Calculate(StartVal, value);
    }


    }
}
