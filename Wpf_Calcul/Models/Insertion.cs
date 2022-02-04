using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Calcul.Models
{
    public static class Insertion
    {
        private static DataTable Table { get; } = new DataTable();
        public static double Calc(string Expression) => Convert.ToDouble(Table.Compute(Expression, string.Empty));
        public static string Square(double a) => Convert.ToString(Math.Sqrt(a));
    }
}
