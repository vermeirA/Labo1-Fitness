using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.Exceptions;

namespace Fitness
{
    public class FileProcessor
    {
        //redundant if elses wegdoen voor exceptions, wordt gehandelt in de objecten zelf! standaard exceptions ook opvangen en wegschrijven
        public List<Sessie> LeesFile(string fileName)
        {
            List<Sessie> loopSessies = new List<Sessie>();
            Sessie currentSessie = null; 

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    List<string> newLine = Split(line);
                    newLine[1] = newLine[1].Replace("'", "");

                    try
                    {
                        if (!int.TryParse(newLine[0], out int sessieNr)) continue;
                        // Als er geen sessie bezig is (null) of het sessienummer komt niet overeen, start een nieuwe sessie.
                        if (currentSessie == null || currentSessie.sessieNr != sessieNr)
                        {
                            // Als er de nummers niet overeen komen, voeg dan de laatste sessie toe aan de overkoepeling.
                            if (currentSessie != null)
                            {
                                loopSessies.Add(currentSessie);
                            }

                            currentSessie = new Sessie
                            {
                                sessieNr = sessieNr
                            };

                            if (!DateTime.TryParseExact(newLine[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime res2)) continue;
                            currentSessie.datumTijd = res2;

                            if (!int.TryParse(newLine[2], out int res3)) continue;
                            currentSessie.klantNr = res3;

                            if (!int.TryParse(newLine[3], out int res4)) continue;
                            currentSessie.totaleDuur = res4;

                            if (!decimal.TryParse(newLine[4], out decimal res5)) continue;
                            currentSessie.gemiddeldeSnelheid = res5;
                        }

                        Interval loopInterval = new Interval();

                        if (!int.TryParse(newLine[5], out int res6)) continue;
                        loopInterval.sequentieNr = res6;

                        if (!int.TryParse(newLine[6], out int res7)) continue;
                        loopInterval.tijdInSeconden = res7;

                        if (!double.TryParse(newLine[7], out double res8)) continue;
                        loopInterval.snelheid = res8;

                        currentSessie._loopIntervallen.Add(loopInterval);

                    }
                    catch (DomeinException ex)
                    {
                        LogError(ex, line); // opgevangen error(s) wegschrijven naar het log bestand
                    }
                }

                // als de laatste lijn gelezen is, die er zeker nog bijzetten
                if (currentSessie != null)
                {
                    loopSessies.Add(currentSessie);
                }
            }

            return loopSessies;
        }

        public List<string> Split(string line)
        {
            List<string> result = new List<string>();

            // begin en eindpunt van data definiëren

            int startIndex = line.IndexOf('(') + 1;
            int endIndex = line.IndexOf(')');

            // splitsen op basis van comma's

            if (startIndex > 0 && endIndex > startIndex)
            {
                string values = line.Substring(startIndex, endIndex - startIndex);

                result = new List<string>( values.Split(','));
            }
            return result;
        }

        public void LogError(DomeinException ex, string line) 
        {
            string logPath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\Errorlog.log";

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine(line + ", " + ex.Message);
            }
        } 
    }
}
