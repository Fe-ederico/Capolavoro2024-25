using System.Collections.ObjectModel;
using System.Text;

using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Project;

public partial class NewPage1 : ContentPage
{
    public ObservableCollection<Giocatore> Giocatori { get; set; } = new();

    private string pathFile = string.Empty;
    private string baseFolder = FileSystem.AppDataDirectory;

    public NewPage1()
    {
        InitializeComponent();
        BindingContext = this;
        _ = InizializzaAsync();
    }

    private async Task InizializzaAsync()
    {
        await ChiediNumeroSquadra();
    }

    private async Task ChiediNumeroSquadra()
    {
        string fileName = "squadra.txt";
        pathFile = Path.Combine(baseFolder, fileName);

        await CaricaGiocatoriDaFile();
        MostraTabellaTestuale();
    }

    private async Task CaricaGiocatoriDaFile()
    {
        Giocatori.Clear();

        if (!File.Exists(pathFile))
            return;

        var righe = File.ReadAllLines(pathFile);

        string? input = await DisplayPromptAsync("Squadra", "Inserisci il numero della squadra:");
        if (input == null)
            return;

        foreach (var riga in righe)
        {
            var parole = riga.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parole.Length >= 19 && input == parole[0])
            {
                Giocatori.Add(new Giocatore
                {
                    Numero = int.Parse(parole[0]),
                    Cognome = parole[1],
                    Nome = parole[2],
                    Maglia = parole[3],
                    PositivoA = int.Parse(parole[4]),
                    NeutroA = int.Parse(parole[5]),
                    NegativoA = int.Parse(parole[6]),
                    PositivoR = int.Parse(parole[7]),
                    NeutroR = int.Parse(parole[8]),
                    NegativoR = int.Parse(parole[9]),
                    PositivoD = int.Parse(parole[10]),
                    NeutroD = int.Parse(parole[11]),
                    NegativoD = int.Parse(parole[12]),
                    PositivoB = int.Parse(parole[13]),
                    NeutroB = int.Parse(parole[14]),
                    NegativoB = int.Parse(parole[15]),
                    PositivoM = int.Parse(parole[16]),
                    NeutroM = int.Parse(parole[17]),
                    NegativoM = int.Parse(parole[18]),
                });
            }
        }
    }

    private void SalvaGiocatoriSuFile()
    {
        var righe = new List<string>();

        foreach (var g in Giocatori)
        {
            string riga = $"{g.Numero} {g.Cognome} {g.Nome} {g.Maglia} " +
                          $"{g.PositivoA} {g.NeutroA} {g.NegativoA} " +
                          $"{g.PositivoR} {g.NeutroR} {g.NegativoR} " +
                          $"{g.PositivoD} {g.NeutroD} {g.NegativoD} " +
                          $"{g.PositivoB} {g.NeutroB} {g.NegativoB} " +
                          $"{g.PositivoM} {g.NeutroM} {g.NegativoM}";

            righe.Add(riga);
        }

        File.WriteAllLines(pathFile, righe);
    }

    private void MostraTabellaTestuale()
    {
        var sb = new StringBuilder();
        sb.AppendLine(" N°  Maglia  Cognome       Nome");
        sb.AppendLine("----------------------------------");

        foreach (var g in Giocatori)
        {
            sb.AppendLine($"{g.Numero,2}   {g.Maglia,6}  {g.Cognome,-12} {g.Nome}");
        }

        lblSquadra.Text = sb.ToString();
    }

    private void Incrementa(Action<Giocatore> azione, object sender)
    {
        if (sender is Button btn && btn.BindingContext is Giocatore g)
        {
            azione(g);
            SalvaGiocatoriSuFile();
        }
    }

    private void IncrementaAttaccoPositivo(object s, EventArgs e) => Incrementa(g => g.PositivoA++, s);
    private void IncrementaAttaccoNeutro(object s, EventArgs e) => Incrementa(g => g.NeutroA++, s);
    private void IncrementaAttaccoNegativo(object s, EventArgs e) => Incrementa(g => g.NegativoA++, s);
    private void IncrementaRicezionePositivo(object s, EventArgs e) => Incrementa(g => g.PositivoR++, s);
    private void IncrementaRicezioneNeutro(object s, EventArgs e) => Incrementa(g => g.NeutroR++, s);
    private void IncrementaRicezioneNegativo(object s, EventArgs e) => Incrementa(g => g.NegativoR++, s);
    private void IncrementaDifesaPositivo(object s, EventArgs e) => Incrementa(g => g.PositivoD++, s);
    private void IncrementaDifesaNeutro(object s, EventArgs e) => Incrementa(g => g.NeutroD++, s);
    private void IncrementaDifesaNegativo(object s, EventArgs e) => Incrementa(g => g.NegativoD++, s);
    private void IncrementaBattutaPositivo(object s, EventArgs e) => Incrementa(g => g.PositivoB++, s);
    private void IncrementaBattutaNeutro(object s, EventArgs e) => Incrementa(g => g.NeutroB++, s);
    private void IncrementaBattutaNegativo(object s, EventArgs e) => Incrementa(g => g.NegativoB++, s);
    private void IncrementaMuroPositivo(object s, EventArgs e) => Incrementa(g => g.PositivoM++, s);
    private void IncrementaMuroNeutro(object s, EventArgs e) => Incrementa(g => g.NeutroM++, s);
    private void IncrementaMuroNegativo(object s, EventArgs e) => Incrementa(g => g.NegativoM++, s);
    private void NuovoSet(object sender, EventArgs e)
    {
        StringBuilder sb = new();
        sb.AppendLine("Statistiche Set Precedente");
        sb.AppendLine("Giocatore       A+  A0  A-   R+  R0  R-   D+  D0  D-   B+  B0  B-   M+  M0  M-");

        foreach (var g in Giocatori)
        {
            // Calcola differenze
            int dA_P = g.PositivoA - g.LastPositivoA;
            int dA_0 = g.NeutroA - g.LastNeutroA;
            int dA_N = g.NegativoA - g.LastNegativoA;

            int dR_P = g.PositivoR - g.LastPositivoR;
            int dR_0 = g.NeutroR - g.LastNeutroR;
            int dR_N = g.NegativoR - g.LastNegativoR;

            int dD_P = g.PositivoD - g.LastPositivoD;
            int dD_0 = g.NeutroD - g.LastNeutroD;
            int dD_N = g.NegativoD - g.LastNegativoD;

            int dB_P = g.PositivoB - g.LastPositivoB;
            int dB_0 = g.NeutroB - g.LastNeutroB;
            int dB_N = g.NegativoB - g.LastNegativoB;

            int dM_P = g.PositivoM - g.LastPositivoM;
            int dM_0 = g.NeutroM - g.LastNeutroM;
            int dM_N = g.NegativoM - g.LastNegativoM;

            sb.AppendLine($"{g.Cognome,-14} {dA_P,2}  {dA_0,2}  {dA_N,2}   {dR_P,2}  {dR_0,2}  {dR_N,2}   {dD_P,2}  {dD_0,2}  {dD_N,2}   {dB_P,2}  {dB_0,2}  {dB_N,2}   {dM_P,2}  {dM_0,2}  {dM_N,2}");

            // Aggiorna i last
            g.LastPositivoA = g.PositivoA;
            g.LastNeutroA = g.NeutroA;
            g.LastNegativoA = g.NegativoA;

            g.LastPositivoR = g.PositivoR;
            g.LastNeutroR = g.NeutroR;
            g.LastNegativoR = g.NegativoR;

            g.LastPositivoD = g.PositivoD;
            g.LastNeutroD = g.NeutroD;
            g.LastNegativoD = g.NegativoD;

            g.LastPositivoB = g.PositivoB;
            g.LastNeutroB = g.NeutroB;
            g.LastNegativoB = g.NegativoB;

            g.LastPositivoM = g.PositivoM;
            g.LastNeutroM = g.NeutroM;
            g.LastNegativoM = g.NegativoM;
        }

        lblSquadra.Text = sb.ToString();
        SalvaGiocatoriSuFile();
    }

    public class Giocatore
    {
        public int Numero { get; set; }
        public string Maglia { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int PositivoA { get; set; } = 0;
        public int NeutroA { get; set; } = 0;
        public int NegativoA { get; set; } = 0;
        public int PositivoR { get; set; } = 0;
        public int NeutroR { get; set; } = 0;
        public int NegativoR { get; set; } = 0;
        public int PositivoB { get; set; } = 0;
        public int NeutroB { get; set; } = 0;
        public int NegativoB { get; set; } = 0;
        public int PositivoM { get; set; } = 0;
        public int NeutroM { get; set; } = 0;
        public int NegativoM { get; set; } = 0;
        public int PositivoD { get; set; } = 0;
        public int NeutroD { get; set; } = 0;
        public int NegativoD { get; set; } = 0;
        public int LastPositivoA { get; set; } = 0;
        public int LastNeutroA { get; set; } = 0;
        public int LastNegativoA { get; set; } = 0;
        public int LastPositivoR { get; set; } = 0;
        public int LastNeutroR { get; set; } = 0;
        public int LastNegativoR { get; set; } = 0;
        public int LastPositivoB { get; set; } = 0;
        public int LastNeutroB { get; set; } = 0;
        public int LastNegativoB { get; set; } = 0;
        public int LastPositivoM { get; set; } = 0;
        public int LastNeutroM { get; set; } = 0;
        public int LastNegativoM { get; set; } = 0;
        public int LastPositivoD { get; set; } = 0;
        public int LastNeutroD { get; set; } = 0;
        public int LastNegativoD { get; set; } = 0;
    }
}


