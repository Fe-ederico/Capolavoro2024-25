
namespace Project;

public partial class AggSquadra : ContentPage
{
    List<Utente> squadra;
    string pathFile;
    string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
    string fileName = "squadra.txt";
    int contatore = 1;

    public AggSquadra()
    {
        InitializeComponent();
        squadra = new();
        pathFile = Path.Combine(baseFolder, fileName);

        if (!File.Exists(pathFile))
        {
            File.Create(pathFile).Close();
        }
        else
        {
            var lines = File.ReadAllLines(pathFile);
            if (lines.Length > 0)
            {
                var lastLine = lines.Last();
                if (int.TryParse(lastLine.Split(' ')[0], out int lastNum))
                {
                    contatore = lastNum + 1;
                }
            }
        }
    }

    private void AggiungiUtente(int contatore, string cognome, string nome, string numeroMaglia,
        int Pa = 0, int Na = 0, int Ma = 0, int Pr = 0, int Nr = 0, int Mr = 0,
        int Pd = 0, int Nd = 0, int Md = 0, int Pb = 0, int Nb = 0, int Mb = 0,
        int Pm = 0, int Nm = 0, int Mm = 0)
    {
        Utente u = new()
        {
            numSquadra = contatore,
            Cognome = cognome,
            Nome = nome,
            NumeroMaglia = numeroMaglia,
            PositivoA = Pa,
            NeutroA = Na,
            NegativoA = Ma,
            PositivoR = Pr,
            NeutroR = Nr,
            NegativoR = Mr,
            PositivoD = Pd,
            NeutroD = Nd,
            NegativoD = Md,
            PositivoB = Pb,
            NeutroB = Nb,
            NegativoB = Mb,
            PositivoM = Pm,
            NeutroM = Nm,
            NegativoM = Mm
        };

        squadra.Add(u);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string cognome = cognomeEntry.Text?.Trim() ?? "";
        string nome = nomeEntry.Text?.Trim() ?? "";
        string numeroMaglia = numeroMagliaEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(cognome) || string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(numeroMaglia))
        {
            await DisplayAlert("Errore", "Tutti i campi devono essere compilati.", "OK");
            return;
        }

        AggiungiUtente(contatore, cognome, nome, numeroMaglia);

        cognomeEntry.Text = string.Empty;
        nomeEntry.Text = string.Empty;
        numeroMagliaEntry.Text = string.Empty;

        Visualizza(null, null);
        await DisplayAlert("Successo", "Giocatore aggiunto alla squadra.", "OK");
    }

    private void Visualizza(object sender, EventArgs e)
    {
        lblSquadra.Text = string.Empty;
        lblSquadra.Text += $"{" Cognome",-15}{"Nome",-15}{"N° Maglia",-10}\n";
        lblSquadra.Text += new string('-', 40) + "\n";

        foreach (var item in squadra)
        {
            lblSquadra.Text += $"   {item.Cognome,-15}{item.Nome,-15}{item.NumeroMaglia,-10}\n";
        }
    }

    private async void Cancella(object sender, EventArgs e)
    {
        bool conferma = await DisplayAlert("Conferma", "Sei sicuro di voler cancellare tutto?", "Sì", "No");
        if (conferma)
        {
            squadra.Clear();
            lblSquadra.Text = string.Empty;
            await DisplayAlert("Successo", "Squadra cancellata.", "OK");
        }
    }

    private async void SalvaSuFile(object sender, EventArgs e)
    {
        if (squadra.Count == 0)
        {
            await DisplayAlert("Errore", "Nessun utente da salvare.", "OK");
            return;
        }

        try
        {
            using StreamWriter writer = new(pathFile, true); // aggiunge al file
            foreach (var item in squadra)
            {
                writer.WriteLine($"{contatore} {item.Cognome} {item.Nome} {item.NumeroMaglia} {item.PositivoA} {item.NeutroA} {item.NegativoA} {item.PositivoR} {item.NeutroR} {item.NegativoR} {item.PositivoB} {item.NeutroB} {item.NegativoB} {item.PositivoM} {item.NeutroM} {item.NegativoM} {item.PositivoD} {item.NeutroD} {item.NegativoD}");
            }

            await DisplayAlert("Successo", $"Squadra {contatore} salvata su file.", "OK");

            contatore++;            // Incrementa per la prossima squadra
            squadra.Clear();        // Pulisce la lista
            lblSquadra.Text = "";   // Pulisce la label
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", $"Errore durante il salvataggio: {ex.Message}", "OK");
        }
    }

    private async void LeggiDaFile(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(pathFile))
            {
                await DisplayAlert("Errore", "File non trovato.", "OK");
                return;
            }

            lblSquadra.Text = string.Empty;

            using StreamReader reader = new(pathFile);
            while (!reader.EndOfStream)
            {
                lblSquadra.Text += reader.ReadLine() + "\n";
            }

            await DisplayAlert("Successo", "Dati letti dal file.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Errore", $"Errore durante la lettura: {ex.Message}", "OK");
        }
    }
}

internal class Utente
{
    public int numSquadra { get; set; }
    public string Cognome { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string NumeroMaglia { get; set; } = null!;
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
}

