﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Week15Project.Models;
using Week15Project.Models.News;
using Week15Project.Services;

namespace Week15Project.Controllers
{
    public class NewsController : Controller
    {
        private string NASA_Key = "rDVAxMGgW9EzEbHCXVxogZrQS1JuDxBJhYI2s2dh";
        private IHttpClientFactory factory;
        private UserManager<User> userManager;
        private IForumRepository repository;

        public NewsController(IHttpClientFactory factory, UserManager<User> _userManager, IForumRepository _repository)
        {
            this.factory = factory;
            userManager = _userManager;
            repository = _repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NasaPOTD(DateTime date)
        {
            string dateRequested = date.ToString("yyyy-MM-dd");
            try
            {
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://api.nasa.gov/planetary/apod");
                    var response = client.GetFromJsonAsync<NASA_POTD>("https://api.nasa.gov/planetary/apod?date=" + dateRequested + "&api_key=" + NASA_Key);
                    var potd = response.Result;
                    potd.DatePosted = DateTime.Parse(potd.DateString);
                    return View(potd);
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

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
                return NotFound();
            }
        }

        public IActionResult ViewUpcomingEvents()
        {
            try
            {
                using (var client = factory.CreateClient())
                {
                    client.BaseAddress = new Uri("https://lldev.thespacedevs.com/2.2.0/");
                    var response = client.GetFromJsonAsync<UpcomingEvent>("https://lldev.thespacedevs.com/2.2.0/event/upcoming/?limit=5");
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
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddEventToForum(EventResult result)
        {
            try
            {
                Post post = new Post()
                {
                    UserId = userManager.GetUserId(User),
                    RoomId = 1,
                    DatePosted = DateTime.Now,
                    Title = result.Name,
                    Message = result.Description,
                    // userimage
                    WebURL = result.NewsURL,
                    EventId = result.Id,
                };

                repository.AddPost(post);
                return RedirectToAction("ViewRoom", "Forum", new { roomId = 1 });
            }
            catch (Exception ex)
            {
                // LOG ERROR HERE
                return View("Index", "Home");
            }
        }
    }
}