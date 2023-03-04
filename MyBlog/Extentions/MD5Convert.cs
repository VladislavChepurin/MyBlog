using System.Security.Cryptography;
using System.Text;

namespace MyBlog.Extentions;

public static class MD5Convert
{
    public static string ConvertMD5(this string data)
    {
        if (data !=null)
        {
            MD5 MD5Hash = MD5.Create(); //создаем объект для работы с MD5
            byte[] inputBytes = Encoding.ASCII.GetBytes(data); //преобразуем строку в массив байтов
            byte[] hash = MD5Hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов
            string output = Convert.ToHexString(hash); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре      
            return output;
        }
        return data;
    }
}
