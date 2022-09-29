using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace cs_0929_1
{
    internal class Program2
    {
        public Program2()
        {
        }

        internal void ReadTextFile()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //"C:\test\1.txt"をShift-JISコードとして開く
            // Object > MarshalByRefObject > TextReader > StreamReader
            StreamReader sr = new StreamReader( @"1.csv", Encoding.GetEncoding("shift_jis"));
            //内容をすべて読み込む
            string s = sr.ReadToEnd();
            //閉じる
            sr.Close();

            var arr1 = Regex.Split(s, "\r\n|\n");
            foreach (string str in arr1)
            {
                Debug.WriteLine($"DBG:{str}");
                var arr2 = str.Split(',');
                foreach (var item in arr2)
                {
                    Debug.WriteLine($"DBG:{item}");
                }
            }

            Debug.WriteLine($"DBG:--------------------------");
            //結果を出力する
            Debug.WriteLine($"DBG:{s}");

        }
    }
}