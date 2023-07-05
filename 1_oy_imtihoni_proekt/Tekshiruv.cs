
using _1_oy_imtihoni_proekt;
using static System.Console;

namespace Sklad// Bu classni man Har bir funksiyalarni tekshirish uchun ochdim
{
    public class Tekshiruv
    {
        public int ShowMainMenu(out bool t) // Menuni tekshirish
        {
            t = false;
            int a = 0;
            Clear();
            WriteLine("Menuni tanlang:\n\t1 -mahsulot kiritish\n\t2 -mahsulot olish\n\t3 -hisobot\n\t0 -chiqish");
            try
            {
                a = int.Parse(ReadLine());
                if (a < 0 || a > 3) throw new Exception();
                return a;
            }
            catch
            {
                WriteLine("To'g'ri buyruq kiritish uchun unchalik aqilli bo'lish shart emas! :)");
                Thread.Sleep(1000);
                t = true;
                return 1;
            }
        }
        public string ReqINN(out bool t)//INN raqamini tekshirish
        {

            string a = "";
            t = true;
            try
            {
                a = ReadLine();
                int.Parse(a);
                if (a.Length != 4) throw new Exception();
                t = false;
            }
            catch
            {
                WriteLine("Iltimos, 4 raqamdan iborat son kiriting!");
                Thread.Sleep(1000);
            }
            return a;
        }
        public string ReqINNIsmi(string iNN, out bool cINNName)// INN ismini tekshirish yani (Hamkorlar)
        {
            string cName = "";
            bool c = true;
            cINNName = true;
            foreach (var s in File.ReadAllText(Fayllar.Hamkorlar).Split("\n").ToList())
            {
                if (s.Split("/").First() == iNN)
                {
                    cName = s.Split("/")[1];
                    c = false;
                    cINNName = false;
                    break;
                }
            }
            if (c)// Agarda c false bulmasa ishlasin
            {
                cName = ReadLine();
                File.AppendAllText(Fayllar.Hamkorlar, $"{iNN}/{cName}/{DateTime.Now}\n");
                cINNName = false;
            }
            return cName;
        }
        public bool ReqMahsulotIsmi(string prodName, out int prodNum, out decimal prodCost, out string uM)// Mahsulotlarni ismini tekshirish
        {
            prodNum = 0;
            prodCost = 0;
            uM = "";
            foreach (var s in File.ReadAllText(Fayllar.Mahsulotlar).Split("\n"))
            {
                if (s.Split("/").First() == prodName)
                {
                    var sl = s.Split("/");
                    prodNum = int.Parse(sl[1]);
                    uM = sl[2];
                    prodCost = decimal.Parse(sl[3]) * 1.1m;
                    return true;
                }
            }
            return false;

        }
        public int ReqMahsulotSoni(out bool t)//Mahsulot sonini kiritishga tekshirish
        {
            int a = 0;
            t = true;
            try
            {
                a = int.Parse(ReadLine());
                if (a < 0) throw new Exception();
                t = false;
                return a;
            }
            catch
            {
                WriteLine("Ma'lumot Kiritishda e'tiborliroq bo'ling!");
                Thread.Sleep(1000);
            }
            return a;
        }
        public decimal ReqMahsulotNarxi(out bool t)//Mahsulot Narxini tekshirish
        {

            decimal a = 0;
            t = true;
            try
            {
                a = decimal.Parse(ReadLine());
                if (a < 0) throw new Exception();
                t = false;
                return a;
            }
            catch
            {
                WriteLine("Ma'lumot Kiritishda e'tiborliroq bo'ling!");
                Thread.Sleep(1000);
            }
            return a;
        }
        public string ReqTasdiqlash()//Tasdiqlashni tekshirish
        {
            string c = "";
            while (c != "h" && c != "y")
            {
                Write("Ma'lumotlarni tasdiqlaysizmi? (H/Y): ");
                c = ReadLine().ToLower();
            }
            return c;
        }

        public string ReqUlchov(string name, out bool t)//Mahsulot ulchovini tekshirish
        {
            t = true;
            string uM = ReadLine().ToLower();
            if (uM != Convert.ToString(EnumClass.UlchovBirligi.kg) && uM != Convert.ToString(EnumClass.UlchovBirligi.litr) && uM != Convert.ToString(EnumClass.UlchovBirligi.dona))
            {
                WriteLine("Oziq ovqatlar uchun o'lchov birliklari: kg, litr, dona");
                Thread.Sleep(1000);
                return uM;
            }
            t = false;
            return uM;
        }
        public int ReqRefill()
        {
            try
            {
                WriteLine("\t1 -qayta to'ldirish\n\t0 -bosh menu");
                int a = int.Parse(ReadLine());
                if ((a < 0) || (a > 1)) new Exception();
                return a;
            }
            catch
            {
                WriteLine("Unaqamasda endi!");
            }
            return 1;
        }
        public int ShowHisobotMenu(out bool t)//Menuda tanlangan menuni tekshirish
        {
            t = false;
            try
            {
                WriteLine("   Hisobotlar:\n\t1 -omborda mavjud mahsulotlar hisoboti\n\t2 -barcha hisobotni ko'rish\n\t3 -importlar hisoboti\n\t4 -eksportlar hisoboti\n\t5 -vaqti bo'yicha izlash\n\t6 -mahsulot nomi bo'yicha izlash\n\t7 -tashkilot bo'yicha izlash\n\t8 -Narxi bo'yicha izlash\n\t9 -hamkor tashkilotlar ro'yxati\n\t0 -orqaga");
                int i = int.Parse(ReadLine());
                if ((i < 0 || i > 9)) throw new Exception();
                return i;
                
            }
            catch
            {
                WriteLine("Notugri buyruq kiritdingizz!!!");
                Thread.Sleep(1000);
                WriteLine("   Hisobotlar:\n\t1 -omborda mavjud mahsulotlar hisoboti\n\t2 -barcha hisobotni ko'rish\n\t3 -importlar hisoboti\n\t4 -eksportlar hisoboti\n\t5 -vaqti bo'yicha izlash\n\t6 -mahsulot nomi bo'yicha izlash\n\t7 -tashkilot bo'yicha izlash\n\t8 -Narxi bo'yicha izlash\n\t9 -hamkor tashkilotlar ro'yxati\n\t0 -orqaga");
                int i = int.Parse(ReadLine());
                if ((i < 0 || i > 9)) throw new Exception();
                return i;
            }
            
        }

    }
}
