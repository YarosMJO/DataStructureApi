using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class User
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }
        public string email { get; set; }
        public List<Post> Posts { get; set; }
        public List<Todo> Todos { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public int userId { get; set; }
        public int likes { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string body { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }
        public int likes { get; set; }
    }

    public class Todo : IComparable
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public bool isComplete { get; set; }
        public int userId { get; set; }

        public int CompareTo(object obj)
        {
            return name.Length.CompareTo(obj.ToString().Length);
        }
    }

    public class PostStruct
    {
        public Post latestPost { get; set; }
        public int? postsCount { get; set; }
        public int? unTodo { get; set; }
        public Post bestCommented { get; set; }
        public Post bestLiked { get; set; }
    }

    public class CommentStruct
    {
        public Comment longestComment { get; set; }
        public Comment mostLikedComment { get; set; }
        public int? commentsCount { get; set; } 
    }
}

