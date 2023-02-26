namespace Sample.API.Models;

public class CertificateCategory
{
    public int Id { get; set; }
    public string? Issuer { get; set; }
    public byte[]? CertPublicKeyRaw { get; set; }
    public byte[]? CertPrivateKeyRaw { get; set; }
    public byte[]? CertRawData { get; set; }
    public DateTime ExpireTime { get; set; }
    public bool IsRevoked { get; set; }
}