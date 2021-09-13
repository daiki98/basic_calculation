using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace basic_calculation
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        // 数字
        void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;
        }


        // □（解）
        void OnAnswer(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;
        }


        // 演算子
        void OnSelectOperator(object sender, EventArgs e) 
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;
        }


        // Cボタン
        void OnClear(object sender, EventArgs e)
        {
            
            questionText.Text = "";
            resultText.Text = "";
        }


        // =ボタン
        void OnSelectEqual(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;
        }

        //Delボタン
        void OnDel(object sender, EventArgs e)　　　　　											　
        {
            if (questionText.Text.Length > 0)
            {
                questionText.Text = questionText.Text.Substring(0, questionText.Text.Length - 1);
            }

        }

        // STARTボタン
        void OnCalculate(object sender, EventArgs e)
        {
            string str = questionText.Text;
            if (str.Contains("=") && !str.Contains("0=") && !str.Contains("=0"))
            {
                //左辺切り出し
                string Left = str.Substring(0, str.IndexOf("="));

                //右辺切り出し
                string Right = str.Substring(str.IndexOf("=") + 1);

                //式 0=F(x)  F(x)=Right(右辺)-Left(左辺)
                string f1 = Right + "-(" + Left + ")";
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                char[] F = f3.ToCharArray();


                //逆ポーランド記法化
                Stack<char> buffer = new Stack<char>();//バッファスタック
                Stack<char> st = new Stack<char>();//作業用スタック
                resultText.Text = string.Empty;

                foreach (char token in F)
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
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        case '+':
                        case '-':
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '*' || st.Peek() == '/' || st.Peek() == '+' || st.Peek() == '-')
                                {
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        //数字（1桁）の場合
                        //2桁以上を利用する場合、F(x)(=Right(右辺)-Left(左辺))の項ごとに空白か","などで区切る必要がある
                        default:
                            buffer.Push(token);
                            break;
                    }
                }

                //スタックが空になるまでpopしてバッファへ移動
                while (st.Count > 0)
                {
                    buffer.Push(st.Pop());
                }

                //スタックの順番を逆順にして文字列に変換
                string r = new string(buffer.Reverse().ToArray());
                resultText.Text = resultText.Text + r + "\r\n";


                //"□"に数値を代入する
                string ans = "0";
                string res = resultText.Text;
                string res2;

                foreach (int n in Enumerable.Range(-100, 100))
                {
                    string m = n.ToString();
                    res2 = res.Replace("□", m);

                    //計算過程
                    //ここも1桁の場合でしか稼働できないため、2桁以上の場合を考える必要あり。
                    Stack<string> inputRPN = new Stack<string>();//逆ポーランド記法化した式を入れるスタック
                    Stack<double> calcResult = new Stack<double>();//計算結果を入れるスタック

                    foreach (char s in res2)//逆ポーランド記法の式をスタックに積む
                    {
                        inputRPN.Push(s.ToString());
                    }

                    Stack<string> reversedRPN = new Stack<string>();

                    while (inputRPN.Count > 0)//式の積み方を逆順にしたスタック
                    {
                        reversedRPN.Push(inputRPN.Pop());
                    }

                    while (reversedRPN.Count > 0)
                    {
                        string token = reversedRPN.Pop();
                        double token_double;
                        if (double.TryParse(token, out token_double))//数値の場合
                        {
                            calcResult.Push(token_double);
                        }

                        else
                        {
                            if (token == "+")
                            {
                                double A = calcResult.Pop();
                                double B = calcResult.Pop();
                                calcResult.Push(B + A);
                            }

                            if (token == "-")
                            {
                                double A = calcResult.Pop();
                                double B = 0;
                                if (calcResult.Count > 0)
                                {
                                    B = calcResult.Pop();
                                }
                                calcResult.Push(B - A);
                            }

                            if (token == "*")
                            {
                                double A = calcResult.Pop();
                                double B = calcResult.Pop();
                                calcResult.Push(B * A);
                            }

                            if (token == "/")
                            {
                                double A = calcResult.Pop();
                                double B = calcResult.Pop();
                                calcResult.Push(B / A);
                            }
                        }
                    }

                    //□に数字を代入して計算した結果がans(0)と同じ場合、
                    //resultTextをm(□への代入値)とする。
                    if (calcResult.Peek().ToString() == ans)
                    {
                        resultText.Text = m;
                    }

                }
            }


            //式が"= 0"で終わるとき
            //計算過程未挿入
            else if (str.Contains("=0"))
            {
                string f1 = str.Substring(0, str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                char[] F = f3.ToCharArray();

                Stack<char> buffer = new Stack<char>();
                Stack<char> st = new Stack<char>();
                resultText.Text = string.Empty;

                foreach (char token in F)
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
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        case '+':
                        case '-':
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '*' || st.Peek() == '/' || st.Peek() == '+' || st.Peek() == '-')
                                {
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        default:
                            buffer.Push(token);
                            break;
                    }
                }

                while (st.Count > 0)
                {
                    buffer.Push(st.Pop());
                }
                string r = new string(buffer.Reverse().ToArray());
                resultText.Text = resultText.Text + r + "\r\n";
            }


            //式が"0 ="で始まるとき
            //計算過程未挿入
            else
            {
                string f1 = str.Substring(str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                char[] F = f3.ToCharArray();

                Stack<char> buffer = new Stack<char>();
                Stack<char> st = new Stack<char>();
                resultText.Text = string.Empty;

                foreach (char token in F)
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
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        case '+':
                        case '-':
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '*' || st.Peek() == '/' || st.Peek() == '+' || st.Peek() == '-')
                                {
                                    buffer.Push(st.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            st.Push(token);
                            break;

                        default:
                            buffer.Push(token);
                            break;
                    }
                }

                while (st.Count > 0)
                {
                    buffer.Push(st.Pop());
                }
                string r = new string(buffer.Reverse().ToArray());
                resultText.Text = resultText.Text + r + "\r\n";
            }
        }
    }
}
