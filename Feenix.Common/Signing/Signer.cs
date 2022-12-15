﻿using System.Security.Cryptography;
using System.Text;
using NSec.Cryptography;

namespace Feenix.Common.Signing;

/// <summary>
/// The signer is a security feature in Feenix that allows signing of Feenix script packages to minimize the risk of
/// malicious code being executed. The Feenix server software can deny non-signed - so called anonymous - packages the
/// access to the transport channel of the event based communication system.
/// <br/><br/>
/// For that to work the developer of a package must create a keypair to sign their packages. The public key as well as
/// the signature and an unique ID will be stored in a hosted Feenix registry (either the official one, or a private one).
/// <br/><br/>
/// In the signing process of the package a signature is generated by generating a package hash. The hash will be
/// computed by iterating through all script files, generating checksums, combining them and hashing the result with
/// SHA512. This hash is than signed with the private key of the developer.
/// <br/><br/>
/// When the package makes it first initial request to the server, it will sent the registry ID as well as the current
/// hash of the package to the server. If the server denies anonymous packages, the current signature and public key
/// will be retrieved from the registry of the server. The server will then verify the signature of the package.
/// If the signature is valid, the package will be allowed an unique network ID will be generated and echoed back to
/// the packages context. Every request to the server after the validation process will be then be transported
/// through the system. If a package fails the validation process, every request will be discarded.
/// <remarks>
/// Currently the keypair will be generated using the ED25519 algorithm.
/// </remarks>
/// </summary>
public static class Signer
{
    /// <summary>
    /// Generates a new ED25519 keypair.
    /// </summary>
    /// <returns>The keypair as <see cref="KeyPair"/> object.</returns>
    public static KeyPair GenerateKeyPair()
    {
        var algorithm = SignatureAlgorithm.Ed25519;
        using var key = Key.Create(algorithm, new KeyCreationParameters()
        {
            ExportPolicy = KeyExportPolicies.AllowPlaintextExport
        });
        var publicKey = Convert.ToBase64String(key.Export(KeyBlobFormat.RawPublicKey));
        var privateKey = Convert.ToBase64String(key.Export(KeyBlobFormat.RawPrivateKey));
        return new KeyPair(publicKey, privateKey);
    }

    /// <summary>
    /// Creates a digital signature for the package of the given directory path.
    /// </summary>
    /// <param name="packageDir">The path to the directory of the target package.</param>
    /// <param name="privateKey">The private key as base64 encoded.</param>
    /// <returns>The digital signature.</returns>
    public static string Sign(string packageDir, string privateKey)
    {
        var algorithm = SignatureAlgorithm.Ed25519;
        var hash = ComputeHash(packageDir);
        
        using var key = Key.Import(algorithm, Convert.FromBase64String(privateKey), KeyBlobFormat.RawPrivateKey);
        var signature = algorithm.Sign(key, hash);
        
        return Convert.ToBase64String(signature);
    }
    
    /// <summary>
    /// Verifies the digital signature of the package identified by the given hash.
    /// </summary>
    /// <param name="hash">The hash of the package.</param>
    /// <param name="signature">The latest signature of the package.</param>
    /// <param name="publicKey">The public key.</param>
    /// <returns>True if the signature is valid.</returns>
    public static bool Verify(byte[] hash, string signature, string publicKey)
    {
        var algorithm = SignatureAlgorithm.Ed25519;
        var key = PublicKey.Import(algorithm, Convert.FromBase64String(publicKey), KeyBlobFormat.RawPublicKey);
        return algorithm.Verify(key, hash, Convert.FromBase64String(signature));
    }
    
    /// <summary>
    /// Generates the hash for the given Feenix package directory.
    /// </summary>
    /// <param name="dir">The directory to the Feenix package.</param>
    /// <returns>The computed hash.</returns>
    public static byte[] ComputeHash(string dir)
    {
        using var md5 = MD5.Create();
        var hashBuilder = new StringBuilder();
        
        foreach (var file in Directory.GetFiles(dir, "*.lua", SearchOption.AllDirectories))
        {
            using var stream = File.OpenRead(file);
            var hash = md5.ComputeHash(stream);
            hashBuilder.Append(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());
        }
        
        using var sha512 = SHA512.Create();
        return sha512.ComputeHash(Encoding.UTF8.GetBytes(hashBuilder.ToString()));
    }
}