namespace HrApp.MVC.Helpers
{
    public static class ImageConversions
    {
        public static async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        }

        public static async Task<string> ConvertToIFormFileAsync(byte[] fileBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                var base64 = Convert.ToBase64String(fileBytes);

                return string.Format("data:image/jpg;base64,{0}", base64);
            }
        }
    }
}
