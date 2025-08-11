using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace keeper.Models;

public class Keep : RepoItem<int>
{
    public string CreatorId { get; set; }
    [MaxLength(255)] public string Name { get; set; }
    [MaxLength(1000)] public string Description { get; set; }
    [Url] public string Img { get; set; }
    public int Views { get; set; }
    public int Kept { get; set; }
    public Account Creator { get; set; }
}