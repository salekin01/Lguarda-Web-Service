using System.Text;

namespace Model.EntityModel.Common
{
    public class BanglaToUnicodeConverter
    {
        public static string convertBanglatoUnicode(string banglaText)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in banglaText) 
            {
                sb.AppendFormat("{1:x4}", c, (int)c); 
            }
            string unicode = sb.ToString().ToUpper(); 
            return unicode;
        }
    }
}