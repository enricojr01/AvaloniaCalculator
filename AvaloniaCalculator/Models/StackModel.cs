using System;
using System.Collections.Generic;
using System.Diagnostics;
using AvaloniaCalculator.Exceptions;

namespace AvaloniaCalculator.Models
{
    public class StackModel {
        private Stack<double> _stack = new Stack<double>();

        public void Push(double value)
        {
            _stack.Push(value);
        }

        public double Pop()
        {
            double v;
            v = _stack.Pop();
            
            return v;
        }

        public double Top()
        {
            double v;
            v = _stack.Peek();
          
            return v;
        }

        public void DoMath(string _operator)
        {
            Debug.WriteLine(Contents());
            if (_stack.Count < 2)
            {
                throw new StackCountException($"Too few arguments on the stack");
            }

            double a = _stack.Pop();
            double b = _stack.Pop();
            double res = 0.0;

            switch(_operator)
            {
                case "+":
                    {
                        res = a + b;
                        _stack.Push(res);
                        break; 
                    }
                case "-": 
                    {
                        res = a - b;
                        _stack.Push(res);
                        break; 
                    }
                case "*":
                    {
                        res = a * b;
                        _stack.Push(res);
                        break; 
                    }
                case "/":
                    {
                        res = a / b;
                        _stack.Push(res);
                        break; 
                    }
                case "%":
                    {
                        // NOTE: The % operator in C# is simply a remainder
                        //       operator, if you want true modulus do it
                        //       this way instead.
                        res = (Math.Abs(a * b) + a) % b;
                        _stack.Push(res);
                        break;
                    }
            }
            Debug.WriteLine($"result is {_stack.Peek().ToString()}");
        }

        public string Contents()
        {
            string output = "";
            foreach (var num in _stack)
            {
                output += $"{num.ToString()} ";
            }
            return output;
        }

        public void Clear()
        {
            _stack.Clear();
        }
    }
}
