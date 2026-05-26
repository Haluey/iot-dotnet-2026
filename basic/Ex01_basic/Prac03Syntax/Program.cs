using System;

namespace Prac03Syntax
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isTrue = true;
            int iValue = 10;
            short shValue = 8;
            float fValue = 3.14f;
            long lValue = 45;
            //long long llValue = 128;
            char cValue = 'A';
            decimal dcValue = 4555;
            
            Console.WriteLine(isTrue);  // System.Console.WriteLine("Hello, C#"); 옛날에는 이런 방식
            Console.WriteLine(iValue);
            Console.WriteLine(fValue);
            Console.WriteLine(cValue);

            Boolean isTrue2 = true;
            Int16 iValue2 = 11;
            Int32 iValue3 = 12;
            Int64 iValue4 = 13;
            Int128 iValue5 = 14;
            Single fValue2 = 3.14f;
            Double dValue = 3.141592;
        }
    }
}
