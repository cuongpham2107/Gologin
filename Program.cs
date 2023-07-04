using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;
using Gologin;

namespace GoLogin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Comman comman = new Comman();
            List<string> ArrayProfile = new List<string>();
            string? idProfile = null;
            string filePath = "ArrayProfile.txt"; // Đường dẫn và tên file
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", comman.token);
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Number of Profiles >>> ");
                Console.ResetColor();
                int number_of_profiles = int.Parse(Console.ReadLine());
                for (int i = 0; i < number_of_profiles; i++)
                {
                    string result = await CreateProfile(httpClient, comman.token);
                    Profile? profile = JsonConvert.DeserializeObject<Profile>(result);
                    if (profile != null)
                    {
                        idProfile = profile.id;
                        ArrayProfile.Add(profile.id);
                        await StartProfile(httpClient, idProfile);
                    }
                    await DeleteProfile(httpClient, idProfile, comman.token);

                    File.WriteAllLines(filePath, ArrayProfile);
                    Console.WriteLine("Mảng IdProfile đã được lưu vào file txt.");
                }
                await StopChrome();
               
                ZipFile.CreateFromDirectory(comman.folderPath, GenerateRandomFile(comman.zipPath));

                DeleteFileProfileGoLogin(comman.folderPath);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Thư mục đã được nén thành công.");
                Console.ResetColor();
            }
        }
        public static async Task StopChrome()
        {
            // Tìm và tắt trình duyệt Google Chrome
            Process[] chromeProcesses = Process.GetProcessesByName("chrome");
            foreach (Process chromeProcess in chromeProcesses)
            {
                if (!chromeProcess.HasExited)
                {
                    await Task.Run(chromeProcess.CloseMainWindow);
                    chromeProcess.WaitForExit(2000); // Chờ 5 giây để trình duyệt tắt
                    if (!chromeProcess.HasExited)
                    {
                        await Task.Run(chromeProcess.Kill); // Khi không tắt được, sử dụng phương thức Kill để đóng trình duyệt
                    }
                }
            }
        }
        public static async Task GetList(HttpClient httpClient){
            HttpResponseMessage response = await httpClient.GetAsync("https://api.gologin.com/browser/v2");
            if(response.ReasonPhrase == "OK"){
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Lỗi: " + response.StatusCode);
            }
        }
        public static async Task<string> CreateProfile(HttpClient httpClient, string token){
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.gologin.com/browser");
                request.Headers.Add("Authorization", $"Bearer {token}");
                // request.Headers.Add("Content-type", "application/json");
                var content = new StringContent("{\r\n    \"name\": \"Test\",\r\n    \"notes\": \"string\",\r\n    \"browserType\": \"chrome\",\r\n    \"os\": \"lin\",\r\n    \"startUrl\": \"string\",\r\n    \"googleServicesEnabled\": false,\r\n    \"lockEnabled\": false,\r\n    \"debugMode\": false,\r\n    \"navigator\": {\r\n        \"userAgent\": \"string\",\r\n        \"resolution\": \"string\",\r\n        \"language\": \"string\",\r\n        \"platform\": \"string\",\r\n        \"doNotTrack\": false,\r\n        \"hardwareConcurrency\": 0,\r\n        \"deviceMemory\": 1,\r\n        \"maxTouchPoints\": 0\r\n    },\r\n    \"geoProxyInfo\": {},\r\n    \"storage\": {\r\n        \"local\": true,\r\n        \"extensions\": true,\r\n        \"bookmarks\": true,\r\n        \"history\": true,\r\n        \"passwords\": true,\r\n        \"session\": true\r\n    },\r\n    \"proxyEnabled\": false,\r\n    \"proxy\": {\r\n       \"id\": \"64a2daa056449f5566a24bda\", \"mode\": \"geolocation\",\r\n        \"host\": \"geo-dc.floppydata.com\",\r\n        \"port\": 10080,\r\n        \"autoProxyRegion\": \"us\",\r\n        \"torProxyRegion\": \"us\",\r\n        \"username\": \"bPKi2WJU5kQJVqM1\",\r\n        \"password\": \"+wy7YS36eVrCJRk2\"\r\n    },\r\n    \"dns\": \"string\",\r\n    \"plugins\": {\r\n        \"enableVulnerable\": true,\r\n        \"enableFlash\": true\r\n    },\r\n    \"timezone\": {\r\n        \"enabled\": true,\r\n        \"fillBasedOnIp\": true,\r\n        \"timezone\": \"string\"\r\n    },\r\n    \"audioContext\": {\r\n        \"mode\": \"off\",\r\n        \"noise\": 0\r\n    },\r\n    \"canvas\": {\r\n        \"mode\": \"off\",\r\n        \"noise\": 0\r\n    },\r\n    \"fonts\": {\r\n        \"families\": [\r\n            \"string\"\r\n        ],\r\n        \"enableMasking\": true,\r\n        \"enableDomRect\": true\r\n    },\r\n    \"mediaDevices\": {\r\n        \"videoInputs\": 0,\r\n        \"audioInputs\": 0,\r\n        \"audioOutputs\": 0,\r\n        \"enableMasking\": false\r\n    },\r\n    \"webRTC\": {\r\n        \"mode\": \"alerted\",\r\n        \"enabled\": true,\r\n        \"customize\": true,\r\n        \"localIpMasking\": false,\r\n        \"fillBasedOnIp\": true,\r\n        \"publicIp\": \"string\",\r\n        \"localIps\": [\r\n            \"string\"\r\n        ]\r\n    },\r\n    \"webGL\": {\r\n        \"mode\": \"noise\",\r\n        \"getClientRectsNoise\": 0,\r\n        \"noise\": 0\r\n    },\r\n    \"clientRects\": {\r\n        \"mode\": \"noise\",\r\n        \"noise\": 0\r\n    },\r\n    \"webGLMetadata\": {\r\n        \"mode\": \"mask\",\r\n        \"vendor\": \"string\",\r\n        \"renderer\": \"string\"\r\n    },\r\n    \"webglParams\": [],\r\n    \"profile\": \"string\",\r\n    \"googleClientId\": \"string\",\r\n    \"updateExtensions\": true,\r\n    \"chromeExtensions\": [\r\n        \"string\"\r\n    ]\r\n}",Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                // Đọc nội dung phản hồi như là một chuỗi
                string responseBody = await response.Content.ReadAsStringAsync();
                // Trả về nội dung phản hồi
                return responseBody;

            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public static async Task DeleteProfile(HttpClient httpClient, string? id,string token){

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://api.gologin.com/browser/{id}");
                request.Headers.Add("Authorization", $"Bearer {token}");
                var content = new StringContent(string.Empty);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

        }
        public static async Task StartProfile(HttpClient httpClient, string? id){
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:36912/browser/start-profile");
                var content = new StringContent(@"
                                    {
                                        ""profileId"": """ + id + @""",
                                        ""sync"": true}"
                                    , null, "application/json");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
           
        }
        public static void DeleteFileProfileGoLogin(string folderPath)
        {
            DirectoryInfo rootDirectory = new DirectoryInfo(folderPath);

            Parallel.ForEach(rootDirectory.GetDirectories(), (subDirectory) =>
            {
                ClearDirectory(subDirectory.FullName);
                subDirectory.Delete();
            });
        }

        public static void ClearDirectory(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            Parallel.ForEach(directory.GetFiles(), (file) =>
            {
                file.Delete();
            });

            Parallel.ForEach(directory.GetDirectories(), (subDirectory) =>
            {
                ClearDirectory(subDirectory.FullName);
                subDirectory.Delete();
            });
        }
        public static string GenerateRandomFile(string zipPath, int length = 5)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string appendedString =  new string(stringChars);

            // Tách tên file và phần mở rộng
            string fileName = Path.GetFileNameWithoutExtension(zipPath);

            string fileExtension = Path.GetExtension(zipPath);

            string newFileName = $"{fileName}-{appendedString}{fileExtension}";

            string newFilePath = Path.Combine(Path.GetDirectoryName(zipPath), newFileName);

            return newFilePath;
        }

    }

}