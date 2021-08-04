using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NewsPortal.Web.ViewModels
{
    public class ImageUploadViewModel
    {
        [Display(Name = "Upload image")]
        public HttpPostedFileBase File { get; set; }

        public string ImageUrl { get; set; }
    }
}