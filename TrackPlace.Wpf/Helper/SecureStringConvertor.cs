using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackPlace.WPF.Helper
{
   public class SecureStringConvertor
    {
        /// <summary>
        /// https://blogs.msdn.microsoft.com/fpintos/2009/06/12/how-to-properly-convert-securestring-to-string/
        /// Converting securestring to string
        /// </summary>
        /// <param name="securePassword"> takes ibn securestring</param>
        /// <returns>string</returns>
        public static string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString =
                    System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
