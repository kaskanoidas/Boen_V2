﻿using Combinatorics.Collections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
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
        Uzklausa problemOld = new Uzklausa();
        RandomElements RandomList = new RandomElements();
        List<string> AtrinktiTipai = new List<string> { };
        List<string> AtrinktosSpalvos = new List<string> { };
        List<Combinations<string>> combinationsList = new List<Combinations<string>> { };
        List<int> kiekiai = new List<int> { };
        List<IList<string>> NR = new List<IList<string>> { };
        SubSablonai subsabl = new SubSablonai();
        SubElementas subelem = new SubElementas();
        Sablonai newsabl = new Sablonai();
        Boolean kill;
        Boolean uzbaigti;
        int SukurtuSkaicius;
        int x;
        int y;
        //BackgroundWorker worker;
        LygciuSistema Lygtys = new LygciuSistema();
        SimplexLentele Lentele = new SimplexLentele();
        SimplexLentele LenteleBack = new SimplexLentele();
        List<int> Likuciai = new List<int> { };
        // Pradinių duomenų gavimas ir apdorojimas:
        public Form1()
        {
            InitializeComponent();
            GetRusisDuomenys();
            GetSablonuDuomenys();
            GetJuosteliuIlgiai();
            Spalvos();
            y = 419;
            x = 696;
        }
        private void Spalvos()
        {
            AtrinktosSpalvos = new List<string> {"FF0000", "00FF00", "0000FF", "FF00FF", "00FFFF", "000000", 
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
                galas = vardas.IndexOf("!", tarpas + 1);
                if (galas < 0)
                {
                    duom.Rus.Add(duomenys);
                }
                else
                {
                    Boolean endsakinys = false;
                    while (endsakinys != true)
                    {
                        string neleidziami = vardas.Substring(tarpas + 1, galas - tarpas - 1);
                        tarpas = galas;
                        string[] ne = neleidziami.Split();
                        int i = 1;
                        string budas = ne[0];
                        if (budas == "ND")
                        {
                            while (i < ne.Length)
                            {
                                duomenys.NeleidziamosSchemos.Add(ne[i++]);
                            }
                            galas = vardas.IndexOf("!", tarpas + 1);
                        }
                        else if (budas == "K")
                        {
                            while (i < ne.Length)
                            {
                                duomenys.KoDeti.Add(ne[i++]);
                                duomenys.PoKiekDeti.Add(int.Parse(ne[i++]));
                            }
                            galas = vardas.IndexOf("!", tarpas + 1);
                        }
                        else if (budas == "DT")
                        {
                            while (i < ne.Length)
                            {
                                duomenys.ButinosSchemos.Add(ne[i++]);
                            }
                            galas = vardas.IndexOf("!", tarpas + 1);
                        }
                        if (galas < 0)
                        {
                            endsakinys = true;
                        }
                    }
                    duom.Rus.Add(duomenys);
                }
            }
            file.Close();
            comboBox1.SelectedIndex = 0;
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
                sablConst.SablonoNr.Add(vardas[0]);
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
            List<int> ilgiai = new List<int> { };
            for (int i = 0; i < sablConst.SablonoElem.Count; i++)
            {
                for (int j = 0; j < sablConst.SablonoElem[i].JuostIlgis.Count; j++)
                {
                    int ilgis = sablConst.SablonoElem[i].JuostIlgis[j];
                    if (ilgiai.IndexOf(ilgis) < 0)
                    {
                        ilgiai.Add(ilgis);
                    }
                }
            }
            ilgiai.Sort();
            for (int j = 0; j < ilgiai.Count; j++)
            {
                comboBox3.Items.Add(ilgiai[j]);
            }
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
            button5.Visible = false;
            DisableAll();
            richTextBox2.Font = new Font(FontFamily.GenericMonospace, richTextBox2.Font.Size);
            uzbaigti = false;
            button2.Enabled = true;
            SukurtuSkaicius = 0;
            if (richTextBox1.Text == "")
            {
                richTextBox2.Text = "Įveskite užsakymą.";
                button2.Enabled = false;
                EnableAll();
            }
            else
            {
                button1.Enabled = false;
                richTextBox2.Text = "";
                Reset();
                int nr = comboBox1.SelectedIndex;
                ReadTextBox();
                SalintiNetinkamusTipus(nr);
                richTextBox2.Text += "Pasirinkta parketo rūšis:  " + duom.vardas[nr] + "\n";
                AtrinktiSchemas(nr);
                kill = false;
                BackgroundWorker bwVar = new BackgroundWorker();
                bwVar.WorkerSupportsCancellation = true;
                bwVar.WorkerReportsProgress = true;
                bwVar.DoWork += new DoWorkEventHandler(bwVar_DoWork);
                bwVar.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwVar_RunWorkerCompleted);
                bwVar.RunWorkerAsync(nr);
            }
        }
        private void Reset()
        {
            problem = new Uzklausa();
            visoP = new Uzklausa();
            RandomList = new RandomElements();
            problemOld = new Uzklausa();
            Lygtys = new LygciuSistema();
            Lentele = new SimplexLentele();
        }
        private int ReadTextBox()
        {
            string[] text = richTextBox1.Text.Split('\n');
            string tipas;
            int ilgis;
            int kiekis;
            for (int i = 0; i < text.Length; i++)
            {
                string[] eilute = text[i].Split();
                if (eilute.Length != 3)
                {
                    return -2;
                }
                tipas = eilute[0];
                int del = 0;
                if (int.TryParse(eilute[1], out del) == false)
                {
                    return -2;
                }
                ilgis = int.Parse(eilute[1]);
                if (int.TryParse(eilute[2], out del) == false)
                {
                    return -2;
                }
                kiekis = int.Parse(eilute[2]);
                Boolean rado = false;
                for (int j = 0; j < problem.tipai.Count; j++)
                {
                    if (problem.tipai[j] == tipas && problem.ilgis[j] == ilgis)
                    {
                        problem.kiekis[j] += kiekis;
                        rado = true;
                    }
                }
                if (rado == false)
                {
                    problem.tipai.Add(eilute[0]);
                    problem.ilgis.Add(int.Parse(eilute[1]));
                    problem.kiekis.Add(int.Parse(eilute[2]));
                }
            }
            return 0;
        }
        private void SalintiNetinkamusTipus(int RusiesNR)
        {
            problemOld = new Uzklausa();
            Uzklausa problemBack = new Uzklausa();
            AtrinktiTipai = new List<string> { };
            for (int i = 0; i < problem.tipai.Count; i++)
            {
                Boolean rado = false;
                for (int j = 0; j < duom.Rus[RusiesNR].pav.Count; j++)
                {
                    if (problem.tipai[i] == duom.Rus[RusiesNR].pav[j])
                    {
                        rado = true;
                    }
                }
                if (rado == true)
                {
                    problemBack.ilgis.Add(problem.ilgis[i]);
                    problemBack.kiekis.Add(problem.kiekis[i]);
                    problemBack.tipai.Add(problem.tipai[i]);
                }
                else
                {
                    problemOld.ilgis.Add(problem.ilgis[i]);
                    problemOld.kiekis.Add(problem.kiekis[i]);
                    problemOld.tipai.Add(problem.tipai[i]);
                }
            }
            problem = problemBack;
            for (int i = 0; i < problem.ilgis.Count; i++)
            {
                if (visoP.ilgis.IndexOf(problem.ilgis[i]) < 0)
                {
                    visoP.ilgis.Add(problem.ilgis[i]);
                }
            }
            for (int i = 0; i < problem.tipai.Count; i++)
            {
                if (AtrinktiTipai.IndexOf(problem.tipai[i]) < 0)
                {
                    AtrinktiTipai.Add(problem.tipai[i]);
                }
            }
        }
        private void AtrinktiSchemas(int RusiesNR)
        {
            sabl = new Sablonai();
            if (duom.Rus[RusiesNR].ButinosSchemos.Count > 0)
            {
                for (int i = 0; i < sablConst.SablonoNr.Count; i++)
                {
                    if (duom.Rus[RusiesNR].ButinosSchemos.IndexOf(sablConst.SablonoNr[i]) >= 0)
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
            }
            else
            {
                for (int i = 0; i < sablConst.SablonoNr.Count; i++)
                {
                    if (duom.Rus[RusiesNR].NeleidziamosSchemos.IndexOf(sablConst.SablonoNr[i]) < 0)
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
        private void bwVar_DoWork(object sender, DoWorkEventArgs e)
        {
            AtrinktiTinkamusVariantus((int)e.Argument);
        }
        private void bwVar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (sabl.SablonoNr.Count == 0 || kill == true)
            {
                richTextBox2.Text += "Pasirinkta parketo rūšis netinkama (nerasta tinkamų schemų)";
                button1.Enabled = true;
                button2.Enabled = false;
                EnableAll();
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
            for (int i = 0; i < sabl.SablonoElem[SablonoNr].JuostIlgis.Count; i++)
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
                    for (int i = 0; i < kiekiai.Count; i++) // tikrinti ar daugiau negu 1
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
                        int n = duom.Rus[parketoRusis].KoDeti.IndexOf(AtrinktiTipai[s]);
                        int k = 0;
                        if (n >= 0)
                        {
                            for (int i = 0; i < kiekiai.Count; i++)
                            {
                                k += Lks[i][s];
                            }
                            if (k != duom.Rus[parketoRusis].PoKiekDeti[n])
                            {
                                tinka = false;
                            }
                        }
                    }
                    if (tinka == true)
                    {
                        for (int s = 0; s < AtrinktiTipai.Count; s++)
                        {
                            int n = duom.Rus[parketoRusis].KoDeti.IndexOf(AtrinktiTipai[s]);
                            if (n < 0)
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
            LygciuSudarimas();
            RandomList = new RandomElements();
            //SimplexTree(Lentele); // su medziu
            //Testing(1);
            //Simplex(Lentele); // be medzio
            SimplexCiklas(Lentele); // su ciklu
            ;
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int kiek = e.ProgressPercentage;
            label10.Text = "Sukurtų detalių skaičius: " + kiek;
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //string help = e.Result.ToString();
            Print();
            button1.Enabled = true;
            EnableAll();
            if (RandomList.random.Count > 0)
            {
                button5.Visible = true;
            }
        }
        private void LygciuSudarimas()
        {
            Lygtys = new LygciuSistema();
            Lygtis LG = new Lygtis();
            for (int i = 0; i < problem.kiekis.Count; i++)
            {
                LG = new Lygtis();
                LG.LygtiesSprendinys = problem.kiekis[i];
                LG.LygtiesLiekana = problem.kiekis[i];
                for (int j = 0; j < subsabl.SablonoElem.Count; j++)
                {
                    for (int h = 0; h < subsabl.SablonoElem[j].JuostIlgis.Count; h++)
                    {
                        if (subsabl.SablonoElem[j].JuostIlgis[h] == problem.ilgis[i] && subsabl.SablonoElem[j].JuostTipas[h] == problem.tipai[i])
                        {
                            LG.kiekis.Add(subsabl.SablonoElem[j].Kiekis[h]);
                            LG.variantas.Add(subsabl.SablonoSubNr[j] - 1);
                        }
                    }
                }
                Lygtys.Lygtis.Add(LG);
            }
            Lentele = new SimplexLentele();
            LenteleBack = new SimplexLentele();
            eilute eil = new eilute();
            eilute eil2 = new eilute();
            for (int i = 0; i < problem.kiekis.Count; i++)
            {
                Lentele.BVS.Add(-1);
                LenteleBack.BVS.Add(-1);
                Lentele.RHS.Add(problem.kiekis[i]);
                LenteleBack.RHS.Add(problem.kiekis[i]);
                eil = new eilute();
                eil2 = new eilute();
                for (int j = 0; j < subsabl.SablonoElem.Count; j++)
                {
                    eil.eilutesReiksmes.Add(0);
                    eil2.eilutesReiksmes.Add(0);
                }
                for (int j = 0; j < Lygtys.Lygtis[i].variantas.Count; j++)
                {
                    eil.eilutesReiksmes[Lygtys.Lygtis[i].variantas[j]] = Lygtys.Lygtis[i].kiekis[j];
                    eil2.eilutesReiksmes[Lygtys.Lygtis[i].variantas[j]] = Lygtys.Lygtis[i].kiekis[j];
                }
                Lentele.eilutes.Add(eil);
                LenteleBack.eilutes.Add(eil2);
            }
            eil = new eilute();
            eil2 = new eilute();
            for (int j = 0; j < subsabl.SablonoElem.Count; j++)
            {
                eil.eilutesReiksmes.Add(1);
                eil2.eilutesReiksmes.Add(1);
            }
            Lentele.eilutes.Add(eil);
            LenteleBack.eilutes.Add(eil2);
        }
        //-------------------------------------------SU CIKLU-------------------------------------------
        private void SimplexCiklas(SimplexLentele Lentele)
        {
            SimplexLentele Lent = new SimplexLentele();
            List<int> Likutis = new List<int> { };
            int kiek = Lentele.eilutes[Lentele.eilutes.Count - 1].eilutesReiksmes.Count;
            int i;
            for (i = 0; i < kiek; i++) // kiek
            {
                SymplexCikloNR(Lentele, i);
                Testing(1);
            }
            ;
        }
        private int RastiMaxCjCikle(SimplexLentele Lent, int cikloNr)
        {
            double pradineReiksme = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[0];
            Boolean VisiVienodi = true;
            for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
            {
                if (Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i] != pradineReiksme)
                {
                    VisiVienodi = false;
                    i = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count;
                }
            }
            if (VisiVienodi == true)
            {
                return cikloNr;
            }
            else
            {
                double max = 0;
                int mx = 0;
                for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
                {
                    if (max < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i])
                    {
                        max = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i];
                        mx = i;
                    }
                }
                return mx;
            }
        }
        private void SymplexCikloNR(SimplexLentele Lentele, int cikloNr)
        {
            SimplexLentele Lent = new SimplexLentele();
            Lent = MakeLentelesCopy(Lentele);
            Boolean stop = false;
            RandomElements LocalList = new RandomElements();
            while (CheckArGalimaTestiSprendima(Lent) == true && stop == false)
            {
                List<int> Likutis = new List<int> { };
                int MaxCj = RastiMaxCjCikle(Lent, cikloNr);
                Tuple<int, double> back = RastiMinRHS(MaxCj, Lent);
                int MinRHS = back.Item1;
                int MinRHSReiksme = Convert.ToInt32(Math.Floor(back.Item2));
                Lent.BVS[MinRHS] = MaxCj;
                PertvarkytiPagrindineEilute(MinRHS, MaxCj, ref Lent);
                PertvarkytiLikusesEilutes(MinRHS, MaxCj, ref Lent);
                PertvarkytiPagrindiniStulpeli(MinRHS, MaxCj, ref Lent);
                PerskaiciuotiRHS(MinRHS, MaxCj, MinRHSReiksme, ref Lent, ref Likutis);
                stop = ParuostiSpausdinimui(Lent, Likutis, ref LocalList);
            }
            RandomList.random.AddRange(LocalList.random);
        }
        //-------------------------------------------SU MEDZIU-------------------------------------------
        private int SimplexTree(SimplexLentele Lent)
        {
            if (CheckArGalimaTestiSprendima(Lent) == true && uzbaigti == false)
            {
                SimplexTreeCj(Lent);
            }
            else
            {
                return 0;
            }
            return 0;
        }
        private int SimplexTreeCj(SimplexLentele Lent)
        {
            if (uzbaigti == false)
            {
                double max = 0;
                SimplexLentele LenteleMedziui = new SimplexLentele();
                for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
                {
                    if (max < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i])
                    {
                        max = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i];
                    }
                }
                for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
                {
                    if (max == Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i])
                    {
                        LenteleMedziui = MakeLentelesCopy(Lent);
                        SimplexTreeMinRHS(i, LenteleMedziui);
                    }
                }
            }
            else
            {
                return 0;
            }
            return 0;
        }
        private int SimplexTreeMinRHS(int MaxCj, SimplexLentele Lent)
        {
            if (uzbaigti == false)
            {
                double min = Lent.RHS[0];
                double tikrinti = 0;
                SimplexLentele LenteleMedziui = new SimplexLentele();
                for (int i = 0; i < Lent.eilutes.Count - 1; i++)
                {
                    if (Lent.eilutes[i].eilutesReiksmes[MaxCj] > 0)
                    {
                        tikrinti = Lent.RHS[i] / Lent.eilutes[i].eilutesReiksmes[MaxCj];
                        if (tikrinti < min && tikrinti > 0) // FIXED?
                        {
                            min = tikrinti;
                        }
                    }
                }
                for (int i = 0; i < Lent.eilutes.Count - 1; i++)
                {
                    tikrinti = Lent.RHS[i] / Lent.eilutes[i].eilutesReiksmes[MaxCj];
                    if (tikrinti == min)
                    {
                        LenteleMedziui = MakeLentelesCopy(Lent);
                        SimplexTreeTransformation(MaxCj, Tuple.Create(i, min), LenteleMedziui);
                    }
                }
            }
            else
            {
                return 0;
            }
            return 0;
        }
        private int SimplexTreeTransformation(int MaxCj, Tuple<int, double> back, SimplexLentele Lent)
        {
            if (uzbaigti == false)
            {
                int MinRHS = back.Item1;
                int MinRHSReiksme = Convert.ToInt32(Math.Floor(back.Item2));
                Lent.BVS[MinRHS] = MaxCj;
                PertvarkytiPagrindineEilute(MinRHS, MaxCj, ref Lent);
                PertvarkytiLikusesEilutes(MinRHS, MaxCj, ref Lent);
                PertvarkytiPagrindiniStulpeli(MinRHS, MaxCj, ref Lent);
                SimplexTreeRHSCount(MinRHS, MaxCj, MinRHSReiksme, ref Lent);
            }
            else
            {
                return 0;
            }
            return 0;
        }
        private int SimplexTreeRHSCount(int MinRHS, int MaxCj, int MinRHSReiksme, ref SimplexLentele Lent)
        {
            if (uzbaigti == false)
            {
                List<int> Likutis = new List<int> { };
                Lent.RHS[MinRHS] = MinRHSReiksme;
                for (int i = 0; i < LenteleBack.eilutes.Count - 1; i++)
                {
                    Likutis.Add(LenteleBack.RHS[i] - MinRHSReiksme * Convert.ToInt32(LenteleBack.eilutes[i].eilutesReiksmes[MaxCj]));
                }
                List<int> iList = new List<int> { };
                for (int i = 0; i < Likutis.Count; i++)
                {
                    if (i != MinRHS && Lent.BVS[i] >= 0)
                    {
                        iList.Add(i);
                    }
                }
                if (iList.Count >= 0)
                {
                    Permutations<int> variantai = new Permutations<int>(iList, GenerateOption.WithoutRepetition);
                    SimplexLentele LenteleMedziui = new SimplexLentele();
                    List<int> LikutisMedziui = new List<int> { };
                    foreach (IList<int> v in variantai)
                    {
                        if (uzbaigti == true)
                        {
                            return 0;
                        }
                        LikutisMedziui.Clear();
                        LikutisMedziui.AddRange(Likutis);
                        LenteleMedziui = MakeLentelesCopy(Lent);
                        for (int i = 0; i < v.Count; i++)
                        {
                            double min = Convert.ToDouble(LikutisMedziui[v[i]]);
                            for (int j = 0; j < LenteleBack.eilutes.Count - 1; j++)
                            {
                                if (LenteleBack.eilutes[j].eilutesReiksmes[LenteleMedziui.BVS[v[i]]] > 0)
                                {
                                    double kiekdalinti = Convert.ToDouble(LikutisMedziui[j]) / LenteleBack.eilutes[j].eilutesReiksmes[LenteleMedziui.BVS[v[i]]];
                                    if (kiekdalinti < min)
                                    {
                                        min = kiekdalinti;
                                    }
                                }
                            }
                            LenteleMedziui.RHS[v[i]] = Convert.ToInt32(Math.Floor(min));
                            // atimti is likuciu
                            for (int j = 0; j < LikutisMedziui.Count; j++)
                            {
                                LikutisMedziui[j] -= LenteleMedziui.RHS[v[i]] * Convert.ToInt32(LenteleBack.eilutes[j].eilutesReiksmes[Lent.BVS[v[i]]]); //?
                            }
                        }
                        for (int i = 0; i < LikutisMedziui.Count; i++)
                        {
                            if (i != MinRHS)
                            {
                                if (LenteleMedziui.BVS[i] < 0)
                                {
                                    LenteleMedziui.RHS[i] = LikutisMedziui[i];
                                }
                            }
                        }
                        ParuostiSpausdinimui(LenteleMedziui, LikutisMedziui, ref RandomList);
                        SimplexTree(LenteleMedziui);// sukti cikla?
                    }
                }
            }
            else
            {
                return 0;
            }
            return 0;
        }
        //-------------------------------------------BE MEDZIO -----------------------------------------
        private void Simplex(SimplexLentele Lentele) 
        {
            SimplexLentele Lent = new SimplexLentele();
            Lent = MakeLentelesCopy(Lentele);
            while (CheckArGalimaTestiSprendima(Lent) == true)
            {
                int MaxCj = RastiMaxCj(Lent);
                Tuple<int, double> back = RastiMinRHS(MaxCj, Lent);
                int MinRHS = back.Item1;
                int MinRHSReiksme = Convert.ToInt32(Math.Floor(back.Item2));
                Lent.BVS[MinRHS] = MaxCj;
                PertvarkytiPagrindineEilute(MinRHS, MaxCj, ref Lent);
                PertvarkytiLikusesEilutes(MinRHS, MaxCj, ref Lent);
                PertvarkytiPagrindiniStulpeli(MinRHS, MaxCj, ref Lent);
                List<int> Likutis = new List<int> { };
                PerskaiciuotiRHS(MinRHS, MaxCj, MinRHSReiksme, ref Lent, ref Likutis);
                ParuostiSpausdinimui(Lent, Likutis, ref RandomList);
            }
            Testing(1);
            ;
        }
        //----------------------------------------SIMPLEX METODAI------------------------------------------
        private SimplexLentele MakeLentelesCopy(SimplexLentele Lent)
        {
            SimplexLentele nauja = new SimplexLentele();
            eilute eil = new eilute();
            nauja.BVS.AddRange(Lent.BVS);
            for (int i = 0; i < Lent.eilutes.Count; i++)
            {
                eil = new eilute();
                eil.eilutesReiksmes.AddRange(Lent.eilutes[i].eilutesReiksmes);
                nauja.eilutes.Add(eil);
            }
            nauja.RHS.AddRange(Lent.RHS);
            return nauja;
        }
        private Boolean CheckArGalimaTestiSprendima(SimplexLentele Lent)
        {
            for (int i = 0; i < Lent.RHS.Count; i++)
            {
                if (Lent.RHS[i] < 0)
                {
                    return false;
                }
            }
            for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
            {
                double tikrinimui = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i];
                if (Math.Round(tikrinimui,2, MidpointRounding.ToEven) > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private int RastiMaxCj(SimplexLentele Lent)
        {
            double max = 0;
            int mx = 0;
            for (int i = 0; i < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes.Count; i++)
            {
                if (max < Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i])
                {
                    max = Lent.eilutes[Lent.eilutes.Count - 1].eilutesReiksmes[i];
                    mx = i;
                }
            }
            return mx;
        }
        private Tuple<int, double> RastiMinRHS(int MaxCj, SimplexLentele Lent)
        {
            double min = Lent.RHS[0];
            int mn = 0;
            double tikrinti = 0;
            for (int i = 0; i < Lent.eilutes.Count - 1; i++)
            {
                if (Lent.eilutes[i].eilutesReiksmes[MaxCj] > 0)
                {
                    tikrinti = Lent.RHS[i] / Lent.eilutes[i].eilutesReiksmes[MaxCj];
                    if (tikrinti < min && tikrinti > 0) // FIXED?
                    {
                        min = tikrinti;
                        mn = i;
                    }
                }
            }
            return Tuple.Create(mn, min);
        }
        private void PertvarkytiPagrindineEilute(int MinRHS, int MaxCj, ref SimplexLentele Lent)
        {
            double IsKoDalinti = Lent.eilutes[MinRHS].eilutesReiksmes[MaxCj];
            for (int i = 0; i < Lent.eilutes[MinRHS].eilutesReiksmes.Count; i++)
            {
                double beta = Lent.eilutes[MinRHS].eilutesReiksmes[i] / IsKoDalinti;
                Lent.eilutes[MinRHS].eilutesReiksmes[i] = Math.Round(beta, 10); // ?
            }
        }
        private void PertvarkytiLikusesEilutes(int MinRHS, int MaxCj, ref SimplexLentele Lent)
        {
            for (int i = 0; i < Lent.eilutes.Count; i++)
            {
                if (i != MinRHS)
                {
                    for (int j = 0; j < Lent.eilutes[i].eilutesReiksmes.Count; j++)
                    {
                        if (j != MaxCj)
                        {
                            double beta = Lent.eilutes[i].eilutesReiksmes[j] - Lent.eilutes[i].eilutesReiksmes[MaxCj] * Lent.eilutes[MinRHS].eilutesReiksmes[j];
                            //if (beta < 0)
                            //{
                            //    beta = 0;
                            //}
                            Lent.eilutes[i].eilutesReiksmes[j] = Math.Round(beta, 10);
                        }
                    }
                }
            }
        }
        private void PertvarkytiPagrindiniStulpeli(int MinRHS, int MaxCj, ref SimplexLentele Lent)
        {
            for (int i = 0; i < Lent.eilutes.Count; i++)
            {
                if (i != MinRHS)
                {
                    Lent.eilutes[i].eilutesReiksmes[MaxCj] = 0;
                }
            }
        }
        private void PerskaiciuotiRHS(int MinRHS, int MaxCj, int MinRHSReiksme, ref SimplexLentele Lent, ref List<int> Likuciai) // BUGGGGG!!!!!!!!!!!!!
        {
            Lent.RHS[MinRHS] = MinRHSReiksme;
            for (int i = 0; i < LenteleBack.eilutes.Count - 1; i++)
            {
                int likutis = LenteleBack.RHS[i] - MinRHSReiksme * Convert.ToInt32(LenteleBack.eilutes[i].eilutesReiksmes[MaxCj]);
                Likuciai.Add(likutis);
            }
            for (int i = 0; i < Likuciai.Count; i++)
            {
                if (i != MinRHS)
                {
                    if (Lent.BVS[i] >= 0)
                    {
                        // rasti minimuma is kiek galima dalinti.
                        double min = Convert.ToDouble(Likuciai[i]);
                        for (int j = 0; j < LenteleBack.eilutes.Count - 1; j++)
                        {
                            if (LenteleBack.eilutes[j].eilutesReiksmes[Lent.BVS[i]] > 0)
                            {
                                double kiekdalinti = Convert.ToDouble(Likuciai[j]) / LenteleBack.eilutes[j].eilutesReiksmes[Lent.BVS[i]]; // ????????????????????
                                if (kiekdalinti < min)
                                {
                                    min = kiekdalinti;
                                }
                            }
                        }
                        Lent.RHS[i] = Convert.ToInt32(Math.Floor(min));
                        // atimti is likuciu
                        for (int j = 0; j < Likuciai.Count; j++)
                        {
                            Likuciai[j] -= Lent.RHS[i] * Convert.ToInt32(LenteleBack.eilutes[j].eilutesReiksmes[Lent.BVS[i]]); //?
                        }
                    }
                }
            }
            // liekanos
            for (int i = 0; i < Likuciai.Count; i++)
            {
                if (i != MinRHS)
                {
                    if (Lent.BVS[i] < 0)
                    {
                        Lent.RHS[i] = Likuciai[i];
                    }
                }
            }
        }
        private Boolean ParuostiSpausdinimui(SimplexLentele Lent, List<int> Likutis, ref RandomElements rnd)
        { 
            RandomiserClass Randomiser = new RandomiserClass();
            Randomiser.sablonas = subsabl;
            for (int i = 0; i < Randomiser.sablonas.SablonoSubNr.Count; i++)
            {
                Randomiser.kiekis.Add(0);
            }
            int dabartinis = 0;
            Boolean blogas = false;
            for (int i = 0; i < Lent.RHS.Count; i++)
            {
                if (Lent.BVS[i] >= 0)
                {
                    if (Lent.RHS[i] < 0)
                    {
                        blogas = true;
                        return true;
                    }
                    dabartinis += Lent.RHS[i];
                    Randomiser.kiekis[Lent.BVS[i]] = Lent.RHS[i];
                }
            }
            if (blogas == false)
            {
                Randomiser.suma.AddRange(Likutis);
                for (int i = 0; i < Likutis.Count; i++)
                {
                    if (Likutis[i] < 0)
                    {
                        blogas = true;
                    }
                    Randomiser.liekana += Likutis[i];
                }
                if (blogas == false)
                {
                    Randomiser.ilgis.AddRange(problem.ilgis);
                    Randomiser.tipas.AddRange(problem.tipai);
                    Randomiser.pagamintaDetaliu = dabartinis;
                    if (rnd.random.Count > 0)
                    {
                        if (rnd.random[0].pagamintaDetaliu < dabartinis)
                        {
                            rnd.random.Clear();
                            rnd.random.Add(Randomiser);
                        }
                        if (rnd.random[0].pagamintaDetaliu == dabartinis)
                        {
                            rnd.random.Add(Randomiser);
                        }
                        if (rnd.random[0].pagamintaDetaliu > dabartinis)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        rnd.random.Add(Randomiser);
                    }
                }
            }
            return false;
        }
        private void Testing(int dalyba)
        {
            RandomElements rand = new RandomElements();
            List<int> k = new List<int> { };
            int KiekAtrinktiVariantu = Convert.ToInt32(Math.Floor(Convert.ToDouble(RandomList.random.Count) / dalyba));
            if (KiekAtrinktiVariantu < 10)
            {
                KiekAtrinktiVariantu = RandomList.random.Count;
            }
            for (int i = 0; i < KiekAtrinktiVariantu; i++)
            {
                int min = 0; int mn = 0;
                for (int j = 0; j < RandomList.random.Count; j++)
                {
                    Boolean neMinusas = true;
                    for (int h = 0; h < RandomList.random[j].suma.Count; h++ )
                    {
                        if (RandomList.random[j].suma[h] < 0)
                        {
                            neMinusas = false;
                        }
                    }
                    for (int h = 0; h < RandomList.random[j].kiekis.Count; h++)
                    {
                        if (RandomList.random[j].kiekis[h] < 0)
                        {
                            neMinusas = false;
                        }
                    }
                    if (RandomList.random[j].pagamintaDetaliu > min && k.IndexOf(j) == -1 && neMinusas == true)
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
        //------------------------------------SIMPLEX END-------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            uzbaigti = true;
            button2.Enabled = false;
        }
        private void Print()
        {
            // Atsakymas:
            richTextBox2.SelectionStart = richTextBox2.TextLength;
            richTextBox2.SelectionLength = 0;
            richTextBox2.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
            richTextBox2.AppendText('\n' + "Atsakymas: " + "\n");
            richTextBox2.SelectionFont = richTextBox2.Font;
            for (int i = 0; i < Math.Min(1, RandomList.random.Count); i++)
            {
                // Liekanos lentele
                int RusiesIlgis = 5;
                int IlgioIlgis = 5;
                int KiekioIlgis = 6;
                for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                {
                    if (RusiesIlgis < RandomList.random[i].tipas[j].Trim().Length)
                    {
                        RusiesIlgis = RandomList.random[i].tipas[j].Trim().Length;
                    }
                    if (IlgioIlgis < RandomList.random[i].ilgis[j].ToString().Length)
                    {
                        IlgioIlgis = RandomList.random[i].ilgis[j].ToString().Length;
                    }
                    if (KiekioIlgis < RandomList.random[i].suma[j].ToString().Length)
                    {
                        KiekioIlgis = RandomList.random[i].suma[j].ToString().Length;
                    }
                }
                for (int j = 0; j < problemOld.kiekis.Count; j++)
                {
                    if (RusiesIlgis < problemOld.tipai[j].Trim().Length)
                    {
                        RusiesIlgis = problemOld.tipai[j].Trim().Length;
                    }
                    if (IlgioIlgis < problemOld.ilgis[j].ToString().Length)
                    {
                        IlgioIlgis = problemOld.ilgis[j].ToString().ToString().Length;
                    }
                    if (KiekioIlgis < problemOld.kiekis[j].ToString().Length)
                    {
                        KiekioIlgis = problemOld.kiekis[j].ToString().Length;
                    }
                }
                int liekana = RandomList.random[i].liekana;
                for (int l = 0; l < problemOld.kiekis.Count; l++)
                {
                    liekana += problemOld.kiekis[l];
                }
                richTextBox2.AppendText("Liekana išskirsčius po schemas: " + liekana + "\n");
                string lentele = "|  " + "Rūšis".PadRight(RusiesIlgis) + "  |  " + "Ilgis".PadRight(IlgioIlgis) + "  |  " + "Kiekis".PadRight(KiekioIlgis) + "  |";
                string linija = "+";
                for (int j = 0; j < lentele.Length - 2; j++)
                {
                    linija += "-";
                }
                linija += "+";
                richTextBox2.AppendText(linija + '\n'); richTextBox2.AppendText(lentele + "\n"); richTextBox2.AppendText(linija + '\n');
                for (int j = 0; j < RandomList.random[i].suma.Count; j++)
                {
                    richTextBox2.AppendText("|  " + RandomList.random[i].tipas[j].PadRight(RusiesIlgis) + "  |  " + RandomList.random[i].ilgis[j].ToString().PadRight(IlgioIlgis) + "  |  " + RandomList.random[i].suma[j].ToString().PadRight(KiekioIlgis) + "  |" + "\n");
                }
                for (int j = 0; j < problemOld.kiekis.Count; j++)
                {
                    richTextBox2.AppendText("|  " + problemOld.tipai[j].PadRight(RusiesIlgis) + "  |  " + problemOld.ilgis[j].ToString().PadRight(IlgioIlgis) + "  |  " + problemOld.kiekis[j].ToString().PadRight(KiekioIlgis) + "  |" + "\n");
                }
                richTextBox2.AppendText(linija + '\n');
                // Schemu lentele
                int viso = 0;
                string pasikartojimas = "";
                List<int> kiekGaminti = new List<int> { };
                List<string> KoGaminti = new List<string> { };
                int maxkiekis = 0;
                //pasikartojimas = RandomList.random[i].sablonas.SablonoNr[0];
                // Paruosimas
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++) // TEST
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        int index = -1;
                        if (KoGaminti.IndexOf(RandomList.random[i].sablonas.SablonoNr[j]) < 0)
                        {
                            KoGaminti.Add(RandomList.random[i].sablonas.SablonoNr[j]);
                            kiekGaminti.Add(0);
                            index = KoGaminti.Count - 1;
                        }
                        else
                        {
                            index = KoGaminti.IndexOf(RandomList.random[i].sablonas.SablonoNr[j]);
                        }
                        int kiekg = 0;
                        for (int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis.Count; h++)
                        {
                            if (RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] != 0)
                            {
                                kiekg++;
                            }
                        }
                        if (kiekGaminti[index] < kiekg)
                        {
                            kiekGaminti[index] = kiekg;
                        }
                        if (maxkiekis < RandomList.random[i].kiekis[j])
                        {
                            maxkiekis = RandomList.random[i].kiekis[j];
                        }
                    }
                }
                //pasikartojimas = -1;
                int lenteliusk = 0;
                for (int j = 0; j < RandomList.random[i].sablonas.SablonoElem.Count; j++)
                {
                    if (RandomList.random[i].kiekis[j] != 0)
                    {
                        // Aprasas
                        if (RandomList.random[i].sablonas.SablonoNr[j] != pasikartojimas)
                        {
                            richTextBox2.AppendText("\n");
                            pasikartojimas = RandomList.random[i].sablonas.SablonoNr[j];
                            int ilgis = 13 + 5;
                            linija = "+";
                            for (int h = 0; h < ilgis; h++)
                            {
                                linija += "-";
                            }
                            linija += "+";
                            ilgis = 7;
                            for (int h = 0; h < AtrinktiTipai.Count; h++)
                            {
                                ilgis += AtrinktiTipai[h].Length + 2;
                            }
                            for (int h = 0; h < ilgis; h++)
                            {
                                linija += "-";
                            }
                            linija += "+";
                            richTextBox2.AppendText(linija + '\n');
                            richTextBox2.AppendText("| Schemos Nr: " + RandomList.random[i].sablonas.SablonoNr[j].ToString().PadLeft(4) + " | Rušys: ");
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
                            string lstring = "+--------------+";
                            for (int h = 0; h < kiekGaminti[lenteliusk]; h++)
                            {
                                lstring += "----------------+";
                            }
                            lstring += "\n";
                            richTextBox2.AppendText(lstring);
                            richTextBox2.AppendText("| Kiek gaminti |".PadLeft(12));
                            for (int h = 0; h < kiekGaminti[lenteliusk]; h++)
                            {
                                richTextBox2.AppendText(" Ilgis * Kiekis |");
                            }
                            richTextBox2.AppendText("\n");
                            richTextBox2.AppendText(lstring);
                            lenteliusk++;
                        }
                        // Lentele
                        string llstring = "";
                        if (lenteliusk - 1 >= 0)
                        {
                            llstring = "+--------------+";
                            for (int h = 0; h < kiekGaminti[lenteliusk - 1]; h++)
                            {
                                llstring += "----------------+";
                            }
                            llstring += "\n";
                        }
                        richTextBox2.AppendText("| " + RandomList.random[i].kiekis[j].ToString().PadRight(12) + " | ");
                        viso += RandomList.random[i].kiekis[j];
                        int kartu = 0;
                        for (int h = 0; h < RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis.Count; h++)
                        {
                            if (RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h] != 0)
                            {
                                int variantas = AtrinktiTipai.IndexOf(RandomList.random[i].sablonas.SablonoElem[j].JuostTipas[h]);
                                Color color = Color.FromArgb(Int32.Parse(AtrinktosSpalvos[variantas], System.Globalization.NumberStyles.HexNumber));
                                richTextBox2.SelectionStart = richTextBox2.TextLength;
                                richTextBox2.SelectionLength = 0;
                                richTextBox2.SelectionColor = color;
                                richTextBox2.AppendText(RandomList.random[i].sablonas.SablonoElem[j].JuostIlgis[h].ToString().PadLeft(5));
                                richTextBox2.SelectionColor = richTextBox2.ForeColor;
                                richTextBox2.AppendText(" * ");
                                richTextBox2.SelectionStart = richTextBox2.TextLength;
                                richTextBox2.SelectionLength = 0;
                                richTextBox2.SelectionColor = color;
                                richTextBox2.AppendText(RandomList.random[i].sablonas.SablonoElem[j].Kiekis[h].ToString().PadRight(6));
                                richTextBox2.SelectionColor = richTextBox2.ForeColor;
                                richTextBox2.AppendText(" | ");
                                kartu++;
                            }
                        }
                        for (int h = 0; h < kiekGaminti[lenteliusk - 1] - kartu; h++)
                        {
                            richTextBox2.AppendText("               | ");
                        }
                        richTextBox2.AppendText("\n");
                        richTextBox2.AppendText(llstring);
                    }
                }
                richTextBox2.AppendText("Viso detalių sukurta: " + viso);
            }
            richTextBox2.SelectionStart = 0;
            richTextBox2.SelectionLength = 0;
            richTextBox2.ScrollToCaret();
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            Boolean rado = false;
            foreach (Form frm in Application.OpenForms)
            {
                if(frm.Equals(Form1.ActiveForm) == true)
                {
                    rado = true;
                }
            }
            if (rado == true)
            {
                int aukstis = Form1.ActiveForm.Height - y;
                int plotis = Form1.ActiveForm.Width - x;
                richTextBox1.Height = 214 + aukstis;
                richTextBox2.Height = 214 + aukstis;
                richTextBox2.Width = 479 + plotis;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            for (int i = 0; i < RandomList.random[0].ilgis.Count; i++)
            {
                richTextBox1.Text += RandomList.random[0].tipas[i] + " " + RandomList.random[0].ilgis[i] + " " + RandomList.random[0].suma[i];
                if (i != RandomList.random[0].ilgis.Count - 1)
                {
                    richTextBox1.Text += "\n";
                }
            }
            for (int j = 0; j < problemOld.ilgis.Count; j++)
            {
                richTextBox1.Text += "\n";
                richTextBox1.AppendText(problemOld.tipai[j] + " " + problemOld.ilgis[j] + " " + problemOld.kiekis[j]);
            }
        }
        private void DisableAll()
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            textBox1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            richTextBox1.Enabled = false;
            richTextBox2.Enabled = false;
        }
        private void EnableAll()
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;
        }
    }
}
// Duomenų klasės:
public class Rusys
{
    public List<string> vardas = new List<string> { };
    public List<Rusis> Rus = new List<Rusis> { };
}
public class Rusis
{
    public List<string> pav = new List<string> { };
    public List<double> pradzia = new List<double> { };
    public List<double> pabaiga = new List<double> { };
    public List<string> NeleidziamosSchemos = new List<string> { };
    public List<string> ButinosSchemos = new List<string> { };
    public List<int> PoKiekDeti = new List<int> { };
    public List<string> KoDeti = new List<string> { };
}
public class Sablonai
{
    public List<string> SablonoNr = new List<string> { };
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
    public List<string> tipai = new List<string> { };
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
    public List<string> SablonoNr = new List<string> { };
    public List<int> SablonoSubNr = new List<int> { };
    public List<SubElementas> SablonoElem = new List<SubElementas> { };
}
public class SubElementas
{
    public List<string> JuostTipas = new List<string> { };
    public List<int> JuostIlgis = new List<int> { };
    public List<int> Kiekis = new List<int> { };
}
public class LygciuSistema
{
    public List<Lygtis> Lygtis = new List<Lygtis> { };
}
public class Lygtis
{
    public List<int> kiekis = new List<int> { };
    public List<int> variantas = new List<int> { };
    public int LygtiesSprendinys;
    public int LygtiesLiekana;
}
public class SimplexLentele
{
    public List<int> BVS = new List<int> { }; // -1 = liekana, x = nezinomasis.
    public List<eilute> eilutes = new List<eilute> { };
    public List<int> RHS = new List<int> { };
}
public class eilute
{
    public List<double> eilutesReiksmes = new List<double> { };
}