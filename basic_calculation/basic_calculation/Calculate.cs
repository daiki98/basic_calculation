using System.Collections.Generic;
using System.Linq;

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


        //後置記法　→　計算
        public static string Calculation(string input)
        {
            Stack<decimal> calcResult = new Stack<decimal>();
            string space = " ";
            char Space = space[0];
            string res = null;
            decimal m = -100.00M;//代入値の初期値

            for (double num = -100.00D; num <= 2000.00D; num += 0.01)//代入値(n)
            {
                string res2 = input.Replace("□", m.ToString());
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
                            decimal AB2 = decimal.Round(B2 * A2, 5);
                            calcResult.Push(AB2);
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
                                //calcResult.Push(B3 / A3);
                                decimal ans = B3 / A3;
                                string ans2 = ans.ToString("F8");
                                calcResult.Push(decimal.Parse(ans2));
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
                    res = m.ToString("G29");
                    break;
                }

                m += 0.01M;

            }

            //解が分数の場合
            if (res == null)
            {
                decimal B = 50M;
                for (double numB = 1D; numB <= 50D; numB += 1)
                {
                    decimal b = 50M;
                    for (double numb = 1D; numb <= 50D; numb += 1)
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
                        b -= 1M;
                    }
                    B -= 1M;
                }
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
