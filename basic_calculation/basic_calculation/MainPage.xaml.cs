using System;
using System.Linq;
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

        void OnSelect(object sender, EventArgs e)
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
            if (str.Count(x => x == '=') == 1 && str.Contains("□"))
            {
                if (str.Contains("++") || str.Contains("+×") || str.Contains("+÷") || str.Contains("+/") || str.Contains("+%") ||
                        str.Contains("-+") || str.Contains("-×") || str.Contains("-÷") || str.Contains("-/") || str.Contains("-%") ||
                        str.Contains("×+") || str.Contains("××") || str.Contains("×÷") || str.Contains("×/") || str.Contains("×%") ||
                        str.Contains("÷+") || str.Contains("÷×") || str.Contains("÷÷") || str.Contains("÷/") || str.Contains("÷%") ||
                        str.Contains("/+") || str.Contains("/×") || str.Contains("/÷") || str.Contains("//") || str.Contains("/%") ||
                        str.Contains("=+") || str.Contains("=×") || str.Contains("=÷") || str.Contains("=/") || str.Contains("=%") ||
                        str.Contains("+=") || str.Contains("×=") || str.Contains("÷=") || str.Contains("/=") ||
                        str.Contains("(+") || str.Contains("(×") || str.Contains("(÷") || str.Contains("(/") || str.Contains("(%") ||
                        str.Contains("+)") || str.Contains("×)") || str.Contains("÷)") || str.Contains("/)") || str.Contains("-)") ||
                        str.Contains("(=") || str.Contains("=)") || str.Contains("==") || str.Contains("%%") || str.Contains("()") || str.Contains(")(")||
                        str.Contains("..") || str.Contains("..."))
                {
                    resultText.Text = "Wrong Input";
                }

                else
                {
                    //左辺切り出し
                    string Left = str.Substring(0, str.IndexOf("="));

                    //右辺切り出し
                    string Right = str.Substring(str.IndexOf("=") + 1);

                    //式 F(x)=Right(右辺)-Left(左辺)
                    string f1 = Right + "-(" + Left + ")";
                    string f2 = f1.Replace("×", "*");
                    char[] F1 = f2.ToCharArray();

                    string RPNres = Calculate.ReversePolishNotation(F1);

                    string RPNres2 = RPNres.Replace("÷", "/");

                    //二分法
                    string Calres = Calculate.BisectionCal(RPNres2).ToString("F7").TrimEnd('0');

                    if (Calres.Contains("."))
                    {
                        int num2 = Calres.Split('.')[1].Length;

                        if (num2 > 5)//分数
                        {
                            string f3 = f2.Replace(")/", ")÷");
                            string f4 = f3.Replace("/(", "÷(");
                            char[] F2 = f4.ToCharArray();

                            string RPNres1 = Calculate2.ReversePolishNotation(F2);

                            string Calres2 = Calculate2.Calculation(RPNres1);

                            resultText.Text = Calres2;
                        }
                        else
                        {
                            resultText.Text = Calres;
                        }
                    }
                }
            }

            else if(str.Count(x => x == '=') > 1)
            {
                resultText.Text = "Many =";
            }

            else
            {
                resultText.Text = "Not Exsit □ or =";
            }
        }
    }
}
