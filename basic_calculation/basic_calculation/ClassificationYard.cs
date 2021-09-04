using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace basic_calculation
{
    public static class ClassificationYard
    {
        /*      OpePriority()     演算子の優先順位
         * 
         * 
         * 
         */

        //演算子の優先順位
        public static void OpePriority()
        {
            Dictionary<int, string> ope_Priority = new Dictionary<int, string>();
            ope_Priority.Add(2, "+");
            ope_Priority.Add(2, "-");
            ope_Priority.Add(3, "*");
            ope_Priority.Add(3, "/");
        }

      

        public static double Calculate(String input)
        {
            Dictionary<int, string> ope_Priority = new Dictionary<int, string>();
            ope_Priority.Add(2, "+");
            ope_Priority.Add(2, "-");
            ope_Priority.Add(3, "*");
            ope_Priority.Add(3, "/");

            //オペレータのスタック
            Stack<String> ope = new Stack<String>();
            //数値のスタック
            Stack<Double> val = new Stack<double>();

            //一文字づつ読み取るため文字と文字の間にスペースが必要(要変更）
            String[] token = input.Split();

            foreach(String t in token)
            {
                if (t.All(char.IsDigit)){
                    val.Push(Double.Parse(t));  //数値スタックに
                }else if (ope_Priority.ContainsValue(t))
                {
                    while (ope.Count > 0)
                    {
                        String lastOpe = ope.Pop();
                        if ()
                        {
                            double val2 = val.Pop();
                            double val1 = val.Pop();
                            val.Push(ApplyOperator(lastOpe, val1, val2));
                        }
                        else
                        {
                            ope.Push(lastOpe);
                            break;
                        }
                    }ope.Push(t);
                }else if (t.Equals("(")){
                    ope.Push(t);
                }else if (t.Equals(")"))
                {
                    while (ope.Count() > 0)
                    {
                        String op = ope.Pop();
                        if (op.Equals("("))
                        {
                            break;
                        }
                        else
                        {
                            double val2 = val.Pop();
                            double val1 = val.Pop();
                            val.Push(ApplyOperator(op, val1, val2));
                        }
                    }
                }
                
            }

            while (ope.Count() > 0)
            {
                String op = ope.Pop();
                if (ope_Priority.ContainsValue(op))
                {
                    double val2 = val.Pop();
                    double val1 = val.Pop();
                    val.Push(ApplyOperator(op, val1, val2));
                }
            }

            return val.Pop();
        }

        private static double ApplyOperator(String op,double val1,double val2)
        {
            decimal b1 = new decimal(val1);
            decimal b2 = new decimal(val2);
            switch (op)
            {
                case "+":
                    return (double)(b1 + b2);
                case "-":
                    return (double)(b1 - b2);
                case "*":
                    return (double)(b1 * b2);
                case "/":
                    return (double)(b1 / b2);
            }

            throw new RuntimeException("Unexpected operator: " + op);
        }
        
        


    }
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// キーが存在しない場合は既定値を返します
        /// </summary>
        public static TValue GetOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> self,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            TValue value;
            return self.TryGetValue(key, out value) ? value : defaultValue;
        }

    }


}
