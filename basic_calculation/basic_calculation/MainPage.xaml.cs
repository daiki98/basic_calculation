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

            if (pressed == "分数")
            {
                SDbutton.Text = "小数";
                SDnumber = 0;

            }
            else if (pressed == "小数")
            {
                SDbutton.Text = "分数";
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
                        str.Contains("(=") || str.Contains("=)") || str.Contains("==") || str.Contains("%%") || str.Contains("()") || str.Contains(")(")||
                        str.Contains(".+") || str.Contains(".×") || str.Contains(".÷") || str.Contains("./") || str.Contains(".%") ||
                        str.Contains("+.") || str.Contains("-.") || str.Contains("×.") || str.Contains("÷.") || str.Contains("%.")||
                        str.Contains(".-") || str.Contains("=.") || str.Contains(".=") || str.Contains(".."))

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

                    {
                        bool FFjudL = judment.FF(Left);
                        bool FFjudR = judment.FF(Right);

                        if (FFjudL == true || FFjudR == true)
                        {
                            FFnum = 1;
                        }
                        else
                        {
                            FFnum = 0;
                        }
                        
                        //式 F(x)=Right(右辺)-Left(左辺)
                        string f1 = Right + "-(" + Left + ")";
                        string f2 = f1.Replace("×", "*");
                        char[] F = f2.ToCharArray();

                        string RPNres = Calculate.ReversePolishNotation(F);//  中置記法 →　後置記法(非分数）
                        string RPNres2 = RPNres.Replace("÷", "/");


                        double result_cal = Calculate.BisectionCal(RPNres2, FFnum);  //二分法答え(double)
                        //FFnum = 1;

                        if (result_cal == 595959595)
                        {
                            resultText.Text = "解なし";
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
                                if (SDnumber == 0)//小数表示
                                {
                                    string Calres2 = result_cal.ToString("F5");
                                    string Calres3 = Calres2.TrimEnd('0');
                                    if (Calres3.Substring(Calres3.Length - 1) == ".")
                                    {
                                        string Calres4 = Calres3.Replace(".", "");
                                        resultText.Text = Calres4;
                                    }
                                    else
                                    {
                                        resultText.Text = result_cal.ToString("F5").TrimEnd('0');
                                    }
                                }
                                else if (SDnumber == 1)//分数表示
                                {
                                    string num = result_cal.ToString("F9");
                                    string num2 = num.Substring(num.IndexOf(".") + 1).TrimEnd('0');
                                    if (num2.Length > 8)
                                    {
                                        if (loopanswer.Loop(result_cal) == "out")
                                        {
                                            resultText.Text = "out";
                                        }
                                        else
                                        {
                                            resultText.Text = loopanswer.Loop(result_cal);
                                        }
                                    }

                                    else
                                    {
                                        string Calres2 = result_cal.ToString("F5");
                                        string Calres3 = Calres2.TrimEnd('0');
                                        string s = Calres3.Substring(Calres3.IndexOf(".") + 1);//コンマの後の数値をs(=12)
                                        double sL = s.Length;//sL：sの桁数(2)
                                        double Deno = Math.Pow(10, sL);//分母：10のsL乗(=10^2=100)
                                        double Nume = double.Parse(Calres3) * Deno;//分子：小数値×10のsL乗(=0.12*100=12)

                                        //約分
                                        double G = Euclid.Gcd(Nume, Deno);//最大公約数G
                                        double Deno2 = Deno / G;
                                        double Nume2 = Nume / G;

                                        resultText.Text = Nume2.ToString() + "/" + Deno2.ToString();
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
