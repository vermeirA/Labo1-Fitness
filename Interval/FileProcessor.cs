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


        public List<Sessie> LeesFile(string fileName)
        {
            List<Sessie> loopSessies = new List<Sessie>();
            Sessie currentSessie = null; //tracker is leeg?

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    List<string> newLine = Split(line);
                    newLine[1] = newLine[1].Replace("'", "");

                    try
                    {
                        int sessieNr;
                        if (int.TryParse(newLine[0], out sessieNr))
                        {
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

                                if (DateTime.TryParseExact(newLine[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime res2))
                                {
                                    currentSessie.datumTijd = res2;
                                }
                                else
                                {
                                    throw new DomeinException($"LoopSessie - ZetdatumTijd|datumTijd: {newLine[1]}");
                                }

                                if (int.TryParse(newLine[2], out int res3))
                                {
                                    currentSessie.klantNr = res3;
                                }
                                else
                                {
                                    throw new DomeinException($"LoopSessie - ZetklantNr|klantNr: {newLine[2]}");
                                }

                                if (int.TryParse(newLine[3], out int res4))
                                {
                                    currentSessie.totaleDuur = res4;
                                }
                                else
                                {
                                    throw new DomeinException($"LoopSessie - ZettotaleDuur|totaleDuur: {newLine[3]}");
                                }

                                if (decimal.TryParse(newLine[4], out decimal res5))
                                {
                                    currentSessie.gemiddeldeSnelheid = res5;
                                }
                                else
                                {
                                    throw new DomeinException($"LoopSessie - ZetgemiddeldeSnelheid|gemiddeldeSnelheid: {newLine[4]}");
                                }
                            }

                            Interval loopInterval = new Interval();

                            if (int.TryParse(newLine[5], out int res6))
                            {
                                loopInterval.sequentieNr = res6;
                            }
                            else
                            {
                                throw new DomeinException($"LoopInterval - ZetSequentieNr|SequentieNr: {newLine[5]}");
                            }

                            if (int.TryParse(newLine[6], out int res7))
                            {
                                loopInterval.tijdInSeconden = res7;
                            }
                            else
                            {
                                throw new DomeinException($"LoopInterval - ZetTijdInSeconden|TijdInSeconden: {newLine[6]}");
                            }

                            if (double.TryParse(newLine[7], out double res8))
                            {
                                loopInterval.snelheid = res8;
                            }
                            else
                            {
                                throw new DomeinException($"LoopInterval - ZetSnelheid|Snelheid: {newLine[7]}");
                            }

                            currentSessie._loopIntervallen.Add(loopInterval);
                        }
                        else
                        {
                            throw new DomeinException($"LoopSessie - ZetSessieNr|SessieNr: {newLine[0]}");
                        }
                    }
                    catch (DomeinException ex)
                    {
                        LogError(ex); // opgevangen error(s) wegschrijven naar het log bestand
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




        public void LogError(DomeinException ex) 
        {
            string logPath = "C:\\Users\\Gebruiker\\Desktop\\Graduaat\\SEM3\\Programmeren gevorderd 1\\Labo 1\\Interval\\Errorlog.log";

            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine(ex.Message);
            }
        } 
    }
}
