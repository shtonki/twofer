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
    public class ParseStream
    {
        private string RawString { get; set; }
        private int RawStringPositionCounter { get; set; }
        public bool AtEndOfStream => RawStringPositionCounter >= RawString.Length;
        public int CurrentLine = 1;
        public int CurrentColumn = 1;

        private char IncrementPositionCounter()
        {
            char rt = RawString[RawStringPositionCounter++];

            if (rt == '\n')
            {
                CurrentLine++;
            }
            else if (rt == '\r')
            {
                CurrentColumn = 1;
            }
            else
            {
                CurrentColumn++;
            }

            return rt;
        }
        private char Peek() => RawString[RawStringPositionCounter];
        private char Last() => RawString[RawStringPositionCounter - 1];

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

                if (currentCharacter == Parser.EndOfStream)
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

            EatWhiteSpace();

            if (EatNonWhiteSpaceCharacter() != Parser.StartOfBlock)
            {
                throw new ParseException(this,
                    String.Format("Start of block expected. (wanted '{0}', found '{1}')",
                    Parser.StartOfBlock, Last()));
            }

            int bracketDepth = 1;

            while (bracketDepth > 0)
            {
                char eatenCharacter = EatCharacter();

                if (eatenCharacter == Parser.StartOfBlock)
                {
                    bracketDepth++;
                }
                else if (eatenCharacter == Parser.EndOfBlock)
                {
                    bracketDepth--;
                }

                sb.Append(eatenCharacter);
            }

            sb.Length--;

            return sb.ToString();
        }

        public IEnumerable<string> EatParamaterList()
        {
            if (EatNonWhiteSpaceCharacter() != Parser.StartOfParamaters)
            {
                throw new ParseException(this, 
                    String.Format("Start of paramaters expected. (wanted '{0}', found '{1}')",
                    Parser.StartOfParamaters, Last()));
            }

            List<string> rt = new List<string>();

            EatWhiteSpace();
            while (true)
            {
                var paramater = EatWhile(c => c != Parser.EndOfParamaters && c != Parser.ParamaterSeparator);
                rt.Add(paramater);

                if (Peek() == Parser.ParamaterSeparator)
                {
                    EatCharacter();
                    EatWhiteSpace();
                }

                if (Peek() == Parser.EndOfParamaters)
                {
                    EatCharacter();
                    return rt;
                }

            }
        }

        public string EatWhile(Func<char, bool> continueFilter)
        {
            return EatWhile(continueFilter, c => true);
        }

        public string EatWhile(Func<char, bool> continueFilter, Func<char, bool> addFilter)
        {
            var sb = new StringBuilder();

            while (continueFilter(Peek()))
            {
                var c = EatCharacter();

                if (c == Parser.EndOfStream)
                {
                    throw new ParseException(this, "Unexpected end of file.");
                }

                if (addFilter(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public string EatWhiteSpace()
        {
            return EatWhile(c => char.IsWhiteSpace(c));
        }

        // utility functions

        public string GenerateContextString(int contextSize)
        {
            int startIndex = RawStringPositionCounter;

            if (startIndex >= RawString.Length)
            {
                startIndex--;
                // fixme: if we get an empty file we error out hard
            }

            while (startIndex > 0 && RawString[startIndex] != '\n' && RawString[startIndex] != '\r')
            {
                startIndex--;
            }

            int endIndex = RawStringPositionCounter;
            while (endIndex < RawString.Length - 1 && RawString[startIndex] != '\n' && RawString[startIndex] != '\r')
            {
                endIndex++;
            }

            return RawString.Substring(startIndex, endIndex - startIndex);
        }

    }
}
