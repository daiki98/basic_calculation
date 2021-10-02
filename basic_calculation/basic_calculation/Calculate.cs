using System;
using System.Collections.Generic;
using System.Linq;

namespace basic_calculation
{
    public static class Calculate
    {
        public static double BisectionCal(string input)
        {
            double initial_val1 = 10000d;       //正の初期値
            double initial_val2 = -10000d;      //負の初期値
            double mid_val = 0;     //中間値

            double res_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));      //解きたい式に初期値1を代入したときの値
            double res_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));      //解きたい式に初期値2を代入したときの値

            double jub = res_initial1 * res_initial2;       //範囲の中に解があるか判断するやつ　正ー無し，負ーあり

            if (jub >= 0)
            {
                //ここに範囲外の処理
                //今はなんとなく1帰ってくる
                return 1;

            }
            else if (jub < 0)
            {
                while (Math.Abs(initial_val1 - initial_val2) > 0.00000000001)          //ここで精度決める
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
            return mid_val;
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
                            string ans2 = ans.ToString("F10");
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


        //中置記法　→　後置記法
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
                            }
                        }
                        break;

                    case '/':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/' && buffer.Peek() != '*')
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
                            }
                            else if (st.Peek() == '*' && buffer.Peek() != '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                                st.Push(token);
                                currentState += 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (currentState == 0)
                        {
                            st.Push(token);
                            currentState += 1;
                        }
                        else
                        {
                            break;
                        }
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
                                currentState *= 0;
                                break;
                            }
                            else if (currentState == 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                buffer.Push(Space);
                                buffer.Push('*');
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
    }
}
