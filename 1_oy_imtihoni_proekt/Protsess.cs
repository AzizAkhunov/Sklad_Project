using static System.Console;

namespace Sklad
{
    public class Protsess : Tekshiruv
    {


        public void ExchangeProduct<T>(int ex) where T : struct
        {
            int t = 1;
            while (t != 0)
            {
                bool cINN = true, cName = true, pName = true, pNum = true, pCost = true, uM = true;
                string corpINN = "", corpName = "", prodName = "", unitMeasure = "", conf = "l";
                int prodNum = 0;
                t = 2;
                decimal prodCost = 0;
                while (t == 2)
                {
                    Clear();
                    WriteLine("Ma'lumotlarni kiriting: ");

                    Write($"Tashkilot INN raqamini kiriting(0000): {"",2}");
                    if (cINN) { corpINN = ReqINN(out cINN); if (cINN) continue; }
                    else WriteLine(corpINN);

                    Write($"Tashkilot nomini kiriting: {"",14}");
                    if (cName) { corpName = ReqINNIsmi(corpINN, out cName); if (!cName) continue; }
                    else WriteLine(corpName);

                    Write($"Mahsulot nomini kiriting: {"",15}");
                    if (pName)
                    {
                        prodName = ReadLine().ToLower();
                        if (ex == 2)
                        {
                            if (ReqMahsulotIsmi(prodName, out prodNum, out prodCost, out unitMeasure)) pName = false;
                            else
                            {
                                WriteLine("Afsuski omborimizda bunday nomli mahsulot yo'q!\nIstasangiz boshqa mahsuot izlab ko'rishingiz mumkin\n\t1 -izlash\n\t0 -chiqish\n");
                                if (ReadLine() == "0") t = 0;
                                continue;
                            }
                        }
                    }
                    else WriteLine(prodName);

                    Write($"Mahsulot miqdori: {"",23}");

                    if (pNum)
                    {
                        if (ex == 2)
                        {
                            int soni = ReqMahsulotSoni(out pNum);
                            if (soni > prodNum)
                            {
                                pNum = true;
                                WriteLine($"Hozirda omborda {prodName} mahsuloti {prodNum} {unitMeasure}ni tashkil qiladi\n Noqulaylik uchun uzr...");
                                Thread.Sleep(2000);
                                continue;
                            }
                            else prodNum = soni;
                        }
                        else prodNum = ReqMahsulotSoni(out pNum); if (pNum) continue;
                    }
                    else WriteLine(prodNum);

                    Write("Mahsulotning o'lchov birligini kiriting: ");
                    if (uM)
                    {
 
                        foreach (var s in File.ReadAllText(Fayllar.Hisobot).Split("\n"))
                        {
                            if (prodName == s.Split("/").First())
                            {
                                unitMeasure = s.Split("/")[2];
                                uM = false;
                                WriteLine(unitMeasure);
                                break;
                            }
                        }
                        if (uM) unitMeasure = ReqUlchov(prodName, out uM);
                        if (uM) continue;
                    }
                    else WriteLine(unitMeasure);


                    Write($"{$"Bir {unitMeasure} mahsulotning tannarxi:",-41}");
                    if (ex == 2) WriteLine(prodCost);
                    else
                    {
                        if (pCost) { prodCost = ReqMahsulotNarxi(out pCost); if (pCost) continue; }
                        else WriteLine(prodCost);
                    }

                    WriteLine($"{$"Jami summa: ",-41}{prodCost * prodNum}");
                    if (conf == "h" || ReqTasdiqlash() == "h")
                    {
                        conf = "h";
                        File.AppendAllText(Fayllar.Hisobot, $"{corpINN}/{corpName}/{prodName}/{prodNum}/{unitMeasure}/{(ex == 1 ? prodCost : " ")}/{(ex == 2 ? prodCost : " ")}/{DateTime.Now}\n");
                        var file = File.ReadAllText(Fayllar.Mahsulotlar).Split("\n");
                        string re = "h";
                        int ind = 0;
                        bool noinFile = true;

                        if (!(file[0] == "" || file == null))
                        {
                            for (int i = 0; i < file.Count(); i++)
                            {
                                if (prodName == file[i].Split("/").First())
                                {
                                    noinFile = false;
                                    int p = int.Parse(file[i].Split("/")[1]);
                                    if (ex == 1)
                                    {
                                        prodNum += p;
                                        if (prodCost < decimal.Parse(file[i].Split("/")[3])) prodCost = decimal.Parse(file[i].Split("/")[3]);
                                    }
                                    else if (ex == 2)
                                    {
                                        if (prodNum <= p) { prodNum = p - prodNum; }
                                        else
                                        {
                                            WriteLine($"Afsuski Omborimizda bunday miqdorda {prodName} mavjud emas!\nIstasangiz {p} {unitMeasure} olishingiz mumkin\n\tH -olish\n\tY -xohlamayman\n");
                                            re = ReadLine().ToLower();
                                            if (re == "h") prodNum = 0;
                                        }
                                    }
                                    file[i] = $"{prodName}/{prodNum}/{unitMeasure}/{prodCost}";
                                    if (re == "h" || conf == "h")
                                    {
                                        File.WriteAllText(Fayllar.Mahsulotlar, file[0]);
                                        int c = file.Count();
                                        for (int d = 1; d < c; d++) File.AppendAllText(Fayllar.Mahsulotlar, "\n" + file[d]);
                                    }
                                    break;
                                }
                            }
                        }

                        if (noinFile && ex == 1) { noinFile = false; File.AppendAllText(Fayllar.Mahsulotlar, $"{prodName}/{prodNum}/{unitMeasure}/{prodCost}\n"); }

                        if (noinFile) WriteLine("Afsuski omborimizda bunday mahsulot yo'q...");
                        else WriteLine("Jarayon Muvaffaqqiyatli yakunlandi!");
                        Thread.Sleep(1000);

                        t = 0;
                    }
                    else t = ReqRefill();
                }
            }
        }



