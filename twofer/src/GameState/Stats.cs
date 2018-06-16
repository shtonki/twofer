using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public class IntegerStat
    {
        private int BaseValue { get; set; }

        public static implicit operator int(IntegerStat stat)
        {
            return stat.BaseValue;
        }

        public static implicit operator IntegerStat(int integerValue)
        {
            return new IntegerStat(integerValue);
        }

        public IntegerStat(int baseValue)
        {
            BaseValue = baseValue;
        }
    }

    public class StringStat
    {
        private string Value { get; set; }

        public static implicit operator string(StringStat stat)
        {
            return stat.Value;
        }

        public static implicit operator StringStat(string stringValue)
        {
            return new StringStat(stringValue);
        }

        public StringStat(string value)
        {
            Value = value;
        }
    }
}
