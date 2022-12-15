using Feenix.Common.Signing;

namespace Feenix.CLI.Commands;

internal class SignerCommand : Command
{
    public SignerCommand() : base(
        "signer",
        "generate-key (-o=<Path to output dir>) - Generate a new key pair for signing. If -o is not specified, the key pair will be stored to the Feenix appdata directory.",
        "sign <Path to package directory> (-k=<Path to private key>) - Signs the package of the given path.",
        "verify <Path to package directory> <Signature> (-k=<Path to public key>) - Verifies the signature of the package of the given path."
    ) { }

    internal override void Execute(CommandArgs args)
    {
        if (args.Subcommand("generate-key"))
        {
            var outputDir = args.Optional("-o");
            if (string.IsNullOrEmpty(outputDir))
            {
                outputDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Feenix", "dev");
                Directory.CreateDirectory(outputDir);
            }

            var keyPair = Signer.GenerateKeyPair();
            File.WriteAllText(Path.Combine(outputDir, "feenix-signer.key.pub"), keyPair.PublicKey);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Public key saved to " + Path.Combine(outputDir, "feenix-signer.key.pub"));
            File.WriteAllText(Path.Combine(outputDir, "feenix-signer.key.priv"), keyPair.PrivateKey);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Private key saved to " + Path.Combine(outputDir, "feenix-signer.key.priv"));
        }
        else if (args.Subcommand("sign"))
        {
            var packageDir = args.Arg(1);
            var privateKey = args.Optional("-k");
            if (string.IsNullOrEmpty(privateKey))
            {
                privateKey = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Feenix", "dev", "feenix-signer.key.priv");
            }
            
            var signature = Signer.Sign(packageDir, File.ReadAllText(privateKey));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Signature generated: \n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(signature + "\n");
            Console.ResetColor();
        }
        else if (args.Subcommand("verify"))
        {
            var packageDir = args.Arg(1);
            var signature = args.Arg(2);
            var publicKey = args.Optional("-k");
            if (string.IsNullOrEmpty(publicKey))
            {
                publicKey = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Feenix", "dev", "feenix-signer.key.pub");
            }

            var result = Signer.Verify(Signer.ComputeHash(packageDir), signature, File.ReadAllText(publicKey));
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Signature verification result: " + result);
        }
    }
}