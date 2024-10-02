using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fitness
{
    internal class Program
    {


        public static void Main(string[] args)
        {
            DateTime show = DateTime.Now;
            Console.WriteLine(show.ToString());

            //errorlog leegmaken
            string logPath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\Errorlog.log";
            File.WriteAllText(logPath, string.Empty);

            //invoegdata localiseren
            string filePath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\insertRunning.sql";

            //instantie van sessie
            FileProcessor fileProcessor1 = new FileProcessor();
            List<Sessie> sessieList = fileProcessor1.LeesFile(filePath);
           
            Console.WriteLine("Welkom in de fitness databank! Wil je zoeken op klant(1) of op datum(2)? : ");
            int key = int.Parse(Console.ReadLine());

            if (key == 1) 
            {
                Console.WriteLine("Geef je klantennummer: ");
                int klantNr = int.Parse(Console.ReadLine());
                Console.Clear();

                Console.Write($"Zoeken op klant {klantNr}");
                Thread.Sleep(50);
                Console.Write("...");
                Thread.Sleep(50);
                Console.WriteLine("...");
                Thread.Sleep(50);               

                //dictionary maken en opvullen met alle klantennummers

                Dictionary<int, List<Sessie>> klantDictionary = new Dictionary<int, List<Sessie>>();

                foreach (var sessie in sessieList)
                {
                    // bestaat het klantennummer al (enkel voor eerst gelezen lijst)
                    if (klantDictionary.TryGetValue(sessie.klantNr, out List<Sessie> sessies))
                    {
                        // voeg de sessie toe aan de bestaande lijst
                        sessies.Add(sessie);
                    }
                    else
                    {
                        // Maak een nieuwe lijst en voeg de sessie toe
                        klantDictionary[sessie.klantNr] = new List<Sessie> { sessie };
                    }
                }

                // Zoek naar het klantNr in de dictionary
                if (klantDictionary.TryGetValue(klantNr, out List<Sessie> gevondenSessies))
                {
                    foreach (var foundSessie in gevondenSessies)
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
                    Console.WriteLine($"Geen sessie gevonden voor KlantNr: {klantNr}");
                }
            } 




            else if (key == 2)
            {
                Console.WriteLine("Geef de gezochte datum : ");
                DateTime datum = DateTime.Parse(Console.ReadLine());

                Console.Write($"Zoeken op {datum.Date}");
                Thread.Sleep(50);
                Console.Write("...");
                Thread.Sleep(50);
                Console.WriteLine("...");
                Thread.Sleep(50);

                Dictionary<DateTime, List<Sessie>> datumDictionary = new Dictionary<DateTime, List<Sessie>>();

                foreach (var sessie in sessieList)
                {

                    DateTime sessieDatum = sessie.datumTijd.Date; // enkel op datum zoeken anders zoekt hij niet correct
                    if (datumDictionary.TryGetValue(sessieDatum, out List<Sessie> sessies))
                    {
                        sessies.Add(sessie);
                    }
                    else
                    {
                        datumDictionary[sessieDatum] = new List<Sessie> { sessie };
                    }
                }

                if (datumDictionary.TryGetValue(datum, out List<Sessie> gevondenSessies))
                {
                    foreach (var foundSessie in gevondenSessies)
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
                    Console.WriteLine($"Geen sessie gevonden voor datum: {datum.Date}");
                }
            }
        }
    }
}
