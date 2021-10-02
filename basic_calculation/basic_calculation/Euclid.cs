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
                double remainder = a % b;
                a = b;
                b = remainder;
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
