using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBlock.Class
{
    public class Transaction
    {
        public string Hash { get; private set; }
        public int Version { get; } = 1;
        public int VinSize { get { return Inputs.Count; } }
        public int VoutSize { get { return Outputs.Count; } }
        public int LockTime { get; } = 0;
        public int Size { get { return ToString().Length; } }
        public List<Input> Inputs { get; private set; }
        public List<Output> Outputs { get; private set; }

        public Transaction(List<Input> inputs, List<Output> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
            Hash = CalculateHash();
        }

        private string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Version);
            sb.Append(VinSize);
            sb.Append(VoutSize);
            sb.Append(LockTime);
            foreach (var input in Inputs) sb.Append(input);
            foreach (var output in Outputs) sb.Append(output);
            return sb.ToString();
        }
    }
}
