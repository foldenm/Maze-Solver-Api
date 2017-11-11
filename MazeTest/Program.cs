using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MazeTest
{
    public class Program
    {
        private class MazeApiResponseModel
        {
            public int steps { get; set; }
            public string solution { get; set; }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: MazeTest <path to maze file>");
                }
                else
                {
                    var fn = args[0];

                    Console.WriteLine($"Loading maze file '{fn}'");
                    var mazeData = File.ReadAllText(fn);
                    if (string.IsNullOrEmpty(mazeData))
                    {
                        throw new Exception("Invalid maze datafile.");
                    }

                    using (var client = new HttpClient())
                    {
                        try
                        {
                            client.BaseAddress = new Uri("http://localhost:8080/");
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = Task.Run(() => client.PostAsJsonAsync("MazeSolver/solveMaze", mazeData))
                                .Result;
                            Console.WriteLine($"HTTP {response.StatusCode}");

                            var json = response.Content.ReadAsStringAsync().Result;
                            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<MazeApiResponseModel>(json);
                            if (model.steps > 0)
                            {
                                Console.WriteLine($"Solution found in {model.steps} steps\r\n{model.solution}");
                            }
                            else
                            {
                                Console.WriteLine("No solution found");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected exception: {ex.Message}\r\n{ex.StackTrace}");
            }
        }

    }
}
