using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace basic_calculation
{
   public static class judment
    {
        //個数判定
        public static int CountChar(string s, string c)
        {
            return s.Length - s.Replace(c,"").Length;
        }

    
        public static bool FF(string input)//式を入れる
        {
            int S_index_num = input.IndexOf('/');//inputの/の番数
            int D_index_num = input.IndexOf('÷');//inputの÷の番数
            
            int S_num = CountChar(input, "/");//"/"の数
            int D_num = CountChar(input, "÷");//"÷"の数
            int X_num = CountChar(input, "□");

            if (X_num ==0)
            {
                return false;
            }


            if (S_num == 0 && D_num == 0)
            {
                return false; //非分数関数

            }

            /*
             *   / を判定
             */

            if (S_num != 0)
            {
                int[] nums = new int[S_num];
                string input2 = input;
                for (int i = 0; i <= S_num-1 ; i++)
                {
                    nums[i] = input2.IndexOf('/');
                    input2 = input2.Substring(nums[i] + 1);
                }

                for (int i = 0; i <= S_num - 1; i++)
                {
                    string next_char = input.Substring(nums[i] + 1, 1);//"/"の後の1文字を切り出し
                    if (next_char == "□")
                    {
                        return true; //分数関数

                    }
                    else if (next_char == "(")
                    {
                        string reinput = input.Substring(nums[i] + 1); //（以降を切り出し
                        string re2input = reinput.Substring(0, reinput.IndexOf(')'));//)の前まで切り出し
                        if (re2input.Contains("□"))
                        {
                            return true; //分数関数
                        }  
                    }
                    else
                    {
                        string next2_char;
                        int l_input = input.Length;//式の長さ

                        for (int j = 1; j < l_input; j++)
                        {
                            next2_char = input.Substring(nums[i] + j, 1);
                            if (next2_char != "□" || next2_char != "(")
                            {
                                return true;  //分数関数
                            }      
                        }

                    }


                }

            }
            /*
              *    ÷を判定
              */
            if (D_num != 0)
            {

                int[] d_nums = new int[D_num];
                string input2d = input;
                for (int i = 0; i <= D_num - 1; i++)
                {
                    d_nums[i] = input2d.IndexOf('÷');
                    input2d = input2d.Substring(d_nums[i] + 1);
                }

                for (int i = 0; i <= D_num - 1; i++)
                {
                    string next_char = input.Substring(d_nums[i] + 1, 1);//"/"の後の1文字を切り出し
                    if (next_char == "□")
                    {
                        return true; //分数関数

                    }
                    else if (next_char == "(")
                    {
                        string reinput = input.Substring(d_nums[i] + 1); //（以降を切り出し
                        string re2input = reinput.Substring(0, reinput.IndexOf(')'));//)の前まで切り出し
                        if (re2input.Contains("□"))
                        {
                            return true; //分数関数
                        }
                        else
                        {
                            return false; //非分数関数
                        }

                    }
                    else
                    {
                        string next2_char;
                        int l_input = input.Length;//式の長さ

                        for (int j = 1; j < l_input; j++)
                        {
                            next2_char = input.Substring(d_nums[i] + j, 1);
                            if (next2_char == "□" || next2_char == "(")
                            {
                                return true; //分数関数
                                //"("のあとgi見る必要あるかも
                            }
                            else if (next2_char == "+" || next2_char == "-")
                            {
                                return false;  //非分数関数
                            }
                        }

                    }

                }

            }
            return false;  //非分数関数

        }


        public static bool HOE(string input)
        {
            int X_num = CountChar(input, "□");
            int M_num = CountChar(input, "×");

            if (X_num > 1)
            {
                int[] nums = new int[X_num];  //□の位置
                string input2 = input;
                int l_input = input.Length;

                for (int i = 0; i <= X_num - 1; i++)
                {
                    nums[i] = input2.IndexOf('□');
                    input2 = input2.Substring(nums[i] + 1);
                }

                for(int i = 0; i <= X_num - 2; i++)
                {
                    string before_input = input.Substring(nums[i] + 1);
                    string after_input = before_input.Substring(0,before_input.IndexOf('□'));
                    if (after_input.Contains(")(") || after_input.Contains(")×(") || after_input.Contains(")×2(") || after_input.Contains(")×3(") || after_input.Contains(")×4(")
                        || after_input.Contains(")×5(") || after_input.Contains(")×6(") || after_input.Contains(")×7(") || after_input.Contains(")×8(") || after_input.Contains(")×9("))
                    {
                        return true;
                    }                  
                }

                for (int i = 0; i <= X_num - 1; i++)
                {
                    string pre_char = "";
                    string next_char = "";

                    if (nums[i] - 1 > 0)
                    {
                        pre_char = input.Substring(nums[i] - 1,1);
                    }

                    if (nums[i] + 1 < l_input)
                    {
                        next_char = input.Substring(nums[i] + 1, 1);
                    }

                    if (pre_char == "×" || next_char == "×")
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }




    }

}
