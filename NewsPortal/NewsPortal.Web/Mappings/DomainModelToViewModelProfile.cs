using AutoMapper;
using NewsPortal.Model.Models;
using NewsPortal.Web.ViewModels;

namespace NewsPortal.Web.Mappings
{
    public class DomainModelToViewModelProfile : Profile
    {
        public DomainModelToViewModelProfile()
        {
            this.CreateMap<Article, PublicationViewModel>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId))
                .ForMember(destination => destination.ImageUrl, act => act.MapFrom(source => source.ImageUrl))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.User.Email));

            this.CreateMap<Article, ArticleFormViewModel>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Description, act => act.MapFrom(source => source.Description))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId));

            this.CreateMap<Article, ArticleViewModel>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Description, act => act.MapFrom(source => source.Description))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.User.Email))
                .ForMember(destination => destination.ImageUrl, act => act.MapFrom(source => source.ImageUrl))
                .ForMember(destination => destination.Comments, act => act.MapFrom(source => source.Comments));

            this.CreateMap<Article, SearchArticleViewModel>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId))
                .ForMember(destination => destination.UserName, act => act.MapFrom(source => source.User.UserName));

            this.CreateMap<Article, AdminArticleViewModel>()
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Title, act => act.MapFrom(source => source.Title))
                .ForMember(destination => destination.Category, act => act.MapFrom(source => source.Category))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate));

            this.CreateMap<ApplicationUser, SecurityDataViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.CreatedDate, act => act.MapFrom(source => source.CreatedDate));

            this.CreateMap<ApplicationUser, UserProfileViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.Profile.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.Profile.LastName))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.CreatedDate, act => act.MapFrom(source => source.CreatedDate))
                .ForMember(destination => destination.Gender, act => act.MapFrom(source => source.Profile.Gender))
                .ForMember(destination => destination.DateOfBirth, act => act.MapFrom(source => source.Profile.DateOfBirth))
                .ForMember(destination => destination.Adress, act => act.MapFrom(source => source.Profile.Adress))
                .ForMember(destination => destination.Country, act => act.MapFrom(source => source.Profile.Country))
                .ForMember(destination => destination.City, act => act.MapFrom(source => source.Profile.City))
                .ForMember(destination => destination.PhoneNumber, act => act.MapFrom(source => source.Profile.PhoneNumber))
                .ForMember(destination => destination.Skype, act => act.MapFrom(source => source.Profile.Skype))
                .ForMember(destination => destination.AvatarUrl, act => act.MapFrom(source => source.AvatarUrl));

            this.CreateMap<ApplicationUser, SearchUserViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.Profile.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.Profile.LastName))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email));

            this.CreateMap<ApplicationUser, FollowingsViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.Profile.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.Profile.LastName))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email));

            this.CreateMap<ApplicationUser, FollowersViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.Profile.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.Profile.LastName))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email));

            this.CreateMap<ApplicationUser, AdminUserViewModel>()
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.Id))
                .ForMember(destination => destination.Email, act => act.MapFrom(source => source.Email))
                .ForMember(destination => destination.State, act => act.MapFrom(source => source.Activated))
                .ForMember(destination => destination.RoleId, act => act.MapFrom(source => source.RoleId))
                .ForMember(destination => destination.Roles, act => act.Ignore());

            this.CreateMap<UserProfile, PersonalDataViewModel>()
                .ForMember(destination => destination.FirstName, act => act.MapFrom(source => source.FirstName))
                .ForMember(destination => destination.LastName, act => act.MapFrom(source => source.LastName))
                .ForMember(destination => destination.Gender, act => act.MapFrom(source => source.Gender))
                .ForMember(destination => destination.DateOfBirth, act => act.MapFrom(source => source.DateOfBirth))
                .ForMember(destination => destination.Adress, act => act.MapFrom(source => source.Adress))
                .ForMember(destination => destination.Country, act => act.MapFrom(source => source.Country))
                .ForMember(destination => destination.City, act => act.MapFrom(source => source.City))
                .ForMember(destination => destination.PhoneNumber, act => act.MapFrom(source => source.PhoneNumber))
                .ForMember(destination => destination.Skype, act => act.MapFrom(source => source.Skype))
                .ForMember(destination => destination.AvatarUrl, act => act.MapFrom(source => source.User.AvatarUrl))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.User.Id))
                .ForMember(destination => destination.UserProfileId, act => act.MapFrom(source => source.UserProfileId));

            this.CreateMap<Comment, CommentViewModel>()
                .ForMember(destination => destination.CommentId, act => act.MapFrom(source => source.CommentId))
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Message, act => act.MapFrom(source => source.Message))
                .ForMember(destination => destination.CommentOwner, act => act.MapFrom(source => source.User.Email))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate))
                .ForMember(destination => destination.LikesCount, act => act.MapFrom(source => source.Likes.Count))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId))
                .ForMember(destination => destination.AvatarUrl, act => act.MapFrom(source => source.User.AvatarUrl))
                .ForMember(destination => destination.ParentCommentId, act => act.MapFrom(source => source.ParentCommentId))
                .ForMember(destination => destination.ChildComments, act => act.MapFrom(source => source.ChildComments));

            this.CreateMap<Comment, CommentFormViewModel>()
                .ForMember(destination => destination.CommentId, act => act.MapFrom(source => source.CommentId))
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.Message, act => act.MapFrom(source => source.Message))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId));

            this.CreateMap<Comment, SearchCommentViewModel>()
                .ForMember(destination => destination.CommentId, act => act.MapFrom(source => source.CommentId))
                .ForMember(destination => destination.Message, act => act.MapFrom(source => source.Message))
                .ForMember(destination => destination.CommentOwner, act => act.MapFrom(source => source.User.Email))
                .ForMember(destination => destination.PublishingDate, act => act.MapFrom(source => source.PublishingDate))
                .ForMember(destination => destination.AvatarUrl, act => act.MapFrom(source => source.User.AvatarUrl))
                .ForMember(destination => destination.ArticleId, act => act.MapFrom(source => source.ArticleId))
                .ForMember(destination => destination.UserId, act => act.MapFrom(source => source.UserId));
        }
    }
}