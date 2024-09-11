using System.Text;
using System.Security.Cryptography;
using BitcoinBlock.Class;


var csp = new ECDsaCng(ECCurve.NamedCurves.nistP256);
var privateKey = csp.ExportParameters(true);
var publicKey = csp.ExportParameters(false);

var transaction1 = new Transaction(
    new List<Input>
    {
        new Input("00000000000000000000000000000000", 0, Convert.ToBase64String(csp.SignData(Encoding.UTF8.GetBytes("tx1 data"))))
    },
    new List<Output>
    {
        new Output(50, Convert.ToBase64String(publicKey.Q.X.Concat(publicKey.Q.Y).ToArray()))
    }
);

var transaction2 = new Transaction(
    new List<Input>
    {
        new Input(transaction1.Hash, 0, Convert.ToBase64String(csp.SignData(Encoding.UTF8.GetBytes("tx2 data"))))
    },
    new List<Output>
    {
        new Output(30, Convert.ToBase64String(publicKey.Q.X.Concat(publicKey.Q.Y).ToArray())),
        new Output(20, Convert.ToBase64String(publicKey.Q.X.Concat(publicKey.Q.Y).ToArray()))
    }
);

List<Transaction> transactions = new List<Transaction> { transaction1, transaction2 };
Block genesisBlock = new Block("00000000000000000000000000000000", transactions, 2);

// Генерация следующего блока
// Block nextBlock = new Block(genesisBlock.CalculateHash(genesisBlock.GetBlockHeader()), transactions, 2);