using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Collections.Generic;

namespace GridMoment.UI.WebSite.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostViewModel model)
        {
            var modelToSend = new Entities.Post
            {
                AccountId = Adapter.GetAccount(User.Identity.Name).Id,
                AuthorName = model.Author,
                Rating = 0,
                Source = "Image",
                NamePost = model.NamePost,
                Avatar = "Image",
                Tags = model.Tags.ToArray(),
                Text = model.Text
            };

            Adapter.CreatePost(modelToSend);

            return View();
        }

        [HttpGet]
        public ActionResult Show(System.Guid postid)
        {
            var post = Adapter.GetPost(postid);

            var model = new PostViewModel()
            {
                Author = post.AuthorName,
                Avatar = post.Avatar,
                DateOfCreation = post.CreatedTime,
                Id = post.AccountId,
                PostId = post.PostId,
                Tags = new List<string>(post.Tags),
                NamePost = post.NamePost,
                Source = post.Source,
                Text = post.Text
            };

            return View("Index", model);
        }

        public ActionResult Add()
        {
            var model = new PostViewModel { PostId = System.Guid.NewGuid() };
            return View(model);
        }
    }
}