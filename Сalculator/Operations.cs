using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Сalculator
{
    public class Operations
    {
        private static double number;

        public Operations() { }

        //public string ConvertToRPN(string input)
        //{
        //    List<string> tokens = new List<string>();
        //    string noSpacesInput = input.Replace(" ", "");

        //    foreach (var item in noSpacesInput) 
        //    {
        //        tokens.Add(item.ToString());
        //    }


        // for (int i = 0; i<tokens.Count; i++)
        // {
        //        if (tokens[i]=="*" || tokens[i]=="/")
        //        {            
        //           double left = double.Parse(tokens[i-1]);
        //           double right = double.Parse(tokens[i+1]);
        //           double result = tokens[i]=="*" ?  left * right: left / right;

        //            tokens[i-1] = result.ToString();
        //            tokens.RemoveAt(i);
        //            tokens.RemoveAt(i);
        //            --i;
        //        }
        // }
        //    for(int i=0; i<tokens.Count; i++)
        //    {
        //        if (tokens[i]=="+" || tokens[i]=="-")
        //        {
        //            double left = double.Parse(tokens[i - 1]);
        //            double right = double.Parse(tokens[i + 1]);
        //            double result = tokens[i] =="+" ? left + right : left - right;
        //            tokens[i - 1] = result.ToString();
        //            tokens.RemoveAt(i);
        //            tokens.RemoveAt(i);
        //            --i;
        //        }
        //    }
        //    double finalResult = double.Parse(tokens[0]);

        //    return finalResult.ToString();
        //}

        public double ConvertToPRN(string input)
        {
            List<string> output = new List<string>();
            Stack<char> operators= new Stack<char> ();
            var regex = new Regex(@"\d+(\.\d+)?|[+\-*/()]");

            foreach(Match match in regex.Matches(input))
            {
                string token = match.Value;

                if (double.TryParse(token,out _))
                   {
                        output.Add(token);
                   }
                else
                {
                    switch (token)
                    {
                        case "(":
                            operators.Push('(');
                            break;
                        case ")":
                            {
                                while (operators.Peek() != '(')
                                {
                                    output.Add(operators.Pop().ToString());
                                }
                                operators.Pop();
                            }
                            break;
                        default:
                            {
                                while (operators.Count > 0 && GetPrecedence(operators.Peek()) > GetPrecedence((token[0])))
                                {
                                    output.Add(operators.Pop().ToString());
                                }
                                operators.Push(token[0]);
                            }
                            break;
                    };

                }

            }
            while (operators.Count > 0)
            {
                output.Add(operators.Pop().ToString());
            }
           var result = EvaluateRPN(output);

            return result;
        }

        static double EvaluateRPN(List<string> output)
        {
            var  stack  =new Stack<double> ();
            foreach(var token in output)
            {
                if (double.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    double left = stack.Pop();
                    double right = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push (left + right);
                            break;
                        case "-":
                            stack.Push(left - right);
                            break;
                        case "*":
                             stack.Push(left * right);
                            break;
                        case "/":
                            stack.Push(left / right);
                            break;
                    }
                }
            }
            return stack.Pop();
        }  

        static int GetPrecedence(char op)
        {
            return op switch
            {
                '+' or '-' => 1,
               '*'  or '/' => 2,
               _=>0
            }; 
        }
    }
}
