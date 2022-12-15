namespace Feenix.Common.Signing;

/// <summary>
/// The generated ED25519 key pair from the <see cref="Signer"/>.
/// </summary>
public class KeyPair
{
    /// <summary>
    /// The public key.
    /// </summary>
    public string PublicKey { get; }
    
    /// <summary>
    /// The private key.
    /// </summary>
    public string PrivateKey { get; }
    
    public KeyPair(string publicKey, string privateKey)
    {
        PublicKey = publicKey;
        PrivateKey = privateKey;
    }
}