using TabloidMVC.Models;
namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
       
        public List<Comments> GetCommentsByPost(int postId);
    }
}
