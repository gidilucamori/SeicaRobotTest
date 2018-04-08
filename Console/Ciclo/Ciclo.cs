using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Seica
{
    public class Ciclo
    {
        private Robot _robot;
        private Compact _compact_1;
        private Compact _compact_2;

        public Ciclo(Robot robot)
        {
            _robot = robot;
            _compact_1 = new Compact(Stazioni.Compact_1);
            _compact_2 = new Compact(Stazioni.Compact_2);

            #region Comando tutto vuoto stazione test 1
            TabellaComandi.Add(new Azione
            {
                Comment = "Postazione test completamente vuota." +
                "Verranno prese 4 schede dalla stazione di carico, e posate all interno della macchina di test.",
                Key = new int[] { 0, 0, 0 },
                Comandi = new List<TableCommand>()
                {
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.In
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.In
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.In
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.In
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                }
            });
            TabellaComandi.Add(new Azione
            {
                Comment = "La macchina di test ha completato il suo lavoro, e restituisce un informazione di 'Tutti Ok'," +
                "ovvero tutte le schede possono essere prese e depositate nella zona di scarico Good.",
                Key = new int[] { 4, 0, 0 },
                Comandi = new List<TableCommand>()
                {
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.G
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.G
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.G
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Pick,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.G
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                }
            });
            #endregion
            XmlSerialize();
        }

        //Lettura segnali da Plc.

        ///I segnali intressati sono:
        ///-Fine test postazione 1
        ///-Fine test postazione 2
        ///-Postazione 1 libera
        ///-Postazione 2 libera
        ///

        bool caricarePostazione1 = true;
        bool caricarePostazione2 = false;
        bool scaricarePostazione1_TutteBuone = false;
        bool scaricarePostazione2_TutteBuone = false;



        private List<Scheda> _schede = new List<Scheda>();

        public void Start()
        {
            if (TabellaComandi == null) Console.WriteLine("La tabella dei comandi è vuota");

            //Lettura stati da plc

            while (true)
            {
                Thread.Sleep(200);

                if (true)//<- azione richiesta dal plc
                {
                    //_robot.WriteCommand(Azioni.Prelievo, Pinza.Pinza_2, Stazioni.Carico, PosizioneSchedaForRobot.Posizione_2);
                    //_robot.WriteCommand(Azioni.Prelievo, Pinza.Pinza_2, Stazioni.Carico, PosizioneSchedaForRobot.Posizione_2);
                    //_robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_2, Stazioni.Compact_2, PosizioneSchedaForRobot.Posizione_1);
                    //_robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_2, Stazioni.Compact_2, PosizioneSchedaForRobot.Posizione_4);

                    //_robot.WriteCommand(Azioni.Prelievo, Pinza.Pinza_1, Stazioni.Home, PosizioneSchedaForRobot.Posizione_1);
                    //    _robot.WriteCommand(Azioni.Prelievo,Pinza.Pinza_2, Stazioni.Carico, PosizioneSchedaForRobot.Posizione_2);
                    //    _robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_1, Stazioni.Compact_2, PosizioneSchedaForRobot.Posizione_3);
                    //    _robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_2, Stazioni.Compact_2, PosizioneSchedaForRobot.Posizione_4);


                    int stazione = 1; //<- lettura da plc, stazione interessata
                    int[] stato = { 4, 0, 0 }; // <- lettura stato stazione
                    if (stato.SequenceEqual(new int[] { 4, 0, 0 })) _compact_2.GetTestResoultFromPLC();

                    Azione a = TabellaComandi.FirstOrDefault(i => i.Key.SequenceEqual(stato));

                    if (a == null) Console.WriteLine($"Attenzione, nessun azione corrisponde alla sequenza {stato[0]}{stato[1]}{stato[2]}");


                    //passare come parametro la compact giusta
                    CommandExecuter(a,Stazioni.Compact_2, _compact_2);
                }
            }

        }

        private void CommandExecuter(Azione a, Stazioni s, Compact compact = null)
        {
            foreach (var c in a.Comandi)
            {
                #region Good
                if (c.Azione == AzioneTabella.Good && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.None)
                {
                    Scheda scheda = _schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                            i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_1);
                    if (scheda == null)
                    {
                        Console.WriteLine("Nessuna scheda da deposiatre nella postazione di scarico, su pinza 1");
                        return;
                    }

                    _robot.WriteCommand(Azioni.Deposito,Pinza.Pinza_1,Stazioni.Scarico,PosizioneSchedaForRobot.Posizione_1);
                    _schede.Remove(scheda);
                    continue;
                }

                if (c.Azione == AzioneTabella.Good && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.None)
                {
                    Scheda scheda = _schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                            i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_2);
                    if (scheda == null)
                    {
                        Console.WriteLine("Nessuna scheda da deposiatre nella postazione di scarico, su pinza 1");
                        return;
                    }

                    _robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_2, Stazioni.Scarico, PosizioneSchedaForRobot.Posizione_1);
                    _schede.Remove(scheda);

                    continue;
                }
                #endregion

                #region Pick Pinza 1

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.G)
                {
                    //Pick di un pezzo buono da compact
#warning check if compact is opened
                    //throw new NotImplementedException("Check if compact is opened");
                    int slot = compact.GiveMeTheFirstGoodToPick();
                    if (slot == -1)
                    {
                        Console.WriteLine("La compact non ha nessun pezzo risultato buono");
                        return;
                    }
                    _robot.WriteCommand(Azioni.Prelievo,Pinza.Pinza_1,compact.StazioneCompact,(PosizioneSchedaForRobot)slot);
                    compact.ReleaseSlot(slot);
                    _schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == compact.StazioneCompact &&
                            i.PosizioniUtilizzate.Last().Posizione == (PosizioneSchedaForRobot)slot)
                            .PosizioniUtilizzate.Add(new PosizioneScheda
                            {
                                Stazione = Stazioni.Pinza,
                                Posizione = PosizioneSchedaForRobot.Posizione_1
                            });
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.In)
                {
                    if (compact.GiveMeTheFirstEmptySlot() == -1)
                    {
                        Console.WriteLine("Attenzione, nessuno slot della compact è vuoto");
                        return;
                    }

                    //Lettura da plc per sapere dove devo andare a prendere la scheda
                    int pos = 1;//<-Lettura da PLC
                    _robot.WriteCommand(Azioni.Prelievo, Pinza.Pinza_1, Stazioni.Carico,(PosizioneSchedaForRobot)pos);
                    Scheda nuova = new Scheda(new PosizioneScheda
                    {
                        Stazione = Stazioni.Carico,
                        Posizione = (PosizioneSchedaForRobot)pos
                    });

                    nuova.AddPosition(new PosizioneScheda
                    {
                        Stazione = Stazioni.Pinza,
                        Posizione = PosizioneSchedaForRobot.Posizione_1
                    });
                    _schede.Add(nuova);
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.R)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.W)
                {
                    continue;
                }

                #endregion

                #region Pick Pinza 2

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.G)
                {
                    //Pick di un pezzo buono da compact
#warning check if compact is opened
                    //throw new NotImplementedException("Check if compact is opened");
                    int slot = compact.GiveMeTheFirstGoodToPick();
                    if (slot == -1)
                    {
                        Console.WriteLine("La compact non ha nessun pezzo risultato buono");
                        return;
                    }
                    _robot.WriteCommand(Azioni.Prelievo,Pinza.Pinza_2,compact.StazioneCompact,(PosizioneSchedaForRobot)slot);
                    compact.ReleaseSlot(slot);
                    _schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == compact.StazioneCompact &&
                            i.PosizioniUtilizzate.Last().Posizione == (PosizioneSchedaForRobot)slot)
                            .PosizioniUtilizzate.Add(new PosizioneScheda
                            {
                                Stazione = Stazioni.Pinza,
                                Posizione = PosizioneSchedaForRobot.Posizione_2
                            });
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.In)
                {
                    if (compact.GiveMeTheFirstEmptySlot() == -1)
                    {
                        Console.WriteLine("Attenzione, nessuno slot della compact è vuoto");
                        return;
                    }

                    //Lettura da plc per sapere dove devo andare a prendere la scheda
                    int pos = 2;//<-Lettura da PLC
                    _robot.WriteCommand(Azioni.Prelievo, Pinza.Pinza_2, Stazioni.Carico,(PosizioneSchedaForRobot)pos);
                    Scheda nuova = new Scheda(new PosizioneScheda
                    {
                        Stazione = Stazioni.Carico,
                        Posizione = (PosizioneSchedaForRobot)pos
                    });

                    nuova.AddPosition(new PosizioneScheda
                    {
                        Stazione = Stazioni.Pinza,
                        Posizione = PosizioneSchedaForRobot.Posizione_2
                    });
                    _schede.Add(nuova);
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.R)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.W)
                {
                    continue;
                }

                #endregion

                #region Place

                if (c.Azione == AzioneTabella.Place && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.None)
                {

                    //check se ci sono schede sulla pinza 1 
                    if (_schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                            i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_1)
                        == null)
                    {
                        Console.WriteLine("Attenzione, Non sono presenti schede da posizionare sulla pinza 1");
                        return;
                    }


                    int slot = compact.GiveMeTheFirstEmptySlot();
                    _robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_1, s, (PosizioneSchedaForRobot)slot);

                    //informare la posizione della compact che è occupata
                    compact.AddToEmptySlot(slot);
                    //informare la scheda della sua nuova posizione

                    Scheda scheda = _schede.FirstOrDefault(
                        i => i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                        i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_1);

                    scheda.PosizioniUtilizzate.Add(new PosizioneScheda()
                    {
                        Stazione = compact.StazioneCompact,
                        Posizione = (PosizioneSchedaForRobot)slot
                    });
                    continue;
                }

                if (c.Azione == AzioneTabella.Place && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.None)
                {

                    //check se ci sono schede sulla pinza 2 
                    if (_schede.FirstOrDefault(i =>
                            i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                            i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_2)
                        == null)
                    {
                        Console.WriteLine("Attenzione, Non sono presenti schede da posizionare sulla pinza 2");
                        return;
                    }

                    int slot = compact.GiveMeTheFirstEmptySlot();
                    _robot.WriteCommand(Azioni.Deposito, Pinza.Pinza_2, s, (PosizioneSchedaForRobot)slot);

                    //informare la posizione della compact che è occupata
                    compact.AddToEmptySlot(slot);
                    //informare la scheda della sua nuova posizione

                    Scheda scheda = _schede.FirstOrDefault(
                        i => i.PosizioniUtilizzate.Last().Stazione == Stazioni.Pinza &&
                        i.PosizioniUtilizzate.Last().Posizione == PosizioneSchedaForRobot.Posizione_2);

                    scheda.PosizioniUtilizzate.Add(new PosizioneScheda()
                    {
                        Stazione = compact.StazioneCompact,
                        Posizione = (PosizioneSchedaForRobot)slot
                    });
                    continue;
                }

                #endregion

                #region Waste
                if (c.Azione == AzioneTabella.Waste && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.None)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Waste && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.None)
                {
                    continue;
                }
                #endregion

            }
        }

        private void Pick1In()
        {

        }

        private void XmlSerialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Azione>));
            using (TextWriter writer = new StreamWriter("Azioni.xml"))
            {
                serializer.Serialize(writer, TabellaComandi);
            }
        }

        public List<Azione> TabellaComandi = new List<Azione>();
    }

    public class Azione
    {
        public string Comment { get; set; }
        public int[] Key { get; set; }
        public List<TableCommand> Comandi { get; set; }
    }

    public class TableCommand
    {
        public AzioneTabella Azione { get; set; }
        public Pinza Pinza { get; set; }
        public ParametriTabella Parametro { get; set; }
    }

    public enum AzioneTabella
    {
        Good,
        Pick,
        Place,
        Waste
    }

    public enum ParametriTabella
    {
        None = 0,
        G,
        In,
        R,
        W,
    }
}
