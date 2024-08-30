using System.Text;

namespace USTHNAS
{
    // 暂时没用，没测试。可以解码 JWT 获取过期时间。目前是不管过期时间，发现 JWT 不好使就重新获取。
    internal static class Base64Utils
    {
        public static bool EncodeBase64(string encoding, string text, out string encoded)
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);
            try
            {
                encoded = Convert.ToBase64String(bytes);
                return true;
            }
            catch
            {
                encoded = string.Empty;
                return false;
            }
        }

        public static bool DecodeBase64(string encoding, string code, out string decoded)
        {
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decoded = Encoding.GetEncoding(encoding).GetString(bytes);
                return true;
            }
            catch
            {
                decoded = string.Empty;
                return false;
            }
        }

        public static bool EncodeBase64Url(string encoding, string text, out string encoded)
        {
            bool res = EncodeBase64(encoding, text, out string base64Encoded);
            if (res)
            {
                encoded = base64Encoded.TrimEnd('=').Replace('+', '-').Replace('/', '_');
                return true;
            }
            encoded = string.Empty;
            return false;
        }

        public static bool DecodeBase64Url(string encoding, string code, out string decoded)
        {
            code = code.Replace('-', '+').Replace('_', '/');
            switch (code.Length % 4)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    code += "==";
                    break;
                case 3:
                    code += '=';
                    break;
            }
            bool res = DecodeBase64(encoding, code, out string base64Decoded);
            if (res)
            {
                decoded = base64Decoded;
                return true;
            }
            decoded = string.Empty;
            return false;
        }
    }
}
