﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.Json.Serialization;
using Week15Project.Models;
using Week15Project.Models.News;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class MediaController : Controller
    {
        private string NASA_Key = "rDVAxMGgW9EzEbHCXVxogZrQS1JuDxBJhYI2s2dh";
        private IHttpClientFactory factory;
        private UserManager<User> userManager;
        private IForumRepository repository;

        public MediaController(IHttpClientFactory _factory, UserManager<User> _userManager, IForumRepository _repository)
        {
            factory = _factory;
            userManager = _userManager;
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("PictureOfTheDay")]
        public IActionResult NasaPOTD(DateTime date)
        {
            try
            {
                string dateRequested = date.ToString("yyyy-MM-dd");
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://api.nasa.gov/planetary/apod");
                    var response = client.GetFromJsonAsync<NASA_POTD>("https://api.nasa.gov/planetary/apod?date=" + dateRequested + "&api_key=" + NASA_Key);
                    var potd = response.Result;
                    potd.DatePosted = DateTime.Parse(potd.DateString);
                    potd.postId = repository.GetPostIdFromPhotoDate(potd.DatePosted);
                    return View(potd);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Route("PreviousEvents")]
        public IActionResult ViewPreviousEvents()
        {
            try
            {
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://lldev.thespacedevs.com/2.2.0/");
                    var response = client.GetFromJsonAsync<UpcomingEvent>("https://lldev.thespacedevs.com/2.2.0/event/previous/?limit=5");
                    var newsList = response.Result.Results;
                    foreach (var item in newsList)
                    {
                        item.postId = repository.GetPostIdFromEventId(item.Id);
                    }
                    return View("EventList", newsList);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Route("UpcomingEvents")]
        public IActionResult ViewUpcomingEvents()
        {
            try
            {
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://lldev.thespacedevs.com/2.2.0/");
                    var response = client.GetFromJsonAsync<UpcomingEvent>("https://lldev.thespacedevs.com/2.2.0/event/upcoming/?limit=1");
                    var newsList = response.Result.Results;
                    foreach (var item in newsList)
                    {
                        item.postId = repository.GetPostIdFromEventId(item.Id);
                    }
                    return View("EventList", newsList);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IActionResult AddEventToForum(EventResult result)
        {
            try
            {
                Post post = new Post()
                {
                    UserId = userManager.GetUserId(User),
                    RoomId = result.RoomId,
                    DatePosted = DateTime.Now,
                    Title = result.Name,
                    Message = result.Description,
                    WebURL = result.NewsURL,
                    MediaType = "image",
                    MediaURL = result.Image,
                    EventId = result.Id,
                };

                repository.AddPost(post);
                return RedirectToAction("ViewRoom", "Forum", new { roomId = post.RoomId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public IActionResult AddPOTDToForum(NASA_POTD photo)
        {
            try
            {
                Post post = new Post()
                {
                    UserId = userManager.GetUserId(User),
                    RoomId = 6,
                    DatePosted = DateTime.Now,
                    Title = photo.Title,
                    Message = photo.Explanation,
                    MediaURL = photo.Url,
                    MediaType = photo.MediaType,
                    POTDDate = photo.DatePosted
                };

                repository.AddPost(post);
                return RedirectToAction("ViewRoom", "Forum", new { roomId = 6 });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
