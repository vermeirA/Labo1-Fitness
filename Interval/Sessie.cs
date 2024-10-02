using Fitness.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fitness
{
    public class Sessie 
    {
        private int _sessieNr;
        private DateTime _datumTijd;
        private int _klantNr;
        private int _totaleDuur; // in minuten
        private decimal _gemiddeldeSnelheid; // in km/h
        public List<Interval> _loopIntervallen;

        public Sessie() 
        {
            _loopIntervallen = new List<Interval>();
        }

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
                if (value > 0)
                {
                    _sessieNr = value;
                }
                else
                {
                    throw new DomeinException($"LoopTraining - Zet sessieNr | Foutief sessieNr: {value}");
                }
            }
        }

        public DateTime datumTijd
        {
            get; set;
        }

        public int klantNr
        {
            get { return _klantNr; }
            set
            {
                if (value > 0)
                {
                    _klantNr = value;
                }
                else
                {
                    throw new DomeinException($"LoopTraining - Zet klantNr | Foutief klantNr: {value}");
                }
            }
        }

        // Een sessie duurt minstens 5 minuten en mag niet langer duren dan 3 uur.

        public int totaleDuur
        {
            get { return _totaleDuur;}
            set
            {
                if ( value >= 5 && value <= 180)
                {
                    _totaleDuur = value;
                } else
                {
                    throw new DomeinException($"LoopTraining - Zet totaleDuur | Foutief totaleDuur: {value}");
                }
            }
        }

        //  De snelheid mag niet lager zijn dan 5 en niet hoger dan 22 km/h.

        public decimal gemiddeldeSnelheid
        {
            get { return _gemiddeldeSnelheid; }
            set
            {
                if (value >= 5 && value <= 22)
                {
                    _gemiddeldeSnelheid = value;
                } else
                {
                    throw new DomeinException($"LoopTraining - Zet gemiddeldeSnelheid | Foutief gemiddeldeSnelheid: {value}");
                }

            }
        }
    }
}
