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
         *    Calculation_Predict() 分数変換　予測バージョン
         *    predictNum()  分子予想
         *     
         *    
         */

      

        public static double BisectionCal(string input, int ffnum,double asypotenum)
        {
            double initial_val1 = 10000d;       //正の初期値
            double initial_val2 = -10000d;      //負の初期値
            double mid_val = 0;     //中間値

            double res_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));      //解きたい式に初期値1を代入したときの値
            double res_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));      //解きたい式に初期値2を代入したときの値

            double jub = res_initial1 * res_initial2;       //範囲の中に解があるか判断するやつ　正ー無し，負ーあり

            var timer = new System.Diagnostics.Stopwatch();  //処理時間計測
            double F0time = 1.0d; //一次関数のときの時間制限(秒）
            double F1time = 2.0d; //分数関数のときの時間制限(秒）
            double F2time = 10.0d; //高次関数のときの時間制限(秒）

            if (ffnum == 2)//高次方程式
            {
                timer.Start();
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
                                if (timer.Elapsed.TotalSeconds > F2time)
                                {
                                    return 5959595959;
                                }
                            }                                      
                            //ffnum = 0;
                            return ToRoundDown(mid_val, 9);
                            }
                            else
                            {
                                initial_val1 += 0.01d;
                                initial_val2 += 0.01d;
                                REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                                REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                                Rejub = REres_initial1 * REres_initial2;
                            if (timer.Elapsed.TotalSeconds > F2time)
                            {
                                return 5959595959;
                            }
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
                                if (timer.Elapsed.TotalSeconds > F2time)
                                {
                                    return 5959595959;
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
                            if (timer.Elapsed.TotalSeconds > F2time)
                            {
                                return 5959595959;
                            }
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
                        if (timer.Elapsed.TotalSeconds > F2time)
                        {
                            return 5959595959;
                        }
                    }
                }
                return ToRoundDown(mid_val, 9);
            }


            //1次方程式の時
            else if (ffnum == 0)
            {
                timer.Start();
                if (jub >= 0)
                {
                    initial_val1 += 10000;
                    initial_val2 -= 10000;
                    double REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                    double REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                    double Rejub = REres_initial1 * REres_initial2;
                    

                    while (Rejub > 0)
                    {
                        
                            initial_val1 += 10000000;
                            initial_val2 -= 10000000;
                            REres_initial1 = double.Parse(Calculation_forBisection(input, initial_val1));
                            REres_initial2 = double.Parse(Calculation_forBisection(input, initial_val2));

                            Rejub = REres_initial1 * REres_initial2;

                        
                        if (timer.Elapsed.TotalSeconds >F0time)
                        {
                            return 5959595959;
                        }
                        
                    }

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

                           
                            if (timer.Elapsed.TotalSeconds > F0time)
                            {
                                return 5959595959;
                            }
                            
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

                        
                        if (timer.Elapsed.TotalSeconds > F0time)
                        {
                            return 5959595959;
                        }
                        
                    }
                }
                return ToRoundDown(mid_val, 9);
            }


            else if (ffnum == 1)
            {
                //分数関数の時　漸近線付近で二分法


                double initial1 = asypotenum + 0.00001;
                double initial2 = asypotenum + 100;
                double mid = 0;

                double res1 = double.Parse(Calculation_forBisection(input, initial1));      //解きたい式に初期値1を代入したときの値
                double res2 = double.Parse(Calculation_forBisection(input, initial2));      //解きたい式に初期値2を代入したときの値

                double Jub = res1 * res2;       //範囲の中に解があるか判断するやつ　正ー無し，負ーあり

                timer.Start();
                while (initial1>asypotenum)
                {
                    //解が見つかった場合
                    if (Jub < 0)
                    {
                        while (Math.Abs(initial1 - initial2) > 0.0000000001)          //ここで精度決める
                        {
                            mid = (initial1 + initial2) / 2;            //中間値の再計算

                            double res_mid = double.Parse(Calculation_forBisection(input, mid));        //中間値の値

                            if (res1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                            {
                                initial1 = mid;
                            }
                            else
                            {
                                initial2 = mid;
                            }
                            if (timer.Elapsed.TotalSeconds > F1time)
                            {
                                return 5959595959;
                            }
                        }
                        //ffnum = 0;
                        return ToRoundDown(mid, 9);
                    }
                    else
                    {
                        initial1 -= 0.000001d;
                        initial2 += 100d;
                        res1 = double.Parse(Calculation_forBisection(input, initial1));
                        res2 = double.Parse(Calculation_forBisection(input, initial2));

                        Jub = res1 * res2;

                        if (timer.Elapsed.TotalSeconds > F1time)
                        {
                            return 5959595959;
                        }
                    }
                }
                initial1 = asypotenum - 0.00001d;       //初期値をリセット
                initial2 = asypotenum - 100d;
                res1 = double.Parse(Calculation_forBisection(input, initial1));
                res2 = double.Parse(Calculation_forBisection(input, initial2));
                Jub = res1 * res2;

               while (initial1 < asypotenum)
                {
                    //解が見つかった時
                    if (Jub < 0)
                    {
                        while (Math.Abs(initial1 - initial2) > 0.0000000001)          //ここで精度決める
                        {
                            mid = (initial1 + initial2) / 2;            //中間値の再計算

                            double res_mid = double.Parse(Calculation_forBisection(input, mid));        //中間値の値

                            if (res1 * res_mid > 0)     //解がどっちの初期値側に寄っているか判別
                            {
                                initial1 = mid;
                            }
                            else
                            {
                                initial2 = mid;
                            }

                            if (timer.Elapsed.TotalSeconds > F1time)
                            {
                                return 5959595959;
                            }
                        }
                        return ToRoundDown(mid, 9);
                    }

                    else
                    {
                        initial1 += 0.000001d;
                        initial2 -= 100d;

                        res1 = double.Parse(Calculation_forBisection(input, initial1));
                        res2 = double.Parse(Calculation_forBisection(input, initial2));

                        Jub = res1 * res2;

                        if (timer.Elapsed.TotalSeconds > F1time)
                        {
                            return 5959595959;
                        }
                    }
                }

                //プラスにもマイナスにも解がない場合
                if (Jub > 0)
                {

                    return 595959595d;

                }

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

                        //分母の優先掛け算
                    case "・":
                        double A3 = calcResult.Pop();
                        double B3 = calcResult.Pop();
                        double AB3 = B3 * A3;
                        calcResult.Push(AB3);
                        break;

                    case "/":
                        
                        double A4 = calcResult.Pop();
                        double B4 = calcResult.Pop();
                        if (A4 == 0)
                        {
                            break;
                        }
                        else
                        {
                            double ans = B4 / A4;
                            string ans2 = ans.ToString("F12");
                            calcResult.Push(double.Parse(ans2));
                        }
                        break;

                    case "%":
                        double A5 = calcResult.Pop();
                        double B5 = 100;
                        calcResult.Push(A5 / B5);
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
            string reviseVal = input.ToString("F9");　//ここのF5は二分法の精度に合わせる
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
                            if (st.Peek() == '・')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                            }
                            else if (st.Peek() == '÷'|| st.Peek() == '/')
                            {
                                buffer.Push(Space);
                                buffer.Push(st.Pop());
                                st.Push('*');
                                currentState += 1;
                                break;
                            }
                            else if (st.Peek() == '*' && buffer.Peek() != '÷' && buffer.Peek() != '/')
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
                            if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '・')
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


                    case '・':
                        st.Push(token);
                        currentState += 1;
                        break;


                    case '+':
                        while (st.Count > 0)
                        {
                            if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-' || st.Peek() == '・')
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
                                if (st.Peek() == '/' || st.Peek() == '*' || st.Peek() == '÷' || st.Peek() == '+' || st.Peek() == '-' || st.Peek() == '・')
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
        public static string Calculation(string input,double asympote)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;
            decimal m = (decimal)asympote-10;//代入値の初期値

            for (double num =0; num <= 1000.00D; num ++)//代入値(n)
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

                        case "・":
                            double A3 = calcResult.Pop();
                            double B3 = calcResult.Pop();
                            double ans3 = B3 * A3;
                            string ans4 = ans3.ToString("F5");
                            calcResult.Push(double.Parse(ans4));
                            break;

                        case "/":
                            double A4 = calcResult.Pop();
                            double B4 = calcResult.Pop();
                            if (A4 == 0)
                            {
                                break;
                            }
                            else
                            {
                                calcResult.Push(B4 / A4);
                                //decimal ans = B3 / A3;
                                //string ans2 = ans.ToString("F8");
                                //calcResult.Push(decimal.Parse(ans2));
                                break;
                            }

                        case "%":
                            double A5 = calcResult.Pop();
                            double B5 = 100;
                            calcResult.Push(A5 / B5);
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

                            case "・":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                double ans3 = B3 * A3;
                                string ans4 = ans3.ToString("F5");
                                calcResult.Push(double.Parse(ans4));
                                break;

                            case "/":
                                double A4 = calcResult.Pop();
                                double B4 = calcResult.Pop();
                                if (A4 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B4 / A4);
                                    double ans5 = B4 / A4;
                                    string ans6 = ans5.ToString("F8");
                                    string ans7 = ans6.Substring(ans6.IndexOf(".") + 1);
                                    if (ans7.Contains("000"))
                                    {
                                        int z = ans7.IndexOf("000", 0);
                                        if (z == 0)
                                        {
                                            calcResult.Push(double.Parse(ans5.ToString("F0")));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString()));
                                            break;
                                        }
                                    }
                                    else if (ans7.Contains("999"))
                                    {
                                        int z = ans7.IndexOf("999", 0);
                                        if (z == 0)
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, 0);
                                            calcResult.Push(double.Parse(a2.ToString()));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString()));
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans6));
                                        break;
                                    }
                                }

                            case "%":
                                double A5 = calcResult.Pop();
                                double B5 = 100;
                                calcResult.Push(A5 / B5);
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
        //分母の範囲1～1000
        public static string Calculation_F1_1000(string input)
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

                            case "・":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                double ans3 = B3 * A3;
                                string ans4 = ans3.ToString("F5");
                                calcResult.Push(double.Parse(ans4));
                                break;

                            case "/":
                                double A4 = calcResult.Pop();
                                double B4 = calcResult.Pop();
                                if (A4 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B4 / A4);
                                    double ans5 = B4 / A4;
                                    string ans6 = ans5.ToString("F8");
                                    string ans7 = ans6.Substring(ans6.IndexOf(".") + 1);
                                    if (ans7.Contains("000"))
                                    {
                                        int z = ans7.IndexOf("000", 0);
                                        if (z == 0)
                                        {
                                            calcResult.Push(double.Parse(ans5.ToString("F0")));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString("F6")));
                                            break;
                                        }
                                    }
                                    else if (ans7.Contains("999"))
                                    {
                                        int z = ans7.IndexOf("999", 0);
                                        if (z == 0)
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, 0);
                                            calcResult.Push(double.Parse(a2.ToString()));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString("F6")));
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans6));
                                        break;
                                    }
                                }

                            case "%":
                                double A5 = calcResult.Pop();
                                double B5 = 100;
                                calcResult.Push(A5 / B5);
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

        public static double predictNum(double decimals,double denominator)
        {
            double ans = 0;

            ans = decimals * denominator;

            return ans;
        }



        public static string Calculation_Predict(string input,double decimalAns)
        {
            Stack<double> calcResult = new Stack<double>();
            string space = " ";
            char Space = space[0];
            string res = null;

            double B = 1000;　//分母初期値
            for (double numB = 1; numB <= 1000; numB += 1)
            {

                double b=Math.Ceiling(predictNum(decimalAns,B))+1; //分子
                for (double numb = 0; numb < 3; numb += 1)
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

                            case "・":
                                double A3 = calcResult.Pop();
                                double B3 = calcResult.Pop();
                                double ans3 = B3 * A3;
                                string ans4 = ans3.ToString("F5");
                                calcResult.Push(double.Parse(ans4));
                                break;

                            case "/":
                                double A4 = calcResult.Pop();
                                double B4 = calcResult.Pop();
                                if (A4 == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    //calcResult.Push(B4 / A4);
                                    double ans5 = B4 / A4;
                                    string ans6 = ans5.ToString("F8");
                                    string ans7 = ans6.Substring(ans6.IndexOf(".") + 1);
                                    if (ans7.Contains("000"))
                                    {
                                        int z = ans7.IndexOf("000", 0);
                                        if (z == 0)
                                        {
                                            calcResult.Push(double.Parse(ans5.ToString("F0")));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString("F8")));
                                            break;
                                        }
                                    }
                                    else if (ans7.Contains("999"))
                                    {
                                        int z = ans7.IndexOf("999", 0);
                                        if (z == 0)
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, 0);
                                            calcResult.Push(double.Parse(a2.ToString()));
                                            break;
                                        }
                                        else
                                        {
                                            decimal a = decimal.Parse(ans6);
                                            decimal a2 = Math.Round(a, z);
                                            calcResult.Push(double.Parse(a2.ToString("F8")));
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        calcResult.Push(double.Parse(ans6));
                                        break;
                                    }
                                }

                            case "%":
                                double A5 = calcResult.Pop();
                                double B5 = 100;
                                calcResult.Push(A5 / B5);
                                break;

                            default:
                                calcResult.Push(double.Parse(token));
                                break;
                        }
                    }

                    if (calcResult.Peek() == 0)
                    {
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
