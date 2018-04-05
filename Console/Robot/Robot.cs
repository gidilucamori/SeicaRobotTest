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
    public class Robot
    {
        private IPAddress _ipAddress { get; set; }
        // Data buffer for incoming data.  
        private byte[] _bytes = new Byte[1024];
        private string _data;
        private IPEndPoint _localEndPoint;
        private Socket _socket;
        private List<RobotPoint> _points { get; set; }
        private Socket _handler;
        public bool AreCommandsEnabled;

        public enum Stazioni
        {
            Carico = 1,
            Scarico = 4,
            ZonaTest_1 = 2,
            ZonaTest_2 = 3,
        }

        public enum Pinza
        {
            Pinza_1 = 1,
            Pinza_2 = 2
        }

        public enum Azioni
        {
            Prelievo = 1,
            Deposito = 0
        }

        public enum PosizioneScheda
        {
            Carico_1 = 1,
            Carico_2 = 2,
            ZonaTest1_1 = 1,
            ZonaTest1_2 = 2,
            ZonaTest1_3 = 3,
            ZonaTest1_4 = 4,
            ZonaTest2_1 = 1,
            ZonaTest2_2 = 2,
            ZonaTest2_3 = 3,
            ZonaTest2_4 = 4,
            Scarico = 1
        }

        public Robot(string s)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            _ipAddress = IPAddress.Parse("192.168.250.5");
            _localEndPoint = new IPEndPoint(_ipAddress, 40000);
            _socket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            #region scrittura punti test
            List<RobotPoint> x = new List<RobotPoint>();
            //for (int i = 0; i < 22; i++)
            //{
            //    x.Add(new RobotPoint()
            //    {
            //        Point = new List<float> { i / 2f, i / 2f, i / 2f, i / 2f, i / 2f, i / 2f }
            //    });
            //}
            //_points = x;
            #endregion

            //XmlSerialize();
            DeserializeObject();
            foreach (var arr in _points)
            {
                arr.Point[0] = arr.Point[0] / 1000;
                arr.Point[1] = arr.Point[1] / 1000;
                arr.Point[2] = arr.Point[2] / 1000;
            }

             StartListeningTheRobot();
        }

        private void StartListeningTheRobot()
        {
            try
            {
                _socket.Bind(_localEndPoint);
                _socket.Listen(10);
                Console.WriteLine("Bindig eseguito, Numero massimo di client = 10.");
                new Thread(RobotListener).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private void XmlSerialize()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<RobotPoint>));
            using (TextWriter writer = new StreamWriter("punti.xml"))
            {
                serializer.Serialize(writer, _points);
            }
        }

        private void DeserializeObject()
        {
            Console.WriteLine("Reading with XmlReader");

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(List<RobotPoint>));

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream("punti.xml", FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Use the Deserialize method to restore the object's state.
            _points = (List<RobotPoint>)serializer.Deserialize(reader);
            fs.Close();
        }

        private void RobotListener()
        {
            while (true)
            {
                //Se ho gia eseguito il loop e la scrittura dei punti è gia stata completata
                if (AreCommandsEnabled) return;

                // Program is suspended while waiting for an incoming connection.
                Console.WriteLine("Attesa connessioni da clients ...");
                _handler = _socket.Accept();
                Console.WriteLine("Connessione Stabilita" + _handler.RemoteEndPoint.AddressFamily.ToString());
                _data = null;



                // An incoming connection needs to be processed.  
                while (!AreCommandsEnabled)
                {
                    //Se perdo la connessione torno ad attendere la riapertura del socket
                    if (!IsRobotConnected(_handler)) return;

                    _bytes = new byte[1024];
                    int bytesRec = _handler.Receive(_bytes);
                    _data = null;
                    _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                    if (_data.IndexOf("#") > -1)
                    {
                        Console.WriteLine(_data);
                        switch (_data)
                        {
                            case "Quotes#":
                                WritePointsToTheRobot(_handler);
                                break;
                            case "Quotes_end#":
                                AreCommandsEnabled = true;
                                break;
                            default:
                                break;
                        }
                    }

                }

            }

        }

        private List<RobotPoint> ParsePoints()
        {
            throw new NotImplementedException();
        }

        public void WriteCommand(Azioni azione, Pinza pinza, Stazioni stazione , PosizioneScheda pos)
        {
            byte[] msg = Encoding.ASCII.GetBytes($"[{(int)azione},{(int)pinza},{(int)stazione},{(int)pos}]");
            _handler.Send(msg);

            _data = null;

            // An incoming connection needs to be processed.  
            while (true)
            {
                _bytes = new byte[1024];
                int bytesRec = _handler.Receive(_bytes);
                _data = null;
                _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                if (_data.IndexOf("cmd_ok#") > -1)
                {
                    Console.WriteLine("Comando Ricevuto da Robot");
                }
                else if (_data.IndexOf("cmd_end#") > -1)
                {
                    Console.WriteLine("Comando Completato dal Robot");
                    break;
                }
            }
        }

        private bool WritePointsToTheRobot(Socket handler)
        {
            try
            {
                foreach (var point in _points)
                {
                    byte[] msg = Encoding.ASCII.GetBytes(point.ToString());
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

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        /// <summary>
        /// Return the status of the connection with the robot
        /// </summary>
        /// <returns>true=Connected, false=Non Connesso, null= errore</returns>
        private bool IsRobotConnected(Socket socket)
        {
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
                }
                catch (SocketException) { return false; }
            }
        }
    }
}




