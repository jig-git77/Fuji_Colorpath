using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ILicenseBusiness : ITaskBase
    {
        List<License> GetLicense(int licenseId,int currentUserId);
        int InsertLicense(License license);
        int UpdateLicense(License license);
        bool DeleteLicense(int licenseId);
        int InsertLicenseType(LicenseType licenseType);
    }
}
