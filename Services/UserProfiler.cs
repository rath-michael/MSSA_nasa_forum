using Microsoft.AspNetCore.Identity;
using Week15Project.Models;
using Microsoft.AspNetCore.Mvc;
using Week15Project.Models.ViewModels;
using Week15Project.Services;
using System.Security.Claims;

namespace Week15Project.Services
{
    public class UserProfiler : IUserProfiler
    {
        public User CurrentUser { get; set; }
        private IForumRepository repository;
        public UserProfiler(IForumRepository forumRepository)
        {
            repository = forumRepository;
        }
        
        public int PostCount { 
            get
            {
                return repository.GetPostsByUser(CurrentUser.Id).Count();
            }
        }
        public int ResponseCount {
            get
            {
                return repository.GetResponsesByUser(CurrentUser.Id).Count();
            }
        }
        public DateTime JoinDate {
            get
            {
                return CurrentUser.DateCreated;
            }
        }

        public void AssignUser(string username)
        {
            CurrentUser = repository.GetUserByName(username);
        }
    }
}
