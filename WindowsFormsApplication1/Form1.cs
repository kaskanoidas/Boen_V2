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
        List<string> AtrinktiTipai = new List<string> { };
        List<int> AtrinktuSumos = new List<int> { };
        System.IO.StreamWriter file;
        List<Combinations<string>> combinationsList = new List<Combinations<string>> { };
        List<int> kiekiai = new List<int> { };
        List<IList<string>> NR = new List<IList<string>> { };
        SubSablonai subsabl = new SubSablonai();
        SubElementas subelem = new SubElementas();
        Sablonai newsabl = new Sablonai();
        Boolean kill;
        public Form1()
        {
            InitializeComponent();
            GetRusisDuomenys();
            GetSablonuDuomenys();
            GetJuosteliuIlgiai();
        }
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
        private void button1_Click(object sender, EventArgs e)
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
                    if (TikrintiPasirinkima(nr) == nr)
                    {
                        richTextBox2.Text += "Pasirinkta parketo rusis:  " + duom.vardas[nr] + "\n";
                    }
                    else
                    {
                        nr = -3;
                        richTextBox2.Text += "Pasirinkta parketo rusis netinkama";
                        button1.Enabled = true;
                    }
                }
                if (nr != -1 && nr!= -2 && nr != -3)
                {
                    AtrinktiSchemas();
                    kill = false;
                    AtrinktiTinkamusVariantus(nr);
                    if (sabl.SablonoNr.Count == 0 || kill == true)
                    {
                        richTextBox2.Text += "Pasirinkta parketo rusis netinkama";
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
                        if (bw.IsBusy != true)
                        {
                            bw.RunWorkerAsync();
                        }
                    }
                }
                else if (nr == -2)
                {
                    richTextBox2.Text = "Blogas uzsakymo formulavimas.";
                    button1.Enabled = true;
                }
                else if (nr == -1)
                {
                    richTextBox2.Text += "Nerasta parketo rusis.";
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
            richTextBox2.Text += "Schemos atitinkancios uzklausos parametrus:" + "\n";
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                richTextBox2.Text += sabl.SablonoNr[i] + " ";
            }
            richTextBox2.Text += "\n";
        }
        private void AtrinktiTinkamusVariantus(int parketoRusis)
        {
            string location = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\SablonaiNew.txt";
            location = location.Substring(6);
            file = new System.IO.StreamWriter(location);
            newsabl = new Sablonai();
            subsabl = new SubSablonai();
            for (int i = 0; i < sabl.SablonoNr.Count; i++)
            {
                KurtiVariantus(i, parketoRusis);
            }
            sabl = newsabl;
            file.WriteLine("Viso rasta: " + subsabl.SablonoElem.Count);
            file.Close();
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
            file.Write(Environment.NewLine + Environment.NewLine);
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
                            file.WriteLine("Schemos Nr: " + sabl.SablonoNr[SablonoNr]);
                            for (int i = 0; i < sabl.SablonoElem[SablonoNr].JuostIlgis.Count; i++)
                            {
                                file.Write(sabl.SablonoElem[SablonoNr].JuostIlgis[i] + " X " + sabl.SablonoElem[SablonoNr].Kiekis[i] + " ");
                            }
                            file.Write(Environment.NewLine + Environment.NewLine);
                        }
                        subelem = new SubElementas();
                        subsabl.SablonoNr.Add(sabl.SablonoNr[SablonoNr]);
                        subsabl.SablonoSubNr.Add(subsabl.SablonoSubNr.Count + 1);
                        for (int i = 0; i < kiekiai.Count; i++)
                        {
                            for (int s = 0; s < AtrinktiTipai.Count; s++)
                            {
                                file.Write(AtrinktiTipai[s] + ": " + sabl.SablonoElem[SablonoNr].JuostIlgis[i] + " X " + Lks[i][s] + " ");
                                subelem.JuostTipas.Add(AtrinktiTipai[s]);
                                subelem.JuostIlgis.Add(sabl.SablonoElem[SablonoNr].JuostIlgis[i]);
                                subelem.Kiekis.Add(Lks[i][s]);
                            }
                            file.Write("        ");
                        }
                        subsabl.SablonoElem.Add(subelem);
                        for (int s = 0; s < AtrinktiTipai.Count; s++)
                        {
                            file.Write(suma[s] + " ");
                        }
                        file.Write(Environment.NewLine);
                    }
                }
            }
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)//test
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int t = 10; int c = t - 1; int k = 30; // 30
            int kiekis = k * sabl.SablonoNr.Count;
            int kiekLiko = 10; int kiek = kiekLiko; int SUMA = 0; //1000
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
            label3.Text = "Iteraciju skaicius: " + kiek;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Print();
            button1.Enabled = true;
        }
        private void NykstukuFabrikas(int kiekis) // TEST +
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
                    Randomiser.suma.Add(problem.kiekis[i]);
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
                RandomList.random.Add(Randomiser);
            }
        }
        private void Testing(int dalyba) // TEST +
        {
            RandomElements rand = new RandomElements();
            List<int> viso = new List<int> { };
            Boolean virsNulio = true;
            for (int i = 0; i < RandomList.random.Count; i++)
            {
                virsNulio = true;
                RandomList.random[i].suma.Clear();
                viso = new List<int> { };
                for (int get = 0; get < problem.kiekis.Count; get++)
                {
                    viso.Add(problem.kiekis[get]);
                }
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoNr.Count; j++)
                {
                    for(int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].Kiekis.Count; h++)
                    {
                        int atimti = RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] * RandomList.random[i].kiekis[j];
                        for (int x = 0; x < viso.Count; x++)
                        {
                            if (problem.ilgis[x] == Randomiser.sablonas.SablonoElem[j].JuostIlgis[h] && problem.tipai[x] == Randomiser.sablonas.SablonoElem[j].JuostTipas[h])
                            {
                                viso[x] -= atimti;
                            }
                        }
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
                    RandomList.random[i].ilgis.Add(problem.ilgis[sum]);
                    RandomList.random[i].tipas.Add(problem.tipai[sum]);
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
        private void CloneBest(int kiekis) // TEST
        {
            Random r = new Random();
            int pradiniai = RandomList.random.Count;
            for (int i = 0; i < pradiniai; i++)
            {
                for (int kiek = 0; kiek < kiekis; kiek++)
                {
                    Randomiser = new RandomiserClass();
                    Randomiser.sablonas = subsabl;
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
                                if(dal != 0)
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
            }
        }
        private void Print() // TEST +
        {
            richTextBox2.Text += "Atsakymas: " + "\n";
            for (int i = 0; i < Math.Min(1, RandomList.random.Count); i++)
            {
                richTextBox2.Text += "Liekana isskirscius po schemas: " + RandomList.random[i].liekana + "\n";
                for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                {
                    richTextBox2.Text += RandomList.random[i].tipas[j] + ": " + RandomList.random[i].ilgis[j] + "= " + RandomList.random[i].suma[j] + "\n";
                }
                richTextBox2.Text += "\n";
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++)
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        richTextBox2.Text += RandomList.random[i].sablonas.SablonoNr[j] + " X " + RandomList.random[i].kiekis[j] + " => ";
                        for (int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis.Count; h++ )
                        {
                            if (RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] != 0)
                            {
                                richTextBox2.Text += "    " + RandomList.random[i].sablonas.SablonoElem[j].JuostTipas[h] + ": " + RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis[h] + " X " + RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h];
                            }
                        }
                        richTextBox2.Text += "\n";
                    }
                }
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
