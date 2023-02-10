using System.ComponentModel.DataAnnotations;

namespace NS_App.Models;

public class FileViewModel
{
    public string Id { get; set; }
    [Display(Name = "File Name")]
    public string Name { get; set; }
    public string Description { get; set; }

    public double Size { get; set; }
    [Display(Name = "Created")]
    public DateTime CreationDate { get; set; }
}
