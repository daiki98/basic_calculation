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

        int currentState = 0;

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
                string space = " ";
                char Space = space[0];
                


                //逆ポーランド記法化
                Stack<char> buffer = new Stack<char>();//バッファスタック
                Stack<char> st = new Stack<char>();//作業用スタック

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
                            if(st.Count > 0 && buffer.Count > 0 )
                            {
                                if(currentState > 0)
                                {
                                    buffer.Push(Space);
                                    buffer.Push(token);
                                    currentState *= 0;
                                    break;
                                }
                                else if(currentState == 0)
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
                string res = resultText.Text + r + "\r\n";


                //計算部分
                //流れ
                //1.□にある数字を代入
                //2.□のない式で計算
                //3.解が０になった場合、画面に□に代入した値を表示

                string ans = "0";
                string res2;

                //スタック
                Stack<string> inputRPN = new Stack<string>();//逆ポーランド記法化した式を入れるスタック
                Stack<double> calcResult = new Stack<double>();//計算結果を入れるスタック
                Stack<string> reversedRPN = new Stack<string>();//inputRPNを逆順（正しい順）で積むスタック

                foreach (int n in Enumerable.Range(-10, 10))//とりあえず-10～10の中から探す（n(m)が代入値）
                {
                    string m = n.ToString();
                    res2 = res.Replace("□", m);

                    foreach (char x in res2)//逆ポーランド記法の式をスタックに積む
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
                                calcResult.Push(B3 * A3);
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
                        resultText.Text = m;
                        break;
                    }

                    else if (calcResult.Peek().ToString() != ans)
                    {
                        break;
                    }
                    break;
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
                string space = " ";
                char Space = space[0];



                //逆ポーランド記法化
                Stack<char> buffer = new Stack<char>();//バッファスタック
                Stack<char> st = new Stack<char>();//作業用スタック

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
                string res = resultText.Text + r + "\r\n";
            }


            //式が"0 ="で始まるとき
            //計算過程未挿入
            else
            {
                string f1 = str.Substring(str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                char[] F = f3.ToCharArray();
                string space = " ";
                char Space = space[0];



                //逆ポーランド記法化
                Stack<char> buffer = new Stack<char>();//バッファスタック
                Stack<char> st = new Stack<char>();//作業用スタック

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
                string res = resultText.Text + r + "\r\n";
            }
        }
    }
}
