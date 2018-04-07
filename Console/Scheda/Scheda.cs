using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seica
{
    public class Scheda
    {
        public List<PosizioniStazione> PosizioniUtilizzate;
        public StazioniTest Stazione { get; }
        public bool IsWaste;

        public Scheda(StazioniTest stazione)
        {
            PosizioniUtilizzate = new List<PosizioniStazione>();
            Stazione = stazione;
        }

        public bool AddPosition(PosizioniStazione nuovaPosizione)
        {
            if (PosizioniUtilizzate.Contains(nuovaPosizione))
            {
                string er = $"Impossibile aggiungere la posizione {nuovaPosizione}, la posizione è gia esistente.";
                Console.WriteLine(er);
                return false;
            }
            PosizioniUtilizzate.Add(nuovaPosizione);
            return true;
        }


    }

    public enum PosizioniStazione
    {
        Posizione_1 = 1,
        Posizione_2 = 2,
        Posizione_3 = 3,
        Posizione_4 = 4
    }

    public enum StazioniTest
    {
        Stazione_1 = 1,
        Stazione_2 = 2
    }
}
