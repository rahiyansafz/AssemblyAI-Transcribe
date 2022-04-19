using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAITranscribe.Processor;

internal class App
{
    static string API_Key = "15b3b6ce5290439286100190e09f35b2";
    public static void Run()
    {
        Console.WriteLine("Hello, World!");

        HttpClient client = new();

        client.BaseAddress = new Uri("https://api.assemblyai.com/v2/");
        client.DefaultRequestHeaders.Add("authorization", API_Key);

        string jsonResult = SendFile(client, @"E:\Downloads HDD\lana-diet-mountain.mp3").Result;
        Console.WriteLine(jsonResult);
    }

    public static async Task<string> SendFile(HttpClient client, string filePath)
    {
        try
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "upload");
            httpRequest.Headers.Add("Transfer-Encoding", "chunked");

            var fileReader = File.OpenRead(filePath);
            var streamContent = new StreamContent(fileReader);

            httpRequest.Content = streamContent;

            HttpResponseMessage responseMessage = await client.SendAsync(httpRequest);

            return await responseMessage.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            throw;
        }
    }
}
