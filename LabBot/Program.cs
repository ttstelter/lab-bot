using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBot
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlBuilder builder = new XmlBuilder();
            builder.fileCreation();
            // Commented out until I get the data portion working correctly
            //MyBot bot = new MyBot();
        }
    }
}
