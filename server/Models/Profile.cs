using System.ComponentModel.DataAnnotations;

namespace keeper.Models;

public class Profile : RepoItem<string>
{
    public string Name { get; set; }
    public string Picture { get; set; }
    [Url] public string CoverImg { get; set; }
}