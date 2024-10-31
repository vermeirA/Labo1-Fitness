using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Managers
{
    public class FitnessManager
    {
        private Dictionary<int, List<Sessie>> klantDictionary;
        private Dictionary<DateTime, List<Sessie>> datumDictionary;

        public FitnessManager(List<Sessie> sessies)
        {
            klantDictionary = new Dictionary<int, List<Sessie>>();
            datumDictionary = new Dictionary<DateTime, List<Sessie>>();
            PopulateDictionaries(sessies);
        }

        // Populates dictionaries based on sessions list
        private void PopulateDictionaries(List<Sessie> sessies)
        {
            foreach (var sessie in sessies)
            {
                // Populate by client number
                if (!klantDictionary.ContainsKey(sessie.klantNr))
                    klantDictionary[sessie.klantNr] = new List<Sessie>();
                klantDictionary[sessie.klantNr].Add(sessie);

                // Populate by date
                DateTime sessieDatum = sessie.datumTijd.Date;
                if (!datumDictionary.ContainsKey(sessieDatum))
                    datumDictionary[sessieDatum] = new List<Sessie>();
                datumDictionary[sessieDatum].Add(sessie);
            }
        }

        // Search for sessions by client number
        public List<Sessie> ZoekOpKlant(int klantNr)
        {
            klantDictionary.TryGetValue(klantNr, out List<Sessie> sessies);
            return sessies;
        }

        // Search for sessions by date
        public List<Sessie> ZoekOpDatum(DateTime datum)
        {
            datumDictionary.TryGetValue(datum, out List<Sessie> sessies);
            return sessies;
        }
    }

}
