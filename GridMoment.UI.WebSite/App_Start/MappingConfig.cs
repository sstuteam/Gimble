using AutoMapper;
using Entities;
using GridMoment.UI.WebSite.Models;

namespace GridMoment.UI.WebSite.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {            
            Mapper.CreateMap<Post, PostViewModel>();      //Маппинг Моделей постов
                                                         
            Mapper.CreateMap<PostViewModel, Post>();      //Маппинг Моделей постов

            Mapper.CreateMap<Photo, PhotoViewModel>();    //маппинг моделей фотографий
                                                         
            Mapper.CreateMap<PhotoViewModel, Photo>();    //маппинг моделей фотографий

            Mapper.CreateMap<Comment, CommentViewModel>();  //Маппинг моделей комментариев
                                                            
            Mapper.CreateMap<CommentViewModel, Comment>();  //Маппинг моделей комментариев
        }
    }
}