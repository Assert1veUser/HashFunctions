using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static void Main()
    {
        Boolean work = true;
        do
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Введите строку для хеширования");
            string? inputString = System.Console.ReadLine();
            if (inputString != "выйти"){
                System.Console.WriteLine("1 - получить хеш строки");
                System.Console.WriteLine("2 - получить хеш строки с солью");
                int inputMode = Convert.ToInt32(System.Console.ReadLine());


                if (inputMode == 1){
                    string hashValue;

                    // Создание объекта для работы с MD5
                    MD5 MD5Hash = MD5.Create();

                    // Преобразование строки в массив байтов
                    byte[] inputBytes = Encoding.ASCII.GetBytes(inputString);

                    // Получение хэша в виде массива байтов
                    byte[] hash = MD5Hash.ComputeHash(inputBytes);

                    // Преобразоввание хэша из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре
                    hashValue = Convert.ToHexString(hash);

                    System.Console.WriteLine("--------------------------");
                    System.Console.WriteLine("Хеш строки:");
                    Console.Write(hashValue);
                }
                else if(inputMode == 2){
                    // Минимальный и максимальный размеры соли
                    int minSaltSize = 4;
                    int maxSaltSize = 8;

                    // Создание случайного числа для размера соли
                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);

                    // Массив байтов, который будет содержать соль
                    byte[] saltBytes = new byte[saltSize];

                    // Заполнение соли криптографически сильными значениями байт
                    RandomNumberGenerator.Fill(saltBytes);
                    
                    // Преобразование обычного текста в массив байтов
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(inputString);

                    // Массив, который будет содержать обычный текст и соль.
                    byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + saltBytes.Length];

                    // Копирование байтов обычного текста в результирующий массив.
                    for (int i = 0; i < plainTextBytes.Length; i++)
                        plainTextWithSaltBytes[i] = plainTextBytes[i];

                    // Добавление байтов соли к полученному массиву
                    for (int i = 0; i < saltBytes.Length; i++)
                        plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
                    
                    // Создание объекта для работы с MD5
                    MD5 MD5Hash = MD5.Create();

                    // Вычисление хэш-значения обычного текста с добавлением соли
                    byte[] hashBytes = MD5Hash.ComputeHash(plainTextWithSaltBytes);

                    // Создание массива, который будет содержать хэш и исходные байты соли
                    byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

                    // Копирование хэш-байтов в результирующий массив
                    for (int i = 0; i < hashBytes.Length; i++)
                        hashWithSaltBytes[i] = hashBytes[i];

                    // Добавление байтов соли к результату
                    for (int i = 0; i < saltBytes.Length; i++)
                        hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

                    // Преобразование результата в строку в кодировке base64
                    string hashValue = Convert.ToHexString(hashWithSaltBytes);

                    System.Console.WriteLine("--------------------------");
                    System.Console.WriteLine("Хеш строки:");
                    Console.Write(hashValue);

                }
                else
                {
                    System.Console.WriteLine("Введено некорректное значение");
                }
            }
            else
            {
                work = false;
            }
            
        } while (work == true);
    }
}