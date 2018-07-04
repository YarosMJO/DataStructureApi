using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    class Service
    {
        public List<User> Result { get; private set; }
        public Service(List<User> result)
        {
            Result = result;
        }

        public Dictionary<Post, int?> GetCommentsCount(int id)
        {
            List<Post> posts = Result.Find(y => y.id.Equals(id))?.Posts;
            Dictionary<Post, int?> CommentsCount = new Dictionary<Post, int?>();
            posts?.ForEach(x => CommentsCount?.Add(x, x?.Comments?.Count));
            return CommentsCount;
        }
        public List<Comment> GetComments(int id)
        {
            List<Post> posts = Result.Find(y => y?.id == id)?.Posts;
            List<Comment> Сomments = new List<Comment>();
            posts?.ForEach(x => x?.Comments?.ForEach(y => Сomments?.Add(y)));
            Сomments = (from com in Сomments where com.body.Length < 50 select com).ToList();
            return Сomments;
        }
        public Dictionary<int?, string> GetTodos(int id)
        {
            List<Todo> Todos = Result.Find(y => y?.id == id)?.Todos.Where(x => x?.isComplete == true).ToList();
            Dictionary<int?, string> TodosNames = new Dictionary<int?, string>();
            Todos?.ForEach(x => TodosNames.Add(x?.id, x?.name));
            return TodosNames;
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            users = Result.OrderBy(x => x.name).ToList();
            users?.ForEach(x => x.Todos.Sort(new Comparer()));
            return users;
        }
        public PostStruct GetPostStructure(int id)
        {
            var user = Result.FirstOrDefault(y => y.id == id);

            var latestPost = user?.Posts?.FirstOrDefault(x => x?.createdAt == user?.Posts?.Max(y => y?.createdAt));
            var postsCount = latestPost?.Comments?.Count();
            var unTodo = user?.Todos?.Where(x => x?.isComplete != true)?.Count();
            var bestCommented = user?.Posts?.FirstOrDefault(x => x?.Comments?.Count > 80);
            var bestLiked = user?.Posts?.Where(x => x?.likes == user?.Posts.Max(y => y?.likes))?.First();

            return new PostStruct()
            {
                latestPost = latestPost ?? null,
                postsCount = postsCount ?? 0,
                unTodo = unTodo ?? 0,
                bestCommented = bestCommented ?? null,
                bestLiked = bestLiked??null
            };
        }

        public CommentStruct GetCommentStructure(int postId)
        {
            var posts = Result.SelectMany(x => x?.Posts).ToList();
            var comments = posts.Find(x => x?.id == postId)?.Comments;

            var longestComment = comments?.Find(x => x?.body?.Length == comments?.Max(y => y?.body?.Length));
            var mostLikedComment = comments?.Find(x => x?.likes == comments?.Max(y => y?.likes));
            var commentsCount = comments?.Where(x => x?.likes == 0 || x?.body?.Length < 80)?.ToList()?.Count;

            return new CommentStruct
            {
                longestComment = longestComment,
                mostLikedComment = mostLikedComment,
                commentsCount = commentsCount
            };
        }

        public class Comparer : IComparer<Todo>
        {
            public int Compare(Todo x, Todo y)
            {
                if (x.name.Length < y.name.Length)
                    return 1;
                else if (x.name.Length > y.name.Length)
                    return -1;
                else return 0;
            }
        }
    }
}

