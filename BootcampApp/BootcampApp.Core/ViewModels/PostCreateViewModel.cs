using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace BootcampApp.Core.ViewModels
{
    public class PostCreateViewModel{

        public int PostId { get; set; }
        
        [Required]
        [Display(Name = "Başlık")]
        public string? Title {get;set;}
 
        [Required]
        [Display(Name = "İçerik")]
        public string? Content {get;set;}

        [Required]
        [Display(Name = "Url")]
        public string? Url {get;set;}
        public IFormFile? Picture {get;set;}

        public bool IsActive {get;set;}
        public List<Category> Categories {get;set;} = new();

    }
}