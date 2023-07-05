using static System.Console;
using System.IO;
using Sklad;





Protsess jarayon = new Protsess();
int menu = 1;
bool m = true, rm = true;
while (menu != 0)
{
    
    if (m) 
    { 
        menu = jarayon.ShowMainMenu(out m); 
        if (m) continue; 
    }

    switch (menu)
    {
        case 1:
        case 2:
            {
                jarayon.ExchangeProduct<int>(menu);
                m = true;
                break;
            }
        case 3:
            {
                int rMenu = 1;

                while (rMenu != 0)
                {
                    WriteLine("+~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~+");
                    string src = "";
                    if (rm)
                    {
                        rMenu = jarayon.ShowHisobotMenu(out rm);
                        if (rm) break;
                    }
                    
                    switch (rMenu)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 9:
                            {
                                jarayon.ShowHisobot(rMenu, src, src);
                                break;
                            }
                        case 5:
                            {
                                Write("Kerakli vaqtni kiriting(dd.mm.yyy): ");
                                jarayon.ShowHisobot(rMenu, ReadLine(), src);
                                break;
                            }
                        case 6:
                            {
                                Write("Kerakli mahsulot nomini kiriting: ");
                                jarayon.ShowHisobot(rMenu, ReadLine(), src);
                                break;
                            }
                        case 7:
                            {
                                Write("Kerakli tashkilot nomini kiriting: ");
                                jarayon.ShowHisobot(rMenu, ReadLine(), src);
                                break;
                            }
                        case 8:
                            {
                                Write("Minimal narxni kiriting: ");
                                src = ReadLine();
                                Write("Maksimal narxni kiriting: ");
                                jarayon.ShowHisobot(rMenu, src, ReadLine());
                                break;
                            }
                    }
                    rm = true;
                }
                m = true;
                break;
            }
        default:
            {
                Clear();
                Write("\n\n\t\tBizning Projectimizdan Foydalanganingiz uchun kotta rahmat!!!!\n\n\n\n");
            }
            break;
    }
}