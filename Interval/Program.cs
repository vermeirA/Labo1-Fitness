using Fitness.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fitness
{
    internal class Program
    {
        //nog een FitnessManager aanmaken die de input verwerkt
        public static void Main(string[] args)
        {
            DateTime show = DateTime.Now;
            Console.WriteLine(show.ToString());

            //errorlog leegmaken
            string logPath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\Errorlog.log";
            File.WriteAllText(logPath, string.Empty);

            //invoegdata localiseren
            string filePath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\insertRunning.sql";

            //instantie van sessie, processor en manager
            FileProcessor fileProcessor1 = new FileProcessor();
            List<Sessie> sessieList = fileProcessor1.LeesFile(filePath);
            FitnessManager fitnessManager = new FitnessManager(sessieList);

            //initial UI checker
            bool startSearch = false;
            int key = 0;

            while (!startSearch)
            {
                Console.WriteLine("Welkom in de fitness databank! Wil je zoeken op klant(1) of op datum(2)? : ");
                bool correctInput = int.TryParse(Console.ReadLine(), out key);
                if (!correctInput || (key != 1 && key != 2))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Geen juiste input. Gebruik gehele getallen 1 (op klant) of 2 (op datum).");
                    Console.ResetColor();
                } else { startSearch = true; Console.Clear(); }
            }

            if (key == 1) 
            {
                Console.WriteLine("Geef je klantennummer: ");
                int klantNr = int.Parse(Console.ReadLine());
                Console.Clear();

                Console.Write($"Zoeken op klant {klantNr}");
                DisplayLoadingMessage();           

                //dictionary maken en opvullen met alle klantennummers

                Dictionary<int, List<Sessie>> klantDictionary = new Dictionary<int, List<Sessie>>();

                List<Sessie> gevondenSessies = fitnessManager.ZoekOpKlant(klantNr);
                DisplaySessions(gevondenSessies, klantNr.ToString());
            } 

            else if (key == 2)
            {
                Console.WriteLine("Geef de gezochte datum : ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                Console.Clear();

                Console.Write($"Zoeken op {datum.Date}");
                DisplayLoadingMessage();

                List<Sessie> gevondenSessies = fitnessManager.ZoekOpDatum(datum);
                DisplaySessions(gevondenSessies, datum.ToShortDateString());
            }
        }

        private static void DisplayLoadingMessage()
        {
            Thread.Sleep(50);
            Console.Write("...");
            Thread.Sleep(50);
            Console.WriteLine("...");
            Thread.Sleep(50);
        }

        private static void DisplaySessions(List<Sessie> sessies, string zoekCriteria) 
        {
            if (sessies != null && sessies.Count > 0)
            {
                foreach (var foundSessie in sessies)
                {
                    Console.WriteLine($"SessieNr: {foundSessie.sessieNr}, DatumTijd: {foundSessie.datumTijd}, " +
                        $"KlantNr: {foundSessie.klantNr}, Duur: {foundSessie.totaleDuur}, Gem. Snelheid: {foundSessie.gemiddeldeSnelheid}, " +
                        $"Intervals: {foundSessie._loopIntervallen.Count}");

                    foreach (var interval in foundSessie._loopIntervallen)
                    {
                        Console.WriteLine($"    SeqNr: {interval.sequentieNr}, Snelheid: {interval.snelheid}km/h, " +
                            $"Duur: {interval.tijdInSeconden}s");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Geen sessie gevonden voor {zoekCriteria}");
            }
        }
    }
}
