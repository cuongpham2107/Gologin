using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace GoLogin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGEyODllMzdjYzAxMTZjYjcxMTJmYjUiLCJ0eXBlIjoiZGV2Iiwiand0aWQiOiI2NGEyYmJmMmM3Y2QwZDUwOTVhZTQxNzEifQ.bZDLKoIEAMg4ZOojttL70xvlpG4HZ51JcBZk9mCQ_NU";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            List<string> ArrayProfile = new List<string>();
            string? idProfile = null; // khởi tạo biến idProfile
            Console.WriteLine("Nhập số Profile cần tạo");
            int number_of_profiles = int.Parse(Console.ReadLine());
            string filePath = "ArrayProfile.txt"; // Đường dẫn và tên file
            for (int i = 0; i < number_of_profiles; i++)
            {
                
                string result = await CreateProfile(httpClient, token);
                Profile? profile = JsonConvert.DeserializeObject<Profile>(result);
                if(profile != null){
                    idProfile = profile.id; //gán giá trị cho idProfile phục vụ cho việc xoá Profile
                    
                    ArrayProfile.Add(profile.id);
                    //Start profile
                    await StartProfile(httpClient, idProfile);
                }
                await DeleteProfile(httpClient, idProfile, token);
                // Ghi mảng ArrayProfile vào file txt
                File.WriteAllLines(filePath, ArrayProfile);
                Console.WriteLine("Mảng đã được lưu vào file txt.");
            }
        //     while (true)
        //     {
               
        //         string? command = Console.ReadLine();
               
        //         if(command == "List"){
        //             await GetList(httpClient);
        //         }
        //         else if(command == "Create"){
        //             string result = await CreateProfile(httpClient, token);
        //             Profile? profile = JsonConvert.DeserializeObject<Profile>(result);
        //             if(profile != null){
        //                 idProfile = profile.id; //gán giá trị cho idProfile phục vụ cho việc xoá Profile
                       
        //                 ArrayProfile.Add(profile.id);
        //                 //Start profile
        //                 await StartProfile(httpClient, idProfile);
        //             }
        //         }
        //         else if(command == "Delete")
        //         {
        //             await DeleteProfile(httpClient, idProfile, token);
        //         }
        //         else if(command == "ArrayProfile"){
        //             string filePath = "ArrayProfile.txt"; // Đường dẫn và tên file
        //             // Ghi mảng ArrayProfile vào file txt
        //             File.WriteAllLines(filePath, ArrayProfile);
        //             Console.WriteLine("Mảng đã được lưu vào file txt.");
        //         }
        //     }
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
            var request = new HttpRequestMessage(HttpMethod.Delete, $"https://api.gologin.com/browser/{id}");
            request.Headers.Add("Authorization", $"Bearer {token}");
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }
        public static async Task StartProfile(HttpClient httpClient, string? id){
          
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

    }

}