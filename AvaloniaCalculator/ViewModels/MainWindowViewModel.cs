using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaCalculator.Exceptions;
using AvaloniaCalculator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;

namespace AvaloniaCalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase    
    {
        private StackModel _stackModel = new StackModel();
        public string Stack { get { return _stackModel.Contents(); } }

        private string _currentNumber = "";
        public string CurrentNumber { get { return _currentNumber; } } 

        public ReactiveCommand<Unit, Unit> PushCommand { get; }
        public ReactiveCommand<String, Unit> ChangeNumber { get; }
        public ReactiveCommand<Unit, Unit> NegateNumberCommand { get; }
        public ReactiveCommand<String, Unit> DoMathCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearDisplayCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearEverythingCommand { get; }

        public MainWindowViewModel()
        {
            PushCommand = ReactiveCommand.Create(StackPush);
            ChangeNumber = ReactiveCommand.Create<string>(UpdateDisplay);
            DoMathCommand = ReactiveCommand.Create<string>(DoMath);
            ClearDisplayCommand = ReactiveCommand.Create(ClearDisplay);
            ClearEverythingCommand = ReactiveCommand.Create(ClearEverything);
            NegateNumberCommand = ReactiveCommand.Create(NegateNumber);
        }

        public void ClearDisplay()
        {
            _currentNumber = "";
            this.RaisePropertyChanged(nameof(CurrentNumber));
        }

        public void ClearStack()
        {
            _stackModel.Clear();
            this.RaisePropertyChanged(nameof(Stack));
        }

        public void UpdateDisplay(string value)
        {
            Debug.WriteLine($"attempting to write value: {value}");
            _currentNumber += value;
            this.RaisePropertyChanged(nameof(CurrentNumber));
        }
        public void NegateNumber()
        {
            double current = Convert.ToDouble(_currentNumber);
            current = current * -1;
            Debug.WriteLine($"NegateNumber converts {_currentNumber} to {current}");
            ClearDisplay();
            UpdateDisplay(current.ToString());
        }

        public void ClearEverything()
        {
            ClearDisplay();
            ClearStack();
        }

        public void StackPush()
        {
            double val = Convert.ToDouble(_currentNumber);
            _stackModel.Push(val);
            this.RaisePropertyChanged(nameof(Stack));
            ClearDisplay();
            Debug.WriteLine($"Pushed {val} to stack!");
        }

        public void DoMath(string _operator)
        {
            try
            {
                _stackModel.DoMath(_operator);
            }
            catch (StackCountException e)
            {
                Debug.WriteLine(e);
                return;
            }
            double result = _stackModel.Top();
            _currentNumber = result.ToString();
            this.RaisePropertyChanged(nameof(Stack));
            this.RaisePropertyChanged(nameof(CurrentNumber));
        }

    }

}
