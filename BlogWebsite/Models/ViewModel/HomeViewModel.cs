using BlogWebsite.Models.Domain;

namespace BlogWebsite.Models.ViewModel
{
    public class HomeViewModel
    {
       public IEnumerable<BlogPost> BlogPosts { get; set;}
        public   IEnumerable<Tag> Tags { get; set;}
    }
}
