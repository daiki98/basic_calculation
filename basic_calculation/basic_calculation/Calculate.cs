using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace basic_calculation
{
    public static class Calculate
    {
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
                            if (st.Peek() == '/')
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
                                break;
                            }
                        }
                        else
                        {
                            buffer.Push(token);
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
        public static string Calculation(string input)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;

            foreach (double n in Enumerable.Range(-10000, 20000))//代入値(n)：－2000～2000
            {
                string res2 = input.Replace("□", n.ToString());
                string[] res3 = res2.Trim().Split(Space);

                foreach (string token in res3)
                {
                    switch (token)
                    {
                        case "+":
                            double A = calcResult.Pop();
                            double B = calcResult.Pop();
                            if (token == "+")
                            {
                                calcResult.Push(A + B);
                            }
                            break;

                        case "-":
                            double A1 = calcResult.Pop();
                            double B1 = 0;
                            if (token == "-")
                            {
                                if (calcResult.Count > 0)
                                {
                                    B1 = calcResult.Pop();
                                }
                                calcResult.Push(B1 - A1);
                            }
                            break;

                        case "*":
                            double A2 = calcResult.Pop();
                            double B2 = calcResult.Pop();
                            if (token == "*")
                            {
                                calcResult.Push(A2 * B2);
                            }
                            break;

                        case "/":
                            double A3 = calcResult.Pop();
                            double B3 = calcResult.Pop();
                            if (token == "/")
                            {
                                calcResult.Push(B3 / A3);
                            }
                            break;

                        default:
                            calcResult.Push(double.Parse(token));
                            break;
                    }
                }

                if (calcResult.Peek() == 0)
                {
                    res = n.ToString();
                    break;
                }
            }
            return res;
        }
    }
}
