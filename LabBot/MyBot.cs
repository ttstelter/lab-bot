using Discord;
using Discord.Commands;
using Discord.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LabBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;
        Random randTwo;

        string[] images;
        string[] compliments;
        string[] greetings;
        string[] commandList;
        string[] eightBallList;

        // For the username, replace "Solus" to whatever your discord user name 
        // is to have check work correctly
        string username = "Solus";

        // TODO: Use an .ini/xml file to fill tokens and other personal data to make the program more modular 

        public MyBot()
        {
            // Random number generator for images list.
            rand = new Random();

            // Random number generator for greetings list.
            randTwo = new Random();

            // List of image links
            images = new string[]
            {
                "Images/LabBot.png"
            };

            // List of image links
            compliments = new string[]
            {
                "beautiful", "outstanding", "respectiful", "engrossing", "zealous", " worthy", "ideal",
                "friendly", "empathetic", "festive", "jolly", "calm", "benevolent", "magnificent",
                "compassionate", "desirable", "expressive", "facinating", "fesity", "harmonious", "imaginative",
                "glowing", "fascinating", "harmonious", "jubilant", "incredible", "charming", "honest",
                "intelligent", "intriguing", "unique", "disciplined", "diverse", "daring", "considerate"
            };

            // List of greetings
            greetings = new string[]
            {
                "Hey ya'll! ", "Bonjour! ", "What's up Everyone, ", "Whaddup ", "How's it going! ", "My fellow Gamers, ",
                "Yooo! ", "G'day mates! ", "Howdy, ", "Hiya! "
            };

            // List of commands
            commandList = new string[]
            {
                "!hello", "!labbot", "!image", "!purge (Professor only)", "!live (Professor Only)", "!wiki", "!compliment",
                "!insult", "!tweet (Professor Only)"
            };

            eightBallList = new string[]
            {
                "It is certain.", "It is decidedly so.", "Without a doubt.", "Yes, definitely.",
                "As I see it, yes.", "Most likely.", "Outlook good.", "Not a Chance.",
                "No.", "Try again later.", "As I see it, no.", "Definitely not."
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            // Used to denote when a command is coming.  char used is '!'
            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            // TODO: Replace arguement "-" for logChannel to user defined main channel name
            discord.UserJoined += async (s, e) => {
                // Create a Channel object by searching for a channel named '#logs' on the server the ban occurred in.
                var logChannel = e.Server.FindChannels("-").FirstOrDefault();
                // Send a message to the server's log channel, stating that a user was banned.
                await logChannel.SendMessage($"Welcome to the Lab, {e.User.Name}!");
            };

            // Every botinstance needs to have commands ready
            commands = discord.GetService<CommandService>();

            //Commands
            registerHelloCommand();
            registerLabbotImageCommand();
            registerImageCommand();
            // registerPurgeCommand();
            registerStreamLiveCommand();
            registerRandWikiPage();
            registerCompliment();
            registerFakeHate();
            registerTwitterTweet();
            registerListCommands();
            registerEightBall();

            // Connect to the discord server
            discord.ExecuteAndWait(async () =>
            {
                // Replace "-" with your own discord token
                await discord.Connect("-", TokenType.Bot);
            });
        }

        // setup tweet 
        static async Task<Status> SendTweet(string greetToUse)
        {
            var auth = new SingleUserAuthorizer
            {
                // Fill in this information with your personal Twitter account tokens
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey =
                "-",
                    ConsumerSecret =
                "-",
                    AccessToken =
                "-",
                    AccessTokenSecret =
                "-"
                }
            };

            var context = new TwitterContext(auth);

            var status = await context.TweetAsync(
                greetToUse + "Come chill watch some classic games!  ~~~> <Twitch Stream Link>"
            );
            return status;
        }

        // LabBot says Hello!
        private void registerHelloCommand()
        {
            commands.CreateCommand("hello").Do(async (e) =>
            {
                await e.Channel.SendMessage("Hello, " + e.User.Name + "!");
            });
        }

        // LabBot shows himself off!
        private void registerImageCommand()
        {
            commands.CreateCommand("labbot")
                .Do(async (e) => 
                {
                    await e.Channel.SendMessage("My creator, Solus, found this perfect representation of me! " +
                        " He didn't create it.. but he thanks the one who did!");
                    await e.Channel.SendFile("IBot/LabBot.png");
                });
        }

        // LabBot posts a random image!
        private void registerLabbotImageCommand()
        {
            commands.CreateCommand("image")
                .Do(async (e) =>
                {
                    int randImageIndex = rand.Next(images.Length);
                    string imageToPost = images[randImageIndex];
                    await e.Channel.SendFile(imageToPost);
                });
        }

        // Commented out currently
        private void registerPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Do(async (e) =>
                {
                    Message[] messagesToDelete;
                    messagesToDelete = await e.Channel.DownloadMessages(70);

                    await e.Channel.DeleteMessages(messagesToDelete);
                });
        }

        // LabBot posts my stream in chat!
        private void registerStreamLiveCommand()
        {
            commands.CreateCommand("live").Do(async (e) =>
            {
                Console.WriteLine(e.User.Name);
                if(e.User.Name == username)
                {
                    // Play attention sound.
                    await e.Channel.SendMessage("@everyone " + username + " is going LIVE! ~~~> <Twitch Stream Link>");
                }
            });
        }

        // LabBot posts a random wiki page for all to see.
        private void registerRandWikiPage()
        {
            commands.CreateCommand("wiki").Do(async (e) =>
            {
                // Sned wiki page

                // Get wiki page link

                // Print out a random wiki page
                await e.Channel.SendMessage("Your Random Wiki page (IT'S RANDOM FOR ALL!!), so share what you get! ~~~> " + "https://en.wikipedia.org/wiki/Special:Random");
            });
        }

        // LabBot compilments my peeps.
        private void registerCompliment()
        {
            commands.CreateCommand("compliment")
                .Do(async (e) =>
                {
                    int randIndex = rand.Next(compliments.Length);
                    string wordToAdd = compliments[randIndex];
                    string print = "You are a " + wordToAdd + " person " + e.User.Name;
                    await e.Channel.SendMessage(print);
                });
        }

        // LabBot trolls.
        private void registerFakeHate()
        {
            commands.CreateCommand("insult")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                });
        }

        // LabBot sends out a twitter message!
        private void registerTwitterTweet()
        {
            commands.CreateCommand("tweet")
                .Do(async (e) =>
                {

                    int randGreet = randTwo.Next(greetings.Length);
                    string greetToUse = greetings[randGreet];

                    // Post my channel + whatever message
                    if (e.User.Name == username)
                    {
                        // Send message in discord saying it was posted OR not
                        try
                        {
                            var tweet = Task.Run(() => SendTweet(greetToUse));
                            tweet.Wait();
                            if (tweet == null)
                            {
                                await e.Channel.SendMessage(username + ", I seem to be having a problem creating the tweet :(." + 
                                    "  It looks like the message returned a NULL status.");
                            }
                        }
                        catch (Exception ex)
                        {
                            await e.Channel.SendMessage(username + ", I seem to be having a problem creating the tweet :(" +
                                "  It appears an exception was caused during the tweet.");
                            return;
                        }

                        await e.Channel.SendMessage(username + ", Twitter post Successful!");
                    }
                    else
                    {
                        await e.Channel.SendMessage("Only " + username + " can use this command.");
                    }
                });
        }

        // LabBot gives out commands!
        private void registerListCommands()
        {
            commands.CreateCommand("commands").Do(async (e) =>
            {
                await e.Channel.SendMessage("!hello \n" +
                    "!labbot \n" +
                    "!image \n" +
                    "!purge (" + username + " Only) \n" +
                    "!live (" + username + " Only) \n" +
                    "!wiki \n" + 
                    "!compliment \n" + 
                    "!insult \n" +
                    "!tweet (" + username + " Only) \n" +
                    "!8ball \n");
            });
        }

        // LabBot acts like an eightball
        private void registerEightBall()
        {
            commands.CreateCommand("8ball").Do(async (e) =>
            {
                int randIndex = rand.Next(eightBallList.Length);
                string opinion = eightBallList[randIndex];
                await e.Channel.SendMessage(opinion);
            });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}