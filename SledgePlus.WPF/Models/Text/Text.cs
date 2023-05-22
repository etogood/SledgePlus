using System.IO;
using System.Text.RegularExpressions;

namespace SledgePlus.WPF.Models.Text;

public static class Text
{
    public static bool PasswordValidation(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var upperChars = new Regex(@"[A-Z]+");

        return hasNumber.IsMatch(password)
               && upperChars.IsMatch(password) && password.Length >= 8;
    }

    public static string? ReadText()
    {
        var sr = new StreamReader(Directory.GetCurrentDirectory() + "/Templates/cpp_template.txt");
        var text = string.Empty;
        string? line;

        do
        {
            line = sr.ReadLine();
            text += "\n" + line;
        } while (line != null);

        sr.Close();
        return text;
    }
}