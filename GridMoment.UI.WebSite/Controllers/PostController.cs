using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Collections.Generic;
using Entities;
using System.Web;
using System.IO;
using System.Drawing;
using AutoMapper;

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
                       
            MemoryStream memoryStream = new MemoryStream(post.Image);

            var model = Mapper.Map<PostViewModel>(post);

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

            var modelToSend = Mapper.Map<Post>(model);

            new Post
            {
                AccountId = postCreator.Id,
                AuthorName = postCreator.Name,
                Rating = 0,
                Image = model.Image,
                NamePost = model.NamePost,
                Tags = tagsArray,
                Text = model.Text,
                MimeType = ext
            };

            Adapter.CreatePost(modelToSend);

            return View();
        }

        [HttpGet]
        public PartialViewResult Show7DaysNews()
        {            
            var model = Mapper.Map<IEnumerable<Post>, 
                List<PostViewModel>>(Adapter.List7Times());

            return PartialView("~/Views/Shared/UsersPosts.cshtml", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult Show30DaysNews()
        {
            var model = Mapper.Map<IEnumerable<Post>,
                List<PostViewModel>>(Adapter.List30Times());

            return PartialView("~/Views/Shared/UsersPosts.cshtml", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult ShowLatestNews()
        {
            var model = Mapper.Map<IEnumerable<Post>,
                List<PostViewModel>>(Adapter.ListOfLatestPosts());

            return PartialView("~/Views/Shared/UsersPosts.cshtml", model);
        }

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult ShowUserNews(string modelName)
        {
            var model = Mapper.Map<IEnumerable<Post>,
                List<PostViewModel>>(Adapter.ListUsersPosts(modelName));

            return PartialView("~/Views/Shared/UsersPosts.cshtml", model);
        }

        [HttpGet]
        public FileResult ShowSourceOfPost(System.Guid postId)
        {
            var image = Mapper.Map<PhotoViewModel>(Adapter.GetSourceOfPost(postId));

            return File(image.Image, image.MimeType);
        }

        public ActionResult ShowComments(System.Guid postId)
        {
            var comments = Adapter.GetComments(postId);

            return PartialView(comments);
        }

        public ActionResult UpdateComment()
            => View();

        [HttpPost]
        public ActionResult UpdateComment(CommentViewModel comment, string text)
        {
            if (Adapter.CheckRules(User.Identity.Name) || User.Identity.Name.Equals(comment.AuthorName))
            {
                comment.Text = text;
                var successful = Adapter.UpdateComment(comment);

                if (successful)
                {
                    RedirectToAction("Show", "Post", new { postId = comment.PostId });
                }
            }
            return RedirectToAction("Index", "Home"); //издевательство
        }
                
        public ActionResult DeleteComment(System.Guid comId)
        {
            if (Adapter.CheckRules(User.Identity.Name))
            {


                //ToDo - Дописать      !!!

            }
                  
            return RedirectToAction("Index", "Home"); //издевательство
        }

        public ActionResult AddComment()
            => PartialView();

        [HttpPost]
        public ActionResult AddComment(CommentViewModel comment)
        {
            comment.AccountId = Adapter.GetIdByName(User.Identity.Name);

            if (Adapter.CreateComment(comment))
            {
                return RedirectToAction("Show", "Post", new { postId = comment.PostId });
            }

            return RedirectToAction("Index", "Home"); //издевательство
        }        
    }
}
