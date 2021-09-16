using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace basic_calculation
{
    public static class  Calculate
    {
        /*  ------------完了(------------------
         * ReversePolishNotation() 逆ポーランド記法　　　　　　　　引数(変換前char[]) 戻り値（変換後String)
         * 
         * -------------途中(はしもと）-----------------
         * CalculateRPN() 逆ポーランドの計算(□とかがない状態で）　 引数(逆ポーランド後の式) 戻り値（計算値）
         * 　
         * Bisection() 二分法準備
         * 
         * -------------途中(ダイスケ)---------------------
         * Calculation()  計算　　　　　　　　　　　　　　　　　　　引数(逆ポーランド後のString）　戻り値（計算結果 float??)
         *
         */

        //まだできてないしスルーで
        public static string Bisection(string input)
        {
            double mid_value = 0;　　　　　//区間の中間値

            double initial_value1 = 5;                  //初期値
            double initial_value2 = -5;
            Boolean Isfinished = true;                  //計算可能か判断するやつ（falseで計算おわり）

            string num1 = initial_value1.ToString();
            string num2 = initial_value2.ToString();
            string formula1 = input.Replace("□", num1);　　　
            string formula2 = input.Replace("□", num2);

            string ans1 = CalculateRPN(formula1);
            string ans2 = CalculateRPN(formula2);

            return ans1;
        }

        //これもスルーで
        public static string CalculateRPN(string input)
        {  
            //スタック
            Stack<string> inputRPN = new Stack<string>();//逆ポーランド記法化した式を入れるスタック
            Stack<double> calcResult = new Stack<double>();//計算結果を入れるスタック
            Stack<string> reversedRPN = new Stack<string>();//inputRPNを逆順（正しい順）で積むスタック
            Stack<char> buffer = new Stack<char>();//バッファスタック

            foreach (char x in input)//逆ポーランド記法の式をスタックに積む
            {
                inputRPN.Push(x.ToString());
            }
           
            while (inputRPN.Count > 0)//式の積み方を逆順にする
            {
                reversedRPN.Push(inputRPN.Pop());
            }
            
            while (reversedRPN.Count > 0)
            {
                string token=reversedRPN.Pop();

                switch (token)
                {
                    case "+":
                        double A = calcResult.Pop();
                        double B = calcResult.Pop();
                        calcResult.Push(B + A);
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
                        calcResult.Push(B2 * A2);
                        break;

                    case "/":
                        double A3 = calcResult.Pop();
                        double B3 = calcResult.Pop();
                        calcResult.Push(B3 / A3);
                        break;

                    case " "://後置記法に半角スペースを入れていたため、スペースは無視する
                        continue;

                    default:
                        if (double.TryParse(token, out double token_double))//数値の場合
                        {
                            calcResult.Push(token_double);
                        }
                        break;
                }
            }
            //スタックが空になるまでpopしてバッファへ移動
            while (calcResult.Count > 0)
            {
                //buffer.Push(Space);
                buffer.Push((char)calcResult.Pop());
            }

            //スタックの順番を逆順にして文字列に変換
            string r = new string(buffer.Reverse().ToArray());
            string res = r ;

            return res;
        }



        //ダイスケがつくってとる途中のやつ

        //計算部分
        //流れ
        //1.□にある数字を代入
        //2.□のない式で計算
        //3.解が０になった場合、画面に□に代入した値を表示
        public static string Calculation(string input)
        {
            string ans = "0";
            string result=" ";

            //スタック
            Stack<string> inputRPN = new Stack<string>();//逆ポーランド記法化した式を入れるスタック
            Stack<double> calcResult = new Stack<double>();//計算結果を入れるスタック
            Stack<string> reversedRPN = new Stack<string>();//inputRPNを逆順（正しい順）で積むスタック



            foreach (int n in Enumerable.Range(-10, 10))//とりあえず-10～10の中から探す（n(m)が代入値）
            {
                string m = n.ToString();
                string res = input.Replace("□", m);

                foreach (char x in res)//逆ポーランド記法の式をスタックに積む
                {
                    inputRPN.Push(x.ToString());
                }

                while (inputRPN.Count > 0)//式の積み方を逆順にする
                {
                    reversedRPN.Push(inputRPN.Pop());
                }

                while (reversedRPN.Count > 0)
                {
                    string token = reversedRPN.Pop();

                    switch (token)
                    {
                        case "+":
                            double A = calcResult.Pop();
                            double B = calcResult.Pop();
                            calcResult.Push(B + A);
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
                            calcResult.Push(B2 * A2);
                            break;

                        case "/":
                            double A3 = calcResult.Pop();
                            double B3 = calcResult.Pop();
                            calcResult.Push(B3 / A3);
                            break;

                        case " "://後置記法に半角スペースを入れていたため、スペースは無視する
                            continue;

                        default:
                            if (double.TryParse(token, out double token_double))//数値の場合
                            {
                                calcResult.Push(token_double);
                            }
                            break;
                    }
                }

                //□に数字を代入して計算した結果がans(0)と同じ場合、
                //resultTextをm(□への代入値)とする。
                if (calcResult.Peek().ToString() == ans)
                {
                    result = m;
                    break;
                }

                else if (calcResult.Peek().ToString() != ans)
                {
                    break;
                }
                break;
            }
            return result;
        }


        /*
         * ダイスケがしてくれた逆ポーランド記法するやつ   
         */
        public static string ReversePolishNotation(char[] input)
        {
            Stack<char> buffer = new Stack<char>();//バッファスタック
            Stack<char> st = new Stack<char>();//作業用スタック

            string space = " ";
            char Space = space[0]; //計算結果に必要？？

            int currentState = 0;//演算子の判定用？？

            foreach (char token in input)
            {
                switch (token)
                {
                    //鍵かっこ処理
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

                    case '*':
                    case '/':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '*' || st.Peek() == '/')
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
                            if (st.Peek() == '*' || st.Peek() == '/' || st.Peek() == '+' || st.Peek() == '-')
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
            string res =  r + "\r\n";

            return res;

        }


    }
}
