using System;
using System.Collections.Generic;
using System.Text;
using Game.Utils;

namespace Game.Entity
{
    /// <summary>
    /// 年龄助手类
    /// </summary>
    public class  AgeHelper
    {
        /// <summary>
        /// 转字符串 按年龄段
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static string Age2Str(int age)
        {
            switch (age)
            {
                case 0:
                    return "保密";
                case 10:
                    return "15岁以下";
                case 20:
                    return "16-25岁";

                case 30:
                    return "26-35岁";

                case 40:
                    return "36-45岁";

                case 50:
                    return "46岁以上";
            }
            return string.Empty;
        }
     
        /// <summary>
        /// 年龄字符串转年龄数字
        /// </summary>
        /// <param name="ageStr"></param>
        /// <returns></returns>
        public static byte Str2Age(string ageStr)
        {
            byte age = 0;
            if (TextUtility.EmptyTrimOrNull(ageStr))
            {
                return age;
            }

            switch (ageStr.Trim())
            {
                case "10":
                case "4":
                case "15岁以下":
                    age = 10;
                    break;

                case "20":
                case "1":
                case "16-25岁":
                    age = 20;
                    break;

                case "30":
                case "2":
                case "26-35岁":
                    age = 30;
                    break;

                case "40":
                case "3":
                case "36-45岁":
                    age = 40;
                    break;

                case "50":
                case "5":
                case "46岁以上":
                    age = 50;
                    break;

                case "":
                case "0":
                case "保密":
                default:
                    age = 0;
                    break;
            }

            return age;
        }
    }
}
