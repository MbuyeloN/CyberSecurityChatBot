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
using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    internal class Chatbot
    {
        private string userName = "";
        private readonly Dictionary<string, string> responses;

        public Chatbot()
        {
            responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "password",
                    "A strong password should be long, unique, and include uppercase letters, lowercase letters, numbers, and special characters. Avoid using your name or birthday."
                },
                {
                    "phishing",
                    "Phishing is a scam where attackers pretend to be trusted organisations to steal your information. Always verify email addresses and avoid suspicious links."
                },
                {
                    "privacy",
                    "Protect your privacy by limiting what you share online, using strong passwords, and checking app permissions regularly."
                },
                {
                    "scam",
                    "Online scams often create urgency or fear. Never send money or personal details without verifying the source first."
                },
                {
                    "help",
                    "You can ask me about passwords, phishing, privacy, scams, safe browsing, or how to protect your information online."
                },
                {
                    "safe browsing",
                    "Safe browsing means visiting trusted websites, avoiding suspicious downloads, checking for HTTPS, and keeping your browser updated."
                },
                {
                    "malware",
                    "Malware is harmful software that can damage your device or steal your data. Install antivirus software and avoid downloading files from unknown sources."
                }
            };
        }

        public void StartChat()
        {
            AskUserName();

            bool chatting = true;

            while (chatting)
            {
                UIHelper.WriteSectionHeader("Ask me something about cybersecurity");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{userName}: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    UIHelper.WriteError("Input cannot be empty. Please type a question or command.");
                    continue;
                }

                input = input.Trim().ToLower();

                if (input == "exit" || input == "quit" || input == "bye")
                {
                    chatting = false;
                    continue;
                }

                RespondToInput(input);
            }
        }

        private void AskUserName()
        {
            while (true)
            {
                UIHelper.WriteBot("Hello! Before we begin, what is your name?");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    UIHelper.WriteError("Please enter a valid name.");
                    continue;
                }

                userName = input.Trim();
                break;
            }

            UIHelper.WriteBot($"Welcome, {userName}! I am your Cybersecurity Awareness Assistant.");
            UIHelper.WriteBot("Type 'help' to see what I can assist you with, or type 'exit' to close the chatbot.");
        }

        private void RespondToInput(string input)
        {
            foreach (var item in responses)
            {
                if (input.Contains(item.Key))
                {
                    UIHelper.WriteBot(item.Value);
                    return;
                }
            }

            if (input.Contains("how are you"))
            {
                UIHelper.WriteBot($"I'm doing great, {userName}. I'm here and ready to help you stay safe online.");
            }
            else if (input.Contains("what can you do"))
            {
                UIHelper.WriteBot("I can teach you about common cybersecurity topics like phishing, passwords, malware, privacy, scams, and safe browsing.");
            }
            else if (input.Contains("thank you") || input.Contains("thanks"))
            {
                UIHelper.WriteBot($"You’re welcome, {userName}! Staying informed is one of the best ways to stay safe online.");
            }
            else
            {
                UIHelper.WriteBot("I’m not sure about that yet. Try asking about passwords, phishing, privacy, scams, malware, or safe browsing.");
            }
        }
    }
}