using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Collections.Generic;
using Entities;
using System.Web;
using System.IO;

namespace GridMoment.UI.WebSite.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
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
                Image = post.Image,
                Text = post.Text
            };

            return View(model);
        }
        
        public ActionResult Add()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Add(PostViewModel model, HttpPostedFileBase uploadImage)
        {
            var postCreator = Adapter.GetAccount(User.Identity.Name);

            string ext;

            var tagsArray = model.TagsAddiction.Split(',');

            if (uploadImage == null)
            {
                return HttpNotFound();
            }
            else
            {
                ext = uploadImage.ContentType;

                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    model.Image = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
            }

            var modelToSend = new Post
            {
                AccountId = postCreator.Id,
                AuthorName = postCreator.Name,
                Rating = 0,
                Image = model.Image,
                NamePost = model.NamePost,
                Tags = tagsArray,
                Text = model.Text,
                MimetypeSource = ext
            };

            Adapter.CreatePost(modelToSend);

            return View();
        }

    }
}