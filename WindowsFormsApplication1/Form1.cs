using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Rusys duom = new Rusys();
        Rusis duomenys = new Rusis();
        Sablonai sabl = new Sablonai();
        Elementas elem = new Elementas();
        Uzklausa problem = new Uzklausa();
        Uzklausa visoP = new Uzklausa();
        RandomiserClass Randomiser = new RandomiserClass();
        RandomElements RandomList = new RandomElements();
        int paskutinisNr = -1;
        int viso = 0;
        public Form1()
        {
            InitializeComponent();
            GetRusisDuomenys();
            GetSablonuDuomenys();
            GetJuosteliuIlgiai();
        }
        private void GetRusisDuomenys()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Rolandas\\Desktop\\Apps\\WindowsFormsApplication1\\WindowsFormsApplication1\\Rusys.txt");
            while (file.EndOfStream != true)
            {
                int galas = 0;
                string vardas = file.ReadLine();
                int tarpas = vardas.IndexOf(",", galas);
                duom.vardas.Add(vardas.Substring(0, tarpas));
                int j = 0;
                Boolean breakWhile = false;
                duomenys = new Rusis();
                while (breakWhile == false)
                {
                    string MedzioRusis = "";
                    double Pradzia = -1;
                    double Pabaiga = -1;
                    galas = vardas.IndexOf(":", tarpas);
                    MedzioRusis = vardas.Substring(tarpas + 2, galas - tarpas - 2);
                    tarpas = galas; galas = vardas.IndexOf("-", tarpas);
                    if (galas < 0)
                    {
                        galas = vardas.IndexOf(",", tarpas);
                        if (galas < 0)
                        {
                            galas = vardas.IndexOf("!", tarpas);
                            breakWhile = true;
                        }
                        Pradzia = Pabaiga = Convert.ToDouble(vardas.Substring(tarpas + 1, galas - tarpas - 1));
                        tarpas = galas;
                    }
                    else
                    {
                        Pradzia = Convert.ToDouble(vardas.Substring(tarpas + 1, galas - tarpas - 1));
                        tarpas = galas;
                        galas = vardas.IndexOf(",", tarpas);
                        if (galas < 0)
                        {
                            galas = vardas.IndexOf("!", tarpas);
                            breakWhile = true;
                        }
                        Pabaiga = Convert.ToDouble(vardas.Substring(tarpas + 1, galas - tarpas - 1));
                        tarpas = galas;
                    }
                    duomenys.pav.Add(MedzioRusis);
                    duomenys.pradzia.Add(Pradzia);
                    duomenys.pabaiga.Add(Pabaiga);
                    j++;
                }
                duom.Rus.Add(duomenys);
            }
        }
        private void GetSablonuDuomenys()
        {
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\Rolandas\\Desktop\\Apps\\WindowsFormsApplication1\\WindowsFormsApplication1\\Sablonai.txt");
            while (file.EndOfStream != true)
            {
                elem = new Elementas();
                string[] vardas = file.ReadLine().Split();
                sabl.SablonoNr.Add(int.Parse(vardas[0]));
                int i = 1;
                while (i < vardas.Length)
                {
                    elem.JuostIlgis.Add(int.Parse(vardas[i++]));
                    elem.Kiekis.Add(int.Parse(vardas[i++]));
                }
                sabl.SablonoElem.Add(elem);
            }
        }
        private void GetJuosteliuIlgiai()
        {
            comboBox3.Items.Add(15);
            comboBox3.Items.Add(20);
            comboBox3.Items.Add(25);
            comboBox3.Items.Add(30);
            comboBox3.Items.Add(35);
            comboBox3.Items.Add(40);
            comboBox3.Items.Add(45);
            comboBox3.Items.Add(48);
            comboBox3.Items.Add(50);
            comboBox3.Items.Add(60);
            for (int i = 0; i < duom.Rus.Count; i++)
            {
                for (int j = 0; j < duom.Rus[i].pav.Count; j++)
                {
                    string test = duom.Rus[i].pav[j];
                    if (comboBox2.Items.IndexOf(test) < 0)
                    {
                        comboBox2.Items.Add(test);
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Reset();
            richTextBox2.Text = "Uzsakymas:" + "\n" + richTextBox1.Text + "\n" + "\n";
            richTextBox2.Text += "Rezultatas:" + "\n";
            ReadTextBox();
            AtrinktiSchemas();
            StartEvo();
        }
        private void Reset()
        {
            problem = new Uzklausa();
            visoP = new Uzklausa();
            Randomiser = new RandomiserClass();
            RandomList = new RandomElements();
            paskutinisNr = -1;
            viso = 0;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.Text += comboBox2.SelectedItem + " ";
            }
            else
            {
                richTextBox1.Text += "\n" + comboBox2.SelectedItem + " ";
            }
            paskutinisNr = 1;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (paskutinisNr == 1)
            {
                richTextBox1.Text +=  comboBox3.SelectedItem + " ";
            }
            else
            {
                richTextBox1.Text += "\n" + comboBox2.SelectedItem + " " + comboBox3.SelectedItem + " ";
            }
            paskutinisNr = 2;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                richTextBox1.Text += textBox1.Text;
                paskutinisNr = 3;
            }
        }
        private int ReadTextBox()
        {
            string[] text = richTextBox1.Text.Split('\n');
            for (int i = 0; i < text.Length; i++)
            {
                string[] eilute = text[i].Split();
                problem.tipai.Add(eilute[0]);
                problem.ilgis.Add(int.Parse(eilute[1]));
                problem.kiekis.Add(int.Parse(eilute[2]));
            }
            int NR = RastiTinkamiausiaRusi();
            richTextBox2.Text += "Dizaino numeris: "+ NR + "\n";
            return NR;
        }
        private int RastiTinkamiausiaRusi()
        {
            List<string> AtrinktiTipai = new List<string> { };
            List<int> AtrinktuSumos = new List<int> { };
            for (int i = 0; i < problem.ilgis.Count; i++)
            {
                if (AtrinktiTipai.IndexOf(problem.tipai[i]) < 0)
                {
                    AtrinktiTipai.Add(problem.tipai[i]);
                    AtrinktuSumos.Add(problem.kiekis[i]);
                }
                else
                {
                    AtrinktuSumos[AtrinktiTipai.IndexOf(problem.tipai[i])] += problem.kiekis[i];
                }
                if (visoP.ilgis.IndexOf(problem.ilgis[i]) < 0)
                {
                    visoP.ilgis.Add(problem.ilgis[i]);
                    visoP.kiekis.Add(problem.kiekis[i]);
                }
                else
                {
                    visoP.kiekis[visoP.ilgis.IndexOf(problem.ilgis[i])] += problem.kiekis[i];
                }
            }
            int suma = 0;
            for (int i = 0; i < AtrinktuSumos.Count; i++)
            {
                suma += AtrinktuSumos[i];
            }
            viso = suma;
            double santykis = Convert.ToDouble(AtrinktuSumos[0]) / suma;
            double ApvalintasSantykis = Math.Round(santykis * 100, 0);
            problem.santykis = ApvalintasSantykis;
            visoP.santykis = ApvalintasSantykis;
            List<Rusis> RusisAtrinkimui = new List<Rusis>{};
            double min = 99999; int mn = -1;
            for (int i = 0; i < duom.Rus.Count; i++)
            {
                if (duom.Rus[i].pav.Count == AtrinktiTipai.Count)
                {
                    int count = 0;
                    for (int j = 0; j < duom.Rus[i].pav.Count; j++)
                    {
                        
                        if (AtrinktiTipai.IndexOf(duom.Rus[i].pav[j]) >= 0)
                        {
                            count++;
                        }
                    }
                    if (count == AtrinktiTipai.Count)
                    {
                        double pradzia = duom.Rus[i].pradzia[duom.Rus[i].pav.IndexOf(AtrinktiTipai[0])];
                        double pabaiga = duom.Rus[i].pabaiga[duom.Rus[i].pav.IndexOf(AtrinktiTipai[0])];
                        if (pradzia <= ApvalintasSantykis && pabaiga >= ApvalintasSantykis)
                        {
                            richTextBox2.Text += "Optimaliausias dizainas:  " + duom.vardas[i] + "\n";
                            return i;
                        }
                        else
                        {
                            if (Math.Min(pradzia - ApvalintasSantykis, ApvalintasSantykis - pabaiga) < min)
                            {
                                min = Math.Min(pradzia - ApvalintasSantykis, ApvalintasSantykis - pabaiga);
                                mn = i;
                            }
                        }
                    }
                }
            }
            if (mn != -1)
            {
                richTextBox2.Text += "Optimaliausias dizainas:  " + duom.vardas[mn] + "\n";
            }
            return mn;
        }
        private void AtrinktiSchemas()
        {
            richTextBox2.Text += "Schemos atitinkancios uzklausos parametrus:" + "\n";
            Sablonai sablonaiBeta = new Sablonai();
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                int count = 0;
                for(int j = 0; j < visoP.ilgis.Count; j++)
                {
                    if (sabl.SablonoElem[i].JuostIlgis.IndexOf(visoP.ilgis[j]) >= 0)
                    {
                        count++;
                    }
                }
                if (count == sabl.SablonoElem[i].JuostIlgis.Count)
                {
                    sablonaiBeta.SablonoNr.Add(sabl.SablonoNr[i]);
                    sablonaiBeta.SablonoElem.Add(sabl.SablonoElem[i]);
                    richTextBox2.Text += sabl.SablonoNr[i] + " ";
                }
            }
            richTextBox2.Text += "\n";
            sabl = sablonaiBeta;
        }
        private void StartEvo()
        {
            richTextBox2.Text += "\n" + "Galutinis rezultatas:" + "\n";
            Randomiser.sablonas = sabl;
            NykstukuFabrikas(500);
            Testing();
        }
        private void NykstukuFabrikas(int kiekis)
        {
            Random r = new Random();
            List<int> minimumai = new List<int> { };
            richTextBox2.Text +="Viso juosteliu: " + viso + " " + "Rastu sablonu skaicius: " +  Randomiser.sablonas.SablonoNr.Count + "\n";
            for (int i = 0; i < Randomiser.sablonas.SablonoNr.Count; i++)
            {
                int min = 99999;
                for (int j = 0; j < Randomiser.sablonas.SablonoElem[i].JuostIlgis.Count; j++)
                {
                    int index = visoP.ilgis.IndexOf(Randomiser.sablonas.SablonoElem[i].JuostIlgis[j]);
                    if (index != -1)
                    {
                        int dalyba = Convert.ToInt32(Math.Floor(Convert.ToDouble(visoP.kiekis[index]) / Convert.ToDouble(Randomiser.sablonas.SablonoElem[i].Kiekis[j])));
                        if (dalyba < min)
                        {
                            min = dalyba;
                        }
                    }
                }
                minimumai.Add(min);
                richTextBox2.Text += "Schemos " + Randomiser.sablonas.SablonoNr[i] + " minimumas: " + min + "\n";
            }
            for (int kiek = 0; kiek < kiekis; kiek++) // keisti (100)
            {
                Randomiser = new RandomiserClass();
                Randomiser.sablonas = sabl;
                for (int add = 0; add < Randomiser.sablonas.SablonoNr.Count; add++)
                {
                    Randomiser.kiekis.Add(r.Next(0, minimumai[add]));
                }
                RandomList.random.Add(Randomiser);
            }
        }
        private void Testing()
        {
            RandomElements rand = new RandomElements();
            richTextBox2.Text += "Atimimas: " + "\n";
            for (int i = 0; i < RandomList.random.Count; i++)
            {
                Boolean deti = true;
                List<int> viso = new List<int> { };
                for (int get = 0; get < visoP.kiekis.Count; get++)
                {
                    viso.Add(visoP.kiekis[get]);
                }
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoNr.Count; j++)
                {
                    for(int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].Kiekis.Count; h++)
                    {
                        int atimti = RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] * RandomList.random[i].kiekis[j];
                        int index = visoP.ilgis.IndexOf(RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis[h]);
                        viso[index] -= atimti;
                    }
                }
                int suma = 0;
                for (int sum = 0; sum < viso.Count; sum++)
                {
                    RandomList.random[i].sumos.Add(viso[sum]);
                    if (viso[sum] < 0)
                    {
                        deti = false;
                    }
                    suma += Math.Abs(viso[sum]);
                }
                RandomList.random[i].liekana = suma;
                if (deti == true)
                {
                    rand.random.Add(RandomList.random[i]);
                }
            }
            RandomList = rand;
            RandomList.SortList();
            for (int i = 0; i < 5; i++)
            {
                for (int h = 0; h < visoP.kiekis.Count; h++)
                {
                    richTextBox2.Text += "Ilgis: " + visoP.ilgis[h] + " " + RandomList.random[i].sumos[h] + "\n";
                }
                richTextBox2.Text += "Liekana: " + RandomList.random[i].liekana + "\n" + "\n";
            }
        }
    }
}
public class Rusys
{
    public List<string> vardas = new List<string>{};
    public List<Rusis> Rus = new List<Rusis>{};
}
public class Rusis
{
    public List<string> pav = new List<string>{};
    public List<double> pradzia = new List<double> { };
    public List<double> pabaiga = new List<double> { };
}
public class Sablonai
{
    public List<int> SablonoNr = new List<int> { };
    public List<Elementas> SablonoElem = new List<Elementas> { };
}
public class Elementas
{
    public List<int> JuostIlgis = new List<int> { };
    public List<int> Kiekis = new List<int> { };
}
public class Uzklausa
{
    public double santykis;
    public List<string> tipai = new List<string>{};
    public List<int> kiekis = new List<int> { };
    public List<int> ilgis = new List<int> { };
}
public class RandomiserClass
{
    public Sablonai sablonas = new Sablonai();
    public List<int> kiekis = new List<int> { };
    public int liekana;
    public List<int> sumos = new List<int> { };
}
public class RandomElements
{
    public List<RandomiserClass> random = new List<RandomiserClass> { };
    public void SortList()
    {
        random.Sort(delegate(RandomiserClass a, RandomiserClass b)
        {
            return a.liekana.CompareTo(b.liekana);
        });
    }
}