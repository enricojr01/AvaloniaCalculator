using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaCalculator.Exceptions
{
    public class StackCountException : Exception
    { 
        public StackCountException() { }
        public StackCountException(string message) : base(message) { }
        public StackCountException(string message, Exception inner): base(message, inner) { }
    }
}
