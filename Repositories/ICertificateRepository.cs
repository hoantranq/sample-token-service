using Sample.API.Models;

namespace Sample.API.Repositories;

public interface ICertificateRepository
{
    Task<CertificateCategory> GetStoredCertificateInfo(string issuer);
}