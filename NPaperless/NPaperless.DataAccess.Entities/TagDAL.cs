using System.ComponentModel.DataAnnotations;

namespace NPaperless.DataAccess.Entities{

    public class TagDAL {
        [Key]
        public long Id { get; set; }
        public string? Slug { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Match { get; set; }
        public long MatchingAlgorithm { get; set; }
        public bool IsInsensitive { get; set; }
        public bool IsInboxTag { get; set; }
    }

}