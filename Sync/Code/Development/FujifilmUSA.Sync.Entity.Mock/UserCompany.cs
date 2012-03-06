namespace FujifilmUSA.Sync.Entity
{
    public class UserCompany
    {
        public int UserCompanyId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public User User { get; set; }
    }
}