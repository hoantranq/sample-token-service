using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Jose;
using Jose.keys;
using Microsoft.IdentityModel.Tokens;
using Sample.API.Dtos;
using Sample.API.Repositories;

namespace Sample.API.Services;

public class TokenService : ITokenService
{
    private readonly ICertificateRepository _certRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public TokenService(ICertificateRepository certRepo, IWebHostEnvironment webHostEnvironment)
    {
        _certRepo = certRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    #region Public methods
    public async Task<string> GetJweTokenAsync(JweRequest request)
    {
        // Payload
        var payload = new Dictionary<string, object>
        {
            { "issuer", "sample.api" },
            { "aud", request.client_id! },
        };

        // Certificate
        var certificate = await GetCertificate2(request.client_id!);

        // Singing using the server private key => Verify with the public key
        var serverCertificatePrivateKey = certificate.GetRSAPrivateKey();

        // Encrypting using the client public key => Decoding using the client private key
        var clientCertificatePublicKey = certificate.GetRSAPublicKey();

        var jws = JWT.Encode(payload, serverCertificatePrivateKey, JwsAlgorithm.RS512);
        var jwe = JWT.Encode(jws, clientCertificatePublicKey, JweAlgorithm.RSA_OAEP_256, JweEncryption.A256CBC_HS512);

        #region Test JWKS
        var jwk = GetJwks(certificate);


        try
        {
            var decodedToken = JWT.Decode(jwe, jwk);
        }
        catch (Exception ex)
        {

            throw;
        }
        #endregion

        return await Task.FromResult(jwe);
    }
    #endregion


    #region Private methods
    private async Task<X509Certificate2> GetCertificate2(string issuer)
    {
        X509Certificate2 certificate;

        var certificateCategory = await _certRepo.GetStoredCertificateInfo(issuer);

        if (certificateCategory != null)
        {
            var certRawData = certificateCategory.CertRawData!;

            certificate = new X509Certificate2(certRawData);
        }
        else
        {
            var crtFileName = await GetCertificatePath();

            certificate = new X509Certificate2(crtFileName, "", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.MachineKeySet);
        }

        return await Task.FromResult(certificate);
    }

    private async Task<string> GetCertificatePath()
    {
        var certPath = Path.Combine(_webHostEnvironment.ContentRootPath + "\\Certificates\\cert.pfx");

        return await Task.FromResult(certPath);
    }

    #endregion

    #region Helper methods
    private async Task ConvertPfxCertToPem(X509Certificate2 certificate)
    {


    }

    private Jwk GetJwks(X509Certificate2 certificate)
    {
        try
        {
            var key = certificate.GetRSAPrivateKey();
            Jwk jwk = new Jwk(key, isPrivate: false); //or 'true' by defaut

            return jwk;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion
}