using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        string faylAdi = "telebeler.txt";

        while (true)
        {
            Console.WriteLine("\n--- Tələbə Siyahısı ---");
            Console.WriteLine("1) Tələbə əlavə et");
            Console.WriteLine("2) Hamısını göstər");
            Console.WriteLine("3) Ada görə axtar");
            Console.WriteLine("4) Çıxış");
            Console.Write("Seçiminiz (1-4): ");

            string secim = Console.ReadLine();

            if (secim == "4")
            {
                Console.WriteLine("Proqramdan çıxılır...");
                break;
            }

            switch (secim)
            {
                case "1":
                    Console.Write("Tələbənin ID-si: ");
                    string id = Console.ReadLine();

                    Console.Write("Tələbənin Adı: ");
                    string ad = Console.ReadLine();

                    Console.Write("Tələbənin Yaşı: ");
                    string yas = Console.ReadLine();

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(faylAdi, append: true, Encoding.UTF8))
                        {
                            sw.WriteLine($"{id};{ad};{yas}");
                        }
                        Console.WriteLine("Tələbə uğurla əlavə edildi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Yazma xətası: {ex.Message}");
                    }
                    break;

                case "2":
                    try
                    {
                        if (!File.Exists(faylAdi))
                        {
                            Console.WriteLine("Siyahı boşdur, hələ heç bir tələbə əlavə edilməyib.");
                        }
                        else
                        {
                            Console.WriteLine("\n--- Bütün Tələbələr ---");
                            using (StreamReader sr = new StreamReader(faylAdi, Encoding.UTF8))
                            {
                                string setir;
                                while ((setir = sr.ReadLine()) != null)
                                {
                                    string[] hisseler = setir.Split(';');
                                    if (hisseler.Length == 3)
                                    {
                                        Console.WriteLine($"ID: {hisseler[0]} | Ad: {hisseler[1]} | Yaş: {hisseler[2]}");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Oxuma xətası: {ex.Message}");
                    }
                    break;

                case "3":
                    Console.Write("Axtarılacaq tələbənin adını daxil edin: ");
                    string axtarilanAd = Console.ReadLine();

                    try
                    {
                        if (!File.Exists(faylAdi))
                        {
                            Console.WriteLine("Siyahı faylı tapılmadı.");
                        }
                        else
                        {
                            bool tapildi = false;
                            Console.WriteLine("\n--- Axtarış Nəticələri ---");
                            using (StreamReader sr = new StreamReader(faylAdi, Encoding.UTF8))
                            {
                                string setir;
                                while ((setir = sr.ReadLine()) != null)
                                {
                                    string[] hisseler = setir.Split(';');
                                    if (hisseler.Length == 3)
                                    {
                                        if (hisseler[1].ToLower().Contains(axtarilanAd.ToLower()))
                                        {
                                            Console.WriteLine($"ID: {hisseler[0]} | Ad: {hisseler[1]} | Yaş: {hisseler[2]}");
                                            tapildi = true;
                                        }
                                    }
                                }
                            }

                            if (!tapildi)
                            {
                                Console.WriteLine("Bu adda tələbə tapılmadı.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Axtarış xətası: {ex.Message}");
                    }
                    break;

                default:
                    Console.WriteLine("Yanlış seçim! Zəhmət olmasa 1 ilə 4 arasında rəqəm daxil edin.");
                    break;
            }
        }
    }
}