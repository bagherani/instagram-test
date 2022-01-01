
namespace Portal.Web.Models
{
    /// <summary>
    /// Post to be uploaded on the Instagram.
    /// </summary>
    public class HomePostedModel
    {
        /// <summary>
        /// Image URL that already uploaded on a server with public url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Comment text on the post
        /// </summary>
        public string Comment { get; set; }
    }
}
