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

        //    //richiesta a plc quale scheda è disponibile
        //    //throw new NotImplementedException("Richiesta posizione a plc");

        //    #region test
        //    #endregion


        //    #region carico zona test 1
        //      if (true)
        //    {

        //        r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);
        //    }

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Carico,
        //                       PosizioneScheda.Carico_2);
        //    }

        //    //Deposito prime due schede
        //     r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_1);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_2);

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);
        //    }

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Carico,
        //                       PosizioneScheda.Carico_2);
        //    }

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_3);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_4);
        //    #endregion

        //    #region carico zona test 2
        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);
        //    }

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Carico,
        //                       PosizioneScheda.Carico_2);
        //    }

        //    //Deposito prime due schede
        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_1);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_2);

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.Carico,
        //                   PosizioneScheda.Carico_1);
        //    }

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Prelievo,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Carico,
        //                       PosizioneScheda.Carico_2);
        //    }

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_3);

        //    r.WriteCommand(Azioni.Deposito,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_4);
        //    #endregion


        //    #region deposito zona test 1 fine
        //    //deposito 4 pezzi
        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_1);


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_2);

        //    //test se plc dichiara che posso andare al deposito
        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_1,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }


        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_3);


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_1,
        //                   PosizioneScheda.ZonaTest1_4);

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_1,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }
        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }
        //    #endregion

        //    #region deposito zona test 2 fine
        //    //deposito 4 pezzi
        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_1);


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_2);

        //    //test se plc dichiara che posso andare al deposito
        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_1,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_1,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_3);


        //    r.WriteCommand(Azioni.Prelievo,
        //                   Pinza.Pinza_2,
        //                   Stazioni.ZonaTest_2,
        //                   PosizioneScheda.ZonaTest2_4);

        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_1,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }
        //    if (true)
        //    {
        //        r.WriteCommand(Azioni.Deposito,
        //                       Pinza.Pinza_2,
        //                       Stazioni.Scarico,
        //                       PosizioneScheda.Scarico);
        //    }
        //    #endregion
        //}


        private static Robot _robot;
        public static void Main()
        {
            _robot = new Robot();
            //while (!_robot.AreCommandsEnabled);
            Console.WriteLine("Punti scritti.");
            Ciclo c = new Ciclo(_robot);
            c.Start();
        }
    }
}


