using System;
using Seica;
namespace Test
{
    class Program
    {

        public class SynchronousSocketListener
        {
            public static void Main(String[] args)
            {
                Seica.Robot r = new Seica.Robot("192.168.250.7");

                while (!r.AreCommandsEnabled) ;

                r.WriteCommand(Robot.Azioni.Prelievo,
                               Robot.Pinza.Pinza_1,
                               Robot.Stazioni.Carico,
                               Robot.PosizioneScheda.Carico_1);

                r.WriteCommand(Robot.Azioni.Prelievo,
                               Robot.Pinza.Pinza_2,
                               Robot.Stazioni.Carico,
                               Robot.PosizioneScheda.Carico_2);

                r.WriteCommand(Robot.Azioni.Deposito,
                               Robot.Pinza.Pinza_1,
                               Robot.Stazioni.ZonaTest_1,
                               Robot.PosizioneScheda.ZonaTest1_1);

                r.WriteCommand(Robot.Azioni.Deposito,
                               Robot.Pinza.Pinza_2,
                               Robot.Stazioni.ZonaTest_1,
                               Robot.PosizioneScheda.ZonaTest1_2);

                r.WriteCommand(Robot.Azioni.Prelievo,
                               Robot.Pinza.Pinza_1,
                               Robot.Stazioni.ZonaTest_1,
                               Robot.PosizioneScheda.ZonaTest1_1);


                r.WriteCommand(Robot.Azioni.Prelievo,
                               Robot.Pinza.Pinza_2,
                               Robot.Stazioni.ZonaTest_1,
                               Robot.PosizioneScheda.ZonaTest1_2);

                r.WriteCommand(Robot.Azioni.Deposito,
                               Robot.Pinza.Pinza_1,
                               Robot.Stazioni.Scarico,
                               Robot.PosizioneScheda.Scarico);

                r.WriteCommand(Robot.Azioni.Deposito,
                               Robot.Pinza.Pinza_2,
                               Robot.Stazioni.Scarico,
                               Robot.PosizioneScheda.Scarico);
            }
        }
    }
}

