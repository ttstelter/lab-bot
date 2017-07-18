using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBot
{
    class XmlBuilder
    {
        private string curDir = System.IO.Directory.GetCurrentDirectory();
        private string fileName = "config.xml";

        public void fileCreation()
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Name of account hold of Discord Channel: ");
                var userName = Console.ReadLine();

                Console.WriteLine("Twitter Consumer Key ");
                var twitterKey= Console.ReadLine();

                Console.WriteLine("blankOne ");
                var blank1 = Console.ReadLine();

                Console.WriteLine("blankTwo ");
                var blank2 = Console.ReadLine();

                // The XmlData class must be public for the XmlHelper to work.  RISK:  Otherwise InvalidOperationExeception is thrown.
                var xmlData = new XmlData
                {
                    User = userName,
                    TwitterKey = twitterKey,
                    blank1 = blank1,
                    blank2 = blank2
                };

                var data = XmlHelper.ToXml(xmlData);
                XmlHelper.ToXmlFile(data, curDir + "/" + fileName);

                Console.WriteLine("\nXml File created in " + curDir + 
                    ".  If you want to change these settings, delete the Xml file and restart the bot program.  Press Enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Setup Xml file exists.  If you want to make changes, then delete " + curDir + "\\" 
                    + fileName + " from the directory.  \nPress Enter to continue...");
                Console.ReadLine();
            }
        }

    }
}
