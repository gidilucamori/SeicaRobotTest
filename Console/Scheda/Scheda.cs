using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seica
{
    public class Scheda
    {
        //Lista delle varie posizioni visitate dalla scheda
        public List<PosizioneScheda> PosizioniUtilizzate;

        //Contiene l'informazione del luogo da dove è stata caricata la scheda dalla stazione di carico
        public PosizioneScheda PosizioneDiPresaCarico;

        public int Id { get; set; }

        // Contatore schede per debug
        public static int IdCounter { get; set; }

        //Viene settato quando la scheda non supera due test su tre, per definire definitivamente lo stato
        //di waste
        public bool IsWaste;

        public int NumeroTestFalliti { get; set; }

        public Scheda(PosizioneScheda posizioneCarico)
        {
            PosizioniUtilizzate = new List<PosizioneScheda>();
            PosizioneDiPresaCarico = posizioneCarico;
            //Assegno l'attuale valore alla scheda e incremento il contatore
            Id = IdCounter++;
            if (IdCounter == 255) IdCounter = 0;
        }

        public bool AddPosition(PosizioneScheda nuovaPosizione)
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

    public enum Stazione
    {
        Carico = 1,
        Stazione_1 = 2,
        Stazione_2 = 3,
        Scarico = 4,
        Pinza = 5,
    }

    public class PosizioneScheda
    {
        public PosizioniStazione Posizione { get; set; }
        public Stazione Stazione { get; set; }
    }
}
