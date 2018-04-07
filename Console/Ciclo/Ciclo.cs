using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Seica
{
    public class Ciclo
    {
        
        public Ciclo()
        {
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
                        Pinza = Pinza.Pinza_1,
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

        public void Start(Robot _robot)
        {
            if (TabellaComandi != null)
            {
                Azione a = TabellaComandi.FirstOrDefault(i => i.Key.SequenceEqual(new int[] { 0, 0, 0 }));
                CommandExecuter(a,Stazioni.ZonaTest_1, _robot);
            }
            else
            {
                Console.WriteLine("La tabella dei comandi è vuota");
            }
        }

        private void CommandExecuter(Azione a,Stazioni s ,Robot _robot)
        {
            foreach (var c in a.Comandi)
            {
                if(c.Azione == AzioneTabella.Pick && c.Parametro == ParametriTabella.In)
                {
                    //Lettura da plc per vedere quale scheda andare a prendere
                    throw new NotImplementedException("lettura da plc");

                    PosizioneScheda PLCReading = PosizioneScheda.Carico_1; // <- funzione interrogazione plc

                    if (PLCReading == PosizioneScheda.NonDefinita) return; // <- nessuna scheda pronta
                    
                    _robot.WriteCommand(Azioni.Prelievo, c.Pinza, Stazioni.ZonaTest_1, PosizioneScheda.Carico_1);
                }


                if(c.Azione == AzioneTabella.Good)
                {
                    _robot.WriteCommand(Azioni.Deposito, c.Pinza, s, PosizioneScheda.ZonaTest1_1);
                }
            }
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
