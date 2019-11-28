using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Įrašykite iš kurios viršūnės pradėti paiešką");
        int x = int.Parse(Console.ReadLine());

        Virsune[] virsunes = SudetiDuomenis();
        SpausdintiDuomenis(virsunes);
        SpausdintiVirsunes(virsunes);

        int[] prec = new int[virsunes.Length];
        int[] nr = new int[virsunes.Length];

        IeskotiPrecIrNr(ref prec, ref nr, virsunes, x);
        SpausdintiPrecirNr(prec, nr);

        List <Briauna> briaunos = RastiVisasBriaunas(virsunes);
        SpausdintiBriaunas(briaunos, "Galimos briaunos:");

        List<Briauna> br = RastiAtvirkstinesBriaunas(virsunes, prec, briaunos);


        Console.ReadKey();
    }

    static void SpausdintiVirsunes(Virsune[] virsunes)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Viršūnės:");
        string line = "";
        for (int i = 0; i < virsunes.Length; i++)
        {            
            line += (i+1).ToString() + " ";            
        }
        Console.WriteLine(line);
    }

    static List<Briauna> RastiAtvirkstinesBriaunas(Virsune[] virsunes, int[] prec, List<Briauna> briaunos)
    {
        List<Briauna> paieskosBriaunos = new List<Briauna>();

        for (int i = 0; i < virsunes.Length; i++)
        {
            if (i+1 != prec[i])
            {
                Briauna nauja = new Briauna(i + 1, prec[i]);
                paieskosBriaunos.Add(nauja);
            }            
        }

        SpausdintiBriaunas(paieskosBriaunos, "Paieškos keliu rastos briaunos:");

        List<Briauna> atvirkstines = PaliktiTikAtvirkstinas(briaunos, paieskosBriaunos);

        SpausdintiBriaunas(briaunos, "Atvirkstines:");

        return briaunos;
    }

    static List<Briauna> PaliktiTikAtvirkstinas(List<Briauna> visos, List<Briauna> salinamos)
    {
        for (int i = 0; i < visos.Count; i++)
        {
            for (int j = 0; j < salinamos.Count; j++)
            {
                if (visos[i].TokiaPati(salinamos[j]))
                {
                    visos.Remove(visos[i]);
                    i--;
                    break;
                }
            }
        }

        return visos;
    }

    static List<Briauna> RastiVisasBriaunas(Virsune[] virsunes)
    { 
        int kuriojeEsu = 1;                                             //pradedu nuo 1
        List<Briauna> visosBriaunos = new List<Briauna>();

        BriaunosRekursija(virsunes, kuriojeEsu, ref visosBriaunos);

        //SpausdintiBriaunas(visosBriaunos, "Visos briaunos:");

        visosBriaunos = SalintiPasikartojancias(visosBriaunos);

        //SpausdintiBriaunas(visosBriaunos, "Galimos briaunos:");

        return visosBriaunos;
    }

    static void BriaunosRekursija(Virsune[] virsunes, int kuriojeEsu, ref List<Briauna> visosBriaunos)
    {
        if (kuriojeEsu != virsunes.Length)
        {
            for (int i = 0; i < virsunes[kuriojeEsu - 1].KaMato.Length; i++)
            {
                Briauna nauja = new Briauna(kuriojeEsu, virsunes[kuriojeEsu - 1].KaMato[i]);
                visosBriaunos.Add(nauja);
            }
            kuriojeEsu++;
            BriaunosRekursija(virsunes, kuriojeEsu, ref visosBriaunos);
        }
        else return;
    }

    static List<Briauna> SalintiPasikartojancias(List<Briauna> briaunos)
    {
        for (int i = 0; i < briaunos.Count; i++)
        {
            for (int j = i+1; j < briaunos.Count; j++)
            {
                if (briaunos[i].TokiaPati(briaunos[j]))
                {
                    briaunos.Remove(briaunos[j]);
                }
            }
        }


        return briaunos;
    }

    static void SpausdintiBriaunas(List<Briauna> briaunos, string text)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine(text);

        for (int i = 0; i < briaunos.Count; i++)
        {
            string line = "";
            line += briaunos[i].v1.ToString() + briaunos[i].v2.ToString() + " ";
            Console.WriteLine(line);
        }
    }

    static void SpausdintiDuomenis(Virsune[] virsunes)
    {
        Console.WriteLine("Grafas:");
        for (int i = 0; i < virsunes.Length; i++)
        {
            string mato = "";
            mato += virsunes[i].Numeris.ToString() + " ---> ";

            for (int j = 0; j < virsunes[i].KaMato.Length; j++)
            {
                mato += virsunes[i].KaMato[j].ToString();
            }

            Console.WriteLine(mato);
        }
    }

    static void IeskotiPrecIrNr(ref int[] prec, ref int[] nr, Virsune[] virsunes, int nuoKurio)
    {
        prec[nuoKurio-1] = nuoKurio;
        nr[nuoKurio-1] = 0;        
        int kuriamEsu = nuoKurio;
        int kiekAplankyta = 1;
        int num = 1;
        virsunes[nuoKurio - 1].arLankyta = true;

        EitiRekursiskai(ref prec, ref nr, kuriamEsu, kiekAplankyta, num, virsunes, nuoKurio);
    }



    static void EitiRekursiskai(ref int[] prec, ref int[] nr, int kuriamEsu, int kiekAplankyta, int num, Virsune[] virsunes, int nuoKurio)
    {
        int lankytuCount = 0;

        if (virsunes.Length != kiekAplankyta)
        {            
            for (int i = 0; i < virsunes[kuriamEsu - 1].KaMato.Length; i++)
            {
                
                if (lankytuCount != virsunes[kuriamEsu - 1].KaMato.Length)
                {
                    if (!(virsunes[virsunes[kuriamEsu - 1].KaMato[i] - 1].arLankyta))
                    {
                        int isKurio = kuriamEsu;
                        kuriamEsu = virsunes[kuriamEsu - 1].KaMato[i];
                        virsunes[kuriamEsu - 1].arLankyta = true;

                        prec[kuriamEsu - 1] = isKurio;
                        nr[kuriamEsu - 1] = num;
                        num++;

                        kiekAplankyta++;
                        Console.WriteLine("Einu is virsunes " + isKurio + " i virsune " +  kuriamEsu);
                        EitiRekursiskai(ref prec, ref nr, kuriamEsu, kiekAplankyta, num, virsunes, nuoKurio);
                    }
                    else
                    {
                        lankytuCount++;
                        Console.WriteLine("Virsune "+  virsunes[virsunes[kuriamEsu - 1].KaMato[i] - 1].Numeris +" jau lankyta");
                    }
                }
                else
                {
                    int temp = kuriamEsu;
                    kuriamEsu = prec[kuriamEsu-1];
                    Console.WriteLine("GRIZTU is virsunes " + temp + " i virsune " + kuriamEsu);
                    EitiRekursiskai(ref prec, ref nr, kuriamEsu, kiekAplankyta, num, virsunes, nuoKurio);
                }
            }
        }
        else return;
    }

    static void SpausdintiPrecirNr (int[] prec, int[] nr)
    {
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Precas:");
        string line = "";
        for (int i = 0; i < prec.Length; i++)
        {
            line +=prec[i].ToString() + " ";
        }

        Console.WriteLine(line);

        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Kelintu numeriu:");
        line = "";
        for (int i = 0; i < nr.Length; i++)
        {
            line += nr[i].ToString() + " ";            
        }

        Console.WriteLine(line);

    }

    static Virsune[] SudetiDuomenis()
    {
        //Virsune v1 = new Virsune(1, new int[] { 2, 7 });
        //Virsune v2 = new Virsune(2, new int[] { 1, 3, 5 });
        //Virsune v3 = new Virsune(3, new int[] { 2, 4, 6 });
        //Virsune v4 = new Virsune(4, new int[] { 3, 8 });
        //Virsune v5 = new Virsune(5, new int[] { 2, 6, 7 });
        //Virsune v6 = new Virsune(6, new int[] { 3, 5, 8 });
        //Virsune v7 = new Virsune(7, new int[] { 1, 5, 8 });
        //Virsune v8 = new Virsune(8, new int[] { 4, 6, 7 });

        //Virsune v1 = new Virsune(1, new int[] { 2, 5, 6 });
        //Virsune v2 = new Virsune(2, new int[] { 1, 3 });
        //Virsune v3 = new Virsune(3, new int[] { 2, 4, 5 });
        //Virsune v4 = new Virsune(4, new int[] { 3, 5, 7 });
        //Virsune v5 = new Virsune(5, new int[] { 1, 3, 4, 6, 8 });
        //Virsune v6 = new Virsune(6, new int[] { 1, 5 });
        //Virsune v7 = new Virsune(7, new int[] { 4 });
        //Virsune v8 = new Virsune(8, new int[] { 5 });

        //Virsune[] virsunes = { v1, v2, v3, v4, v5, v6, v7, v8 };

        Virsune v1 = new Virsune(1, new int[] { 2 });
        Virsune v2 = new Virsune(2, new int[] { 3, 1, 4 });
        Virsune v3 = new Virsune(3, new int[] { 4, 2 });
        Virsune v4 = new Virsune(4, new int[] { 2, 3 });

        Virsune[] virsunes = { v1, v2, v3, v4};

        return virsunes;
    }
}

public class Briauna
{
    public int v1 { get; set; }
    public int v2 { get; set; }

    public Briauna()
    {
    }

    public Briauna(int v1, int v2)
    {
        this.v1 = v1;
        this.v2 = v2;
    }

    public bool TokiaPati(Briauna kita)
    {
        if ((v1 == kita.v1 && v2==kita.v2) || (v1==kita.v2 && v2==kita.v1))
        {
            return true;
        }
        return false;
    }
}


public class Virsune
{
    public int Numeris { get; set; }
    public int[] KaMato { get; set; }
    public bool arLankyta { get; set; }

    public Virsune()
    {
    }

    public Virsune(int numeris, int[] kamato)
    {
        Numeris = numeris;
        KaMato = kamato;
        arLankyta = false;
    }

    public override string ToString()
    {
        string kaimynai = "";
        foreach (var item in KaMato)
        {
            kaimynai += String.Format(item.ToString() + ","); ;
        }
        return String.Format("Virsunes numeris: " + Numeris + ", mato:" + kaimynai + " ar lankyta: " + arLankyta) ;
    }
}