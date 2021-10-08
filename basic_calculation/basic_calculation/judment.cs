using System;
using System.Collections.Generic;
using System.Text;

namespace basic_calculation
{
   public static class judment
    {

        public static bool FF(string left ,string right)
        { 

             
           // ① 5 / (□+3）+3
            
            //式①に”/”が入っている時
            if ()  
            {
                /* 
                 * 式①の以前の文字削除　　①　5/(□+3）+3 →　②　(□+3）+3 
                 */ 

                //式②に()が入っている
                if ()
                {
                    /*
                     * 式②の( ) と）以降を削除   　②　(□+3）+3　→　③　□+3
                     */

                    //式③に□が入っている
                    if ()
                    {
                       //分数関数
                        return true;
                    }

                    //普通の分数
                    return false;

                //  式①に□系が入っている　　　5/□ etc
                }else if ()
                {
                    //分数関数
                    return true;
                }

                //普通の分数
                return false;

                //高次方程式の時
                //①　5+6□*2□　
                // □を含んでいるか
            }else if ()
            {
                /*□をいくつ含んでいるか取得
                 * 
                 * 2つ以上のとき
                 * ①□の前後の文字をみる
                 * ②演算子が来るまで読む
                 * ③＋―の時は一次方程式
                 * ④＊の時は，次の演算子が来るまで読む
                 * ⑤その中で□がある場合は高次方程式
                 */

            }

            return false;
        }
    }
*/
}
