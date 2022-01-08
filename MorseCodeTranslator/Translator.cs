using System;
using System.Text;

#pragma warning disable S2368

namespace MorseCodeTranslator
{
    public static class Translator
    {
        public static string TranslateToMorse(string message)
        {
            StringBuilder result = new StringBuilder();
            WriteMorse(MorseCodes.CodeTable, message, result);

            return result.ToString();
        }

        public static string TranslateToText(string morseMessage)
        {
            StringBuilder result = new StringBuilder();
            WriteText(MorseCodes.CodeTable, morseMessage, result);

            return result.ToString();
        }

        public static void WriteMorse(char[][] codeTable, string message, StringBuilder morseMessageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable == null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (morseMessageBuilder == null)
            {
                throw new ArgumentNullException(nameof(morseMessageBuilder));
            }

            message = message.ToUpperInvariant();

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i] == dot || message[i] == ',' || message[i] == ' ')
                {
                    continue;
                }

                if (i > 0)
                {
                    morseMessageBuilder.Append(" ");
                }

                morseMessageBuilder.Append(codeTable[message[i] - 65][1..]);
            }

            morseMessageBuilder.Replace('.', dot);
            morseMessageBuilder.Replace('-', dash);
            morseMessageBuilder.Replace(' ', separator);
        }

        public static void WriteText(char[][] codeTable, string morseMessage, StringBuilder messageBuilder, char dot = '.', char dash = '-', char separator = ' ')
        {
            if (codeTable == null)
            {
                throw new ArgumentNullException(nameof(codeTable));
            }

            if (morseMessage == null)
            {
                throw new ArgumentNullException(nameof(morseMessage));
            }

            if (messageBuilder == null)
            {
                throw new ArgumentNullException(nameof(messageBuilder));
            }

            morseMessage = morseMessage.Replace(separator, ' ');
            morseMessage = morseMessage.Replace(dot, '.');
            morseMessage = morseMessage.Replace(dash, '-');

            for (int i = 0; i < morseMessage.Length; i++)
            {
                int beginIndex = i;
                int endIndex = morseMessage.IndexOf(' ', beginIndex);
                endIndex = endIndex < 0 ? morseMessage.Length : endIndex;

                string phrase = morseMessage[beginIndex..endIndex];
                for (int j = 0; j < codeTable.GetLength(0); j++)
                {
                    string phraseInTable = string.Join(string.Empty, codeTable[j][1..]);

                    if (phrase == phraseInTable)
                    {
                        messageBuilder.Append(codeTable[j][0]);
                        break;
                    }
                }

                i = endIndex;
            }
        }
    }
}
