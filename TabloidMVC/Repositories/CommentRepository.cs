using TabloidMVC.Models;
using TabloidMVC.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;



namespace TabloidMVC.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public CommentRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Comments> GetCommentsByPost(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT co.Id as CommentId, co.PostId as CommentPostId, p.Title AS PostTitle,
                co.UserProfileId, co.Subject as CommentSubject,
                co.Content as CommentContent, co.CreateDateTime as CommentDate, u.DisplayName AS DisplayName,
                u.Id as UserProfileId, u.CreateDateTime, u.ImageLocation as AvatarImage,
                u.UserTypeId
                FROM Comment co
                JOIN UserProfile u ON co.UserProfileId = u.Id
                JOIN Post p ON p.Id = co.PostId
                WHERE co.PostId = @postId";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    var reader = cmd.ExecuteReader();
                    var comments = new List<Comments>();
                    while (reader.Read())
                    {
                        var comment = new Comments
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("CommentId")),
                            PostId = reader.GetInt32(reader.GetOrdinal("CommentPostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("CommentSubject")),
                            Content = reader.GetString(reader.GetOrdinal("CommentContent")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CommentDate")),
                            UserProfile = new UserProfile
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            }
                        };
                        comments.Add(comment);
                    }
                    reader.Close();
                    return comments;
                }
            }
        }




        //public Comments GetCommentById(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"Select c.Id, c.Subject, c.UserProfileId, c.Content, c.CreateDateTime, 
        //              FROM Comment c
        //              LEFT JOIN Post p ON p.PostId = p.Id
        //            Where c.Id = @id";

        //            cmd.Parameters.AddWithValue("id", id);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            Comments comment = null;
        //            while (reader.Read())
        //            {
        //                comment = new Comments
        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    Subject = reader.GetString(reader.GetOrdinal("Subject")),
        //                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
        //                    Content = reader.GetString(reader.GetOrdinal("Content")),
        //                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
        //                };
        //            }
        //            reader.Close();
        //            return comment;
        //        }
        //    }
        //}
        //public Comments GetCommentByPost(int postId)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"Select c.Id, c.Subject, c.UserProfileId, c.Content, c.CreateDateTime, 
        //              FROM Comment c
        //              LEFT JOIN Post p ON p.PostId = p.Id
        //            Where p.PostId = @postId";
        //            cmd.Parameters.AddWithValue("id", postId);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.Read())

        //            {
        //                Comments comment = new Comments()

        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
        //                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
        //                    Subject = reader.IsDBNull(reader.GetOrdinal("Subject"))
        //                                ? null
        //                                : reader.GetString(reader.GetOrdinal("Subject")),

        //                    Content = reader.IsDBNull(reader.GetOrdinal("Content"))
        //                                    ? null
        //                                   : reader.GetString(reader.GetOrdinal("Content")),
        //                };

        //                reader.Close();
        //                return comment;


        //            };

        //            reader.Close();
        //            return null;
        //        }
        //    }
        //}

        //public List<Comments> GetCommentsByPostId(int postId)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //        SELECT Id, Subject, UserProfileId, content, CreateDateTime
        //        FROM Comment
        //        WHERE PostId = @postId
        //    ";

        //            cmd.Parameters.AddWithValue("@postId", postId);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            List<Comments> comments = new List<Comments>();

        //            while (reader.Read())
        //            {
        //                Comments comment = new Comments()
        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    Subject = reader.GetString(reader.GetOrdinal("Subject")),
        //                    UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
        //                    Content = reader.GetString(reader.GetOrdinal("Content")),
        //                    CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
        //                };

        //                // Check if optional columns are null
        //                if (reader.IsDBNull(reader.GetOrdinal("Subject")) == false)
        //                {
        //                    comment.Subject = reader.GetString(reader.GetOrdinal("Subject"));
        //                }
        //                if (reader.IsDBNull(reader.GetOrdinal("Content")) == false)
        //                {
        //                    comment.Content = reader.GetString(reader.GetOrdinal("Content"));
        //                }

        //                comments.Add(comment);
        //            }
        //            reader.Close();
        //            return comments;
        //        }
        //    }
        //}
    }
}

       