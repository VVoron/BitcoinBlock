using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBlock.Class
{
    public class Output
    {
        public long Value { get; }
        public string ScriptPubKey { get; }

        public Output(long value, string scriptPubKey)
        {
            Value = value;
            ScriptPubKey = scriptPubKey;
        }

        public override string ToString()
        {
            return $"{Value}{ScriptPubKey}";
        }
    }
}
