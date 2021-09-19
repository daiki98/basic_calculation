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
            if (str.Contains("=") && !str.StartsWith("0=") && !str.EndsWith("=0"))
            {
                //左辺切り出し
                string Left = str.Substring(0, str.IndexOf("="));

                //右辺切り出し
                string Right = str.Substring(str.IndexOf("=") + 1);

                //式 0=F(x)  F(x)=Right(右辺)-Left(左辺)
                string f1 = Right + "-(" + Left + ")";
                string f2 = f1.Replace("×", "*");
                char[] F = f2.ToCharArray();

                string RPNres = Calculate.ReversePolishNotation(F);

                //resultText.Text = RPNres;

                string RPNres2 = RPNres.Replace("÷", "/");

                //resultText.Text = RPNres2;

                string Calres = Calculate.Calculation(RPNres2);

                resultText.Text = Calres;
            }


            //式が"= 0"で終わるとき
            //計算過程未挿入
            else if (str.EndsWith("=0"))
            {
                string f1 = str.Substring(0, str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                char[] F = f2.ToCharArray();

                string RPNres = Calculate.ReversePolishNotation(F);

                resultText.Text = RPNres;

                //string RPNres2 = RPNres.Replace("÷", "/");

                //resultText.Text = RPNres2;

                //string Calres = Calculate.Calculation(RPNres2);

                //resultText.Text = Calres;
            }


            //式が"0 ="で始まるとき
            //計算過程未挿入
            else if (str.StartsWith("0="))
            {
                string f1 = str.Substring(str.IndexOf("="));
                string f2 = f1.Replace("×", "*");
                char[] F = f2.ToCharArray();

                string RPNres = Calculate.ReversePolishNotation(F);

                resultText.Text = RPNres;

                //string RPNres2 = RPNres.Replace("÷", "/");

                //resultText.Text = RPNres2;

                //string Calres = Calculate.Calculation(RPNres2);

                //resultText.Text = Calres;
            }
        }
    }


}
