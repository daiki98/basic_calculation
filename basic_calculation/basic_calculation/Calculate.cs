using System;
using System.Collections.Generic;
using System.Linq;

namespace basic_calculation
{
    public static class Calculate
    {
   
        /*   
         * 
         * 　 BisectionCal() 二分法
         * 　 ToRoundDown() 少数まるめ？？？
         *    Calculation_forBisection() 二分法の計算
         *    ReversePolishNotation() 中置記法　→　後置記法(非分数）
         *    Calculation()  //後置記法　→　計算(非分数）
         *    ReversePolishNotation_Fraction() 中置記法　→　後置記法(分数）
         *    Calculation_Fraction()  後置記法　→　計算(分数）
         *    IntMinJub()（整数，少数判定） 
         *    
         */

        public static double BisectionCal(string input, int ffnum,string left,string right)
        {
            double initial_val1 = 10000d;       //正の初期値
            double initial_val2 = -10000d;      //負の初期値
            double mid_val = 0;     //中間値

            double res_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));      //解きたい式に初期値1を代入したときの値
            double res_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));      //解きたい式に初期値2を代入したときの値

            double jub = res_initial1 * res_initial2;       //範囲の中に解があるか判断するやつ　正ー無し，負ーあり

            if (ffnum == 2)//高次方程式
            {
                if (jub >= 0)//初期値で答えが無い場合
                { 
                    initial_val1 = 10000d;       //正の初期値
                     initial_val2 = 0.0001d;      //負の初期値
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));        //再計算
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                    double Rejub = REres_initial1 * REres_initial2;

                    //プラス側に範囲をずらす
                    for (int i = 0; i < 100000; i++)
                    {
                        //解が見つかった場合
                        if (Rejub < 0)
                        {
                            while (Math.Abs(initial_val1 - initial_val2) > 0.0000000001)          //ここで精度決める
                            {
                                mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                                double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                                if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                                {
                                    initial_val1 = mid_val;
                                }
                                else
                                {
                                    initial_val2 = mid_val;
                                }
                            }
                            ffnum = 0;
                            return ToRoundDown(mid_val, 9);
                        }
                        else
                        {
                            initial_val1 += 0.01d;
                            initial_val2 += 0.01d;
                            REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                            REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                            Rejub = REres_initial1 * REres_initial2;
                        }
                    }

                    initial_val1 = 0.0001d;       //初期値をリセット
                    initial_val2 = -10000d;
                    REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                    Rejub = REres_initial1 * REres_initial2;

                    //プラス側に無く，マイナス側に範囲ずらす
                    for (int i = 0; i < 100000; i++)
                    {
                        //解が見つかった時
                        if (Rejub < 0)
                        {
                            while (Math.Abs(initial_val1 - initial_val2) > 0.0000000001)          //ここで精度決める
                            {
                                mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                                double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                                if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                                {
                                    initial_val1 = mid_val;
                                }
                                else
                                {
                                    initial_val2 = mid_val;
                                }
                            }
                            return ToRoundDown(mid_val, 9);
                        }

                        else
                        {   
                            initial_val1 -= 0.01d;
                            initial_val2 -= 0.01d;

                            REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                            REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                            
                            Rejub = REres_initial1 * REres_initial2;
                        }
                    }

                    //プラスにもマイナスにも解がない場合
                    if (Rejub > 0)
                    {

                        return 595959595d;

                    }
                    //初期値で答えがある場合（高次，分数関数）
                }

                else 
                {
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));
                    double Rejub = REres_initial1 * REres_initial2;

                    while (Math.Abs(initial_val1 - initial_val2) > 0.0000000001)          //ここで精度決める
                    {
                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }
                    }
                }           
                return ToRoundDown(mid_val, 9);
            }

            //1次方程式の時
            else if (ffnum == 0)
            {
                if (jub >= 0)
                {
                    initial_val1 += 10000;
                    initial_val2 -= 10000;
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                    double Rejub = REres_initial1 * REres_initial2;

                    while (Rejub >= 0)
                    {
                        initial_val1 += 10000;
                        initial_val2 -= 10000;
                        REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                        REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                        Rejub = REres_initial1 * REres_initial2;
                    }

                    while (Math.Abs(initial_val1 - initial_val2) > 0.000000001)          //ここで精度決める
                    {
                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (REres_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }
                    }
                }

                else if (jub < 0)
                {
                    while (Math.Abs(initial_val1 - initial_val2) > 0.0000000001)          //ここで精度決める
                    {

                        mid_val = (initial_val1 + initial_val2) / 2;            //中間値の再計算

                        double res_mid = double.Parse(Calculation_forBisection(input, mid_val));        //中間値の値

                        if (res_initial1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                        {
                            initial_val1 = mid_val;

                        }
                        else
                        {
                            initial_val2 = mid_val;

                        }
                    }
                }
                return ToRoundDown(mid_val,9);
            }

            else if (ffnum == 1)
            {
                //分数関数の時　総当たり
                //漸近線付近に解を想定

                if (left != null&&right==null)
                {
                    int S_num = judment.CountChar(left, "/"); //"/"の数
                    int S_index = left.IndexOf("/");
                   

                    // "/"の全位置確認
                    int[] nums = new int[S_num];
                    string input2 = input;
                    for (int i = 0; i <= S_num - 1; i++)
                    {
                        nums[i] = input2.IndexOf('/');
                        input2 = input2.Substring(nums[i] + 1);
                    }

                    //"/"以下を確認
                    for (int i = 0; i <= S_num - 1; i++)
                    {
                        string next_char = input.Substring(nums[i] + 1, 1);//"/"の後の1文字を切り出し
                        if (next_char == "□")
                        {
                            string reinput = input.Substring(nums[i] + 2);  //□の次の位置から最後まで
                            int ppos = reinput.IndexOf("+");
                            int mpos = reinput.IndexOf("-");
                            if (ppos == 0 || mpos == 0||(ppos==-1&&mpos==-1))
                            {
                                //0付近に答え
                            }
                            else if(ppos!=0&&ppos!=-1)
                            {
                                string cal = input.Substring(nums[i] + 1, ppos);
                                // calの計算結果が漸近線
                            }else if (mpos != 0&&mpos!=-1)
                            {
                                string cal = input.Substring(nums[i] + 1, mpos);
                                // calの計算結果が漸近線
                            }
                        }
                        else if (next_char == "(")
                        {
                            string reinput = input.Substring(nums[i] + 1); //（以降を切り出し
                            string re2input = reinput.Substring(0, reinput.IndexOf(')'));//)の前まで切り出し

                            //leftに"("が2つ以上ある場合は一番最後の(と）の全位置取得
                            //( () ) と()() ()+()に場合分け
                            
                            //1つの場合はreinputの答えが漸近線
                        }
                        else
                        {
                            string next2_char;
                            int l_input = input.Length;//式の長さ

                            for (int j = 1; j < l_input; j++)
                            {
                                next2_char = input.Substring(nums[i] + j, 1);
                                if (next2_char == "□" )
                                {
                                    string next3_char;
                                    if (nums[i] + j + 1 < l_input)
                                    {
                                        next3_char = input.Substring(nums[i] + j+1, 1);
                                    }
                                    else
                                    {
                                        // 0が漸近線
                                    }

                                    if (next3_char == "(")
                                    {
                                        //leftに"("が2つ以上ある場合は一番最後の(と）の全位置取得
                                        //( () ) と()() ()+()に場合分け

                                        //1つの場合はreinputの答えが漸近線
                                    }

                                }
                                else if (next2_char == "(")
                                {
                                    //( () ) と()() ()+()に場合分け
                                    //()内の答えが漸近線
                                }
                            }

                        }


                    }

                }
                else if (right != null&&left==null)
                {

                }else if (left != null && right != null)
                {

                }








        /*
                if (Calculation(input) != "Out of Range")
                {
                    double ansd = double.Parse(Calculation(input));
                    return ToRoundDown(ansd, 9);
                }
                else
                {
                    if (Calculation_F(input) != "Out of Range")
                    {
                        string ansd = Calculation_F(input);
                        return ToRoundDown(double.Parse(ansd), 9);
                    }
                    else
                    {
                        return 595959595d;
                    }
                }
        */
            }
            return 595959595d;
        }


        public static double ToRoundDown(double dValue, int iDigits)
        {
            double dCoef = Math.Pow(10, iDigits);

            return dValue > 0 ? Math.Floor(dValue * dCoef) / dCoef :
                                Math.Ceiling(dValue * dCoef) / dCoef;
        }


        //後置記法　→　計算（二分法）
        public static string Calculation_forBisection(string input, double value)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];

            string res2 = input.Replace("□", value.ToString());
            string[] res3 = res2.Trim().Split(Space);
            foreach (string token in res3)
            {
                switch (token)
                {
                    case "+":
                        double A0 = calcResult.Pop();
                        double B0 = calcResult.Pop();
                        calcResult.Push(B0 + A0);
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
                        double AB2 = B2 * A2;
                        calcResult.Push(AB2);
                        break;

                    case "/":
                        
                        double A3 = calcResult.Pop();
                        double B3 = calcResult.Pop();
                        if (A3 == 0)
                        {
                            break;
                        }
                        else
                        {
                            double ans = B3 / A3;
                            string ans2 = ans.ToString("F12");
                            calcResult.Push(double.Parse(ans2));
                        }
                        break;

                    case "%":
                        double A4 = calcResult.Pop();
                        double B4 = 100;
                        calcResult.Push(A4 / B4);
                        break;

                    default:
                        calcResult.Push(double.Parse(token));
                        break;
                }
            }

            string result = (calcResult.Peek() * -1).ToString();

            return result;
        }

        //整数or小数
        public static bool IntMinJub(double input)
        {
            string reviseVal = input.ToString("F9");  //ここのF5は二分法の精度に合わせる
            double newinput = double.Parse(reviseVal);

            if ((int)newinput == newinput)
            {
                return true; //整数
            }
            else
            {

                return false;//少数
            }
        }



        //中置記法　→　後置記法
        public static string ReversePolishNotation(char[] input)
        {
            Stack<char> buffer = new Stack<char>();//バッファスタック
            Stack<char> st = new Stack<char>();//作業用スタック

            string space = " ";
            char Space = space[0];
            int currentState = 0;

            foreach (char token in input)
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
                                buffer.Push(Space);
                                buffer.Push(t);
                            }
                        }
                        break;

                    case '/':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else if (st.Peek() == '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                                st.Push('*');
                                currentState += 1;
                                break;
                            }
                            else if (st.Peek() == '*' && buffer.Peek() != '÷')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                        while (st.Count == 0 || st.Peek() == '+'|| st.Peek() == '-'|| st.Peek() == '(')
                        {
                            st.Push(token);
                            currentState += 1;
                            break;
                        }
                        currentState += 1;
                        break;

                    case '*':
                    case '÷':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷')
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
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-')
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

                    case '-':
                        if (buffer.Count > 0 && currentState == 0)
                        {
                            while (st.Count > 0)
                            {
                                if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-')
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
                        }

                        else if (buffer.Count == 0 && currentState == 0)
                        {
                            buffer.Push(token);
                            currentState *= 0;
                        }

                        else if (currentState > 0)
                        {
                            buffer.Push(Space);
                            buffer.Push(token);
                            currentState *= 0;
                        }
                        break;

                    case '□':
                        if (buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                currentState *= 0;
                            }
                            else if (currentState == 0 && st.Count > 0)
                            {
                                if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷')
                                {
                                    buffer.Push(Space);
                                    buffer.Push(st.Pop());
                                    buffer.Push(Space);
                                    buffer.Push(token);
                                    st.Push('*');
                                }
                                else
                                {
                                    buffer.Push(Space);
                                    buffer.Push(token);
                                    st.Push('*');
                                }
                            }
                            else if (currentState == 0 && st.Count == 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                                st.Push('*');
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                        }
                        currentState *= 0;
                        break;

                    case '%':
                        buffer.Push(Space);
                        buffer.Push(token);
                        break;

                    case '.':
                        buffer.Push(token);
                        break;


                    //数字の場合
                    default:
                        if (st.Count > 0 && buffer.Count > 0)
                        {
                            if (currentState > 0)
                            {
                                buffer.Push(Space);
                                buffer.Push(token);
                            }
                            else if (currentState == 0)
                            {
                                buffer.Push(token);
                            }
                        }
                        else
                        {
                            buffer.Push(token);
                        }
                        currentState *= 0;
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
            string res = r + "\r\n";

            return res;
        }



        //後置記法　→　計算（数字を入れて総当たり）
        public static string Calculation(string input)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;
            decimal m = -100.00M;//代入値の初期値

            for (double num = -100.00D; num <= 2000.00D; num += 0.01)//代入値(n)
            {
                string res2 = input.Replace("□", m.ToString());
                string[] res3 = res2.Trim().Split(Space);
                foreach (string token in res3)
                {
                    switch (token)
                    {
                        case "+":
                            double A0 = calcResult.Pop();
                            double B0 = calcResult.Pop();
                            calcResult.Push(B0 + A0);
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
                            double ans = B2 * A2;
                            string ans2 = ans.ToString("F5");
                            calcResult.Push(double.Parse(ans2));
                            break;

                        case "/":
                            double A3 = calcResult.Pop();
                            double B3 = calcResult.Pop();
                            if (A3 == 0)
                            {
                                break;
                            }
                            else
                            {
                                calcResult.Push(B3 / A3);
                                //decimal ans = B3 / A3;
                                //string ans2 = ans.ToString("F8");
                                //calcResult.Push(decimal.Parse(ans2));
                                break;
                            }

                        case "%":
                            double A4 = calcResult.Pop();
                            double B4 = 100;
                            calcResult.Push(A4 / B4);
                            break;

                        default:
                            calcResult.Push(double.Parse(token));
                            break;
                    }
                }

                if (calcResult.Peek() == 0)
                {
                    res = m.ToString("G29");
                    break;
                }

                m += 0.01M;

            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }




        //後置記法　→　計算（分数を入れて総当たり）*returnが小数
        public static string Calculation_F(string input)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;

            double B = 1000;
            for (double numB = 1; numB <= 1000; numB += 1)
            {
                double b = 100;
                for (double numb = 1; numb <= 200; numb += 1)
                {
                    string res2 = input.Replace("□", b.ToString() + " " + B.ToString() + " " + "/");
                    string[] res3 = res2.Trim().Split(Space);
                    foreach (string token in res3)
                    {
                        switch (token)
                        {
                            case "+":
                                double A0 = calcResult.Pop();
                                double B0 = calcResult.Pop();
                                calcResult.Push(B0 + A0);
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
                                double ans = B2 * A2;
                                string ans2 = ans.ToString("F5");
                                calcResult.Push(double.Parse(ans2));
                                break;

                            case "/":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                if (A3 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B3 / A3);
                                    double ans3 = B3 / A3;
                                    string ans4 = ans3.ToString("F8");
                                    string ans5 = ans4.Substring(ans4.IndexOf(".") + 1).Substring(0,3);
                                    if (ans5 == "000")
                                    {
                                        calcResult.Push(double.Parse(ans4.Substring(0, ans4.IndexOf("."))));
                                        break;
                                    }
                                    else if (ans5 == "999")
                                    {
                                        double ans6 = double.Parse(ans4.Substring(0, ans4.IndexOf(".")));
                                        calcResult.Push(ans6 + 1);
                                        break;
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans4));
                                        break;
                                    }
                                }

                            case "%":
                                double A4 = calcResult.Pop();
                                double B4 = 100;
                                calcResult.Push(A4 / B4);
                                break;

                            default:
                                calcResult.Push(double.Parse(token));
                                break;
                        }
                    }

                    if (calcResult.Peek() == 0)
                    {
                        double A = b/B;
                        res = A.ToString("G29");
                        break;
                    }
                    b -= 1;
                }
                B -= 1;
            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }



        //後置記法　→　計算（分数を入れて総当たり）*returnが分数
        //inputは後置記法の式
        public static string Calculation_F1_500(string input)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;

            double B = 500;
            for (double numB = 1; numB <= 500; numB += 1)
            {
                double b = 100;
                for (double numb = 1; numb <= 200; numb += 1)
                {
                    string res2 = input.Replace("□", b.ToString() + " " + B.ToString() + " " + "/");
                    string[] res3 = res2.Trim().Split(Space);
                    foreach (string token in res3)
                    {
                        switch (token)
                        {
                            case "+":
                                double A0 = calcResult.Pop();
                                double B0 = calcResult.Pop();
                                calcResult.Push(B0 + A0);
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
                                double ans = B2 * A2;
                                string ans2 = ans.ToString("F5");
                                calcResult.Push(double.Parse(ans2));
                                break;

                            case "/":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                if (A3 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B3 / A3);
                                    double ans3 = B3 / A3;
                                    string ans4 = ans3.ToString("F8");
                                    string ans5 = ans4.Substring(ans4.IndexOf(".") + 1).Substring(0, 3);
                                    if (ans5 == "000")
                                    {
                                        calcResult.Push(double.Parse(ans4.Substring(0, ans4.IndexOf("."))));
                                        break;
                                    }
                                    else if (ans5 == "999")
                                    {
                                        double ans6 = double.Parse(ans4.Substring(0, ans4.IndexOf(".")));
                                        calcResult.Push(ans6 + 1);
                                        break;
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans4));
                                        break;
                                    }
                                }

                            case "%":
                                double A4 = calcResult.Pop();
                                double B4 = 100;
                                calcResult.Push(A4 / B4);
                                break;

                            default:
                                calcResult.Push(double.Parse(token));
                                break;
                        }
                    }

                    if (calcResult.Peek() == 0)
                    {
                        //double A = b / B;
                        res = b.ToString() + "/" + B.ToString();
                        break;
                    }
                    b -= 1;
                }
                B -= 1;
            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }



        //後置記法　→　計算（分数を入れて総当たり）*returnが分数
        //inputは後置記法の式
        public static string Calculation_F500_1000(string input)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;

            double B = 1000;
            for (double numB = 1; numB <= 500; numB += 1)
            {
                double b = 100;
                for (double numb = 1; numb <= 200; numb += 1)
                {
                    string res2 = input.Replace("□", b.ToString() + " " + B.ToString() + " " + "/");
                    string[] res3 = res2.Trim().Split(Space);
                    foreach (string token in res3)
                    {
                        switch (token)
                        {
                            case "+":
                                double A0 = calcResult.Pop();
                                double B0 = calcResult.Pop();
                                calcResult.Push(B0 + A0);
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
                                double ans = B2 * A2;
                                string ans2 = ans.ToString("F5");
                                calcResult.Push(double.Parse(ans2));
                                break;

                            case "/":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                if (A3 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B3 / A3);
                                    double ans3 = B3 / A3;
                                    string ans4 = ans3.ToString("F8");
                                    string ans5 = ans4.Substring(ans4.IndexOf(".") + 1).Substring(0, 3);
                                    if (ans5 == "000")
                                    {
                                        calcResult.Push(double.Parse(ans4.Substring(0, ans4.IndexOf("."))));
                                        break;
                                    }
                                    else if (ans5 == "999")
                                    {
                                        double ans6 = double.Parse(ans4.Substring(0, ans4.IndexOf(".")));
                                        calcResult.Push(ans6 + 1);
                                        break;
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans4));
                                        break;
                                    }
                                }

                            case "%":
                                double A4 = calcResult.Pop();
                                double B4 = 100;
                                calcResult.Push(A4 / B4);
                                break;

                            default:
                                calcResult.Push(double.Parse(token));
                                break;
                        }
                    }

                    if (calcResult.Peek() == 0)
                    {
                        //double A = b / B;
                        res = b.ToString() + "/" + B.ToString();
                        break;
                    }
                    b -= 1;
                }
                B -= 1;
            }

            //解が範囲外のとき
            if (res == null)
            {
                res = "Out of Range";
            }

            return res;
        }

    }

}
