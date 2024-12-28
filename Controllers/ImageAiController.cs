using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebProject.Controllers
{
    public class ImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // عرض الصفحة لتحميل الصورة
        public IActionResult UploadImage()
        {
            return View();
        }

        // استقبال الصورة من المستخدم
        [HttpPost]
        public async Task<IActionResult> ProcessImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            // تحميل الصورة إلى الذاكرة
            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                var imageBytes = stream.ToArray();

                try
                {
                    // إرسال الصورة إلى خدمة الذكاء الاصطناعي
                    var resultImage = await ProcessWithAI(imageBytes);

                    // إرسال الصورة المعدلة إلى الواجهة الأمامية
                    return File(resultImage, "image/jpeg");
                }
                catch (Exception ex)
                {
                    // إذا حدث خطأ أثناء معالجة الصورة
                    return BadRequest($"Error: {ex.Message}");
                }
            }
        }

        private async Task<byte[]> ProcessWithAI(byte[] imageBytes)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            // إعداد الطلب (multipart/form-data)
            var content = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(imageBytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            content.Add(imageContent, "image", "uploaded-image.jpg");

            // إعداد الهيدر الخاص بـ RapidAPI
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "7616cc0407msh7180835d03f9371p180522jsnf4d939332427"); // المفتاح الخاص بك
            httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "hairstyle-changer.p.rapidapi.com");

            // إرسال الطلب إلى API
            var response = await httpClient.PostAsync("https://rapidapi.com/ailabapi-ailabapi-default/api/hairstyle-changer/playground/apiendpoint_98389214-31c8-41d7-9024-3a3b3f539acb", content);
            if (response.IsSuccessStatusCode)
            {
                // قراءة الصورة المعدلة كـ مصفوفة بايت
                return await response.Content.ReadAsByteArrayAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error processing the image. Status Code: {response.StatusCode}, Details: {errorContent}");
            }
        }
    }
}