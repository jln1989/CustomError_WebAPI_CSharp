using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace WebAPIConsole
{
    /// <summary>
    /// Department Class
    /// </summary>
    public class Department
    {

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
    /// <summary>
    /// Sample Program by Lakshmi to test the Web API
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                PostTest();
            }
            catch (Exception)
            {

                throw;
            }
            Console.ReadKey();
        }


        /// <summary>
        /// Gets the test.
        /// </summary>
        public static void GetTest()
        {
            using (var client = new HttpClient())
            {
                var department = new Department() { DepartmentId = 200, DepartmentName = "TTT" };
                var json = JsonConvert.SerializeObject(department);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("http://localhost:9198/api/employee/");
                var getTask = client.GetAsync(client.BaseAddress);
                //var postTask = client.PostAsync("", client.PostAsync<Department>("student", student);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var outResult = readTask.Result;
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
        }

        /// <summary>
        /// Posts the test.
        /// </summary>
        public static void PostTest()
        {
            using (var client = new HttpClient())
            {
                var department = new Department() { DepartmentId = 555, DepartmentName = "TTT" };
                var json = JsonConvert.SerializeObject(department);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("http://localhost:9198/api/employee/");
                //client.BaseAddress = new Uri("http://localhost:24367/api/employee/");
                
                var postTask = client.PostAsync(client.BaseAddress + "AddDepartment", null);
                //var postTask = client.PostAsync("", client.PostAsync<Department>("student", student);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var insertedDepartment = readTask.Result;
                }
                else
                {
                    var sss = result.Content.ReadAsStringAsync().Result;
                    var oo = JsonConvert.DeserializeObject<CustomErrorClass>(sss);
                    Console.WriteLine(result.Content.ReadAsStringAsync().Result);
                }
            }
        }

    }
}

