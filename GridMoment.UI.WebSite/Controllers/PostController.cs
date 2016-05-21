using System.Web.Mvc;
using GridMoment.UI.WebSite.Models;
using System.Collections.Generic;
using Entities;
using System.Web;
using System.IO;
using AutoMapper;
using System;

namespace GridMoment.UI.WebSite.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Show(System.Guid? postid)
        {
            var post = Adapter.GetPost(postid);

            if (post == null || postid == null)
            {
                return HttpNotFound();
            }

            MemoryStream memoryStream = new MemoryStream(post.Image);
            var model = Mapper.Map<PostViewModel>(post);

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }
            return View(model);
        }
        
        public ActionResult Add()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
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
            modelToSend.MimeType = ext;
            modelToSend.AccountId = postCreator.Id;
            modelToSend.AuthorName = postCreator.Login;
            Adapter.CreatePost(modelToSend);

            return RedirectToAction("Index", "Home");
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

        [HttpGet]
        public ActionResult ShowComments(System.Guid postId)
        {
            var comments = Adapter.GetComments(postId);

            return PartialView(comments);
        }

        [HttpGet]
        public ActionResult ShowUserBookmarks(string modelName)
            => PartialView("~/Views/Shared/UsersPosts.cshtml", Adapter.GetLikedPost(modelName));
                               
        public ActionResult DeleteComment(System.Guid comId, System.Guid accountId)
        {
            var account = Adapter.GetAccount(User.Identity.Name);

            if (Adapter.CheckRulesAdmin(User.Identity.Name) || Adapter.CheckRulesModer(User.Identity.Name) || account != null && account.Id == accountId)
            {
                Adapter.DeleteComment(comId);
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
        
        [HttpGet]
        public string ShowRating(Guid post, Guid account)
        {
            var result = "";
            var temporary = Adapter.GetLikes(post, account);
            foreach (var item in temporary)
            {
                result = item.Value.ToString();
            }
            return result;
        }
        
        public ActionResult SetLike(Guid post, Guid account)
        {
            Adapter.SetLike(post, account);
            return RedirectToAction("Show", new { postid = post });
        }

        public ActionResult Search(string tag)
        {
            if (tag == null)
               return RedirectToAction("Index", "Home");

            var result = Adapter.SearchPosts(tag);
            if (result.Count != 0)
            {
                if (Request.IsAjaxRequest())
                    return PartialView(result);

                return View(result);
            }
            return HttpNotFound();
        }
    }
}
