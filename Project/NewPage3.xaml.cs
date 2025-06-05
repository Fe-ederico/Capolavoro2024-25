using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project
{
    public partial class StatisticheGiocatori : ContentPage
    {
        public StatisticheGiocatori()
        {
            InitializeComponent();
            MostraStatistiche();
        }

        private class Utente
        {
            public int numSquadra { get; set; }
            public string Cognome { get; set; } = string.Empty;
            public string Nome { get; set; } = string.Empty;
            public string NumeroMaglia { get; set; } = string.Empty;
            public int PositivoA { get; set; }
            public int NeutroA { get; set; }
            public int NegativoA { get; set; }
            public int PositivoR { get; set; }
            public int NeutroR { get; set; }
            public int NegativoR { get; set; }
            public int PositivoD { get; set; }
            public int NeutroD { get; set; }
            public int NegativoD { get; set; }
            public int PositivoB { get; set; }
            public int NeutroB { get; set; }
            public int NegativoB { get; set; }
            public int PositivoM { get; set; }
            public int NeutroM { get; set; }
            public int NegativoM { get; set; }
        }

        private List<Utente> LeggiGiocatori()
        {
            string pathFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "squadra.txt");
            var utenti = new List<Utente>();

            if (!File.Exists(pathFile))
                return utenti;

            var righe = File.ReadAllLines(pathFile);

            foreach (var riga in righe)
            {
                var parts = riga.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 19 && int.TryParse(parts[0], out int numSquadra))
                {
                    utenti.Add(new Utente
                    {
                        numSquadra = numSquadra,
                        Cognome = parts[1],
                        Nome = parts[2],
                        NumeroMaglia = parts[3],
                        PositivoA = int.Parse(parts[4]),
                        NeutroA = int.Parse(parts[5]),
                        NegativoA = int.Parse(parts[6]),
                        PositivoR = int.Parse(parts[7]),
                        NeutroR = int.Parse(parts[8]),
                        NegativoR = int.Parse(parts[9]),
                        PositivoB = int.Parse(parts[10]),
                        NeutroB = int.Parse(parts[11]),
                        NegativoB = int.Parse(parts[12]),
                        PositivoM = int.Parse(parts[13]),
                        NeutroM = int.Parse(parts[14]),
                        NegativoM = int.Parse(parts[15]),
                        PositivoD = int.Parse(parts[16]),
                        NeutroD = int.Parse(parts[17]),
                        NegativoD = int.Parse(parts[18])
                    });
                }
            }

            return utenti;
        }

        private void MostraStatistiche()
        {
            var utenti = LeggiGiocatori();

            foreach (var u in utenti)
            {
                var gruppo = new Label
                {
                    Text = $"Giocatore: {u.Nome} {u.Cognome} | Maglia: {u.NumeroMaglia} | Squadra: {u.numSquadra}",
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    TextColor = Colors.DarkBlue
                };

                var stats = new Label
                {
                    Text = $"ATTACCO   → +{u.PositivoA}  0:{u.NeutroA}  -{u.NegativoA}\n" +
                           $"RICEZIONE → +{u.PositivoR}  0:{u.NeutroR}  -{u.NegativoR}\n" +
                           $"DIFESA    → +{u.PositivoD}  0:{u.NeutroD}  -{u.NegativoD}\n" +
                           $"BATTUTA   → +{u.PositivoB}  0:{u.NeutroB}  -{u.NegativoB}\n" +
                           $"MURO      → +{u.PositivoM}  0:{u.NeutroM}  -{u.NegativoM}",
                    FontSize = 14,
                    TextColor = Colors.Black
                };

                statisticheLayout.Children.Add(gruppo);
                statisticheLayout.Children.Add(stats);
                statisticheLayout.Children.Add(new BoxView { HeightRequest = 1, Color = Colors.Gray });
            }
        }
    }
}
