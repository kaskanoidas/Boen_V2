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
        // Globalus kintamieji:
        Rusys duom = new Rusys();
        Rusis duomenys = new Rusis();
        Sablonai sablConst = new Sablonai();
        Sablonai sabl = new Sablonai();
        Elementas elem = new Elementas();
        Uzklausa problem = new Uzklausa();
        Uzklausa visoP = new Uzklausa();
        RandomiserClass Randomiser = new RandomiserClass();
        RandomElements RandomList = new RandomElements();
        List<string> AtrinktiTipai = new List<string> { };
        List<string> AtrinktosSpalvos = new List<string> { };
        List<int> AtrinktuSumos = new List<int> { };
        List<Combinations<string>> combinationsList = new List<Combinations<string>> { };
        List<int> kiekiai = new List<int> { };
        List<IList<string>> NR = new List<IList<string>> { };
        SubSablonai subsabl = new SubSablonai();
        SubElementas subelem = new SubElementas();
        Sablonai newsabl = new Sablonai();
        Boolean kill;
        Boolean uzbaigti;
        int SukurtuSkaicius;
        DateTime time;
        DateTime now;
        DateTime now2;
        DateTime now3;
        DateTime now4;
        DateTime now5;
        DateTime now6;
        // Pradinių duomenų gavimas ir apdorojimas:
        public Form1()
        {
            InitializeComponent();
            GetRusisDuomenys();
            GetSablonuDuomenys();
            GetJuosteliuIlgiai();
            Spalvos();
        }
        private void Spalvos()
        {
            AtrinktosSpalvos = new List<string> {"FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000", 
        "800000", "008000", "000080", "808000", "800080", "008080", "808080", 
        "C00000", "00C000", "0000C0", "C0C000", "C000C0", "00C0C0", "C0C0C0", 
        "400000", "004000", "000040", "404000", "400040", "004040", "404040", 
        "200000", "002000", "000020", "202000", "200020", "002020", "202020", 
        "600000", "006000", "000060", "606000", "600060", "006060", "606060", 
        "A00000", "00A000", "0000A0", "A0A000", "A000A0", "00A0A0", "A0A0A0", 
        "E00000", "00E000", "0000E0", "E0E000", "E000E0", "00E0E0", "E0E0E0",  };
        }
        private void GetRusisDuomenys()
        {
            comboBox1.Items.Add("<Surasti automatiškai>");
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
                        Pradzia = Convert.ToDouble(vardas.Substring(tarpas + 1, galas - tarpas - 1)); // OAK Finale 0-20 gadina kazkodel.
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
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1 && comboBox3.SelectedIndex != -1 && textBox1.Text != "")
            {
                if (richTextBox1.Text != "")
                {
                    richTextBox1.Text += '\n';
                }
                richTextBox1.Text += comboBox2.SelectedItem + " " + comboBox3.SelectedItem + " " + textBox1.Text;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Natur 20 3590" + "\n" + "Natur 40 8820" + "\n" + "Natur 50 5040" + "\n" + "Natur 60 6930" + "\n";
            richTextBox1.Text += "Baltic1.5 40 3640" + "\n" + "Baltic1.5 50 2520" + "\n" + "Baltic1.5 60 2310";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            time = new DateTime();
            time = DateTime.Now;
            uzbaigti = false;
            button2.Enabled = true;
            SukurtuSkaicius = 0;
            if (richTextBox1.Text == "")
            {
                richTextBox2.Text = "Įveskite užsakymą.";
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
                        richTextBox2.Text += "Rasta parketo rūšis:  " + duom.vardas[nr] + "\n";
                    }
                }
                else
                {
                    ReadTextBox();
                    nr = comboBox1.SelectedIndex - 1;
                    richTextBox2.Text += "Pasirinkta parketo rūšis:  " + duom.vardas[nr] + "\n";
                }
                if (nr != -1 && nr!= -2 && nr != -3)
                {
                    AtrinktiSchemas();
                    kill = false;
                    AtrinktiTinkamusVariantus(nr);
                    if (sabl.SablonoNr.Count == 0 || kill == true)
                    {
                        richTextBox2.Text += "Pasirinkta parketo rūšis netinkama (nerasta tinkamų schemų)";
                        button1.Enabled = true;
                    }
                    else
                    {
                        PrintSchemas();
                        BackgroundWorker bw = new BackgroundWorker();
                        bw.WorkerSupportsCancellation = true;
                        bw.WorkerReportsProgress = true;
                        bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                        bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                        now5 = DateTime.Now;
                        if (bw.IsBusy != true)
                        {
                            bw.RunWorkerAsync();
                        }
                    }
                }
                else if (nr == -2)
                {
                    richTextBox2.Text = "Blogas užsakymo formulavimas.";
                    button1.Enabled = true;
                }
                else if (nr == -1)
                {
                    richTextBox2.Text += "Nerasta parketo rūšis.";
                    button1.Enabled = true;
                }
            }
        }
        private void Reset()
        {
            problem = new Uzklausa();
            visoP = new Uzklausa();
            Randomiser = new RandomiserClass();
            RandomList = new RandomElements();
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
        private int RastiTinkamiausiaRusi()
        {
            AtrinktiTipai = new List<string> { };
            AtrinktuSumos = new List<int> { };
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
        private int TikrintiPasirinkima(int nr)
        {
            if (duom.Rus[nr].pav.Count == AtrinktiTipai.Count)
            {
                int count = 0;
                for (int j = 0; j < duom.Rus[nr].pav.Count; j++)
                {

                    if (AtrinktiTipai.IndexOf(duom.Rus[nr].pav[j]) >= 0)
                    {
                        count++;
                    }
                }
                if (count == AtrinktiTipai.Count)
                {
                        return nr;
                }
            }
            return -1;
        }
        private void AtrinktiSchemas()
        {
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
                }
            }
        }
        private void PrintSchemas()
        {
            richTextBox2.Text += "Schemos atitinkančios užklausos parametrus:" + "\n";
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                richTextBox2.Text += sabl.SablonoNr[i] + " ";
            }
            richTextBox2.Text += "\n";
        }
        private void AtrinktiTinkamusVariantus(int parketoRusis)
        {
            newsabl = new Sablonai();
            subsabl = new SubSablonai();
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                KurtiVariantus(i, parketoRusis);
            }
            sabl = newsabl;
        }
        private void KurtiVariantus(int SablonoNr, int parketoRusis)
        {
            combinationsList = new List<Combinations<string>> { };
            kiekiai = new List<int> { };
            NR = new List<IList<string>> { };
            for(int i = 0; i < sabl.SablonoElem[SablonoNr].JuostIlgis.Count; i++)
            {
                int k = 0;
                List<string> inputSet = new List<string> { };
                for (int j = 0; j < problem.ilgis.Count; j++)
                {
                    if (problem.ilgis[j] == sabl.SablonoElem[SablonoNr].JuostIlgis[i])
                    {
                        ++k;
                        inputSet.Add(problem.tipai[j]);
                    }
                }
                int kiekis = sabl.SablonoElem[SablonoNr].Kiekis[i];
                Combinations<string> combinations = new Combinations<string>(inputSet, kiekis, GenerateOption.WithRepetition);
                combinationsList.Add(combinations);
                kiekiai.Add(kiekis);
                NR.Add(null);
            }
            rekursiveKurimas(0, SablonoNr, parketoRusis);
        }
        private void rekursiveKurimas(int nr, int SablonoNr, int parketoRusis)
        {
            foreach (IList<string> v in combinationsList[nr])
            {
                NR[nr] = v;
                if (nr != combinationsList.Count - 1)
                {
                    rekursiveKurimas(nr + 1, SablonoNr, parketoRusis);
                }
                else
                {
                    List<int> suma = new List<int> { };
                    List<int> ks = new List<int> { };
                    List<List<int>> Lks = new List<List<int>> { };
                    for (int s = 0; s < AtrinktiTipai.Count; s++)
                    {
                        suma.Add(0);
                    }
                    for (int i = 0; i < kiekiai.Count; i++)
                    {
                        int index;
                        ks = new List<int> { };
                        for (int s = 0; s < AtrinktiTipai.Count; s++)
                        {
                            ks.Add(0);
                        }
                        for (int j = 0; j < kiekiai[i]; j++)
                        {
                            index = AtrinktiTipai.IndexOf(NR[i][j]);
                            if (index < 0)
                            {
                                kill = true;
                            }
                            else
                            {
                                ks[index] += 1;
                            }
                        }
                        for (int s = 0; s < AtrinktiTipai.Count; s++)
                        {
                            suma[s] += ks[s] * sabl.SablonoElem[SablonoNr].JuostIlgis[i];
                        }
                        Lks.Add(ks);
                    }
                    Boolean tinka = true;
                    for (int s = 0; s < AtrinktiTipai.Count; s++)
                    {
                        int index = duom.Rus[parketoRusis].pav.IndexOf(AtrinktiTipai[s]);
                        if (index < 0)
                        {
                            tinka = false;
                            kill = true;
                        }
                        else
                        {
                            int pr = Convert.ToInt32(duom.Rus[parketoRusis].pradzia[index] * 660 / 100);
                            int pb = Convert.ToInt32(duom.Rus[parketoRusis].pabaiga[index] * 660 / 100);
                            int paklaidosRibaPr = Convert.ToInt32(Math.Floor(Convert.ToDouble(pr * 0.05)));
                            int paklaidosRibaPb = Convert.ToInt32(Math.Floor(Convert.ToDouble(pb * 0.05)));
                            if (suma[s] < pr - paklaidosRibaPr || suma[s] > pb + paklaidosRibaPb)
                            {
                                tinka = false;
                            }
                        }
                    }
                    if (tinka == true)
                    {
                        if (newsabl.SablonoNr.IndexOf(sabl.SablonoNr[SablonoNr]) < 0)
                        {
                            newsabl.SablonoNr.Add(sabl.SablonoNr[SablonoNr]);
                            newsabl.SablonoElem.Add(sabl.SablonoElem[SablonoNr]);
                        }
                        subelem = new SubElementas();
                        subsabl.SablonoNr.Add(sabl.SablonoNr[SablonoNr]);
                        subsabl.SablonoSubNr.Add(subsabl.SablonoSubNr.Count + 1);
                        for (int i = 0; i < kiekiai.Count; i++)
                        {
                            for (int s = 0; s < AtrinktiTipai.Count; s++)
                            {
                                subelem.JuostTipas.Add(AtrinktiTipai[s]);
                                subelem.JuostIlgis.Add(sabl.SablonoElem[SablonoNr].JuostIlgis[i]);
                                subelem.Kiekis.Add(Lks[i][s]);
                            }
                        }
                        subsabl.SablonoElem.Add(subelem);
                    }
                }
            }
        }
        // Pagrindinis skaičiavimas ir rezultatų spausdinimas:
        private void bw_DoWork(object sender, DoWorkEventArgs e)//MAIN
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int t = 10; int c = t - 1; int k = 30; // 30
            int kiekis = k * subsabl.SablonoSubNr.Count;
            //int kiekis = Convert.ToInt32(Math.Floor(Convert.ToDouble(10000) / Convert.ToDouble(subsabl.SablonoSubNr.Count)));
            //int kiekLiko = Convert.ToInt32(textBox2.Text); 
            int kiek = 0; int SUMA = 0;
            worker.ReportProgress(SUMA);
            NykstukuFabrikas(kiekis);
            now = DateTime.Now;
            Testing(t);
            now2 = DateTime.Now;
            int min = 0;
            worker.ReportProgress(SUMA++);

            //CloneBest(c);
            //Testing(t);

            while (kiek < Convert.ToInt32(textBox2.Text) && uzbaigti == false) // liekana => pagamintaDetaliu
            {
                CloneBest(c);
                Testing(t);
                if (RandomList.random[0].pagamintaDetaliu > min)
                {
                    min = RandomList.random[0].pagamintaDetaliu;
                    kiek = 0;
                }
                if (RandomList.random[0].pagamintaDetaliu == min)
                {
                    kiek++;
                }
                if (RandomList.random[0].pagamintaDetaliu < min)
                {
                    kiek = 0;
                }
                SukurtuSkaicius = RandomList.random[0].pagamintaDetaliu;
                worker.ReportProgress(SUMA++);
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int kiek = e.ProgressPercentage;
            label3.Text = "Iteracijų skaičius: " + kiek;
            label10.Text = "Sukurtų detalių skaičius: " + SukurtuSkaicius;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //long laikas = now5.Ticks - time.Ticks;
            //richTextBox1.Text += "\n" + "Laikas iki paleidimo: " + Convert.ToDouble(laikas / 10000).ToString() + "\n";
            //laikas = now.Ticks - time.Ticks;
            //richTextBox1.Text += "\n" + "Laikas farikas: " + Convert.ToDouble(laikas / 10000).ToString() + "\n";
            //laikas = now2.Ticks - now.Ticks;
            //richTextBox1.Text += "\n" + "Laikas testing: " + Convert.ToDouble(laikas / 10000).ToString() + "\n";
            //laikas = now3.Ticks - now2.Ticks;
            //richTextBox1.Text += "\n" + "Laikas clone kurimas: " + Convert.ToDouble(laikas / 10000).ToString() + "\n";
            //laikas = now4.Ticks - now3.Ticks;
            //richTextBox1.Text += "\n" + "Laikas clone tvarkymas: " + Convert.ToDouble(laikas / 10000).ToString() + "\n";
            //laikas = now5.Ticks - now4.Ticks;
            //laikas = now6.Ticks - now5.Ticks;
            Print();
            button1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            uzbaigti = true;
            button2.Enabled = false;
        }
        private void NykstukuFabrikas(int kiekis)
        {
            Random r = new Random();
            for (int kiek = 0; kiek < kiekis; kiek++)
            {
                Randomiser = new RandomiserClass();
                Randomiser.sablonas = subsabl;
                List<int> viso = new List<int> { };
                List<int> likeNr = new List<int> { };
                for (int i = 0; i < problem.kiekis.Count; i++)
                {
                    viso.Add(problem.kiekis[i]);
                }
                for (int i = 0; i < Randomiser.sablonas.SablonoSubNr.Count; i++)
                {
                    Randomiser.kiekis.Add(0);
                    likeNr.Add(Randomiser.sablonas.SablonoSubNr[i]);
                }
                while (likeNr.Count != 0)
                {
                    int randomSablNr = r.Next(0, likeNr.Count);
                    int min = 99999;
                    for (int j = 0; j < Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis.Count; j++)
                    {
                        for (int h = 0; h < viso.Count; h++)
                        {
                            if (problem.ilgis[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis[j] && problem.tipai[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostTipas[j])
                            {
                                double dal = Convert.ToDouble(Randomiser.sablonas.SablonoElem[randomSablNr].Kiekis[j]);
                                int dalyba;
                                if(dal != 0)
                                {
                                    dalyba = Convert.ToInt32(Math.Floor(Convert.ToDouble(viso[h]) / dal));
                                }
                                else
                                {
                                    dalyba = 9999999;
                                }
                                if (dalyba < min)
                                {
                                    min = dalyba;
                                }
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
                            for (int h = 0; h < viso.Count; h++)
                            {
                                if (problem.ilgis[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis[j] && problem.tipai[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostTipas[j])
                                {
                                    viso[h] -= atimti;
                                }
                            }
                        }
                    }
                }
                RandomElements rand = new RandomElements();
                Boolean virsNulio = true;
                int suma = 0;
                for (int sum = 0; sum < viso.Count; sum++)
                {
                    if (viso[sum] < 0)
                    {
                        virsNulio = false;
                    }
                    Randomiser.suma.Add(viso[sum]);
                    Randomiser.ilgis.Add(problem.ilgis[sum]);
                    Randomiser.tipas.Add(problem.tipai[sum]);
                    suma += Math.Abs(viso[sum]);
                }
                Randomiser.liekana = suma;
                if (virsNulio == true)
                {
                    int pagaminta = 0;
                    for (int j = 0; j < Randomiser.sablonas.SablonoElem.Count; j++)
                    {
                        if (Randomiser.kiekis[j] != 0)
                        {
                            pagaminta += Randomiser.kiekis[j];
                        }
                    }
                    Randomiser.pagamintaDetaliu = pagaminta;
                    RandomList.random.Add(Randomiser);
                }
            }
        }
        private void Testing(int dalyba)
        {
            RandomElements rand = new RandomElements();
            List<int> k = new List<int> { };
            int KiekAtrinktiVariantu = Convert.ToInt32(Math.Floor(Convert.ToDouble(RandomList.random.Count) / dalyba));
            for (int i = 0; i < Math.Min(RandomList.random.Count, KiekAtrinktiVariantu); i++)
            {
                int min = 0; int mn = -1;
                for (int j = 0; j < RandomList.random.Count; j++)
                {

                    if (RandomList.random[j].pagamintaDetaliu > min && k.IndexOf(j) == -1)
                    {
                        min = RandomList.random[j].pagamintaDetaliu;
                        mn = j;
                    }
                }
                k.Add(mn);
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
                    Randomiser.sablonas = subsabl;
                    Randomiser.liekana = RandomList.random[i].liekana;
                    Randomiser.pagamintaDetaliu = RandomList.random[i].pagamintaDetaliu;
                    for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                    {
                        Randomiser.suma.Add(RandomList.random[i].suma[j]);
                        Randomiser.ilgis.Add(RandomList.random[i].ilgis[j]);
                        Randomiser.tipas.Add(RandomList.random[i].tipas[j]);
                    }
                    for (int j = 0; j < RandomList.random[i].kiekis.Count; j++)
                    {
                        Randomiser.kiekis.Add(RandomList.random[i].kiekis[j]);
                    }
                    for (int j = 0; j < Randomiser.sablonas.SablonoElem.Count; j++)  // Take out.
                    {
                        int pp = r.Next(10, 20); // 10-20
                        int KiekNaikinti = Convert.ToInt32(Math.Floor(Convert.ToDouble(Randomiser.kiekis[j]) / pp));
                        Randomiser.kiekis[j] -= KiekNaikinti;
                        for (int h = 0; h < Randomiser.sablonas.SablonoElem[j].Kiekis.Count; h++)
                        {
                            int prideti = Randomiser.sablonas.SablonoElem[j].Kiekis[h] * KiekNaikinti;
                            for (int x = 0; x < problem.kiekis.Count; x++)
                            {
                                if (problem.ilgis[x] == Randomiser.sablonas.SablonoElem[j].JuostIlgis[h] && problem.tipai[x] == Randomiser.sablonas.SablonoElem[j].JuostTipas[h])
                                {
                                    Randomiser.suma[x] += prideti;
                                }
                            }
                        }
                    }
                    RandomList.random.Add(Randomiser);
                }
            }
            now3 = DateTime.Now;
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
                        for (int x = 0; x < problem.kiekis.Count; x++)
                        {
                            if (problem.ilgis[x] == RandomList.random[i].sablonas.SablonoElem[randomSablNr].JuostIlgis[j] && problem.tipai[x] == RandomList.random[i].sablonas.SablonoElem[randomSablNr].JuostTipas[j])
                            {
                                double dal = Convert.ToDouble(RandomList.random[i].sablonas.SablonoElem[randomSablNr].Kiekis[j]);
                                int dalyba;
                                if (dal != 0)
                                {
                                    dalyba = Convert.ToInt32(Math.Floor(Convert.ToDouble(RandomList.random[i].suma[x]) / dal));
                                }
                                else
                                {
                                    dalyba = 999999;
                                }
                                if (dalyba < min)
                                {
                                    min = dalyba;
                                }
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
                            for (int h = 0; h < problem.kiekis.Count; h++)
                            {
                                if (problem.ilgis[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostIlgis[j] && problem.tipai[h] == Randomiser.sablonas.SablonoElem[randomSablNr].JuostTipas[j])
                                {
                                    RandomList.random[i].suma[h] -= atimti;
                                }
                            }
                        }
                    }
                }
                int suma = 0;
                for (int sum = 0; sum < RandomList.random[i].suma.Count; sum++)
                {
                    suma += Math.Abs(RandomList.random[i].suma[sum]);
                }
                RandomList.random[i].liekana = suma;
                int pagaminta = 0;
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++)
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        pagaminta += RandomList.random[i].kiekis[j];
                    }
                }
                RandomList.random[i].pagamintaDetaliu = pagaminta;
            }
            now4 = DateTime.Now;
        }
        private void Print()
        {
            richTextBox2.AppendText("Atsakymas: " + "\n");
            for (int i = 0; i < Math.Min(1, RandomList.random.Count); i++)
            {
                richTextBox2.AppendText("Liekana išskirsčius po schemas: " + RandomList.random[i].liekana + "\n");
                for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                {
                    richTextBox2.AppendText(RandomList.random[i].tipas[j] + " " + RandomList.random[i].ilgis[j] + " " + RandomList.random[i].suma[j] + "\n");
                }
                int viso = 0;
                int pasikartojimas = -1;
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++)
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        if (RandomList.random[i].sablonas.SablonoNr[j] != pasikartojimas)
                        {
                            pasikartojimas = RandomList.random[i].sablonas.SablonoNr[j];
                            richTextBox2.AppendText("+-------------------------------------------------------------------------------------------------------------------------+" + '\n');
                            richTextBox2.AppendText("|| Schemos Nr: " + RandomList.random[i].sablonas.SablonoNr[j].ToString().PadLeft(4 - RandomList.random[i].sablonas.SablonoNr[j].ToString().Length) + " | Rušys: ");
                            for (int h = 0; h < AtrinktiTipai.Count; h++)
                            {
                                Color color = Color.FromArgb(Int32.Parse(AtrinktosSpalvos[h], System.Globalization.NumberStyles.HexNumber));
                                richTextBox2.SelectionStart = richTextBox2.TextLength;
                                richTextBox2.SelectionLength = 0;
                                richTextBox2.SelectionColor = color;
                                richTextBox2.AppendText(RandomList.random[i].sablonas.SablonoElem[j].JuostTipas[h]);
                                richTextBox2.SelectionColor = richTextBox2.ForeColor;
                                if (h != AtrinktiTipai.Count - 1)
                                {
                                    richTextBox2.AppendText(", ");
                                }
                                else
                                {
                                    richTextBox2.AppendText(" |" + '\n');
                                }
                            }
                            richTextBox2.AppendText("\n");
                            //richTextBox2.AppendText("+---------------------------+------------------------------------------------------------+" + '\n');
                            richTextBox2.AppendText("||| Kiek gaminti   | Ilgis * Kiekis ||..................." + "\n"); // bug 
                            //for (int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis.Count; h++)
                            //{
                            //    if (RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] != 0)
                            //    {
                            //        richTextBox2.AppendText(" Ilgis | Kiekis ||");
                            //    }
                            //}
                            //richTextBox2.AppendText("\n");
                        }
                        richTextBox2.AppendText("|||  " + RandomList.random[i].kiekis[j].ToString().PadRight(20 - RandomList.random[i].kiekis[j].ToString().Length) + " | ");
                        viso += RandomList.random[i].kiekis[j];
                        for (int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis.Count; h++ )
                        {
                            if (RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] != 0)
                            {
                                int variantas = AtrinktiTipai.IndexOf(RandomList.random[i].sablonas.SablonoElem[j].JuostTipas[h]);
                                Color color = Color.FromArgb(Int32.Parse(AtrinktosSpalvos[variantas], System.Globalization.NumberStyles.HexNumber));
                                richTextBox2.SelectionStart = richTextBox2.TextLength;
                                richTextBox2.SelectionLength = 0;
                                richTextBox2.SelectionColor = color;
                                richTextBox2.AppendText(RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis[h].ToString().PadLeft(6 - RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis[h].ToString().Length) + " * ");
                                richTextBox2.AppendText(RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h].ToString().PadRight(9 - RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h].ToString().Length) + " || ");
                                richTextBox2.SelectionColor = richTextBox2.ForeColor;
                            }
                        }
                        richTextBox2.AppendText("\n");
                    }
                }
                richTextBox2.AppendText("+-------------------------------------------------------------------------------------------------------------------------+" + '\n');
                richTextBox2.AppendText("Viso detalių sukurta: " + viso);
               // InsertTable(richTextBox2, 10, 10, 100, 700);
            }
        }
        //public void InsertTable(RichTextBox Rtb, int Rows, int Cols, int BrdrW, int TableWidth)
        //{
        //    String A; int i = 1, j = 1, a = 1, b = 1;
        //    A = @"{\rtf1\fbidis\ansi\ansicpg1252\deff0\deflang1033" + @"{\fonttbl{\f0\froman\fprq2\fcharset0 Times New Roman;}" + @"{\f1\fnil\fcharset0 Microsoft Sans Serif;}}" + @"\viewkind4\uc1\trowd";
        //    for (; j <= Rows; j++) // for every row
        //    {
        //        for (; i <= Cols; i++) // for every column
        //        {
        //            A += @"\clbrdrt\brdrw" + BrdrW + "\brdrs" + // + 
        //                 @"\clbrdrl\brdrw" + BrdrW + "\brdrs" +
        //                 @"\clbrdrb\brdrw" + BrdrW + "\brdrs" +
        //                 @"\clbrdrr\brdrw" + BrdrW + "\brdrs" +
        //                 @"\cellx" + ((TableWidth / Cols) * 10 * i);
        //        }
        //        A += @"\pard\intbl\ltrpar\lang1023\f0\fs24";
        //        for (; a <= Rows; a++)
        //        {
        //            A += @"\intbl\clmrg";
        //            for (; b <= Cols; b++) A += @"\cell";
        //            A += @"\row";
        //        }
        //    }
        //    A += "}";
        //    Rtb.SelectedRtf = A; // send table to RichTextBox 
        //    Rtb.Rtf = Rtb.Rtf.Replace("\\\'08rdrs", "");  // (\'08rdrs seems to be the side effect here)
        //}
    }
}
// Duomenų klasės:
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
    public SubSablonai sablonas = new SubSablonai();
    public List<int> kiekis = new List<int> { };
    public List<int> suma = new List<int> { };
    public List<int> ilgis = new List<int> { };
    public List<string> tipas = new List<string> { };
    public int liekana;
    public int pagamintaDetaliu;
}
public class RandomElements
{
    public List<RandomiserClass> random = new List<RandomiserClass> { };
}
public class SubSablonai
{
    public List<int> SablonoNr = new List<int> { };
    public List<int> SablonoSubNr = new List<int> { };
    public List<SubElementas> SablonoElem = new List<SubElementas> { };
}
public class SubElementas
{
    public List<string> JuostTipas = new List<string> { };
    public List<int> JuostIlgis = new List<int> { };
    public List<int> Kiekis = new List<int> { };
}
