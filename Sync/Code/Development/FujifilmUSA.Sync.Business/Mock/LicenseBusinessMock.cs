using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class LicenseBusinessMock : ILicenseBusiness
    {
        #region IDeviceBusiness Members

        public List<License> GetLicenses()
        {
            return new List<License>();
        }

        public int InsertLicense(License license)
        {
            return 0;
        }

        public int UpdateLicense(License license)
        {
            return 0;
        }

        public bool DeleteLicense(int licenseId)
        {
            return false;
        }

        public int InsertLicenseType(LicenseType licenseType)
        {
            return 0;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
