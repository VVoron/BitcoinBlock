using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinBlock.Class
{
    public class Block
    {
        public int Version { get; } = 1;
        public string PreviousBlockHash { get; }
        public string MerkleRoot { get; }
        public uint Time { get; }
        public uint Bits { get; }
        public uint Nonce { get; private set; }
        public int TransactionCount { get; }
        public int Size { get; }

        public List<Transaction> Transactions { get; private set; }

        public Block(string previousBlockHash, List<Transaction> transactions, uint bits)
        {
            PreviousBlockHash = previousBlockHash;
            Transactions = transactions;
            MerkleRoot = CalculateMerkleRoot(transactions);
            Time = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Bits = bits;
            Nonce = 0;
            TransactionCount = transactions.Count;
            Size = CalculateBlockSize();

            string header = GetBlockHeader();
            string headerHash = CalculateHash(header);

            while (!IsValidHash(headerHash, bits))
            {
                Nonce++;
                header = GetBlockHeader();
                headerHash = CalculateHash(header);
            }

            Console.WriteLine("Block created!");
            Console.WriteLine("Hash: " + headerHash);
            Console.WriteLine("Nonce: " + Nonce);
        }

        public string GetBlockHeader()
        {
            return $"{Version}{PreviousBlockHash}{MerkleRoot}{Time}{Bits}{Nonce}";
        }

        public string CalculateHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        private bool IsValidHash(string hash, uint bits)
        {
            int targetZeros = (int)(bits / 2);
            return hash.Take(targetZeros).All(c => c == '0');
        }

        private string CalculateMerkleRoot(List<Transaction> transactions)
        {
            var hashes = transactions.Select(t => t.Hash).ToArray();

            if (hashes.Length == 1)
                return CalculateHash(hashes[0]);

            while (hashes.Length > 1)
            {
                if (hashes.Length % 2 != 0)
                {
                    hashes = hashes.Append(hashes[^1]).ToArray();
                }
                string[] newLevel = new string[hashes.Length / 2];
                for (int i = 0; i < hashes.Length; i += 2)
                {
                    newLevel[i / 2] = CalculateHash(hashes[i] + hashes[i + 1]);
                }
                hashes = newLevel;
            }
            return hashes[0];
        }

        private int CalculateBlockSize()
        {
            int size = Version.ToString().Length + PreviousBlockHash.Length + MerkleRoot.Length +
                       sizeof(uint) * 3 + TransactionCount.ToString().Length;

            foreach (var tx in Transactions)
            {
                size += tx.Size;
            }

            return size;
        }
    }
}
