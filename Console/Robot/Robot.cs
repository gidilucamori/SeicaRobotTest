using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seica
{
    public class Robot
    {
        private string _ipAddress { get; set; }

        public Robot(string s)
        {

        }
        public List<RobotPoint> Points { get; set; }


    }

    public class RobotPoint
    {
        public List<float> Point { get; set; }
    }

}
