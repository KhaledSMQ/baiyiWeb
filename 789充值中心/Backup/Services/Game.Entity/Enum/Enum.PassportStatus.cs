using System;
using Game.Utils;

namespace Game.Entity
{
    /// <summary>
    ///  证件类别
    /// </summary>
    [Serializable]
    [EnumDescription("证件类别")]
    public enum PassportStatus : int
    {
        /// <summary>
        /// 没有进行证件设置
        /// </summary>
        [EnumDescription("没有设置",0)]
        NotSet = 0,

        /// <summary>
        /// 身份证
        /// </summary>
        [EnumDescription("身份证",1)]
        IdentityCard = 1,

        /// <summary>
        /// 学生证
        /// </summary>
        [EnumDescription("学生证",2)]
        PassportCard = 2,

        /// <summary>
        /// 军官证
        /// </summary>
        [EnumDescription("军官证",3)]
        ArmymanCard = 3,

        /// <summary>
        /// 驾驶执照
        /// </summary>
        [EnumDescription("驾驶证", 4)]
        DrivingLicence = 4,

        /// <summary>
        /// 其他
        /// </summary>
        [EnumDescription("其他", 5)]
        Other = 5
    }


    /// <summary>
    /// 证件助手类
    /// </summary>
    public class PassportHelper
    {
        /// <summary>
        /// 转中文字符串
        /// </summary>
        /// <param name="passport"></param>
        /// <returns></returns>
        public static string Passport2Str(PassportStatus passport)
        {
            switch (passport)
            {
                case PassportStatus.NotSet:
                    return string.Empty;

                case PassportStatus.IdentityCard:
                    return "身份证";

                case PassportStatus.ArmymanCard:
                    return "军官证";
               
                case PassportStatus.DrivingLicence:
                    return "驾驶执照";
                
                case PassportStatus.Other:
                    return "其他";
                
                case PassportStatus.PassportCard:
                    return "护照";
            }
            return string.Empty;
        }       
    }
}
