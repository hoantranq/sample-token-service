using Microsoft.EntityFrameworkCore;
using Sample.API.Context;
using Sample.API.Models;

namespace Sample.API.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly AppDbContext _context;

    public CertificateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CertificateCategory> GetStoredCertificateInfo(string issuer)
    {
        // Get certificate raw data from database
        var certRawData = await _context.CertificateCategories
            .SingleOrDefaultAsync(x => x.Issuer == issuer && x.IsRevoked == false && x.ExpireTime > DateTime.Now);

        return await Task.FromResult(certRawData ?? null!);
    }
}