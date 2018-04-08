using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Seica
{
    public class RobotCommand
    {
        public Azioni azione;
        public Pinza pinza;
        public Stazioni stazioni;
        public PosizioneScheda posizioneScheda;
    }

    public class Robot
    {
        #region properties
        private IPAddress _ipAddress { get; set; }

        // Data buffer for incoming data.  
        private byte[] _bytes = new Byte[1024];
        private string _data { get; set; }
        //endpoint per la comunicazione standard con il robot
        private IPEndPoint _localEndPointMain;
        //endpoint per handshake con robot per verifica comunicazione ok
        private IPEndPoint _localEndPointComunicationCheck;

        private Socket _socketMain;
        private Socket _socketCominucationCheck;
        private Socket _handlerMain;
        private Socket _handlerComunication;

        private RobotPoint _points { get; set; }


        //bool per definire se l'istanza è pronta a mandare i comandi al robot, 
        //ovvero solamente dopo aver scaricato tutte le quote
        public bool AreCommandsEnabled;
        public bool CommandPending;
        public bool ComunicationOK;
        public int SchedePresenti;

        #endregion


        /// <summary>
        /// Costruttore di classe, 
        /// </summary>
        /// <param name="s"></param>
        public Robot()
        {
            //Imposto la culture info per poter convertire i numeri float in stringa, delimitati dal punto
            //invece che dalla virgola
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            //Ip definito scaticamente dato che il pc non cambierà mai indirizzo
            _ipAddress = IPAddress.Parse("192.168.250.5");

            // La porta 40000 è quella che utilizza il robot per stabile una connessione tcp,
            // Attenzione, Il progemma del robot, esegue il tentativo di connessione solamente al suo inizio
            _localEndPointMain = new IPEndPoint(_ipAddress, 40000);

            _socketMain = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _points = new RobotPoint();
            _points.Aree = new List<Area>();

            #region Punti per test
            //_points.Aree.Add(new Area()
            //{
            //    Name = "Zona Carico",
            //    Posizioni = new List<Posizione>()
            //    {
            //       new Posizione()
            //       {
            //           Name = "Prel_1_g_1_zn_1",
            //           Punto = new Point(1.23f,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "prel_1_g_2_zn_1",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "prel_2_g_1_zn_1",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "prel_2_g_2_zn_1",
            //           Punto = new Point(1,2,3,4,5,6)
            //       }
            //    }
            //});

            //_points.Aree.Add(new Area()
            //{
            //    Name = "Zona Test 1",
            //    Posizioni = new List<Posizione>()
            //    {

            //       new Posizione()
            //       {
            //           Name = "dep_1_g_1_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_1_g_2_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_2_g_1_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_2_g_2_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_3_g_1_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_3_g_2_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_4_g_1_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_4_g_2_zn_2",
            //           Punto = new Point(1,2,3,4,5,6)
            //       }
            //    }
            //});


            //_points.Aree.Add(new Area()
            //{
            //    Name = "Zona Test 2",
            //    Posizioni = new List<Posizione>()
            //    {

            //       new Posizione()
            //       {
            //           Name = "dep_1_g_1_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_1_g_2_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_2_g_1_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_2_g_2_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_3_g_1_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_3_g_2_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //       new Posizione()
            //       {
            //           Name = "dep_4_g_1_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_4_g_2_zn_3",
            //           Punto = new Point(1,2,3,4,5,6)
            //       }
            //    }
            //});


            //_points.Aree.Add(new Area()
            //{
            //    Name = "Zona Scarico",
            //    Posizioni = new List<Posizione>()
            //    {
            //       new Posizione()
            //       {
            //           Name = "dep_1_g_1_zn_4",
            //           Punto = new Point(1,2,3,4,5,6)
            //    },
            //       new Posizione()
            //       {
            //           Name = "dep_1_g_2_zn_4",
            //           Punto = new Point(1,2,3,4,5,6)
            //       },
            //    }
            //});

            #endregion

            //XmlSerialize();

            //Se non avviene la deseriallizzazione correttamente, l'applicazione viene interrota
            if (!DeserializePoints()) return;

            //esegue il binding e fa partire un thread per l'invio dei punti al Robot
            StartListeningTheRobot();

        }

        private void StartListeningTheRobot()
        {
            try
            {
                _socketMain.Bind(_localEndPointMain);
                _socketMain.Listen(2);
                Console.WriteLine("Bindig eseguito.");
                new Thread(SendQuotesToRobot).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void XmlSerialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(RobotPoint));
            using (TextWriter writer = new StreamWriter("punti.xml"))
            {
                serializer.Serialize(writer, _points);
            }
        }

        private bool DeserializePoints()
        {
            try
            {
                Console.WriteLine("Reading with XmlReader");

                // Create an instance of the XmlSerializer specifying type and namespace.
                XmlSerializer serializer = new
                XmlSerializer(typeof(RobotPoint));

                // A FileStream is needed to read the XML document.
                FileStream fs = new FileStream("punti.xml", FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);

                // Use the Deserialize method to restore the object's state.
                _points = (RobotPoint)serializer.Deserialize(reader);

                //Correzione punti in millimetri, solamente i primi tre parametri di ogni array
                foreach (var a in _points.Aree)
                {
                    foreach (var p in a.Posizioni)
                    {
                        p.Punto.x = p.Punto.x / 1000;
                        p.Punto.y = p.Punto.y / 1000;
                        p.Punto.z = p.Punto.z / 1000;
                    }
                }

                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore deserializzazione punti da foglio xml :" + ex.Message);
                return false;
            }

        }

        private void SendQuotesToRobot()
        {

            // Program is suspended while waiting for an incoming connection.
            Console.WriteLine("Attesa connessioni da clients ...");
            _handlerMain = _socketMain.Accept();

            //Una volta che il client stabilisce una connessione procedo a mettermi in ascolto per un eventuale comando
            Console.WriteLine("Connessione Stabilita" + _handlerMain.RemoteEndPoint.AddressFamily.ToString());
            _data = null;



            // An incoming connection needs to be processed.  
            while (!AreCommandsEnabled)
            {
                _bytes = new byte[1024];

                //rimango in ascolto per un comando    
                int bytesRec = _handlerMain.Receive(_bytes);

                //ricevuta una stringa dal client, controllo se contiene il sombolo #, che delimita 
                //una richiesta valida mandata dal robot
                _data = null;
                _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);

                if (_data.IndexOf("#") > -1)
                {
                    Console.WriteLine("Il client richiede : " + _data);
                    switch (_data)
                    {
                        //Richiesta del robot per le quote di lavoro
                        case "Quotes#":
                            WritePointsToTheRobot(_handlerMain);
                            break;

                        //Handshake finale da parte del robot per informarmi che tutti i punti sono stati ricevuti
                        case "Quotes_end#":
                            AreCommandsEnabled = true;
                            break;
                        default:
                            break;
                    }
                }

            }



        }

        private List<RobotPoint> ParsePoints()
        {
            throw new NotImplementedException();
        }

        public void WriteCommand(Azioni azione, Pinza pinza, Stazioni stazione, PosizioneScheda pos)
        {
            CommandPending = true;
            byte[] msg = Encoding.ASCII.GetBytes($"[{(int)azione},{(int)pinza},{(int)stazione},{(int)pos}]");
            _handlerMain.Send(msg);

            _data = null;

            // An incoming connection needs to be processed.  
            while (true)
            {
                _bytes = new byte[1024];
                int bytesRec = _handlerMain.Receive(_bytes);
                _data = null;
                _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                if (_data.IndexOf("cmd_ok#") > -1)
                {
                    Console.WriteLine("Comando Ricevuto da Robot");
                }
                else if (_data.IndexOf("cmd_end#") > -1)
                {
                    Console.WriteLine("Comando Completato dal Robot");
                    CommandPending = false;
                    break;
                }
            }
        }

        private bool WritePointsToTheRobot(Socket handler)
        {
            try
            {
                foreach (var a in _points.Aree)
                {
                    foreach (var point in a.Posizioni)
                    {
                        byte[] msg = Encoding.ASCII.GetBytes(point.Punto.ToString());
                        handler.Send(msg);


                        _data = null;

                        // An incoming connection needs to be processed.  
                        while (true)
                        {
                            _bytes = new byte[1024];
                            int bytesRec = handler.Receive(_bytes);
                            _data = null;
                            _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                            if (_data.IndexOf("ok#") > -1)
                            {
                                break;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}




