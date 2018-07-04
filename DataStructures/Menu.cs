using System;
using System.Collections.Generic;

namespace DataStructures
{
     class Menu
    {
        private int id = 0;
        public Service Service { get;  set; }
        public Menu(Service service)
        {
            Service = service;
        }
        public void ShowCommentsCount(int id)
        {
            Dictionary<Post, int?> newDict = Service.GetCommentsCount(id)??null;
            if (newDict.Count == 0)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            foreach (KeyValuePair<Post, int?> keyValue in newDict)
            {
                Console.WriteLine($"\"{keyValue.Key.title}\" - {keyValue.Value ?? 0} comments");
            }
        }
        public void ShowGetComments(int id)
        {
            Console.WriteLine("[User's comments]:");
            List<Comment> CommentList = Service.GetComments(id);
            if (CommentList.Count == 0)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            CommentList?.ForEach(x => Console.WriteLine($"\"{x?.body}\""));
        }

        public void ShowGetTodos(int id)
        {
            Dictionary<int?, string> GetTodos = Service.GetTodos(id);
            if (GetTodos.Count == 0)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            Console.WriteLine("[Completed tasks]: id - name");
            foreach (KeyValuePair<int?, string> keyValue in GetTodos)
            {
                Console.WriteLine($"{keyValue.Key} - \"{keyValue.Value}\"");
            }
        }

        public void ShowGetUsers()
        {
            List<User> GetUsers = Service.GetUsers();
            if (GetUsers.Count == 0)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            Console.WriteLine("<User name>: <todo's title> (<length>)");
            GetUsers.ForEach(x => x.Todos.ForEach(y => Console.WriteLine($"{x.name}: {y.name} ({y.name.Length} symbols)")));
        }

        public void ShowGetPostStructure(int id)
        {
            PostStruct ps = Service.GetPostStructure(id);
            if (ps == null)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            Console.WriteLine($"Latest post is \"{ps?.latestPost?.title}\" ({ps?.latestPost?.createdAt})");
            Console.WriteLine($"Post's comment count: {ps?.postsCount}");
            Console.WriteLine($"Count of unfulfilled tasks for the user: {ps?.unTodo}");
            Console.WriteLine($"Most commented post: \"{ps?.bestCommented?.title}\"");
            Console.WriteLine($"Most liked post: \"{ps?.bestLiked?.title}\"");
        }

        public void ShowGetCommentStructure(int id)
        {
            CommentStruct cs = Service.GetCommentStructure(id);
            if (cs == null)
            {
                Console.WriteLine("No comments in such user...");
                return;
            }
            Console.WriteLine($"Longest comment is \"{cs?.longestComment?.body}\" ({cs?.longestComment?.body?.Length} symbols)");
            Console.WriteLine($"Most liked comment is \"{cs?.mostLikedComment?.body}\" ({cs?.mostLikedComment?.likes} likes)");
            Console.WriteLine($"Count of comments on condition: {cs?.commentsCount} ");
        }

        public void ShowMenu()
        {
            string buf;
            while (true)
            {
                PrintMenuTemplate();
                buf = Console.ReadLine();
                Console.Clear();
                switch (buf)
                {
                    case "1":
                        ParseId();
                        ShowCommentsCount(id);
                        break;
                    case "2":
                        ParseId();
                        ShowGetComments(id);
                        break;
                    case "3":
                        ParseId();
                        ShowGetTodos(id);
                        break;
                    case "4":
                        ShowGetUsers();
                        break;
                    case "5":
                        ParseId();
                        ShowGetPostStructure(id);
                        break;
                    case "6":
                        ParseId();
                        ShowGetCommentStructure(id);
                        break;
                    case "7":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Please enter only existing menu option numbers!");
                        break;
                }
            }
        }
        private static void PrintMenuTemplate()
        {
            Console.WriteLine("┌───────────────────────────────────┐");
            Console.WriteLine("│1)Show comments count              │");
            Console.WriteLine("│2)Show comments                    │");
            Console.WriteLine("│3)Show todos                       │");
            Console.WriteLine("│4)Show users                       │");
            Console.WriteLine("│5)Show post structure              │");
            Console.WriteLine("│6)Show comment structure           │");
            Console.WriteLine("│7)Exit                             │");
            Console.WriteLine("└───────────────────────────────────┘");
        }
        public void ParseId()
        {
            Console.WriteLine("Please enter user id: ");
            while (!Int32.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Enter valid id please: ");
            };
            return;
        }
    }

}
