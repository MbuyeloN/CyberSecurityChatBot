using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace CyberSecurityChatbot
{
    internal class Program
    {
        // Global speech engine used throughout the program
        static SpeechSynthesizer speaker = new SpeechSynthesizer();

        // Entry point of the application
        static void Main(string[] args)
        {
            ConfigureSpeaker();
            SetTheme();
            ShowWelcomeBanner();
            PlayGreeting();

            string userName = AskUserName();
            StartChat(userName);

            ShowGoodbyeMessage();
        }

        // Configures the voice settings once at the start
        static void ConfigureSpeaker()
        {
            try
            {
                speaker.Volume = 100;
                speaker.Rate = 2;
            }
            catch
            {
                // Prevents crashes if voice configuration fails
            }
        }

        // Sets console background and title
        static void SetTheme()
        {
            Console.Title = "Cybersecurity Awareness Chatbot";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        // Displays welcome banner and ASCII art
        static void ShowWelcomeBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================");
            Console.WriteLine("             CYBERSECURITY AWARENESS CHATBOT");
            Console.WriteLine("============================================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"   ____       _                  ____                      _ _         ");
            Console.WriteLine(@"  / ___|  ___| |__   ___ _ __   / ___|___  _ __  ___  ___ | | |        ");
            Console.WriteLine(@"  \___ \ / __| '_ \ / _ \ '__| | |   / _ \| '_ \/ __|/ _ \| | |        ");
            Console.WriteLine(@"   ___) | (__| | | |  __/ |    | |__| (_) | | | \__ \ (_) | | |        ");
            Console.WriteLine(@"  |____/ \___|_| |_|\___|_|     \____\___/|_| |_|___/\___/|_|_|        ");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("           Stay smart. Stay alert. Stay safe online.");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Plays the starting voice greeting
        static void PlayGreeting()
        {
            try
            {
                speaker.Speak("Hello. Welcome to the Cybersecurity Awareness Chatbot.");
                speaker.Speak("I am here to help you stay safe online.");
            }
            catch
            {
                WriteError("Voice greeting could not be played.");
            }
        }

        // Asks the user for their name and validates the input
        static string AskUserName()
        {
            string userName;

            while (true)
            {
                WriteBot("Hello! Welcome to the Cybersecurity Awareness Chatbot.");
                WriteBot("Please enter your name to begin.");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Name cannot be empty. Please enter your name.");
                    continue;
                }

                userName = input.Trim();

                if (userName.Length < 2)
                {
                    WriteError("Name must be at least 2 characters long.");
                    continue;
                }

                if (userName.Length > 20)
                {
                    WriteError("Name is too long. Please enter a shorter name.");
                    continue;
                }

                break;
            }

            WriteBot("Nice to meet you, " + userName + "!");
            SpeakText("Nice to meet you " + userName);

            WriteBot("I can help you learn about phishing, passwords, privacy, malware, scams, safe browsing, and two-factor authentication.");
            WriteBot("Type 'help' to see available topics or type 'exit' to close the chatbot.");

            return userName;
        }

        // Main chatbot conversation loop
        static void StartChat(string userName)
        {
            Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "A strong password should be long, unique, and hard to guess. Use uppercase letters, lowercase letters, numbers, and special characters. Never use your name, birthday, or common words." },
                { "phishing", "Phishing is a cyber attack where criminals pretend to be trusted organisations to steal personal information such as passwords or bank details. Always check links, email addresses, and suspicious attachments carefully." },
                { "privacy", "You can protect your privacy by limiting what you share online, checking privacy settings on social media, using strong passwords, and avoiding suspicious apps or websites." },
                { "malware", "Malware is harmful software that can damage your device, spy on you, or steal your information. Keep your antivirus updated and avoid downloading files from unknown sources." },
                { "scam", "Scams often use fear, urgency, or fake promises to trick people. Never share sensitive information or send money unless you are sure the request is genuine." },
                { "safe browsing", "Safe browsing means visiting trusted websites, checking for HTTPS, avoiding suspicious downloads, and keeping your browser and software updated." },
                { "two-factor authentication", "Two-factor authentication adds an extra layer of security by requiring a second step when logging in, such as a code sent to your phone. It is one of the best ways to protect your accounts." },
                { "2fa", "Two-factor authentication adds an extra layer of security by requiring a second step when logging in, such as a code sent to your phone. It is one of the best ways to protect your accounts." },
                { "cybersecurity", "Cybersecurity is the practice of protecting computers, devices, networks, and personal data from digital attacks and unauthorised access." },
                { "help", "You can ask about password safety, phishing, privacy, malware, scams, safe browsing, two-factor authentication, or general cybersecurity." }
            };

            bool chatting = true;

            while (chatting)
            {
                WriteSectionHeader("Ask a cybersecurity question");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(userName + ": ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Input cannot be empty. Please type a question.");
                    continue;
                }

                input = input.Trim().ToLower();

                if (input == "exit" || input == "quit" || input == "bye")
                {
                    chatting = false;
                }
                else if (input.Contains("how are you"))
                {
                    WriteBot("I am doing well, " + userName + ". Thank you for asking. I am ready to help you stay safe online.");
                }
                else if (input.Contains("what can you do"))
                {
                    WriteBot("I can answer questions about password safety, phishing, online privacy, malware, scams, safe browsing, and account protection.");
                }
                else if (input.Contains("thank you") || input.Contains("thanks"))
                {
                    WriteBot("You are welcome, " + userName + "! Staying informed is one of the best ways to stay safe online.");
                }
                else if (input.Contains("your name"))
                {
                    WriteBot("I am the Cybersecurity Awareness Chatbot, your digital safety assistant.");
                }
                else
                {
                    bool foundResponse = false;

                    foreach (KeyValuePair<string, string> item in responses)
                    {
                        if (input.Contains(item.Key))
                        {
                            WriteBot(item.Value);
                            foundResponse = true;
                            break;
                        }
                    }

                    if (!foundResponse)
                    {
                        WriteBot("I do not have an answer for that yet. Please ask about password safety, phishing, privacy, malware, scams, safe browsing, or two-factor authentication.");
                    }
                }
            }
        }

        // Displays chatbot messages in a styled format and speaks them
        static void WriteBot(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Bot: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();

            SpeakText(message);
        }

        // Helper method for speech output
        static void SpeakText(string message)
        {
            try
            {
                speaker.SpeakAsyncCancelAll();
                speaker.SpeakAsync(message);
            }
            catch
            {
                // Prevents crashes if speech fails
            }
        }

        // Displays error messages
        static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        // Displays section headers
        static void WriteSectionHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("------------------------------------------------------------");
            Console.ResetColor();
        }

        // Displays goodbye message and speaks it
        static void ShowGoodbyeMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thank you for using the Cybersecurity Awareness Chatbot.");
            Console.WriteLine("Goodbye and stay safe online!");
            Console.ResetColor();

            try
            {
                speaker.Speak("Goodbye and stay safe online.");
            }
            catch
            {
                // Prevents crashes on exit
            }
        }
    }
}