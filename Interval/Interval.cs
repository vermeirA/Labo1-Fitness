using Fitness.Exceptions;

namespace Fitness
{
    public class Interval 
    {
        private int _sequentieNr;
        private int _tijdInSeconden;
        private double _snelheid;

        // Constructor
        public Interval(int sequentieNr, int tijdInSeconden, double snelheid)
        {
            _sequentieNr = sequentieNr;
            _tijdInSeconden = tijdInSeconden;
            _snelheid = snelheid;
        }

        public Interval() { }   

        public int sequentieNr { 
            get { return _sequentieNr; }
            set {

                if (value > 0)
                { _sequentieNr = value; }
                else { throw new DomeinException($"LoopInterval - Zet sequentienummer | Foutief sequentienummer: {value}"); }
            } 
        }

        // Een interval  duurt minstens 5 seconden en maximaal 3 uur.

        public int tijdInSeconden
        {
            get { return _tijdInSeconden; }
            set { if (value >= 5 && value <= 10800)
                {
                    _tijdInSeconden = value;
                }
                else

                { throw new DomeinException($"LoopInterval - Zet tijdInSeconden | Foutief tijdInSeconden: {value}"); }
                }
        }

        // Net  zoals  bij  de  gemiddelde  snelheid tussen  5  en  22 km/h.

        public double snelheid
        {
            get { return _snelheid; }
            set
            {
                if (value >= 5 && value <= 22)
                {
                    _snelheid = value;
                }
                else
                {
                    throw new DomeinException($"LoopInterval - Zet snelheid | Foutief snelheid: {value}");
                }

            }
        }
    }
}
