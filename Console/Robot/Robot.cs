using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public Robot(string s)
        {
            _ipAddress = IPAddress.Parse("192.168.250.5");
            _localEndPoint = new IPEndPoint(_ipAddress, 40000);
            _socket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            #region scrittura punti test
            List<RobotPoint> x = new List<RobotPoint>();
            for (int i = 0; i < 20; i++)
            {
                x.Add(new RobotPoint()
                {
                    Point = new List<float> { i, i, i, i, i, i }
                });
            }
            _points = x;
            #endregion

            StartListeningTheRobot();
            RobotListener();
        }

        private void RobotListener()
        {
            while (true)
            {
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = _socket.Accept();
                Console.WriteLine("Connessione Stabilita");
                _data = null;

                // An incoming connection needs to be processed.  
                while (handler.Connected)
                {
                    _bytes = new byte[1024];
                    int bytesRec = handler.Receive(_bytes);
                    _data = null;
                    _data += Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                    if (_data.IndexOf("#") > -1)
                    {
                        Console.WriteLine(_data);
                        switch (_data)
                        {
                            case "SendMeTheQuotes#":
                                WritePointsToTheRobot(handler);
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
                        if (_data.IndexOf("Ok#") > -1)
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

        private void StartListeningTheRobot()
        {
            try
            {
                _socket.Bind(_localEndPoint);
                _socket.Listen(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Return the status of the connection with the robot
        /// </summary>
        /// <returns>true=Connected, false=Non Connesso, null= errore</returns>
        private bool? IsRobotConnected()
        {
            throw new NotImplementedException();
        }

    }

}
