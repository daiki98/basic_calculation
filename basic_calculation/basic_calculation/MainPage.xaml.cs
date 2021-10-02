using System;
using System.Linq;
using Xamarin.Forms;


namespace basic_calculation
{
    public partial class MainPage : ContentPage
    {

        int SDnumber = 0; //0-S,1-D
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

        //分数，小数変換ボタン
        void OnSelectSD(object sender, EventArgs e)
        {
            Label SDbutton = (Label)sender;
            string pressed = SDbutton.Text;

            if (pressed == "小数")
            {
                SDbutton.Text = "分数";
                SDnumber = 0;

            }else if (pressed == "分数")
            {
                SDbutton.Text = "小数";
                SDnumber = 1;
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
                        str.Contains("(=") || str.Contains("=)") || str.Contains("==") || str.Contains("%%") || str.Contains("()") || str.Contains(")("))
                {
                    resultText.Text = "Wrong";
                }

                else
                {

                    //左辺切り出し
                    string Left = str.Substring(0, str.IndexOf("="));
                    int L = Left.Length;

                    //右辺切り出し
                    string Right = str.Substring(str.IndexOf("=") + 1);
                    int R = Right.Length;

                    if (L < 1||R<1)
                    {
                        resultText.Text = "Wrong";  //＝の後になんもないやつ
                    }
                    else
                    {

                        //式 F(x)=Right(右辺)-Left(左辺)
                        string f1 = Right + "-(" + Left + ")";
                        string f2 = f1.Replace("×", "*");
                        char[] F = f2.ToCharArray();

                        string RPNres = Calculate.ReversePolishNotation(F);       //  中置記法 →　後置記法(非分数）
                        string RPNres2 = RPNres.Replace("÷", "/");


                        double result_cal = Calculate.BisectionCal(RPNres2);  //二分法答え(double)
                        bool IMjub = Calculate.IntMinJub(result_cal);       //答えが整数か少数か(trueで整数）；

                        if (IMjub)
                        {
                            String Calres2 = result_cal.ToString("F0");  //整数表示
                            resultText.Text = Calres2;
                        }
                        else
                        {
                            if (SDnumber == 0)                 //小数表示
                            {
                                String Calres2 = result_cal.ToString("F3");
                                resultText.Text = Calres2;
                            }
                            else if (SDnumber == 1)               //分数表示
                            {
                                string f3 = f2.Replace(")/", ")÷");
                                string f4 = f3.Replace("/(", "÷(");
                                string f5 = f4.Replace("/□", "÷□");
                                char[] F2 = f5.ToCharArray();

                                string RPNres_f = Calculate.ReversePolishNotation_Fraction(F2);
                                string Cal = Calculate.Calculation_Fraction(RPNres_f);
                                resultText.Text = Cal;
                            }
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
