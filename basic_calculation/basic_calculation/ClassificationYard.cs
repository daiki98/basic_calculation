using System;
using System.Collections.Generic;
using System.Text;

namespace basic_calculation
{
    public static class ClassificationYard

    {
       public static int op_preced(char c)
        {
            switch (c)
            {
                case '!':
                    return 4;

                case '*':
                case '/':
                case '%':
                    return 3;

                case '+':
                case '-':
                    return 2;

                case '=':
                    return 1;

            }
            return 0;
        }

        public static　bool op_left_assoc(char c)
        {
            switch (c)
            {
                // 左結合性
                case '*':
                case '/':
                case '%':
                case '+':
                case '-':
                    return true;
                // 右結合性
                case '=':
                case '!':
                    return false;
            }
            return false;
        }

        public static int op_arg_count(char c)
        {
            switch (c)
            {
                case '*':
                case '/':
                case '%':
                case '+':
                case '-':
                case '=':
                    return 2;
                case '!':
                    return 1;
                default: // 関数の場合、A()の引数は0個、B()の引数は1個、C()の引数は2個... と定義
                    return c - 'A';
            }
            return 0;
        }
             
    }
}
