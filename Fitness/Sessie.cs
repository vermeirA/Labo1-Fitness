using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sessie
{
    internal class Sessie
    {
        private int _sessieNr;
        private DateTime _datumTijd;
        private int _klantNr;
        private int _totaleDuur; // in minuten
        private decimal _gemiddeldeSnelheid; // in km/h
        private List<Interval> _loopIntervallen;

        public Sessie(int sessieNr, DateTime datumTijd, int klantNr, int totaleDuur, decimal gemiddeldeSnelheid)
        {
            _sessieNr = sessieNr;
            _datumTijd = datumTijd;
            _klantNr = klantNr;
            _totaleDuur = totaleDuur;
            _gemiddeldeSnelheid = gemiddeldeSnelheid;
            _loopIntervallen = new List<Interval>();
        }

        public int sessieNr
        {
            get { return _sessieNr; }
            set
            {
                if (_sessieNr > 0)
                {
                    _sessieNr = value;
                }
                else
                {
                    throw new ArgumentException("Het sessienummer mag niet 0 of lager zijn.");
                }
            }
        }

        public DateTime datumtijd
        {
            get; set;
        }

        public int klantNr
        {
            get { return _klantNr; }
            set
            {
                if (_klantNr > 0)
                {
                    _klantNr = value;
                }
                else
                {
                    throw new ArgumentException("Het klantnummer mag niet 0 of lager zijn.");
                }
            }
        }

        // Een sessie duurt minstens 5 minuten en mag niet langer duren dan 3 uur.

        public int totaleDuur
        {
            get { return _totaleDuur;}
            set
            {
                if ( _totaleDuur >= 5 && _totaleDuur <= 180)
                {
                    _totaleDuur = value;
                } else
                {
                    throw new ArgumentException("Een sessie duurt minstens 5 minuten en mag niet langer duren dan 3 uur.");
                }
            }
        }

        //  De snelheid mag niet lager zijn dan 5 en niet hoger dan 22 km/h.

        public decimal gemiddeldeSnelheid
        {
            get { return _gemiddeldeSnelheid; }
            set
            {
                if (_gemiddeldeSnelheid >= 5 && _gemiddeldeSnelheid <= 22)
                {
                    _gemiddeldeSnelheid = value;
                } else
                {
                    throw new ArgumentException("De gemiddelde snelheid mag niet lager zijn dan 5 en niet hoger dan 22 km/h.");
                }

            }
        }

        public void VoegLoopIntervalToe(Interval interval)
        {
            _loopIntervallen.Add(interval);
        }

        // Methode om alle intervallen op te vragen
        public List<Interval> GetLoopIntervallen()
        {
            return _loopIntervallen;
        }
    }
}
