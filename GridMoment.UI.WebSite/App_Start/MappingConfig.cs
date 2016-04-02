using AutoMapper;
using Entities;
using GridMoment.UI.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GridMoment.UI.WebSite.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Post, PostViewModel>();

            Mapper.CreateMap<PostViewModel, Post>();

            Mapper.CreateMap<Photo, PhotoViewModel>();

            Mapper.CreateMap<PhotoViewModel, Photo>();
        }
    }
}