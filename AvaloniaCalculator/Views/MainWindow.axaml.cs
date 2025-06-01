using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using NCalc;

namespace AvaloniaCalculator.Views
{
    public partial class MainWindow : Window
    {
        private Stack<double> _stack;
        private string _expression;

        public MainWindow()
        {
            InitializeComponent();
            _stack = new Stack<double>();
        }

        public void ButtonClick_Operand(object? sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string bContent = (string)b.Content;

            Debug.WriteLine(_expression);
            calculatorDisplay.Text += bContent;
        }

        public void ButtonClick_Operator(object? sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string bContent = (string)b.Content;

            if (_stack.Count < 2)
            {
                Debug.WriteLine($"Stack needs more than 2 arguments to do math, {_stack.Count} found.");
                return;
            }

            switch (bContent)
            {
                case "+":
                    {
                        double x = _stack.Pop();
                        double y = _stack.Pop();
                        double res = x + y;
                        Debug.WriteLine($"x + y = {res}");
                        _stack.Push(res);
                        calculatorDisplay.Text = res.ToString();
                        break;
                    }
                case "-":
                    {
                        double x = _stack.Pop();
                        double y = _stack.Pop();
                        double res = x - y;
                        Debug.WriteLine($"x - y = {res}");
                        calculatorDisplay.Text = res.ToString();
                        _stack.Push(res);
                        break;
                    }
                case "/":
                    {
                        double x = _stack.Pop();
                        double y = _stack.Pop();
                        double res = x / y;
                        Debug.WriteLine($"x / y = {res}");
                        calculatorDisplay.Text = res.ToString();
                        _stack.Push(res);
                        break;
                    }
                case "*":
                    {
                        double x = _stack.Pop();
                        double y = _stack.Pop();
                        double res = x * y;
                        Debug.WriteLine($"x * y = {res}");
                        calculatorDisplay.Text = res.ToString();
                        _stack.Push(res);
                        break;
                    }
            }
            calculatorDisplay.Text = "";
        }
        
        public void ButtonClick_Push(object? sender, RoutedEventArgs e)
        {
            double value;
            string bContent;

            if (String.IsNullOrWhiteSpace(calculatorDisplay.Text) == false)
            {
                bContent = calculatorDisplay.Text!;
                value = Convert.ToDouble(bContent);
                _stack.Push(value);
                calculatorDisplay.Text = "";
                return;
            }
            else 
            { 
                Debug.WriteLine("Cannot push, nothing in the display!"); 
            }
        }

        public void ButtonClick_Clear(object? sender, RoutedEventArgs e)
        {
            calculatorDisplay.Text = "";
        }

        public void ButtonClick_ClearExp(object sender, RoutedEventArgs e)
        {
            calculatorDisplay.Text = "";
            _stack.Clear();
        }

        public string StackContents()
        {
            string output = "";
            foreach (var num in _stack)
            {
                output += num.ToString();
            }
            return output;
        }
    }
}