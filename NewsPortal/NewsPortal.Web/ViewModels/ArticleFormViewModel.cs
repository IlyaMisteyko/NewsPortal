using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Web.ViewModels
{
    public class ArticleFormViewModel
    {
        public int ArticleId { get; set; }

        [Display(Name = "Upload image")]
        public HttpPostedFileBase File { get; set; }

        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Fill in this field")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Fill in this field")]
        [Display(Name = "Description")]
        public string Description{ get; set; }

        [Required(ErrorMessage = "Choose the category")]
        [Display(Name = "Category")]
        public string Category { get; set; }
        public string UserId { get; set; }
        public SelectList CategoryTypes { get; }

        public ArticleFormViewModel()
        {
            CategoryTypes = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Technology", Value = "Technology" },
                    new SelectListItem { Text = "Sport", Value = "Sport" },
                    new SelectListItem { Text = "Society", Value = "Society" },
                    new SelectListItem { Text = "Politic", Value = "Politic" },
                    new SelectListItem { Text = "Business", Value = "Business" }
                }, "Value", "Text");
        }
    }
}