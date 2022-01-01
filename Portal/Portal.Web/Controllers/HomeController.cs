using Microsoft.AspNetCore.Mvc;
using Portal.Web.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Portal.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// App configs obtained from appsettings.json
        /// </summary>
        private readonly AppMetaDataModel _appMeta;

        public HomeController(AppMetaDataModel appMeta)
        {
            // store injected appMeta parameter
            _appMeta = appMeta;
        }

        /// <summary>
        /// Home page entry
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Process user form submittion request and try to post the image to instagram through FB API
        /// </summary>
        /// <param name="postedModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(HomePostedModel postedModel)
        {
            HomeViewModel model = new();

            try
            {
                var media = await PostMediaToFBServer(postedModel);

                if (string.IsNullOrEmpty(media.ID))
                    throw new Exception("Media ID is null!");

                var publishResult = await PublishMedia(media.ID);

                if (string.IsNullOrEmpty(publishResult.ID))
                    throw new Exception("Media publish failed!");

                model.Success = true;
                model.Message = $"Image successfuly posted with id= {publishResult.ID}";
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Message = ex.Message;
            }

            return View(model);
        }

        /// <summary>
        /// Send image media with a caption to FB API and get the media id
        /// </summary>
        /// <param name="post">contains imageUrl and post comment</param>
        /// <returns>id within MediaContainerModel</returns>
        private async Task<MediaContainerModel> PostMediaToFBServer(HomePostedModel post)
        {
            var mediaEndPoint = $"{_appMeta.APIEndpoint}/{_appMeta.UserID}/media?image_url={HttpUtility.UrlEncode(post.ImageUrl)}&caption={HttpUtility.HtmlEncode(post.Comment)}";

            using var httpReq = new HttpClient();
            var postContent = new StringContent(string.Empty);
            postContent.Headers.Add("access_token", _appMeta.AccessToken);

            var response = await httpReq.PostAsync(mediaEndPoint, postContent);
            var containerResponseData = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<MediaContainerModel>(containerResponseData);

                default:
                    var errorMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseErrorModel>(containerResponseData);
                    throw new Exception(errorMessage.Error.Message);
            }
        }

        /// <summary>
        /// Publish image media to instagram
        /// </summary>
        /// <param name="mediaId">media id obtained from <see cref="PostMediaToFBServer"/></param>
        /// <returns>post id withing MediaPublishModel</returns>
        private async Task<PublishResultModel> PublishMedia(string mediaId)
        {
            var mediaEndPoint = $"{_appMeta.APIEndpoint}/{_appMeta.UserID}/media_publish?creation_id={mediaId}";

            using var httpReq = new HttpClient();
            var postContent = new StringContent(string.Empty);
            postContent.Headers.Add("access_token", _appMeta.AccessToken);

            var response = await httpReq.PostAsync(mediaEndPoint, postContent);
            var publishResponseData = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<PublishResultModel>(publishResponseData);

                default:
                    var errorMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseErrorModel>(publishResponseData);
                    throw new Exception(errorMessage.Error.Message);
            }
        }

    }
}
