using System;
using System.Collections.Generic;
using System.Linq;

namespace basic_calculation
{
    public static class Calculate
    {

        /*      Calculation2も計算系やしCalculateのクラスにまとめてひとつにしたわ
         * 
         * 　 BisectionCal() 二分法
         * 　 ToRoundDown() 少数まるめ？？？
         *    Calculation_forBisection() 二分法の計算
         *    ReversePolishNotation() 中置記法　→　後置記法(非分数）
         *    Calculation()  //後置記法　→　計算(非分数）
         *    ReversePolishNotation_Fraction() 中置記法　→　後置記法(分数）
         *    Calculation_Fraction()  後置記法　→　計算(分数）
         *    
         *    追加
         *     IntMinJub()（整数，少数判定） 
         *    
         */
        public static double BisectionCal(string input, int ffnum)
        {
            double initial_val1 = 10000d;       //正の初期値
            double initial_val2 = -10000d;      //負の初期値
            double mid_val = 0;     //中間値

            double res_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));      //解きたい式に初期値1を代入したときの値
            double res_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));      //解きたい式に初期値2を代入したときの値

            double jub = res_initial1 * res_initial2;       //範囲の中に解があるか判断するやつ　正ー無し，負ーあり

            if (ffnum == 1)//分数関数，高次方程式
            {
                if (jub >= 0)//初期値で答えが無い場合
                {
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));        //再計算
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                    double Rejub = REres_initial1 * REres_initial2;

                    //プラス側に範囲をずらす
                    for (int i = 0; i < 5000; i++)
                    {
                        initial_val1 += 10d;
                        initial_val2 += 10d;
                        REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                        REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                        Rejub = REres_initial1 * REres_initial2;

                        //解が見つかった場合
                        if (Rejub < 0)
                        {
                            while (Math.Abs(initial_val1 - initial_val2) > 0.00001)          //ここで精度決める
                            {

                                mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                                double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                                if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                                {
                                    initial_val1 = mid_val;

                                }
                                else
                                {
                                    initial_val2 = mid_val;

                                }

                            }
                            ffnum = 0;
                            return ToRoundDown(mid_val, 10);
                        }
                    }

                    initial_val1 = 10000d;       //初期値をリセット
                    initial_val2 = -10000d;

                    //プラス側に無く，マイナス側に範囲ずらす
                    for (int i = 0; i < 5000; i++)
                    {

                        initial_val1 -= 10d;
                        initial_val2 -= 10d;
                        REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                        REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                        Rejub = REres_initial1 * REres_initial2;

                        //解が見つかった時
                        if (Rejub < 0)
                        {
                            while (Math.Abs(initial_val1 - initial_val2) > 0.00001)          //ここで精度決める
                            {

                                mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                                double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                                if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                                {
                                    initial_val1 = mid_val;

                                }
                                else
                                {
                                    initial_val2 = mid_val;

                                }

                            }

                            return ToRoundDown(mid_val, 10);
                        }
                    }

                    //プラスにもマイナスにも解がない場合
                    if (Rejub > 0)
                    {

                        return 595959595d;

                    }

                    //初期値で答えがある場合（高次，分数関数）
                }
                else
                {

                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                    double Rejub = REres_initial1 * REres_initial2;

                    while (Math.Abs(initial_val1 - initial_val2) > 0.00001)          //ここで精度決める
                    {

                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }
                    }
                }
                ffnum = 0;
                return ToRoundDown(mid_val, 10);

            }

            //1次方程式の時
            else if (ffnum == 0)
            {


                if (jub >= 0)
                {
                    initial_val1 += 100;
                    initial_val2 -= 100;
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                    double Rejub = REres_initial1 * REres_initial2;

                    while (Rejub >= 0)
                    {
                        initial_val1 += 100;
                        initial_val2 -= 100;
                        REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                        REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                        Rejub = REres_initial1 * REres_initial2;
                    }

                    while (Math.Abs(initial_val1 - initial_val2) > 0.00001)          //ここで精度決める
                    {

                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }

                    }

                }
                else if (jub < 0)
                {
                    while (Math.Abs(initial_val1 - initial_val2) > 0.00001)          //ここで精度決める
                    {

                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (res_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }

                    }

                }

            }
            return ToRoundDown(mid_val, 10);


        }




        public static double ToRoundDown(double dValue, int iDigits)
        {
            double dCoef = Math.Pow(10, iDigits);

            return dValue > 0 ? Math.Floor(dValue * dCoef) / dCoef :
                                Math.Ceiling(dValue * dCoef) / dCoef;
        }


        public static string Calculation_forBisection(string input, double value)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];

            string res2 = input.Replace("□", value.ToString());
            string[] res3 = res2.Trim().Split(Space);
            foreach (string token in res3)
            {
                switch (token)
                {
                    case "+":
                        double A0 = calcResult.Pop();
                        double B0 = calcResult.Pop();
                        calcResult.Push(B0 + A0);
                        break;

                    case "-":
                        double A1 = calcResult.Pop();
                        double B1 = 0;
                        if (calcResult.Count > 0)
                        {
                            B1 = calcResult.Pop();
                        }
                        calcResult.Push(B1 - A1);
                        break;

                    case "*":
                        double A2 = calcResult.Pop();
                        double B2 = calcResult.Pop();
                        double AB2 = B2 * A2;
                        calcResult.Push(AB2);
                        break;

                    case "/":
                        double A3 = calcResult.Pop();
                        double B3 = calcResult.Pop();
                        if (A3 == 0)
                        {
                            break;
                        }
                        else
                        {
                            double ans = B3 / A3;
                            string ans2 = ans.ToString("F12");
                            calcResult.Push(double.Parse(ans2));
                            break;
                        }

                    case "%":
                        double A4 = calcResult.Pop();
                        double B4 = 100;
                        calcResult.Push(A4 / B4);
                        break;

                    default:
                        calcResult.Push(double.Parse(token));
                        break;
                }
            }

            string result = (calcResult.Peek() * -1).ToString();

            return result;
        }


        public static bool IntMinJub(double input)
        {
            string reviseVal = input.ToString("F5");  //ここのF5は二分法の精度に合わせる
            double newinput = double.Parse(reviseVal);

            if ((int)newinput == newinput)
            {
                return true; //整数
            }
            else
            {

                return false;//少数
            }
        }




        /*
         * 総当たり計算 
         */

        //中置記法　→　後置記法(非分数）
        public static string ReversePolishNotation(char[] input)
        {
            Stack<char> buffer = new Stack<char>();//バッファスタック
            Stack<char> st = new Stack<char>();//作業用スタック

            string space = " ";
            char Space = space[0];
            int currentState = 0;

            foreach (char token in input)
            {
                switch (token)
                {
                    case '(':
                        if (buffer.Count > 0 && currentState == 0)
                        {
                            st.Push('*');
                            currentState += 1;
                        }
                        st.Push(token);
                        break;

                    case ')':
                        while (st.Count > 0)
                        {
                            char t = st.Pop();
                            if (t == '(')
                            {
                                break;
                            }
                            else
                            {
                                buffer.Push(Space);
                                buffer.Push(t);
                                currentState += 1;
                            }
                        }
                        break;

                    case '/':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else if (st.Peek() == '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                                st.Push('*');
                                currentState += 1;
                                break;
                            }
                            else if (st.Peek() == '*' && buffer.Peek() != '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        st.Push(token);
                        currentState += 1;
                        break;

                    case '*':
                    case '÷':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        st.Push(token);
                        currentState += 1;
                        break;

                    case '+':
                    case '-':
                        if (buffer.Count > 0 && currentState == 0)
                        {
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-')
                                {
                                    buffer.Push(Space);
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            currentState += 1;
                        }

                        else if (buffer.Count == 0 && currentState == 0)
                        {
                            buffer.Push(token);
                            currentState *= 0;
                        }

                        else if (currentState > 0)
                        {
                            buffer.Push(Space);
                            buffer.Push(token);
                            currentState *= 0;
                        }
                        break;

                    case '□':
                        if (buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                            }
                            else if (currentState == 0 && st.Count > 0)
                            {
                                if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷')
                                {
                                    buffer.Push(Space);
                                    buffer.Push(st.Pop());
                                    buffer.Push(Space);
                                    buffer.Push(token);
                                    st.Push('*');
                                }
                                else
                                {
                                    buffer.Push(Space);
                                    buffer.Push(token);
                                    st.Push('*');
                                }
                            }
                            else if (currentState == 0 && st.Count == 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                st.Push('*');
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                        }
                        currentState *= 0;
                        break;

                    case '%':
                        buffer.Push(Space);
                        buffer.Push(token);
                        break;

                    case '.':
                        buffer.Push(token);
                        break;


                    //数字の場合
                    default:
                        if (st.Count > 0 && buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                            }
                            else if (currentState == 0)
                            {
                                buffer.Push(token);
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                        }
                        currentState *= 0;
                        break;
                }
            }

            //スタックが空になるまでpopしてバッファへ移動
            while (st.Count > 0)
            {
                buffer.Push(Space);
                buffer.Push(st.Pop());
            }

            //スタックの順番を逆順にして文字列に変換
            string r = new string(buffer.Reverse().ToArray());
            string res = r + "\r\n";

            return res;
        }


        //後置記法　→　計算(非分数）
        public static string Calculation(string input)
        {
            Stack<decimal> calcResult = new Stack<decimal>();
            string space = " ";
            char Space = space[0];
            string res = null;

            decimal B = 50;
            for (decimal numB = 1; numB <= 50; numB += 1)
            {
                decimal b = 50;
                for (decimal numb = 1; numb <= 50; numb += 1)
                {
                    string res2 = input.Replace("□", b.ToString() + " " + B.ToString() + " " + "/");
                    string[] res3 = res2.Trim().Split(Space);
                    foreach (string token in res3)
                    {
                        switch (token)
                        {
                            case "+":
                                decimal A0 = calcResult.Pop();
                                decimal B0 = calcResult.Pop();
                                calcResult.Push(B0 + A0);
                                break;

                            case "-":
                                decimal A1 = calcResult.Pop();
                                decimal B1 = 0;
                                if (calcResult.Count > 0)
                                {
                                    B1 = calcResult.Pop();
                                }
                                calcResult.Push(B1 - A1);
                                break;

                            case "*":
                                decimal A2 = calcResult.Pop();
                                decimal B2 = calcResult.Pop();
                                calcResult.Push(B2 * A2);
                                break;

                            case "/":
                                decimal A3 = calcResult.Pop();
                                decimal B3 = calcResult.Pop();
                                if (A3 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    calcResult.Push(B3 / A3);
                                    //decimal ans = B3 / A3;
                                    //double ans1 = ToRoundDown((double)ans, 20);
                                    //string ans2 = ans1.ToString();
                                    //calcResult.Push(decimal.Parse(ans2));
                                    break;
                                }

                            case "%":
                                decimal A4 = calcResult.Pop();
                                decimal B4 = 100;
                                calcResult.Push(A4 / B4);
                                break;

                            default:
                                calcResult.Push(decimal.Parse(token));
                                break;
                        }
                    }

                    if (calcResult.Peek() == 0)
                    {
                        res = b.ToString() + "/" + B.ToString();
                        break;
                    }
                    b -= 1;
                }
                B -= 1;
            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }




        //中置記法　→　後置記法(分数）
        public static string ReversePolishNotation_Fraction(char[] input)
        {
            Stack<char> buffer = new Stack<char>();//バッファスタック
            Stack<char> st = new Stack<char>();//作業用スタック

            string space = " ";
            char Space = space[0];
            int currentState = 0;

            foreach (char token in input)
            {
                switch (token)
                {
                    case '(':
                        if (buffer.Count > 0 && currentState == 0)
                        {
                            st.Push('*');
                            currentState += 1;
                        }
                        st.Push(token);
                        break;

                    case ')':
                        while (st.Count > 0)
                        {
                            char t = st.Pop();
                            if (t == '(')
                            {
                                break;
                            }
                            else
                            {
                                buffer.Push(Space);
                                buffer.Push(t);
                            }
                        }
                        break;

                    case '/':
                        buffer.Push(token);
                        currentState *= 0;
                        break;

                    case '*':
                    case '÷':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '*' || st.Peek() == '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        st.Push(token);
                        currentState += 1;
                        break;

                    case '+':
                    case '-':
                        if (buffer.Count > 0 && currentState == 0)
                        {
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-')
                                {
                                    buffer.Push(Space);
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            currentState += 1;
                        }

                        else if (buffer.Count == 0 && currentState == 0)
                        {
                            buffer.Push(token);
                            currentState *= 0;
                        }

                        else if (currentState > 0)
                        {
                            buffer.Push(Space);
                            buffer.Push(token);
                            currentState *= 0;
                        }
                        break;

                    case '□':
                        if (buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                currentState *= 0;
                                break;
                            }
                            else if (currentState == 0 && buffer.Peek() != '/')
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                buffer.Push(Space);
                                buffer.Push('*');
                                currentState *= 0;
                                break;
                            }
                            else if (currentState == 0 && buffer.Peek() == '/')
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                currentState *= 0;
                                break;
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                            currentState *= 0;
                            break;
                        }
                        break;

                    case '%':
                        buffer.Push(Space);
                        buffer.Push(token);
                        break;

                    case '.':
                        buffer.Push(token);
                        break;


                    //数字の場合
                    default:
                        if (st.Count > 0 && buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                currentState *= 0;
                                break;
                            }
                            else if (currentState == 0)
                            {
                                buffer.Push(token);
                                currentState *= 0;
                                break;
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                            currentState *= 0;
                            break;
                        }
                        break;
                }
            }

            //スタックが空になるまでpopしてバッファへ移動
            while (st.Count > 0)
            {
                buffer.Push(Space);
                buffer.Push(st.Pop());
            }

            //スタックの順番を逆順にして文字列に変換
            string r = new string(buffer.Reverse().ToArray());
            string res = r + "\r\n";

            return res;
        }

        //後置記法　→　計算
        //解が分数の場合
        public static string Calculation_Fraction(string input)
        {
            Stack<string> calcResult = new Stack<string>();
            string space = " ";
            char Space = space[0];
            string res = null;

            decimal B = 50;
            for (decimal numB = 1; numB <= 50; numB += 1)
            {
                decimal b = 50;
                for (decimal numb = 1; numb <= 100; numb += 1)
                {
                    string res2 = input.Replace("□", b.ToString() + "/" + B.ToString());
                    string[] res3 = res2.Trim().Split(Space);
                    foreach (string token in res3)
                    {
                        switch (token)
                        {
                            //ex 1/2 + 1/3 (1/2 1/3 +)
                            case "+":
                                //B0 = 1/3
                                string B0 = calcResult.Pop();
                                double numeB0 = double.Parse(B0.Substring(0, B0.IndexOf("/")));//numeB0 = 1
                                double denoB0 = double.Parse(B0.Substring(B0.IndexOf("/") + 1));//denoB0 = 3

                                //A0 = 1/2
                                string A0 = calcResult.Pop();
                                double numeA0 = double.Parse(A0.Substring(0, A0.IndexOf("/")));//numeA0 = 1
                                double denoA0 = double.Parse(A0.Substring(A0.IndexOf("/") + 1));//denoA0 = 2

                                double deno0 = Euclid.Lcm(denoA0, denoB0);//deno0 = 6(2と3の最小公倍数)
                                double nume0 = numeA0 * (deno0 / denoA0) + numeB0 * (deno0 / denoB0);//nume0 = 1*(6/2) + 1*(6/3) = 5

                                //約分
                                double G0 = Euclid.Gcd(nume0, deno0);//最大公約数G0
                                double Deno0 = deno0 / G0;
                                double Nume0 = nume0 / G0;

                                calcResult.Push(Nume0.ToString() + "/" + Deno0.ToString());
                                break;

                            case "-":
                                //B1 = 1/3
                                string B1 = calcResult.Pop();
                                double numeB1 = double.Parse(B1.Substring(0, B1.IndexOf("/")));//numeB1 = 1
                                double denoB1 = double.Parse(B1.Substring(B1.IndexOf("/") + 1));//denoB1 = 3

                                //A1 = 1/2
                                string A1 = calcResult.Pop();
                                double numeA1 = double.Parse(A1.Substring(0, A1.IndexOf("/")));//numeA1 = 1
                                double denoA1 = double.Parse(A1.Substring(A1.IndexOf("/") + 1));//denoA1 = 2

                                double deno1 = Euclid.Lcm(denoA1, denoB1);//deno1 = 6(2と3の最小公倍数)
                                double nume1 = numeA1 * (deno1 / denoA1) - numeB1 * (deno1 / denoB1);//nume1 = 1*(6/2) - 1*(6/3) = 1

                                //約分
                                double G1 = Euclid.Gcd(nume1, deno1);//最大公約数G1
                                double Deno1 = deno1 / G1;
                                double Nume1 = nume1 / G1;

                                calcResult.Push(Nume1.ToString() + "/" + Deno1.ToString());
                                break;

                            case "*":
                                //B2 = 1/3
                                string B2 = calcResult.Pop();
                                double numeB2 = double.Parse(B2.Substring(0, B2.IndexOf("/")));//numeB2 = 1
                                double denoB2 = double.Parse(B2.Substring(B2.IndexOf("/") + 1));//denoB2 = 3

                                //A2 = 1/2
                                string A2 = calcResult.Pop();
                                double numeA2 = double.Parse(A2.Substring(0, A2.IndexOf("/")));//numeA2 = 1
                                double denoA2 = double.Parse(A2.Substring(A2.IndexOf("/") + 1));//denoA2 = 2

                                double deno2 = denoA2 * denoB2;//deno2 = 2*3 = 6
                                double nume2 = numeA2 * numeB2;//nume2 = 1*1 = 1

                                //約分
                                double G2 = Euclid.Gcd(nume2, deno2);//最大公約数G2
                                double Deno2 = deno2 / G2;
                                double Nume2 = nume2 / G2;

                                calcResult.Push(Nume2.ToString() + "/" + Deno2.ToString());
                                break;

                            case "÷":
                                //B3 = 1/3
                                string B3 = calcResult.Pop();
                                double numeB3 = double.Parse(B3.Substring(0, B3.IndexOf("/")));//numeB3 = 1
                                double denoB3 = double.Parse(B3.Substring(B3.IndexOf("/") + 1));//denoB3 = 3

                                //A3 = 1/2
                                string A3 = calcResult.Pop();
                                double numeA3 = double.Parse(A3.Substring(0, A3.IndexOf("/")));//numeA3 = 1
                                double denoA3 = double.Parse(A3.Substring(A3.IndexOf("/") + 1));//denoA3 = 2

                                double deno3 = denoA3 * numeB3;//deno2 = 2*1 = 2
                                double nume3 = numeA3 * denoB3;//nume2 = 1*3 = 3

                                //約分
                                double G3 = Euclid.Gcd(nume3, deno3);//最大公約数G3
                                double Deno3 = deno3 / G3;
                                double Nume3 = nume3 / G3;

                                calcResult.Push(Nume3.ToString() + "/" + Deno3.ToString());
                                break;

                            case "%":
                                string A4 = calcResult.Pop();//A4 = 1/2
                                double Nume4 = double.Parse(A4.Substring(0, A4.IndexOf("/")));//Nume4 = 1
                                double denoA4 = double.Parse(A4.Substring(A4.IndexOf("/") + 1));//denoA4 = 2

                                double Deno4 = denoA4 * 100;//Deno4 = 200
                                calcResult.Push(Nume4.ToString() + "/" + Deno4.ToString());
                                break;

                            //数値の場合
                            default:
                                //分数の場合
                                if (token.Contains("/"))
                                {
                                    calcResult.Push(token);
                                }

                                //小数値の場合
                                else if (token.Contains("."))
                                {
                                    double num = double.Parse(token);//(ex.0.12)

                                    string s = token.Substring(token.IndexOf(".") + 1);//コンマの後の数値をs(=12)
                                    double sL = s.Length;//sL：sの桁数(2)
                                    double Deno = Math.Pow(10, sL);//分母：10のsL乗(=10^2=100)
                                    double Nume = num * Deno;//分子：小数値×10のsL乗(=0.12*100=12)

                                    calcResult.Push(Nume.ToString() + "/" + Deno.ToString());//(12/100)
                                }

                                //整数の場合
                                else
                                {
                                    calcResult.Push(token + "/" + "1");
                                }
                                break;
                        }
                    }

                    string cal = calcResult.Peek();
                    double cal2 = double.Parse(cal.Substring(0, cal.IndexOf("/")));
                    if (cal2 == 0)
                    {
                        res = b.ToString() + "/" + B.ToString();
                        break;
                    }
                    b -= 1;
                }
                B -= 1;
            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }
    }

}
