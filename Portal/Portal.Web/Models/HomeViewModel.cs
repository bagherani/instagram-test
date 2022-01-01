
namespace Portal.Web.Models
{
    /// <summary>
    /// FB API call final result model
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// will be true if post successfuly done, otherwise false
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Text message that represent the FB API call result.
        /// </summary>
        public string Message { get; set; }
    }
}
