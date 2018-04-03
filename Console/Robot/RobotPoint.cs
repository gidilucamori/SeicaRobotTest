using System.Collections.Generic;

namespace Seica
{
    public class RobotPoint
    {
        public List<float> Point { get; set; }

        public override string ToString()
        {
            if (Point == null) return null;

            string txt = "[";
            foreach (var point in Point)
            {
                txt += point.ToString("R") +",";
            }

            //Tolgo l'ultima virgola
            txt = txt.Remove(txt.Length-1);
            txt += "]";
            return txt;
        }
    }

}
