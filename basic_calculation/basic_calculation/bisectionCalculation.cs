using System;
using System.Collections.Generic;
using System.Text;

namespace basic_calculation
{
    public static class bisectionCalculate
    {
        public static double Calculate(double x, double value1, double value2, string opetator)
        {
            double result = 0;   //計算結果
            int stateNum = 0;   //演算子判定値


            //演算子判定(なんかうまくいかんくてdefaultに入ってしまうし放置したわ！）
            switch (opetator)
            {
                case "+":
                    stateNum = 1;
                    break;
                case "-":
                    stateNum = 2;
                    break;
                case "×":
                    stateNum = 3;
                    break;
                case "÷":
                    stateNum = 4;
                    break;

                default:
                    stateNum = -1;
                    result = value1 * x + value2;               //仕方なく
                    break;
            }

            return result;
        }


        /*
         二分法をするやつ
        ＜問題点＞
        ・一回目の計算が結果が０になる（num1,num2を再入力すれば計算できる）
        ・初期値で計算したf1とf2の積がプラスになる範囲では計算できない
        ＜TODO＞

        */

        public static double bisection(double num1, double num2, string ope)
        {
            double mid_value = 0;　　　　　//区間の中間値

            double initial_value1 = 100;                  //初期値
            double initial_value2 = -100;
            Boolean Isfinished = true;                  //計算可能か判断するやつ（falseで計算おわり）



            while (Isfinished)
            {
                double f1 = Calculate(initial_value1, num1, num2, ope);        // 初期値の計算
                double f2 = Calculate(initial_value2, num1, num2, ope);

                double enableJud = f1 * f2;                                 //初期値区間に解が存在するか判定(負で存在する）


                if (enableJud < 0)                                              //解が存在したとき
                {
                    while (Math.Abs(initial_value1 - initial_value2) > 0.01)        //計算精度(テキトーにきめたわ）
                    {
                        mid_value = (initial_value1 + initial_value2) / 2;            //中間値計算
                        double fmid = Calculate(mid_value, num1, num2, ope);

                        if (fmid * f1 < 0)                                            //中間値から端の値変更
                        {
                            initial_value2 = mid_value;
                        }
                        else
                        {
                            initial_value1 = mid_value;
                        }
                    }
                    Isfinished = false;                 //計算おしまい
                }
                else
                {
                    /*
                    この中に初期外で計算できんかった時の処理
                    一回目の計算が失敗してしまうから計算終わらず∞ループ！
                    一回目の計算の問題点解決してから処理追加
                    */

                    Isfinished = false;            //仕方なく計算おしまい
                }
            }

            return mid_value;                   //結果

        }

    }
}
