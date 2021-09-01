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
        //ステイタス判断してないしいまはいらんかな？？
        //今後使わなそうなら削除
        //int currentState = 1;

        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);
        }

        
    /*  つかってないし削除？？
        
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            _ = e.NewTextValue;
        }

        void OnEntryCompleted(object sender, EventArgs e)
        {
            _ = ((Entry)sender).Text;
        }
    */

        // 数字
        void OnSelectNumber(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;

            /*　ここもいらない？？
            if (questionText.Text == "" || currentState < 0)
            {
                questionText.Text = "";
                if (currentState < 0)
                {
                    currentState *= -1;
                }
            }
            */
        }


        // □（解）
        void OnAnswer(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;

            /*　ここもいらない
            if (questionText.Text == "" || currentState < 0)
            {
                questionText.Text = "";
                if (currentState < 0)
                {
                    currentState *= -1;
                }
            }
            */
        }


        // 演算子
        void OnSelectOperator(object sender, EventArgs e) 
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            questionText.Text += pressed;

            /* ここもいらない
            if (questionText.Text == "" || currentState < 0)
            {
                questionText.Text = "";
                if (currentState < 0)
                {
                    currentState *= -1;
                }
            }
            */
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

            /* ここもいらない？？
            if (questionText.Text == "" || currentState < 0)
            {
                questionText.Text = "";
                if (currentState < 0)
                {
                    currentState *= -1;
                }
            }
            */
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

        /*      やること
         *     ★ Fを逆ポーランド法で()を消した状態する
         *      ①式変形　()をない形にする
         *      ②計算     
         *      
         *      提案方法(ダイスケ）
         *      操車場アルゴリズムで順序変更(計算も？？）
         *      
         *      操車場アルゴリズムはClassificationYardのクラスに
         *      
         *     ★()がなく，□を含む式を□について解く
         *       二分法を使って解く
         *       アルゴリズムはbesectionCalulationのクラスに記述
         *      
         */

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
                string F = Right + "-(" + Left + ")";
                resultText.Text = F;

                
            }
            else if(str.Contains("=0"))
            {
                string F = str.Substring(0, str.IndexOf("="));
                
                resultText.Text = F;
            }
            else
            {
                string f= str.Substring(str.IndexOf("="));
                string F = f.Substring(1);
                resultText.Text = F;
            }
        }
    }
}
