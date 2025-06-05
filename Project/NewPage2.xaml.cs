using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Project
{
    public partial class VisualizzaSquadre : ContentPage
    {
        public VisualizzaSquadre()
        {
            InitializeComponent();
            CaricaSquadre();
        }

        private void CaricaSquadre()
        {
            string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "squadra.txt");

            if (!File.Exists(pathFile))
            {
                DisplayAlert("Errore", "File squadra.txt non trovato", "OK");
                return;
            }

            var righe = File.ReadAllLines(pathFile);
            var squadreDict = new Dictionary<int, List<string>>();

            foreach (var riga in righe)
            {
                if (string.IsNullOrWhiteSpace(riga)) continue;

                var parts = riga.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 4 && int.TryParse(parts[0], out int numSquadra))
                {
                    string giocatore = $"{parts[1],-15} {parts[2],-15} {parts[3],-10}";
                    if (!squadreDict.ContainsKey(numSquadra))
                        squadreDict[numSquadra] = new List<string>();

                    squadreDict[numSquadra].Add(giocatore);
                }
            }

            // Pulisce layout e carica squadre
            mainLayout.Children.Clear();

            foreach (var kvp in squadreDict.OrderBy(k => k.Key))
            {
                var titolo = new Label
                {
                    Text = $"🟩 Squadra {kvp.Key}",
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White
                };

                var listaGiocatori = string.Join("\n", kvp.Value);
                var intestazione = "Cognome         Nome            Maglia";
                var giocatoriLabel = new Label
                {
                    Text = $"{intestazione}\n{listaGiocatori}",
                    FontFamily = "Courier New",
                    FontSize = 14,
                    TextColor = Colors.White
                };

                var contenitore = new VerticalStackLayout
                {
                    Spacing = 5,
                    Padding = new Thickness(10),
                    BackgroundColor = Colors.DarkGreen,
                    Children = { titolo, giocatoriLabel }
                };

                mainLayout.Children.Add(contenitore);
            }
        }
    }
}
