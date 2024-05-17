using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace birthday.Models
{
    [ModelMetadataType(typeof(z_metaUsers))]
    public partial class Users
    {
        
    }
}

public class z_metaUsers
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "學號")]
    public string? UserNo { get; set; }
    [Display(Name = "姓名")]
    public string? UserName { get; set; }
    [Display(Name = "祝福的話")]
    public string? Bless { get; set; }
}