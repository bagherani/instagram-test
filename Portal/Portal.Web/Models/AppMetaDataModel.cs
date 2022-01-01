
namespace Portal.Web.Models
{
    /// <summary>
    /// Model to parse AppMetaData section in the appsettings.json
    /// </summary>
    public class AppMetaDataModel
    {
        /// <summary>
        /// Default facebook api url
        /// </summary>
        public string APIEndpoint { get; set; }

        /// <summary>
        /// IG-UserID that should be placed withing requests URLs
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// User generated AccessToken that should be included within requests headers
        /// </summary>
        public string AccessToken { get; set; }
    }
}
