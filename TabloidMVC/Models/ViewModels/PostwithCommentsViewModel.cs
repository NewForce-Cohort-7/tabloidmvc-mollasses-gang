using System.Security.Policy;

namespace TabloidMVC.Models.ViewModels
{
    public class PostwithCommentsViewModel
    { 
    
        
            public int PostId { get; set; }
           public List<Comments> Comments { get; set; }

        public string PostTitle { get; set; }

    }
    
}

