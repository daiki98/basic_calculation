using System;
using System.Linq;
using Xamarin.Forms;


namespace basic_calculation
{
    public partial class MainPage : ContentPage
    {

        int SDnumber = 1; //0-S,1-D
        int FFnum = 0; //1-分数関数

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
            Button SDbutton = (Button)sender;
            string pressed = SDbutton.Text;

            if (pressed == "D")
            {
                SDbutton.Text = "F";
                SDnumber = 0;
                SD.Text = "小数";

            }
            else if (pressed == "F")
            {
                SDbutton.Text = "D";
                SDnumber = 1;
                SD.Text = "分数";
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

                    if (L < 1 || R < 1)
                    {
                        resultText.Text = "Wrong";  //＝の後になんもないやつ
                    }
                    else
                    {    //分数関数,高次方程式か判断(多分全然足りてない）//またあとで考えるし，とりあえずこれで
                        if (str.Contains("/□") || str.Contains("/2□") || str.Contains("/3□") || str.Contains("/4□") || str.Contains("/5□") || str.Contains("/6□")
                            || str.Contains("/7□") || str.Contains("/8□") || str.Contains("/9□") || str.Contains("÷□") || str.Contains("÷2□") || str.Contains("÷3□")
                            || str.Contains("÷4□") || str.Contains("÷5□") || str.Contains("÷6□") || str.Contains("÷7□") || str.Contains("÷8□") || str.Contains("÷9□")
                            || str.Contains("×□") || str.Contains("×2□") || str.Contains("×3□") || str.Contains("×4□") || str.Contains("×5□") || str.Contains("×6□")
                            || str.Contains("×7□") || str.Contains("×8□") || str.Contains("×9□") || str.Contains("□×") || str.Contains("/(□") || str.Contains("/(2□") ||
                            str.Contains("/(3□") || str.Contains("/(4□") || str.Contains("/(5□") || str.Contains("/(6□") || str.Contains("/(7□") || str.Contains("/(8□") ||
                            str.Contains("/(9□") || str.Contains("÷(□") || str.Contains("÷(2□") || str.Contains("÷(3□") || str.Contains("÷(4□") || str.Contains("÷(5□")
                            || str.Contains("÷(6□") || str.Contains("÷(7□") || str.Contains("÷(8□") || str.Contains("÷(9□"))
                        {
                            FFnum = 1;
                        }

                        //式 F(x)=Right(右辺)-Left(左辺)
                        string f1 = Right + "-(" + Left + ")";
                        string f2 = f1.Replace("×", "*");
                        char[] F = f2.ToCharArray();

                        string RPNres = Calculate.ReversePolishNotation(F);//  中置記法 →　後置記法(非分数）
                        string RPNres2 = RPNres.Replace("÷", "/");


                        double result_cal = Calculate.BisectionCal(RPNres2, FFnum);  //二分法答え(double)
                        FFnum = 0;

                        if (result_cal == 595959595)
                        {
                            string f3 = f2.Replace(")/", ")÷");
                            string f4 = f3.Replace("/(", "÷(");
                            string f5 = f4.Replace("/□", "÷□");
                            string f6 = f5.Replace("□/", "□÷");
                            char[] F2 = f6.ToCharArray();

                            string RPNres_f = Calculate.ReversePolishNotation_Fraction(F2);
                            //resultText.Text = RPNres_f;
                            string Cal = Calculate.Calculation_Fraction(RPNres_f);
                            if (SDnumber == 0)
                            {
                                decimal CalL = decimal.Parse(Cal.Substring(0, Cal.IndexOf("/")));
                                decimal CalR = decimal.Parse(Cal.Substring(Cal.IndexOf("/") + 1));
                                decimal CalA = CalL / CalR;
                                string CalA2 = CalA.ToString("F3");
                                string CalA3 = CalA2.TrimEnd('0');
                                if (CalA3.Substring(CalA3.Length - 1) == ".")
                                {
                                    string CalA4 = CalA3.Replace(".", "");
                                    resultText.Text = CalA4;
                                }
                                else
                                {
                                    resultText.Text = CalA3;
                                }
                            }

                            else if (SDnumber == 1)
                            {
                                string Cal2 = Cal.Substring(Cal.IndexOf("/") + 1);
                                if (Cal2 == "1")
                                {
                                    string Cal3 = Cal.Replace("/1", "");
                                    resultText.Text = Cal3;
                                }
                                else
                                {
                                    resultText.Text = Cal;
                                }
                            }
                            FFnum = 0;
                        }
                        else
                        {
                            bool IMjub = Calculate.IntMinJub(result_cal);       //答えが整数か少数か(trueで整数）；

                            if (IMjub)
                            {
                                string Calres2 = result_cal.ToString("F0");  //整数表示
                                resultText.Text = Calres2;
                            }
                            else
                            {
                                if (SDnumber == 0)                 //小数表示
                                {
                                    string Calres2 = result_cal.ToString("F3");
                                    string Calres3 = Calres2.TrimEnd('0');
                                    if (Calres3.Substring(Calres3.Length - 1) == ".")
                                    {
                                        string Calres4 = Calres3.Replace(".", "");
                                        resultText.Text = Calres4;
                                    }
                                    else
                                    {
                                        resultText.Text = Calres3;
                                    }
                                }
                                else if (SDnumber == 1)               //分数表示
                                {
                                    string f3 = f2.Replace(")/", ")÷");
                                    string f4 = f3.Replace("/(", "÷(");
                                    string f5 = f4.Replace("/□", "÷□");
                                    string f6 = f5.Replace("□/", "□÷");
                                    char[] F2 = f6.ToCharArray();

                                    //resultText.Text = f6;

                                    string RPNres_f = Calculate.ReversePolishNotation_Fraction(F2);
                                    //resultText.Text = RPNres_f;
                                    string Cal = Calculate.Calculation_Fraction(RPNres_f);
                                    //resultText.Text = Cal;
                                    string Cal2 = Cal.Substring(Cal.IndexOf("/") + 1);
                                    if (Cal2 == "1")
                                    {
                                        string Cal3 = Cal.Replace("/1", "");
                                        resultText.Text = Cal3;
                                    }
                                    else
                                    {
                                        resultText.Text = Cal;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            else if (str.Count(x => x == '=') > 1)
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
