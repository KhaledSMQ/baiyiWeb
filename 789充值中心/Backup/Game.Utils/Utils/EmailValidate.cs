using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Utils.Utils
{
  public  class EmailValidate
    {
        public void sendMsg(string senderServerIp, string toMailAddress, string fromMailAddress, string subjectInfo, string bodyInfo, string mailUsername, string mailPassword, string mailPort, string attachPath)
        {
            try
            {
                //smtp.163.com
                 senderServerIp = "123.125.50.133";
                //smtp.gmail.com
                //string senderServerIp = "74.125.127.109";
                //smtp.qq.com
                //string senderServerIp = "58.251.149.147";
                //string senderServerIp = "smtp.sina.com";
                EmailCommon email = new EmailCommon(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
                email.AddAttachments("");
                email.Send();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
