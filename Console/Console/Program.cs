using System;
using Seica;
namespace Test
{
    class Program
    {

        //public static void Main(String[] args)
        //{
        //    Seica.Robot r = new Seica.Robot();

        //    while (!r.AreCommandsEnabled) ;

        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);

        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_2);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_1);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_2);

        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);

        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_2);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_3);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_4);

        //    //deposito 4 pezzi

        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_1);


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_2);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Scarico,
        //                   PosizioneScheda.Scarico);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.Scarico,
        //                   PosizioneScheda.Scarico);



        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Scarico,
        //                   PosizioneScheda.Scarico);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.Scarico,
        //                   PosizioneScheda.Scarico);
        //}


        private static Robot _robot;
        public static void Main()
        {
            _robot = new Robot();
            //while (!_robot.AreCommandsEnabled);
            Console.WriteLine("Punti scritti.");
            Ciclo c = new Ciclo();
            c.Start(_robot);
        }
    }
}


