namespace LinkVault.Models
{
    public class Linkentry
    {
        public string code { get; set; } = string.Empty;
        public string orginalurl { get; set;}=string.Empty;
        public DateTime CreatedAt { get; set; }
        public int hit;
    }
}
