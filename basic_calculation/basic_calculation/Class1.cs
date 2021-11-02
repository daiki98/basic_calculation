//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace basic_calculation
//{
//    class Class1
//    {
//        //Aは入力式(中置記法)
//        //入力式には/があることが前提
//        //A == 5/3(2+6)+6
//        public static string Bunbo(string A)
//        {
//            string B = A.Substring(A.IndexOf("/") + 1);//B == 3(2+6)+6
//            if (B.Contains("+"))
//            {
//                string C = B.Substring(0, B.IndexOf("+"));//C == 3(2
//                int a = C.Length;//3

//                while (C.Contains("("))
//                {
//                    C = B.Substring(0, B.IndexOf("+"));
//                }
//            }
//            return 
//        }
//    }
//}
