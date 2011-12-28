namespace FujifilmUSA.Sync.Entity
{
    public class ProfilerOptionDetail
    {
       public int ProfilerOptionDetailId { get; set;}
       public int ProfilerOptionId { get; set;}
       public string Key { get; set;}
       public string Value { get; set;}
       public ProfileType ProfilerType { get; set; }
    }
}
