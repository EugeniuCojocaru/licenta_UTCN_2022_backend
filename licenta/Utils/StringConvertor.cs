namespace licenta.Utils
{
    public class StringConvertor
    {
        public static string SEPARATOR = "֍";

        public StringConvertor()
        {
        }

        public static string MapStringListToString(List<string> array)
        {
            if (array.Count >= 1)
            {
                return String.Join(SEPARATOR, array);

            }
            else
            {
                return "-";
            }
        }
        public static List<string> MapStringToStringList(string myString)
        {
            return myString.Split(SEPARATOR).ToList();

        }


    }
}
