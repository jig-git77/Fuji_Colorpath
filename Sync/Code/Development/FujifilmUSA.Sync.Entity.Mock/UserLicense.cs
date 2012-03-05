namespace FujifilmUSA.Sync.Entity.Mock
{
    public class UserLicense
    {
        public int UserLicenseId { get; set; }
        public int UserId { get; set; }
        public int LicenseId { get; set; }
        public License License { get; set; }
        public User User { get; set; }
    }
}