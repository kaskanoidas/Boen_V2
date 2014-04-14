using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Combinatorics.Collections;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Rusys duom = new Rusys();
        Rusis duomenys = new Rusis();
        Sablonai sablConst = new Sablonai();
        Sablonai sabl = new Sablonai();
        Elementas elem = new Elementas();
        Uzklausa problem = new Uzklausa();
        Uzklausa visoP = new Uzklausa();
        RandomiserClass Randomiser = new RandomiserClass();
        RandomElements RandomList = new RandomElements();
        int paskutinisNr = -1;
        // START
        public Form1()
        {
            InitializeComponent();
            GetRusisDuomenys();
            GetSablonuDuomenys();
            GetJuosteliuIlgiai();
        }
        // Data Management
        private void GetRusisDuomenys()
        {
            comboBox1.Items.Add("");
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Rusys.txt";
            location = location.Substring(6);
            System.IO.StreamReader file = new System.IO.StreamReader(location);
            while (file.EndOfStream != true)
            {
                int galas = 0;
                string vardas = file.ReadLine();
                int tarpas = vardas.IndexOf(",", galas);
                duom.vardas.Add(vardas.Substring(0, tarpas));
                comboBox1.Items.Add(vardas.Substring(0, tarpas));
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
            file.Close();
        }
        private void GetSablonuDuomenys()
        {
            sablConst = new Sablonai();
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Sablonai.txt";
            location = location.Substring(6);
            System.IO.StreamReader file = new System.IO.StreamReader(location);
            while (file.EndOfStream != true)
            {
                elem = new Elementas();
                string[] vardas = file.ReadLine().Split();
                sablConst.SablonoNr.Add(int.Parse(vardas[0]));
                int i = 1;
                while (i < vardas.Length)
                {
                    elem.JuostIlgis.Add(int.Parse(vardas[i++]));
                    elem.Kiekis.Add(int.Parse(vardas[i++]));
                }
                sablConst.SablonoElem.Add(elem);
            }
            file.Close();
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
        // Data Management END      // END      // UI Functioning
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
                richTextBox1.Text += comboBox3.SelectedItem + " ";
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
        private void button1_Click(object sender, EventArgs e) // MAIN-->CALC START
        {
            if (richTextBox1.Text == "")
            {
                richTextBox2.Text = "Iveskite uzsakyma.";
            }
            else
            {
                button1.Enabled = false;
                richTextBox2.Text = "";
                Reset();
                int nr = -1;
                if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedIndex == 0)
                {
                    nr = ReadTextBox();
                    if (nr != -1)
                    {
                        richTextBox2.Text += "Rasta parketo rusis:  " + duom.vardas[nr] + "\n";
                    }
                }
                else
                {
                    ReadTextBox();
                    nr = comboBox1.SelectedIndex - 1;
                    richTextBox2.Text += "Pasirinkta parketo rusis:  " + duom.vardas[nr] + "\n";
                }
                if (nr != -1 && nr!= -2)
                {
                    AtrinktiSchemas();
                    AtrinktiTinkamusVariantus(nr);
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.WorkerSupportsCancellation = true;
                    bw.WorkerReportsProgress = true;
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                    bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    if (bw.IsBusy != true)
                    {
                        bw.RunWorkerAsync();
                    }
                }
                else if (nr == -2)
                {
                    richTextBox2.Text = "Blogas uzsakymo formulavimas.";
                    button1.Enabled = true;
                }
                else if (nr == -1)
                {
                    richTextBox2.Text = "Nerasta parketo rusis.";
                    button1.Enabled = true;
                }
            }
        }
        // UI Functioning END       // CALC START     // PREP WORK
        private void Reset()
        {
            problem = new Uzklausa();
            visoP = new Uzklausa();
            Randomiser = new RandomiserClass();
            RandomList = new RandomElements();
            paskutinisNr = -1;
        }
        private int ReadTextBox()
        {
            string[] text = richTextBox1.Text.Split('\n');
            for (int i = 0; i < text.Length; i++)
            {
                string[] eilute = text[i].Split();
                if (eilute.Length != 3)
                {
                    return -2;
                }
                problem.tipai.Add(eilute[0]);
                int del = 0;
                if (int.TryParse(eilute[1], out del) == false)
                {
                    return -2;
                }
                problem.ilgis.Add(int.Parse(eilute[1]));
                if (int.TryParse(eilute[2], out del) == false)
                {
                    return -2;
                }
                problem.kiekis.Add(int.Parse(eilute[2]));
            }
            int NR = RastiTinkamiausiaRusi();
            return NR;
        }
        private int RastiTinkamiausiaRusi() //TODO:% 
        {
            List<string> AtrinktiTipai = new List<string> { };
            List<int> AtrinktuSumos = new List<int> { };
            for (int i = 0; i < problem.ilgis.Count; i++)
            {
                if (AtrinktiTipai.IndexOf(problem.tipai[i]) < 0)
                {
                    AtrinktiTipai.Add(problem.tipai[i]);
                    AtrinktuSumos.Add(problem.kiekis[i] * problem.ilgis[i]);
                }
                else
                {
                    AtrinktuSumos[AtrinktiTipai.IndexOf(problem.tipai[i])] += problem.kiekis[i] * problem.ilgis[i];
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
            for (int i = 0; i < AtrinktuSumos.Count; i++)
            {
                double sant = Convert.ToDouble(AtrinktuSumos[i]) / Convert.ToDouble(suma);
                problem.santykis.Add(sant);
                visoP.santykis.Add(sant);
            }
            double santykis = Convert.ToDouble(AtrinktuSumos[0]) / Convert.ToDouble(suma);
            double ApvalintasSantykis = Math.Round(santykis * 100, 0); // kiek po kableliu galima keisti
            List<Rusis> RusisAtrinkimui = new List<Rusis> { };
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
            return mn;
        }
        private void AtrinktiSchemas()
        {
 
            richTextBox2.Text += "Schemos atitinkancios uzklausos parametrus:" + "\n";
            sabl = new Sablonai();
            for (int i = 0; i < sablConst.SablonoNr.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < visoP.ilgis.Count; j++)
                {
                    if (sablConst.SablonoElem[i].JuostIlgis.IndexOf(visoP.ilgis[j]) >= 0)
                    {
                        count++;
                    }
                }
                if (count == sablConst.SablonoElem[i].JuostIlgis.Count)
                {
                    sabl.SablonoNr.Add(sablConst.SablonoNr[i]);
                    sabl.SablonoElem.Add(sablConst.SablonoElem[i]);
                    richTextBox2.Text += sablConst.SablonoNr[i] + " ";
                }
            }
            richTextBox2.Text += "\n";
        }
        private void AtrinktiTinkamusVariantus(int parketoRusis) // KVIESTI KurtiVariantus
        {
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                KurtiVariantus(i);
            }
        }
        private void KurtiVariantus(int SablonoNr) // PAKEISTI TIK PVZ
        {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\SablonaiNew.txt";
            location = location.Substring(6);
            System.IO.StreamWriter file = new System.IO.StreamWriter(location);
            int[] inputSet = { 1 }; // 20x
            int kiekis = 3; // 3
            Combinations<int> combinations = new Combinations<int>(inputSet, kiekis, GenerateOption.WithRepetition);
            file.WriteLine("Kiekis 1: " + combinations.Count); // console

            int[] inputSet2 = { 1, 2 }; // 40x
            int kiekis2 = 6; // 6
            Combinations<int> combinations2 = new Combinations<int>(inputSet2, kiekis2, GenerateOption.WithRepetition);
            file.WriteLine("Kiekis 2: " + combinations2.Count);

            int[] inputSet3 = { 1, 2 }; // 60x
            int kiekis3 = 6; // 6
            Combinations<int> combinations3 = new Combinations<int>(inputSet3, kiekis3, GenerateOption.WithRepetition);
            file.WriteLine("Kiekis 3: " + combinations3.Count);

            file.WriteLine("Bendras kiekis: " + combinations.Count * combinations2.Count * combinations3.Count);
            for (int kiek = 0; kiek < 1; kiek++)
            {
                foreach (IList<int> v in combinations)
                {
                    foreach (IList<int> v2 in combinations2)
                    {
                        foreach (IList<int> v3 in combinations3)
                        {
                            for (int i = 0; i < kiekis; i++)
                            {
                                file.Write(v[i]);
                                if (i != kiekis - 1)
                                {
                                    file.Write(" ");
                                }
                                else
                                {
                                    file.Write(" | ");
                                }
                            }
                            for (int j = 0; j < kiekis2; j++)
                            {
                                file.Write(v2[j]);
                                if (j != kiekis2 - 1)
                                {
                                    file.Write(" ");
                                }
                                else
                                {
                                    file.Write(" | ");
                                }
                            }
                            for (int h = 0; h < kiekis3; h++)
                            {
                                file.Write(v3[h]);
                                if (h != kiekis3 - 1)
                                {
                                    file.Write(" ");
                                }
                                else
                                {
                                    file.Write(Environment.NewLine);
                                }
                            }
                        }
                        file.Write(Environment.NewLine);
                    }
                    file.Write(Environment.NewLine + Environment.NewLine);
                }
            }
            file.Close();
        }
        // PREP WORK END        // CALC-MAIN
        private void bw_DoWork(object sender, DoWorkEventArgs e) // CALC-MAIN-->START
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int t = 10; int c = t - 1; int k = 50; // 10 ir 20
            int kiekis = k * sabl.SablonoNr.Count;
            int kiekLiko = 1000; int kiek = kiekLiko; int SUMA = 0; // 1000
            worker.ReportProgress(SUMA);
            NykstukuFabrikas(kiekis);
            Testing(t);
            int min = RandomList.random[0].liekana;
            worker.ReportProgress(SUMA++);
            while (kiek != 0)
            {
                CloneBest(c); 
                Testing(t);
                if (RandomList.random[0].liekana < min)
                {
                    min = RandomList.random[0].liekana;
                    kiek = kiekLiko;
                }
                if (RandomList.random[0].liekana == min)
                {
                    kiek--;
                }
                if (RandomList.random[0].liekana > min)
                {
                    kiek = kiekLiko;
                }
                worker.ReportProgress(SUMA++);
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int kiek = e.ProgressPercentage;
            label3.Text = "Kiek liko procentu: " + kiek;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Print();
            button1.Enabled = true;
        }
        // CALC-MAIN--> FUNCTIONS
        private void NykstukuFabrikas(int kiekis)
        {
            Random r = new Random();
            for (int kiek = 0; kiek < kiekis; kiek++)
            {
                Randomiser = new RandomiserClass();
                Randomiser.sablonas = sabl;
                List<int> viso = new List<int> { };
                List<int> likeNr = new List<int> { };
                for (int i = 0; i < visoP.kiekis.Count; i++)
                {
                    Randomiser.suma.Add(visoP.kiekis[i]);
                    viso.Add(visoP.kiekis[i]);
                }
                for (int i = 0; i < Randomiser.sablonas.SablonoNr.Count; i++)
                {
                    Randomiser.kiekis.Add(0);
                    likeNr.Add(Randomiser.sablonas.SablonoNr[i]);
                }
                while (likeNr.Count != 0)
                {
                    int randomSablNr = r.Next(0, likeNr.Count);
                    int min = 99999;
                    for (int j = 0; j < Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis.Count; j++)
                    {
                        int index = visoP.ilgis.IndexOf(Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis[j]);
                        if (index != -1)
                        {
                            int dalyba = Convert.ToInt32(Math.Floor(Convert.ToDouble(viso[index]) / Convert.ToDouble(Randomiser.sablonas.SablonoElem[randomSablNr].Kiekis[j]))); // visoP.kiekis
                            if (dalyba < min)
                            {
                                min = dalyba;
                            }
                        }
                    }
                    if (min == 0)
                    {
                        likeNr.RemoveAt(randomSablNr);
                    }
                    else
                    {
                        int atm = r.Next(0, min + 1);
                        Randomiser.kiekis[randomSablNr] += atm;
                        for (int j = 0; j < Randomiser.sablonas.SablonoElem[randomSablNr].Kiekis.Count; j++)
                        {
                            int atimti = Randomiser.sablonas.SablonoElem[randomSablNr].Kiekis[j] * atm;
                            int index = visoP.ilgis.IndexOf(Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis[j]);
                            viso[index] -= atimti;
                        }
                    }
                }
                RandomList.random.Add(Randomiser);
            }
        }
        private void Testing(int dalyba)
        {
            RandomElements rand = new RandomElements();
            List<int> viso = new List<int> { };
            Boolean virsNulio = true;
            for (int i = 0; i < RandomList.random.Count; i++)
            {
                virsNulio = true;
                RandomList.random[i].suma.Clear();
                viso = new List<int> { };
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
                    if (viso[sum] < 0)
                    {
                        virsNulio = false;
                    }
                    RandomList.random[i].suma.Add(viso[sum]);
                    RandomList.random[i].ilgis.Add(visoP.ilgis[sum]);
                    suma += Math.Abs(viso[sum]);
                }
                RandomList.random[i].liekana = suma;
                if (virsNulio == true)
                {
                    rand.random.Add(RandomList.random[i]);
                }
            }
            RandomList.random = rand.random;
            rand = new RandomElements();
            List<int> k = new List<int> { };
            int KiekAtrinktiVariantu = Convert.ToInt32(Math.Floor(Convert.ToDouble(RandomList.random.Count) / dalyba));
            for (int i = 0; i < Math.Min(RandomList.random.Count, KiekAtrinktiVariantu); i++)
            {
                int min = 999999999; int mn = -1;
                for (int j = 0; j < RandomList.random.Count; j++)
                {
                    if (RandomList.random[j].liekana < min && k.IndexOf(j) == -1)
                    {
                        min = RandomList.random[j].liekana;
                        mn = j;
                    }
                }
                k.Add(mn);
            }
            for (int i = 0; i < k.Count; i++)
            {
                rand.random.Add(RandomList.random[k[i]]);
            }
            RandomList = rand;
        }
        private void CloneBest(int kiekis)
        {
            Random r = new Random();
            int pradiniai = RandomList.random.Count;
            for (int i = 0; i < pradiniai; i++)
            {
                for (int kiek = 0; kiek < kiekis; kiek++)
                {
                    Randomiser = new RandomiserClass();
                    Randomiser.sablonas = sabl;
                    for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                    {
                        Randomiser.suma.Add(RandomList.random[i].suma[j]);
                    }
                    for (int j = 0; j < RandomList.random[i].kiekis.Count; j++)
                    {
                        Randomiser.kiekis.Add(RandomList.random[i].kiekis[j]);
                    }

                    for (int j = 0; j < Randomiser.sablonas.SablonoElem.Count; j++)  // Take out.
                    {
                        int pp = r.Next(10, 20); // 10
                        int KiekNaikinti = Convert.ToInt32(Math.Floor(Convert.ToDouble(Randomiser.kiekis[j]) / pp));
                        Randomiser.kiekis[j] -= KiekNaikinti;
                        for (int h = 0; h < Randomiser.sablonas.SablonoElem[j].Kiekis.Count; h++)
                        {
                            int prideti = Randomiser.sablonas.SablonoElem[j].Kiekis[h] * KiekNaikinti;
                            int index = visoP.ilgis.IndexOf(Randomiser.sablonas.SablonoElem[j].JuostIlgis[h]);
                            Randomiser.suma[index] += prideti;
                        }
                    }
                    RandomList.random.Add(Randomiser);
                }
            }
            for (int i = 0; i < RandomList.random.Count; i++)   // Put back in
            {
                List<int> likeNr = new List<int> { };
                for (int h = 0; h < RandomList.random[i].sablonas.SablonoNr.Count; h++)
                {
                    likeNr.Add(RandomList.random[i].sablonas.SablonoNr[h]);
                }
                while (likeNr.Count != 0)
                {
                    int randomSablNr = r.Next(0, likeNr.Count);
                    int min = 99999;
                    for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem[randomSablNr].JuostIlgis.Count; j++)
                    {
                        int index = visoP.ilgis.IndexOf(RandomList.random[i].sablonas.SablonoElem[randomSablNr].JuostIlgis[j]);
                        if (index != -1)
                        {
                            int dalyba = Convert.ToInt32(Math.Floor(Convert.ToDouble(RandomList.random[i].suma[index]) / Convert.ToDouble(RandomList.random[i].sablonas.SablonoElem[randomSablNr].Kiekis[j]))); // visoP.kiekis
                            if (dalyba < min)
                            {
                                min = dalyba;
                            }
                        }
                    }
                    if (min == 0)
                    {
                        likeNr.RemoveAt(randomSablNr);
                    }
                    else
                    {
                        int atm = r.Next(0, min + 1);
                        RandomList.random[i].kiekis[randomSablNr] += atm;
                        for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem[randomSablNr].Kiekis.Count; j++)
                        {
                            int atimti = RandomList.random[i].sablonas.SablonoElem[randomSablNr].Kiekis[j] * atm;
                            int index = visoP.ilgis.IndexOf(RandomList.random[i].sablonas.SablonoElem[randomSablNr].JuostIlgis[j]);
                            RandomList.random[i].suma[index] -= atimti;
                        }
                    }
                }
            }
        }
        private void Print()
        {
            richTextBox2.Text += "Atsakymas: " + "\n";
            for (int i = 0; i < Math.Min(1, RandomList.random.Count); i++)
            {
                richTextBox2.Text += "Liekana isskirscius po sablonus: " + RandomList.random[i].liekana + "\n";
                for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                {
                    richTextBox2.Text += RandomList.random[i].ilgis[j] + "= " + RandomList.random[i].suma[j] + ";  ";
                }
                richTextBox2.Text += "\n";
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++)
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        richTextBox2.Text += "Sablonas " + RandomList.random[i].sablonas.SablonoNr[j] + " panaudotas " + RandomList.random[i].kiekis[j] + "   kartu." + "\n";
                    }
                }
            }
        }
        // CALC-MAIN--> FUNCTIONS END       // CALC END
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
    public List<double> santykis = new List<double> { };
    public List<string> tipai = new List<string>{};
    public List<int> kiekis = new List<int> { };
    public List<int> ilgis = new List<int> { };
}
public class RandomiserClass
{
    public Sablonai sablonas = new Sablonai();
    public List<int> kiekis = new List<int> { };
    public List<int> suma = new List<int> { };
    public List<int> ilgis = new List<int> { };
    public int liekana;
}
public class RandomElements
{
    public List<RandomiserClass> random = new List<RandomiserClass> { };
}