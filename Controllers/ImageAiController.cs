using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebProject.Controllers
{
    public class ImageAiController : Controller
    {
        [HttpPost]
        public IActionResult ProcessImage(IFormFile uploadedImage, string style)
        {
            if (uploadedImage == null || uploadedImage.Length == 0)
            {
                TempData["ErrorMessage"] = "Please upload a photo.";
                return RedirectToAction("Index");
            }

            try
            {

                using var memoryStream = new MemoryStream();
                uploadedImage.CopyTo(memoryStream);


                var client = new RestClient("https://hairstyle-changer.p.rapidapi.com/huoshan/facebody/hairstyle");
                var request = new RestRequest
                {
                    Method = Method.Post
                };

                request.AddHeader("x-rapidapi-key", "1b052648e8msh6db3509001e1afcp11dd19jsnadfbd057279b");
                request.AddHeader("x-rapidapi-host", "hairstyle-changer.p.rapidapi.com");
                request.AddHeader("Content-Type", "multipart/form-data");

                request.AddFile("image_target", memoryStream.ToArray(), uploadedImage.FileName);
                request.AddParameter("hair_type", style);

                RestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response.Content);
                    string base64Image = responseData?.data?.image;
                    ViewBag.ProcessedImage = $"data:image/png;base64,{base64Image}";
                }
                else
                {
                    TempData["ErrorMessage"] = "Image processing failed. Error: " + response.Content;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return Index();
        }

        private Stream imageBytes()
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            ViewBag.HairStyles = new Dictionary<int, string>
            {
            { 101, "Bangs (Default)" },
            { 201, "Long Hair" },
            { 301, "Bangs + Long Hair" },
            { 401, "Medium-Length Hair" },
            { 402, "Light Hair Enhancement" },
            { 403, "Intense Hair Enhancement" },
            { 502, "Light Curls" },
            { 503, "Intense Curls" },
            { 603, "Short Hair" },
            { 801, "Blonde Hair" },
            { 901, "Straight Hair" },
            { 1001, "Oil-Free Hair" },
            { 1101, "Hairline Filling" },
            { 1201, "Neat Hair" },
            { 1301, "Filling Hair Gaps" }

            };
            return View("Index");
        }
    }
}