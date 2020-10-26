using System.Globalization;

public static class Extensions
{
    public static string ToFormatedScore(this int score)
    {
        NumberFormatInfo n = new NumberFormatInfo
        {
            NumberGroupSeparator = "'",
            NumberDecimalDigits = 0
        };
        return score.ToString("n", n);
    }
    
    public static string ToFormatedScore(this float score)
    {
        NumberFormatInfo n = new NumberFormatInfo
        {
            NumberGroupSeparator = "'",
            NumberDecimalDigits = 2
        };
        return score.ToString("n", n);
    }
    
    public static string ToFormatedScore(this double score)
    {
        NumberFormatInfo n = new NumberFormatInfo
        {
            NumberGroupSeparator = "'",
            NumberDecimalDigits = 2
        };
        return score.ToString("n", n);
    }
}
