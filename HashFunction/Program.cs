using System.Security.Cryptography;
using System.Text;

static string CalculateSHA256Hash(string input)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}

string text = "какой-то текст";
string hashPrefix = "000";
string hashEnd = "1";
int counter = 0;

while (true)
{
    string testText = text + counter;
    string hashValue = CalculateSHA256Hash(testText);

    if (hashValue.StartsWith(hashPrefix) && hashValue.EndsWith(hashEnd))
    {
        Console.WriteLine($"Строка: {testText}\n" +
                          $"Хеш: {hashValue}");
        break;
    }

    counter++;
}