using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coding_events_practice.Controllers
{
    public class EventsController : Controller
    {

        //static private Dictionary<string, string> Events = new Dictionary<string, string>();
        //static private List<Event> Events = new List<Event>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }

        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        // POST: /Events/Add
        // Will create/add a new Event
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            /*if (!Events.Contains(new Event(name)))
            {
                Events.Add(new Event(name));
            }
            else
            {
                ViewBag.error = "ERROR";
                return Redirect("/Events/Add");
            }*/
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail
                };

                EventData.Add(newEvent);
            

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        // GET: /Events/Delete
        [HttpGet]
        public IActionResult Delete()
        {
            List<Event> events = new List<Event>(EventData.GetAll());
            return View(events);
        }

        // POST: /Events/Delete
        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }

        //GET: /Events/Edit/Event id
        [HttpGet]
        [Route("/Events/Edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            Event editingEvent = EventData.GetById(eventId);
            ViewBag.eventToEdit = editingEvent;

            ViewBag.title = $"Edit Event {editingEvent.Name} (id={editingEvent.Id})";
            return View(editingEvent);
        }

        //POST: /Events/Edit
        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event editingEvent = EventData.GetById(eventId);
            editingEvent.Name = name;
            editingEvent.Description = description;
            return Redirect("/Events");
        }
    }
}