        public void ShowHisobot(int ex, string src, string src1)
        {
            Clear();
            if (ex != 1 && ex != 9)
            {
                WriteLine("+---------------------------------------------------------------------------------------------------------------------------------+");
                WriteLine("|            Tashkilot             |             Mahsulot             |                Qiymati              |                     |");
                WriteLine("|----------------------------------|----------------------------------|-------------------------------------|                     |");
                WriteLine("|      |                           |               |          |O'lchov|              |              |       |                     |");
                WriteLine("| INN  |          Nomi             |      Nomi     |  Miqdori |birligi|    Eksport   |    Import    |Valyuta|         Vaqti       |");
                WriteLine("+------|---------------------------|---------------|----------|-------|--------------|--------------|-------|---------------------|");

                var file = File.ReadAllText(Fayllar.Hisobot).Split("\n").ToList();

                decimal sumImp = 0, sumExp = 0;
                int sumImpKg = 0, sumExpKg = 0, sumImpLitr = 0, sumExpLitr = 0, sumImpDona = 0, sumExpDona = 0;
                for (int i = 0; i < file.Count - 1; i++)
                {
                    var s = file[i].Split("/");
                    if (file[i] == " ") continue;
                    else if (ex == 3 && s[6] == " ") continue;
                    else if (ex == 4 && s[5] == " ") continue;
                    else if (ex == 5 && !s[7].Contains(src)) continue;
                    else if (ex == 6 && src != s[2]) continue;
                    else if (ex == 7 && src != s[1]) continue;
                    else if (ex == 8)
                    {
                        if (s[6] == " " && (decimal.Parse(src) > decimal.Parse(s[5]) || decimal.Parse(src1) < decimal.Parse(s[5]))) continue;
                        else if (s[5] == " " && (decimal.Parse(src) > decimal.Parse(s[6]) || decimal.Parse(src1) < decimal.Parse(s[6]))) continue;
                    }
                    if (s[6] == " ")
                    {
                        sumExp += decimal.Parse(s[5]);
                        if (s[4] == "kg") sumImpKg += int.Parse(s[3]);
                        else if (s[4] == "dona") sumImpDona += int.Parse(s[3]);
                        else if (s[4] == "litr") sumImpLitr += int.Parse(s[3]);
                    }
                    if (s[5] == " ")
                    {
                        sumImp += decimal.Parse(s[6]);
                        if (s[4] == "kg") sumExpKg += int.Parse(s[3]);
                        else if (s[4] == "dona") sumExpDona += int.Parse(s[3]);
                        else if (s[4] == "litr") sumExpLitr += int.Parse(s[3]);
                    }

                    WriteLine($"| {s[0],-4} | {s[1],-25} | {s[2],-13} | {s[3],-8} | {s[4],-5} | {s[5],-12} | {s[6],-12} | {"so'm",5} | {s[7],-19} |");

                }
                WriteLine("+---------------------------------------------------------------------------------------------------------------------------------+");
                WriteLine($"Import qilingan mahsulotlar:\n\t\t{sumImpDona,-15} dona\n\t\t{sumImpLitr,-15} litr\n\t\t{sumImpKg,-15} kg\n\nImport uchun sarflangan summa {sumImp} so'm\n\n");
                WriteLine($"Export qilingan mahsulotlar:\n\t\t{sumExpDona,-15} dona\n\t\t{sumExpLitr,-15} litr\n\t\t{sumExpKg,-15} kg\n\nImport uchun sarflangan summa {sumExp} so'm \n\n");
                WriteLine($"Umumiy summa: {sumImp + sumExp} so'mni tashkil qilgan\nSof foyda: {(sumExp) * 0.1m}");
            }
            else if (ex == 1)
            {
                decimal sumProd = 0;
                WriteLine("Hozirda omborimizda mavjud mahsulotlar:");
                WriteLine("+----------------------------------------------------------------------+");
                WriteLine("|      Nomi     | Miqdori  |Birligi|      Narxi      |    Jami summa   |");
                WriteLine("|---------------|----------|-------|-----------------|-----------------|");
                var file = File.ReadAllText(Fayllar.Mahsulotlar).Split("\n").ToList();
                foreach (var item in file)
                {
                    if (item == "") continue;
                    var s = item.Split("/");
                    sumProd += decimal.Parse(s[3]) * decimal.Parse(s[1]);
                    WriteLine($"| {s[0],-13} | {s[1],-8} | {s[2],-5} | {s[3],10} so'm | {decimal.Parse(s[3]) * decimal.Parse(s[1]),10} so'm |");
                }
                WriteLine("|----------------------------------------------------------------------|");
                WriteLine($"|Jami mahsulotlar qiymati:{sumProd,45}|");
                WriteLine("+----------------------------------------------------------------------+");
            }
            else if (ex == 9)
            {
                WriteLine("Hamkor tashkilotlar ro'yxati:");
                WriteLine("+---------------------------------------------------------------------+");
                WriteLine("|    Tashkilot nomi    | INN hisob raqami | Hamkorlik boshlangan sana |");
                WriteLine("|----------------------|------------------|---------------------------|");
                var file = File.ReadAllText(Fayllar.Hamkorlar).Split("\n").ToList();
                foreach (var item in file)
                {
                    if (item == "") continue;
                    var s = item.Split("/");
                    WriteLine($"| {s[1],-20} | {s[0],-16} |    {s[2],-22} |");
                }
                WriteLine("+---------------------------------------------------------------------+");
            }
        }
    }
}