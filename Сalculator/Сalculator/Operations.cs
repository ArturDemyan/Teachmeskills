﻿using System;
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
