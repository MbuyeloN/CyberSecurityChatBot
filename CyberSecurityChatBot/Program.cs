using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Speech.Synthesis;
using System.Collections.Generic;

namespace CyberSecurityChatbot
{
    internal class Program
    {
        // Entry point of the application
        static void Main(string[] args)
        {
            // Set the console window title
            Console.Title = "Cybersecurity Awareness Chatbot";

            // Apply console theme and display welcome screen
            SetTheme();
            ShowWelcomeBanner();

            // Play audio greeting (beep sounds)
            PlayGreeting();

            // Ask for user's name and start chatbot interaction
            string userName = AskUserName();
            StartChat(userName);

            // Display goodbye message when user exits
            ShowGoodbyeMessage();
        }

        // Sets console background and clears screen
        static void SetTheme()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        // Displays ASCII art and welcome banner
        static void ShowWelcomeBanner()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================");
            Console.WriteLine("             CYBERSECURITY AWARENESS CHATBOT");
            Console.WriteLine("============================================================");

            // ASCII art for visual appeal
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"   ____       _                  ____                      _ _         ");
            Console.WriteLine(@"  / ___|  ___| |__   ___ _ __   / ___|___  _ __  ___  ___ | | |        ");
            Console.WriteLine(@"  \___ \ / __| '_ \ / _ \ '__| | |   / _ \| '_ \/ __|/ _ \| | |        ");
            Console.WriteLine(@"   ___) | (__| | | |  __/ |    | |__| (_) | | | \__ \ (_) | | |        ");
            Console.WriteLine(@"  |____/ \___|_| |_|\___|_|     \____\___/|_| |_|___/\___/|_|_|        ");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("              Stay smart. Stay alert. Stay safe online.");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("============================================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Plays a simple beep-based greeting sound
        static void PlayGreeting()
        {

            try
            {
                var speaker = new SpeechSynthesizer();
                speaker.Volume = 100;
                speaker.Rate = 0;

                speaker.Speak("Hello. Welcome to the Cybersecurity Awareness Chatbot.");
                speaker.Speak("I am here to help you stay safe online.");
            }
            catch
            {
                WriteError("Voice greeting could not be played.");
            }
        }

        // Prompts user to enter their name with validation
        static string AskUserName()
        {
            string userName = "";

            while (true)
            {
                WriteBot("Hello! Welcome to the Cybersecurity Awareness Chatbot.");
                WriteBot("Please enter your name to begin:");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                string input = Console.ReadLine();

                // Validate empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Name cannot be empty. Please enter a valid name.");
                    continue;
                }

                userName = input.Trim();

                // Validate minimum name length
                if (userName.Length < 2)
                {
                    WriteError("Your name is too short. Please enter at least 2 characters.");
                    continue;
                }

                break;
            }

            // Personalised greeting
            WriteBot("Nice to meet you, " + userName + "!");
            WriteBot("I can help you learn about phishing, passwords, privacy, malware, scams, and safe browsing.");
            WriteBot("Type 'help' to see available topics or type 'exit' to close the chatbot.");

            return userName;
        }

        // Main chatbot loop handling user interaction
        static void StartChat(string userName)
        {
            // Dictionary storing chatbot responses (keyword-based)
            Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "A strong password should be long, unique, and difficult to guess. Use uppercase letters, lowercase letters, numbers, and special characters. Do not use your name or birth date." },
                { "phishing", "Phishing is a cyber attack where criminals pretend to be trusted organisations to steal your personal information. Do not click suspicious links or open unknown attachments." },
                { "privacy", "You can protect your privacy by limiting what you share online, using strong passwords, enabling privacy settings, and checking app permissions regularly." },
                { "malware", "Malware is harmful software designed to damage your computer, steal data, or spy on you. Avoid downloading files from unknown websites and keep your antivirus updated." },
                { "scam", "Online scams often try to create panic, urgency, or excitement. Always verify messages, websites, and payment requests before responding." },
                { "safe browsing", "Safe browsing means visiting trusted websites, checking for HTTPS, avoiding suspicious downloads, and keeping your browser and software updated." },
                { "help", "Available topics: password, phishing, privacy, malware, scam, safe browsing. You can also ask: 'how are you', 'what can you do', or type 'exit'." }
            };

            bool chatting = true;

            // Chat loop continues until user exits
            while (chatting)
            {
                WriteSectionHeader("Ask a cybersecurity question");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(userName + ": ");
                string input = Console.ReadLine();

                // Handle empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    WriteError("Input cannot be empty. Please type a question.");
                    continue;
                }

                input = input.Trim().ToLower();

                // Exit conditions
                if (input == "exit" || input == "quit" || input == "bye")
                {
                    chatting = false;
                }
                // Basic conversational responses
                else if (input.Contains("how are you"))
                {
                    WriteBot("I am doing well, " + userName + ". Thank you for asking. I am ready to help you stay safe online.");
                }
                else if (input.Contains("what can you do"))
                {
                    WriteBot("I can answer questions about password safety, phishing, online privacy, malware, scams, and safe browsing.");
                }
                else if (input.Contains("thank you") || input.Contains("thanks"))
                {
                    WriteBot("You are welcome, " + userName + "! It is important to stay informed and alert online.");
                }
                else
                {
                    bool foundResponse = false;

                    // Loop through dictionary to find matching keyword
                    foreach (KeyValuePair<string, string> item in responses)
                    {
                        if (input.Contains(item.Key))
                        {
                            WriteBot(item.Value);
                            foundResponse = true;
                            break;
                        }
                    }

                    // Default response if no keyword matches
                    if (!foundResponse)
                    {
                        WriteBot("I do not have an answer for that yet. Please ask about password, phishing, privacy, malware, scam, or safe browsing.");
                    }
                }
            }
        }

        // Displays chatbot messages in styled format
        static void WriteBot(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Bot: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        // Displays error messages in red
        static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine();
        }

        // Displays section headers for better UI structure
        static void WriteSectionHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine(title);
            Console.WriteLine("------------------------------------------------------------");
            Console.ResetColor();
        }

        // Displays closing message when chatbot ends
        static void ShowGoodbyeMessage()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thank you for using the Cybersecurity Awareness Chatbot.");
            Console.WriteLine("Goodbye and stay safe online!");
            Console.ResetColor();
        }
    }
}