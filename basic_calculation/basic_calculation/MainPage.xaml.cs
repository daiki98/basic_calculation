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
            Button button = (Button)sender;
            if (this.questionText.Text.Length > 0)
            {
                this.questionText.Text = this.questionText.Text.Substring(0, this.questionText.Text.Length - 1);
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
                //resultText.Text = f;
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
                string r = new String(buffer.Reverse().ToArray<char>());
                resultText.Text = resultText.Text + r + "\r\n";


            }

            else if(str.Contains("=0"))
            {
                string f1 = str.Substring(0, str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                //resultText.Text = f;
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
                string r = new String(buffer.Reverse().ToArray<char>());
                resultText.Text = resultText.Text + r + "\r\n";
            }
            else
            {
                string f1= str.Substring(str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                string f3 = f2.Replace("÷", "/");
                //resultText.Text = f;
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
                string r = new String(buffer.Reverse().ToArray<char>());
                resultText.Text = resultText.Text + r + "\r\n";
            }
        }
    }
}
