using System;

namespace Seica
{
    public class Compact
    {
        public CompactSlotStatus Posizione_1;
        public CompactSlotStatus Posizione_2;
        public CompactSlotStatus Posizione_3;
        public CompactSlotStatus Posizione_4;

        public int[] Status = new int[3];

        public void ReadStatusFromPLC()
        {
            throw new NotImplementedException();
        }
    }

    public enum CompactSlotStatus
    {
        Vuota,
        Occupata
    }
}
