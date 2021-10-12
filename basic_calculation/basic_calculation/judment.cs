using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace basic_calculation
{
   public static class judment
    {
        //個数判定
        public static int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

    
        public static bool FF(string input)//式を入れる
        {
            int S_index_num = input.IndexOf('/');//inputの/の番数
            int D_index_num = input.IndexOf('÷');//inputの÷の番数
            int S_num = CountChar(input, '/');//"/"の数
            int D_num = CountChar(input, '÷');//"÷"の数

            if (S_num == 0 && D_num == 0)
            {
                return false; //非分数関数

            }
            else if (S_num == 1 && D_num == 0)
            {
                string next_char = input.Substring(S_index_num + 1, 1);//"/"の後の1文字を切り出し

                if (next_char == "□")
                {
                    return true; //分数関数

                }
                else if (next_char == "(")
                {
                    string reinput = input.Substring(S_index_num + 1); //（以降を切り出し
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

                    for (int i = 1; i < l_input; i++)
                    {
                        next2_char = input.Substring(S_index_num + i, 1);
                        if (next2_char != "□" || next2_char != "(")
                        {
                            return true;
                        }
                        else if (next2_char != "+" || next2_char != "-")
                        {
                            return false;
                        }
                    }
                    return false;

                }
            }
            else if (S_num == 0 && D_num == 1)
            {
                string next_char = input.Substring(D_index_num + 1, 1);

                if (next_char == "□")
                {
                    return true; //分数関数

                }
                else if (next_char == "(")
                {
                    string reinput = input.Substring(D_index_num + 1); //　（以降を切り出し
                    string re2input = reinput.Substring(0, reinput.IndexOf(')'));
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
                    int l_input = input.Length;

                    for (int i = 1; i < l_input; i++)
                    {
                        next2_char = input.Substring(D_index_num + i, 1);
                        if (next2_char != "□" || next2_char != "(")
                        {
                            return true;
                        }
                        else if (next2_char != "+" || next2_char != "-")
                        {
                            return false;
                        }
                    }
                    return false;
                }
            }
            else
            {
                int[] nums = new int[S_num];
                string input2 = input;
                for(int i = 0; i < S_num-1; i++)
                {
                    nums[i] = input2.IndexOf('/');
                    input2 = input2.Substring(nums[i]+1);
                }

                for (int i = 0; i < S_num-1; i++)
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
                            next2_char = input.Substring(nums[i] + j, 1);
                            if (next2_char != "□" || next2_char != "(")
                            {
                                return true;
                            }
                            else if (next2_char != "+" || next2_char != "-")
                            {
                                return false;
                            }
                        }
                        
                    }

                }



            }

            return false;

            
        }
      
    }

}
