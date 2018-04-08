using System;

namespace Seica
{
    public class Compact
    {
        public Stazioni StazioneCompact { get; }

        public Slot Posizione_1;
        public Slot Posizione_2;
        public Slot Posizione_3;
        public Slot Posizione_4;

        public int[] Status = new int[3];


        public Compact(Stazioni stazione)
        {
            StazioneCompact = stazione;
            Posizione_1 = new Slot();
            Posizione_2 = new Slot();
            Posizione_3 = new Slot();
            Posizione_4 = new Slot();
        }

        public void ReadStatusFromPLC()
        {
            throw new NotImplementedException();
        }

        public int GiveMeTheFirstEmptySlot()
        {
            if (Posizione_1.Status == CompactSlotStatus.Vuota) return 1;
            else if (Posizione_2.Status == CompactSlotStatus.Vuota) return 2;
            else if (Posizione_3.Status == CompactSlotStatus.Vuota) return 3;
            else if (Posizione_4.Status == CompactSlotStatus.Vuota) return 4;
            else return -1;
        }

        public void AddToEmptySlot(int slot)
        {
            if (slot == 1)
            {
                Posizione_1.Status = CompactSlotStatus.Occupata;
                Posizione_1.TestResult = CompactSlotTestResoult.NotTesteYet;
            }
            if (slot == 2)
            {
                Posizione_2.Status = CompactSlotStatus.Occupata;
                Posizione_2.TestResult = CompactSlotTestResoult.NotTesteYet;
            }
            if (slot == 3)
            {
                Posizione_3.Status = CompactSlotStatus.Occupata;
                Posizione_3.TestResult = CompactSlotTestResoult.NotTesteYet;
            }
            if (slot == 4)
            {
                Posizione_4.Status = CompactSlotStatus.Occupata;
                Posizione_4.TestResult = CompactSlotTestResoult.NotTesteYet;
            }
        }

        public void ReleaseSlot(int slot)
        {
            if (slot == 1)
            {
                Posizione_1.Status = CompactSlotStatus.Vuota;
                Posizione_1.TestResult = CompactSlotTestResoult.NotTesteYet;
                return;
            }
            if (slot == 2)
            {
                Posizione_2.Status = CompactSlotStatus.Vuota;
                Posizione_2.TestResult = CompactSlotTestResoult.NotTesteYet;
                return;
            }
            if (slot == 3)
            {
                Posizione_3.Status = CompactSlotStatus.Vuota;
                Posizione_3.TestResult = CompactSlotTestResoult.NotTesteYet;
                return;
            }
            if (slot == 4)
            {
                Posizione_4.Status = CompactSlotStatus.Vuota;
                Posizione_4.TestResult = CompactSlotTestResoult.NotTesteYet;
                return;
            }
        }

        public void GetTestResoultFromPLC()
        {
            //Simulo la lettura dati da plc, e setto tutte e quattro le schede a Good
            Posizione_1.TestResult = CompactSlotTestResoult.Good;
            Posizione_2.TestResult = CompactSlotTestResoult.Good;
            Posizione_3.TestResult = CompactSlotTestResoult.Good;
            Posizione_4.TestResult = CompactSlotTestResoult.Good;
        }

        public int GiveMeTheFirstGoodToPick()
        {
            if (Posizione_1.Status == CompactSlotStatus.Occupata && Posizione_1.TestResult == CompactSlotTestResoult.Good)
                return 1;
            else if (Posizione_2.Status == CompactSlotStatus.Occupata && Posizione_2.TestResult == CompactSlotTestResoult.Good)
                return 2;
            else if (Posizione_3.Status == CompactSlotStatus.Occupata && Posizione_3.TestResult == CompactSlotTestResoult.Good)
                return 3;
            else if (Posizione_4.Status == CompactSlotStatus.Occupata && Posizione_4.TestResult == CompactSlotTestResoult.Good)
                return 4;

            else return -1;
        }
    }

    public class Slot
    {
        public CompactSlotStatus Status;
        public CompactSlotTestResoult TestResult;
    }

    public enum CompactSlotStatus
    {
        Vuota,
        Occupata
    }

    public enum CompactSlotTestResoult
    {
        NotTesteYet,
        Good,
        Failed
    }
}
