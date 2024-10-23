using System;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static string Md5(string input)
    {
        string hashValue;
        MD5 MD5Hash = MD5.Create();

        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hash = MD5Hash.ComputeHash(inputBytes);
        hashValue = Convert.ToHexString(hash);

        return hashValue;
    }

    public static void Decrypt()
    {
        Console.WriteLine("Введите длину сообщения");
        int length = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите hash");
        string hash = Console.ReadLine();
        char[] chars = new char[length];

        void Fun(char[] chars_, int count)
        {
            for (int j = 32; j <= 1104; j++)
            {
                chars[count] = (char)j;
                if (count + 1 < chars_.Length)
                    Fun(chars, count + 1);
                string str = new string(chars_);
                if (Md5(str).Contains(hash))
                {
                    Console.WriteLine(str);
                }
            }
        }

        Fun(chars, 0);
    }

    public static void Main()
    {
        Decrypt();
    }
}