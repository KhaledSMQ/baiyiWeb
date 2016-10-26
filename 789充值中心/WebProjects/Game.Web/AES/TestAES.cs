using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace payapi_mobile_demo
{
    class TestAES
    {
        public static void testAES()
        {
            
            //AES加密测试
            for (int i = 0; i < 10; i++)
            {
                string str = "ABC";
                string aeskey = AES.GenerateAESKey();
                Console.WriteLine(aeskey);

                string miwen = AES.Encrypt(str, aeskey);
                Console.WriteLine(miwen);
                string mingwen = AES.Decrypt(miwen, aeskey);
                Console.WriteLine(mingwen);
            }

            Console.WriteLine("AES加密算法测试结束");

            Console.ReadLine();
        }
    }
}
