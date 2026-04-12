using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Chatbot";

            UIHelper.SetTheme();
            UIHelper.ShowWelcomeBanner();

            AudioHelper.PlayGreeting();

            Chatbot bot = new Chatbot();
            bot.StartChat();

            UIHelper.ShowGoodbyeMessage();
        }
    }
}
