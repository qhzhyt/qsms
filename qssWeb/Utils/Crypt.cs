namespace qssWeb.Utils
{
    public class Crypt
    {
         public static string SHA_1(string str)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider SHA1CSP = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = SHA1CSP.ComputeHash(bytValue);
            SHA1CSP.Clear();
            string hashstr = "", tempstr = "";
            for (int counter = 0; counter < bytHash.Length; counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                    tempstr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempstr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr += ((char)(i + 0x30)).ToString();
                hashstr += tempstr;
            }
            return hashstr;
        }

        /// <summary>
        /// SHA-256
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_256(string str)
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider SHA256CSP = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = SHA256CSP.ComputeHash(bytValue);
            SHA256CSP.Clear();
            string hashstr = "", tempstr = "";
            for (int counter = 0; counter < bytHash.Length; counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                    tempstr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempstr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr += ((char)(i + 0x30)).ToString();
                hashstr += tempstr;
            }
            return hashstr;
        }

        /// <summary>
        /// SHA-384
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_384(string str)
        {
            System.Security.Cryptography.SHA384CryptoServiceProvider SHA384CSP = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = SHA384CSP.ComputeHash(bytValue);
            SHA384CSP.Clear();
            string hashstr = "", tempstr = "";
            for (int counter = 0; counter < bytHash.Length; counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                    tempstr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempstr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr += ((char)(i + 0x30)).ToString();
                hashstr += tempstr;
            }
            return hashstr;
        }

        /// <summary>
        /// SHA-512
        /// </summary>
        /// <param name="str"></param> 
        public static string SHA_512(string str)
        {
            System.Security.Cryptography.SHA512CryptoServiceProvider SHA512CSP = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytHash = SHA512CSP.ComputeHash(bytValue);
            SHA512CSP.Clear();
            string hashstr = "", tempstr = "";
            for (int counter = 0; counter < bytHash.Length; counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                    tempstr = ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr = ((char)(i + 0x30)).ToString();
                i = bytHash[counter] % 16;
                if (i > 9)
                    tempstr += ((char)(i - 10 + 0x41)).ToString();
                else
                    tempstr += ((char)(i + 0x30)).ToString();
                hashstr += tempstr;
            }
            return hashstr;
        }
    }
}