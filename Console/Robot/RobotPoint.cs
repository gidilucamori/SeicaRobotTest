using System.Collections.Generic;
using System.Xml.Serialization;

namespace Seica
{
    public class RobotPoint
    {
        [XmlArray]
        public List<Area> Aree { get; set; }
    }

    public class Point
    {
        public Point(float x, float y, float z, float rx, float ry, float rz)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.rx = rx;
            this.ry = ry;
            this.rz = rz;
        }

        public Point()
        {

        }

        public override string ToString()
        {
            return $"[{this.x},{this.y},{this.z},{this.rx},{this.ry},{this.rz}]";
        }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float rx { get; set; }
        public float ry { get; set; }
        public float rz { get; set; }

        //public float Value { get; set; }
    }

    public class Posizione
    {
        public string Name { get; set; }
        public Point Punto { get; set; }
    }

    public class Area
    {
        public string Name { get; set; }
        [XmlArray]
        public List<Posizione> Posizioni { get; set; }

    }
}
