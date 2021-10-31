using System;
using System.Collections.Generic;
using System.Text;

namespace basic_calculation
{
    class Euclid
    {
        //最大公約数
        public static double Gcd(double a, double b)
        {
            if (a < b)
            {
                return Gcd(b, a);
            }

            while (b != 0)
            {
                decimal remainder = decimal.Parse(a.ToString()) % decimal.Parse(b.ToString());
                a = b;
                b = double.Parse(remainder.ToString());
            }
            return a;
        }

        //最小公倍数
        public static double Lcm(double a, double b)
        {
            return a * b / Gcd(a, b);
        }
    }
}
