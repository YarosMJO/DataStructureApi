using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static HttpClient client = new HttpClient();

        static async Task<List<T>> GetAsync<T>(string path)
        {
            List<T> data = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<List<T>>();
            }
            return data;
        }
        static async Task RunAsync()
        {

            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                List<User> Users = await GetAsync<User>("https://5b128555d50a5c0014ef1204.mockapi.io/users");
                List<Todo> Todos = await GetAsync<Todo>("https://5b128555d50a5c0014ef1204.mockapi.io/todos");
                List<Post> Posts = await GetAsync<Post>("https://5b128555d50a5c0014ef1204.mockapi.io/posts");
                List<Comment> Comments = await GetAsync<Comment>("https://5b128555d50a5c0014ef1204.mockapi.io/comments");

                var PostsWithComments = from p in Posts
                                        join c in Comments on p.id equals c.postId
                                        select (Posts: p.id, p.createdAt, p.title, p.body, p.userId, p.likes, p.Comments = Comments);

                List<User> result = (from u in Users
                                     join p in PostsWithComments on u.id equals p.userId
                                     join t in Todos on u.id equals t.userId
                                     select new User
                                     {
                                         id = u.id,
                                         createdAt = u.createdAt,
                                         name = u.name,
                                         avatar = u.avatar,
                                         email = u.email,
                                         Posts = u.Posts = Posts,
                                         Todos = u.Todos = Todos
                                     }).ToList();

                Menu menu = new Menu(new Service(result));
                menu.ShowMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            Console.ReadLine();
        }

    }

}