namespace licenta.Utils
{
    public class StringConvertor
    {
        private static string SEPARATOR = "֍";

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
            if (myString.Equals("-")) return new List<string>();
            return myString.Split(SEPARATOR).ToList();

        }


    }
}