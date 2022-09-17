using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class Seguridad
    {
        public static string EncriptarTexto(string texto)
        {
            try
            {
                System.Security.Cryptography.MD5 md5;
                md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

                Byte[] encodedBytes = md5.ComputeHash(ASCIIEncoding.Default.GetBytes(texto));
                return System.Text.RegularExpressions.Regex.Replace(BitConverter.ToString(encodedBytes).ToLower(), @"-", "");
            }
            catch (Exception ex)

            {
                return string.Empty;
            }
        }
    }
}
