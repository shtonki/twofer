using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    /// <summary>
    /// Exposes a stream of charachters to be parsed
    /// </summary>
    class ParseStream
    {
        public const char EndOfStream = '\0';
        private const char StartOfBlock = '{';
        private const char EndOfBlock = '}';

        private string RawString { get; set; }
        private int RawStringPositionCounter { get; set; }
        public bool AtEndOfStream => RawStringPositionCounter >= RawString.Length;
        private char IncrementPositionCounter() { return RawString[RawStringPositionCounter++]; }
        

        public ParseStream(string filename)
        {
            RawString = File.ReadAllText(filename);
        }

        // eating functions

        /// <summary>
        /// Eats a words from the input.
        /// If the end of the stream is reached before any non white space is encountered; null is returned.
        /// </summary>
        /// <returns>The next word in the stream</returns>
        public string EatWord()
        {
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                var currentCharacter = EatCharacter();

                if (currentCharacter == EndOfStream)
                {
                    // if we've eaten anything; return it
                    if (sb.Length > 0)
                    {
                        break;
                    }
                    else
                    {
                        return null;
                    }
                }

                // eat a character

                // nested if's because logic
                if (char.IsWhiteSpace(currentCharacter))
                {
                    // only return if we've eaten anything
                    if (sb.Length > 0)
                    {
                        break;
                    }
                }
                else
                {
                    sb.Append(currentCharacter);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Eats a character from the input. If the end of the stream is reached; the null character is returned.
        /// </summary>
        /// <returns>The next character in the stream.</returns>
        public char EatCharacter()
        {
            // check if we've reached the end of the stream
            if (AtEndOfStream)
            {
                return '\0';
            }
            
            return IncrementPositionCounter();
        }

        public char EatNonWhiteSpaceCharacter()
        {
            char rt;
            while (char.IsWhiteSpace(rt = EatCharacter())) { }
            return rt;
        }


        public string EatBlock()
        {
            var sb = new StringBuilder();

            char eaten = EatNonWhiteSpaceCharacter();
            if (eaten != StartOfBlock)
            {
                throw new ParseError(this, "Start of block ('{') expected.");
            }
            sb.Append(StartOfBlock);

            var blockText = EatWhile(c => c == EndOfBlock);
            sb.Append(blockText);

            return sb.ToString();
        }

        public string EatWhile(Func<char, bool> stopOn)
        {
            return EatWhileAndFilter(stopOn, c => true);
        }

        public string EatWhileAndFilter(Func<char, bool> stopOn, Func<char, bool> filter)
        {
            var sb = new StringBuilder();

            while (true)
            {
                var c = EatCharacter();

                if (c == EndOfStream)
                {
                    throw new ParseError(this, "Unexpected end of file.");
                }

                if (filter(c))
                {
                    sb.Append(c);
                }

                if (stopOn(c))
                {
                    return sb.ToString();
                }
            }
        }


        // utility functions


        public string GenerateContextString(int contextSize)
        {
            int startIndex = Math.Max(0, RawStringPositionCounter - contextSize);
            int endIndex = Math.Min(RawString.Length - 1, RawStringPositionCounter + contextSize);

            return RawString.Substring(startIndex, endIndex - startIndex);
        }

    }
}
