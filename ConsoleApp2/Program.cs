using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContoCorrenteApp
{
    public class ContoCorrente
    {
        public string Titolare { get; private set; }
        public string NumeroConto { get; private set; }
        public decimal Saldo { get; private set; }
        public bool IsAperto { get; private set; }

        public ContoCorrente(string titolare, string numeroConto)
        {
            Titolare = titolare;
            NumeroConto = numeroConto;
            IsAperto = false;
        }

        public void ApriConto(decimal importoIniziale)
        {
            if (IsAperto)
            {
                Console.WriteLine("Il conto è già aperto.");
                return;
            }

            IsAperto = true;
            Saldo = importoIniziale;
            Console.WriteLine($"Conto aperto con un saldo di {Saldo} euro.");
        }

        public void Versamento(decimal importo)
        {
            if (!IsAperto)
            {
                Console.WriteLine("Il conto è chiuso. Apri il conto prima di effettuare operazioni.");
                return;
            }
            Saldo += importo;
            Console.WriteLine($"Versati {importo} euro. Saldo attuale: {Saldo} euro.");
        }

        public void Prelievo(decimal importo)
        {
            if (!IsAperto)
            {
                Console.WriteLine("Il conto è chiuso. Apri il conto prima di effettuare operazioni.");
                return;
            }
            if (Saldo < importo)
            {
                Console.WriteLine("Saldo insufficiente.");
                return;
            }
            Saldo -= importo;
            Console.WriteLine($"Prelevati {importo} euro. Saldo attuale: {Saldo} euro.");
        }

        public void VisualizzaInfo()
        {
            Console.WriteLine($"Conto di {Titolare}");
            Console.WriteLine($"Numero conto: {NumeroConto}");
            Console.WriteLine($"Saldo: {Saldo} euro");
            Console.WriteLine($"Stato: {(IsAperto ? "Aperto" : "Chiuso")}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ContoCorrente[] conti = new ContoCorrente[10];
            int contiCreati = 4;

            // Inizializza i conti
            string[] nomiPrecaricati = { "Sauron", "Gandalf", "Frodo", "Bilbo" };
            for (int i = 0; i < 4; i++)
            {
                string titolare = nomiPrecaricati[i];
                string numeroConto = "Conto" + (i + 1).ToString();
                ContoCorrente conto = new ContoCorrente(titolare, numeroConto);
                conto.ApriConto(1000);
                conti[i] = conto;
            }

            ContoCorrente contoCorrenteSelezionato = null;

            while (true)
            {
                if (contoCorrenteSelezionato == null)
                {
                    Console.WriteLine("Scegli un'opzione:");
                    Console.WriteLine("1: Apri nuovo conto");
                    Console.WriteLine("2: Cerca per nome");
                    Console.WriteLine("3: Esci");
                }
                else
                {
                    Console.WriteLine("Operazioni sul conto selezionato:");
                    Console.WriteLine("4: Versamento");
                    Console.WriteLine("5: Prelievo");
                    Console.WriteLine("6: Informazioni conto");
                    Console.WriteLine("7: Deseleziona conto");
                }

                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        if (contiCreati >= 10)
                        {
                            Console.WriteLine("Numero massimo di conti raggiunto.");
                            break;
                        }

                        Console.WriteLine("Inserisci il nome del titolare del nuovo conto:");
                        string nuovoTitolare = Console.ReadLine();

                        Console.WriteLine("Inserisci il numero del nuovo conto:");
                        string nuovoNumeroConto = Console.ReadLine();

                        Console.WriteLine("Inserisci l'importo iniziale (minimo 1000 euro):");
                        decimal importoIniziale;
                        if (decimal.TryParse(Console.ReadLine(), out importoIniziale) && importoIniziale >= 1000)
                        {
                            ContoCorrente nuovoConto = new ContoCorrente(nuovoTitolare, nuovoNumeroConto);
                            nuovoConto.ApriConto(importoIniziale);
                            conti[contiCreati] = nuovoConto;
                            contiCreati++;
                            Console.WriteLine("Nuovo conto creato con successo.");
                        }
                        else
                        {
                            Console.WriteLine("Importo iniziale insufficiente. Operazione annullata.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Cerca il Cliente:");
                        string nomeDaRicercare = Console.ReadLine();
                        for (int i = 0; i < contiCreati; i++)
                        {
                            if (conti[i].Titolare == nomeDaRicercare)
                            {
                                Console.WriteLine($"{nomeDaRicercare} ha un conto presso la nostra banca.");
                                contoCorrenteSelezionato = conti[i];
                                break;
                            }
                        }
                        break;

                    case "3":
                        return;

                    case "4":
                        if (contoCorrenteSelezionato != null)
                        {
                            Console.WriteLine("Quanto vuoi versare?");
                            decimal importoVersamento = decimal.Parse(Console.ReadLine());
                            contoCorrenteSelezionato.Versamento(importoVersamento);
                        }
                        break;

                    case "5":
                        if (contoCorrenteSelezionato != null)
                        {
                            Console.WriteLine("Quanto vuoi prelevare?");
                            decimal importoPrelievo = decimal.Parse(Console.ReadLine());
                            contoCorrenteSelezionato.Prelievo(importoPrelievo);
                        }
                        break;

                    case "6":
                        if (contoCorrenteSelezionato != null)
                        {
                            contoCorrenteSelezionato.VisualizzaInfo();
                        }
                        break;

                    case "7":
                        contoCorrenteSelezionato = null;
                        break;

                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            }
        }
    }
}