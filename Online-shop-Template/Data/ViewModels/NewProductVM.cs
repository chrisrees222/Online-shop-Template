using Online_shop_Template.Data.Base;
using Online_shop_Template.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_shop_Template.Models
{
    public class NewProductVM 
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Product start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Product end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        //[Display(Name = "Select a category")]
        //[Required(ErrorMessage = "Movie category is required")]
        //public MovieCatergory MovieCatergory { get; set; }

       

    }
}
