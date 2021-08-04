using AutoMapper;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;

namespace NewsPortal.Web.Mappings
{
    public class ViewModelToDomainModelProfile : Profile
    {
        public ViewModelToDomainModelProfile()
        {
            this.CreateMap<ArticleFormViewModel, Article>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Description, act => act.MapFrom(source => source.Description))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.ImageUrl, act => act.MapFrom(source => source.ImageUrl))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId));

            this.CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.UserName, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.Activated, act => act.MapFrom(source => true));

            this.CreateMap<SecurityDataViewModel, ApplicationUser>()
                .ForMember(destination => destination.UserName, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email));

            this.CreateMap<AdminUserViewModel, ApplicationUser>()
                .ForMember(destination => destination.RoleId, act => act.MapFrom(source => source.RoleId))
                .ForMember(destination => destination.Roles, act => act.Ignore());

            this.CreateMap<PersonalDataViewModel, UserProfile>()
                .ForMember(destination => destination.UserProfileId, act => act.MapFrom(source => source.UserProfileId))
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.LastName))
                .ForMember(destination => destination.Gender, act => act.MapFrom(source => source.Gender))
                .ForMember(destination => destination.DateOfBirth, act => act.MapFrom(source => source.DateOfBirth))
                .ForMember(destination => destination.Adress, act => act.MapFrom(source => source.Adress))
                .ForMember(destination => destination.Country, act => act.MapFrom(source => source.Country))
                .ForMember(destination => destination.City, act => act.MapFrom(source => source.City))
                .ForMember(destination => destination.PhoneNumber, act => act.MapFrom(source => source.PhoneNumber))
                .ForMember(destination => destination.Skype, act => act.MapFrom(source => source.Skype));

            this.CreateMap<CommentFormViewModel, Comment>()
                .ForMember(destination => destination.CommentId, act => act.MapFrom(source => source.CommentId))
                .ForMember(destination => destination.Message, act => act.MapFrom(source => source.Message))
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId))
                .ForMember(destination => destination.ParentCommentId, act => act.MapFrom(source => source.ParentCommentId));
        }
    }
}