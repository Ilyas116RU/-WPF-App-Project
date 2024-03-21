using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Calculation
    {
        public string Equation { get; set; }
        public string Result { get; set; }

        public Calculation(string equation, string result)
        {
            Equation = equation;
            Result = result;
        }
    }

}
