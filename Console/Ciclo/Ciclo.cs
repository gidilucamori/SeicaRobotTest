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
        private Robot _robot = new Robot();

        public Ciclo( Robot robot)
        {
            _robot = robot;

            #region Comando tutto vuoto stazione test 1
            TabellaComandi.Add(new Azione
            {
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
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_1,
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
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Place,
                        Pinza = Pinza.Pinza_1,
                        Parametro = ParametriTabella.None
                    },
                }
            });
            TabellaComandi.Add(new Azione
            {
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
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_1,
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
                        Pinza = Pinza.Pinza_2,
                        Parametro = ParametriTabella.None
                    },
                    new TableCommand()
                    {
                        Azione = AzioneTabella.Good,
                        Pinza = Pinza.Pinza_1,
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
                    int stazione = 1; //<- lettura da plc, stazione interessata
                    int[] stato = { 0, 0, 0 }; // <- lettura stato stazione


                    Azione a = TabellaComandi.FirstOrDefault(i => i.Key.SequenceEqual(stato));

                    if (a == null) Console.WriteLine($"Attenzione, nessun azione corrisponde alla sequenza {stato[0]}{stato[1]}{stato[2]}");

                    CommandExecuter(a, (Stazioni)stazione);
                }
            }
            
        }

        private void CommandExecuter(Azione a, Stazioni s)
        {
            foreach (var c in a.Comandi)
            {
                #region Good
                if (c.Azione == AzioneTabella.Good && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.None)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Good && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.None)
                {
                    continue;
                }
                #endregion

                #region Pick Pinza 1

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.G)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.In)
                {
                    //Lettura da plc per sapere dove devo andare a prendere la scheda
                    PosizioneScheda pos = (PosizioneScheda)1;//<-Lettura da PLC
                    _robot.WriteCommand(Azioni.Prelievo,Pinza.Pinza_1,Stazioni.Carico, pos);
                   // _schede.Add(new Scheda());
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
                    continue;
                }

                if (c.Azione == AzioneTabella.Pick && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.In)
                {
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

                #region Place

                if (c.Azione == AzioneTabella.Place && c.Pinza == Pinza.Pinza_1 && c.Parametro == ParametriTabella.None)
                {
                    continue;
                }

                if (c.Azione == AzioneTabella.Place && c.Pinza == Pinza.Pinza_2 && c.Parametro == ParametriTabella.None)
                {
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

        private void Pick1In( )
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
