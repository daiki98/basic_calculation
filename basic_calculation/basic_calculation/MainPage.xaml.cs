using System;
using System.Linq;
using Xamarin.Forms;


namespace basic_calculation
{
    public partial class MainPage : ContentPage
    {

        int SDnumber = 1; //0-S,1-D
        int FFnum = -1; //1-分数関数 0-一次関数　2-高次関数
        string questionText = null;

        string numerator = null;　　//分子
        string denominator = null;　//分母
        string savestring = null;　　//保存用

        double AsympoteNum;　　//漸近線

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        void OnSelect(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            questionText += pressed;　　//計算用
            displayText.Text += pressed;　//表示用


           // displayText.CursorPosition += 1;　
        }

        //分数ボタン
        void OnF(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;

            //文字変更
            if (F.Text == " ") 
            {
                savestring = displayText.Text;
                F.Text = "分子";
                displayText.Text = "";
                button.Text = "決定";
            }
            else if (F.Text == "分母")
            {
                F.Text = " ";
                denominator = displayText.Text;
                displayText.Text = savestring + "{" + numerator + "/" + denominator + "}";
                questionText = savestring + "(" + numerator + ")"+ "/"  + "("+denominator + ")";
                button.Text = "分数";
            }
            else if (F.Text == "分子")
            {
                F.Text = "分母";
                numerator = displayText.Text;
                displayText.Text = "";
                button.Text = "決定";
            }

        }

        

        // Cボタン
        void OnClear(object sender, EventArgs e)
        {
            questionText = "";
            displayText.Text = "";
            resultText.Text = "";
        }

        //Delボタン
        void OnDel(object sender, EventArgs e)
        {
            if (questionText.Length > 0)
            {
                questionText = questionText.Substring(0, questionText.Length - 1);
            }
            if (displayText.Text.Length > 0)
            {
                displayText.Text = displayText.Text.Substring(0, displayText.Text.Length - 1);
            }
            displayText.CursorPosition = displayText.Text.Length;
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
           // resultText.Text = denominator;
            string str = questionText;
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
                        str.Contains("(=") || str.Contains("=)") || str.Contains("==") || str.Contains("%%") || str.Contains("()") || str.Contains(")(") ||
                        str.Contains(".+") || str.Contains(".×") || str.Contains(".÷") || str.Contains("./") || str.Contains(".%") ||
                        str.Contains("+.") || str.Contains("-.") || str.Contains("×.") || str.Contains("÷.") || str.Contains("%.") ||
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
                        bool HOEjudL = judment.HOE(Left);
                        bool HOEjudR = judment.HOE(Right);

                        if (HOEjudL == true || HOEjudR == true)
                        {
                            FFnum = 2;
                        }

                        if (FFjudL == true || FFjudR == true)
                        {
                            FFnum = 1;
                           //AsympoteNum = judment.Asympote(denominator);
                        }

                        if (HOEjudL == false && HOEjudR == false && FFjudL == false && FFjudR == false)
                        {
                            FFnum = 0;
                        }

                        //式 F(x)=Right(右辺)-Left(左辺)
                        string f1 = Right + "-(" + Left + ")";

                      //*の変換
                        string f2 = f1.Replace("×", "*");
                        string f3 = f2.Replace("0(", "0・(");
                        string f4 = f3.Replace("1(", "1・(");
                        string f5 = f4.Replace("2(", "2・(");
                        string f6 = f5.Replace("3(", "3・(");
                        string f7 = f6.Replace("4(", "4・(");
                        string f8 = f7.Replace("5(", "5・(");
                        string f9 = f8.Replace("6(", "6・(");
                        string f10 = f9.Replace("7(", "7・(");
                        string f11 = f10.Replace("8(", "8・(");
                        string f12 = f11.Replace("9(", "9・(");
                       string f13 = f12.Replace("□(", "□・(");
                       char[] F = f13.ToCharArray();

                        string RPNres = Calculate.ReversePolishNotation(F);//  中置記法 →　後置記法(非分数）

                        

                        string RPNres2 = RPNres.Replace("÷", "/");
                        //resultText.Text = RPNres2;
                        //resultText.Text = questionText;
                       
                        /* 確認用
                         * 分母に□が2つあるときにエラー
                         * denominatorのRPNresがおかしそう
                         * (□×□）→□＊□＊で出力
                         * (□＋３）×(□ー３）→□３＋＊□＊３ーで出力
                         * 
                        char[] F1 = denominator.ToCharArray();
                        string RPNres = Calculate.ReversePolishNotation(F1);//  中置記法 →　後置記法(非分数）
                        string f21 = RPNres.Replace("×", "*");
                        resultText.Text = f21;
                        */

               //     } } } } } }

                        double result_cal = Calculate.BisectionCal(RPNres2, FFnum,AsympoteNum);  //二分法答え(double)
                        

                        if (result_cal == 595959595)
                        {
                            resultText.Text = "sorry...";
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
                                        resultText.Text = result_cal.ToString("F8").TrimEnd('0');
                                    }
                                }

                                else if (SDnumber == 1)//分数表示
                                {
                                    string num = result_cal.ToString("F8");
                                    string num2 = num.Substring(num.IndexOf(".") + 1).TrimEnd('0');

                                    //循環小数のとき
                                    if (num2.Length >= 7)
                                    {
                                        if (loopanswer.Loop(result_cal) == "out")
                                        {
                                            string ansd = Calculate.Calculation_Predict(RPNres2,result_cal);

                                            resultText.Text = ansd;
                                            /*
                                            if (Calculate.Calculation_F1_500(RPNres2) != "Out of Range" && Calculate.Calculation_F500_1000(RPNres2) == "Out of Range")
                                            {
                                                string ansd = Calculate.Calculation_F1_500(RPNres2);
                                                resultText.Text = ansd;
                                            }
                                            else if (Calculate.Calculation_F500_1000(RPNres2) != "Out of Range" && Calculate.Calculation_F1_500(RPNres2) == "Out of Range")
                                            {
                                                string ansd = Calculate.Calculation_F500_1000(RPNres2);
                                                resultText.Text = ansd;
                                            }
                                            else
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
                                                    resultText.Text = result_cal.ToString("F8").TrimEnd('0');
                                                }
                                            }
                                        */   // 
                                        }

                                        else
                                        {
                                            resultText.Text = loopanswer.Loop(result_cal);
                                        }
                                    }

                                    //循環小数ではないとき
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

                                        if (Deno2.ToString() == "1")
                                        {
                                            resultText.Text = Nume2.ToString();
                                        }
                                        else
                                        {
                                            resultText.Text = Nume2.ToString() + "/" + Deno2.ToString();
                                        }
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
  