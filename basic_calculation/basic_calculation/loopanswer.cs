using System;
using System.Collections.Generic;
using System.Text;

namespace basic_calculation
{
    class loopanswer
    {
        public static string Loop(double input)
        {
            string res = null;

            string junkan = input.ToString();
            string kan = junkan.Substring(junkan.IndexOf(".") + 1);//"."の後を切り出し
            int l = kan.Length;
            for (int i = 0; i < l; i++)
            {
                string k = kan.Substring(i, 3);//i番目から3文字を切り出し
                if (kan.IndexOf(k, i + 1) > 1)
                {
                    int an = kan.IndexOf(k, i + 1);//i+1番目以降の3文字がある番数
                    if (i == 0)
                    {
                        double y = Math.Pow(10, an);//10の(an)乗
                        double x = input * y;
                        double X = Math.Truncate(x) - Math.Truncate(input);//分子
                        double Y = y - 1;//分母


                        //約分
                        double G = Euclid.Gcd(X, Y);//最大公約数G
                        double Deno = Y / G;
                        double Nume = X / G;

                        string Deno2 = Deno.ToString("F4").TrimEnd('0');
                        string Nume2 = Nume.ToString("F4").TrimEnd('0');
                        res = Nume2.Substring(0, Nume2.IndexOf(".")) + "/" + Deno2.Substring(0, Deno2.IndexOf("."));
                        break;
                    }
                    else
                    {
                        double y = Math.Pow(10, i);//10の(i)乗
                        double x = input * y;
                        double X = Calculate.ToRoundDown(x, i) - Calculate.ToRoundDown(input, i);//分子
                        double Y = y - 1;//分母


                        //約分
                        double G = Euclid.Gcd(X, Y);//最大公約数G
                        double Deno = Y / G;
                        double Nume = X / G;

                        string Deno2 = Deno.ToString("F4").TrimEnd('0');
                        string Nume2 = Nume.ToString("F4").TrimEnd('0');
                        res = Nume2.Substring(0, Nume2.IndexOf(".")) + "/" + Deno2.Substring(0, Deno2.IndexOf("."));
                        break;
                    }
                }

                else if (i == l - 1)
                {
                    res = "out";
                    break;
                }
            }
            return res;
        }
    }
}
