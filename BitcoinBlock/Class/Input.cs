using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBlock.Class
{
    public class Input
    {
        public string PreviousTransactionHash { get; }
        public int OutputIndex { get; }
        public string ScriptSig { get; }

        public Input(string previousTransactionHash, int outputIndex, string scriptSig)
        {
            PreviousTransactionHash = previousTransactionHash;
            OutputIndex = outputIndex;
            ScriptSig = scriptSig;
        }

        public override string ToString()
        {
            return $"{PreviousTransactionHash}{OutputIndex}{ScriptSig}";
        }
    }
}
