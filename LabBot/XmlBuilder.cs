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
        private string[] fileData = ["Name of account hold of Discord Channel: ",
            "Discord Token: ",
            "Twitter Consumer Key: ",
            "Twitter Consumer Secret: ",
            "Twitter Access Token: ",
            "Twitter Access Token Secret: ",
            "Twitch Stream Link (i.e. www.twitch.tv/<your-username>): "];
        private List<string> xmlList = new List<string>();

        public void fileCreation()
        {
            if (!File.Exists(fileName))
            {
                for(int i =0; i < fileData.Length; i++)
                {
                    Console.WriteLine(fileData[i]);
                    var item = Console.ReadLine().Trim();
                    xmlList.Add(item);
                }

                // The XmlData class must be public for the XmlHelper to work.  RISK:  Otherwise InvalidOperationExeception is thrown.
                var xmlData = new XmlData
                {
                    User = xmlList.ElementAt(0),
                    DiscordToken = xmlList.ElementAt(1),
                    TwitterKey = xmlList.ElementAt(2),
                    TwitterConsumerKey = xmlList.ElementAt(3),
                    TwitterConsumerSecret = xmlList.ElementAt(4),
                    TwitterAccessToken = xmlList.ElementAt(5),
                    TwitterAccessSecret = xmlList.ElementAt(6),
                    TwitchLink = xmlList.ElementAt(7)
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
