using System;
using System.Security.Cryptography;
using System.Text;

namespace Fire.Cryptography
{
    public class PwdHash
    {
        public string sha256encrypt(string Pwd, string UserName)
        {
            string salt = UserName;
            UTF8Encoding encoder = new UTF8Encoding();
         
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(Pwd));
            string hashedPwd = byteArrayToString(hashedDataBytes);
            string saltAndPwd = String.Concat(hashedPwd, salt);
            byte[] hashedDataBytes2 = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwdAndSalt = byteArrayToString(hashedDataBytes2);
            return hashedPwdAndSalt;
        }

        public string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
    }
}
