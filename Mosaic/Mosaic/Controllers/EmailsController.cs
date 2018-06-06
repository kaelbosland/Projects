using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mosaic.Models;
using Microsoft.AspNetCore.Http;
using Mosaic.Services;


namespace Mosaic.Controllers
{
    public class EmailsController : Controller
    {
        private readonly MosaicContext _context;
        private readonly IEmailAuthentication _service;

        public EmailsController(MosaicContext context, IEmailAuthentication service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Read(int id)
        {
            Email email = _context.Email.SingleOrDefault(m => m.Id == id );
            if (email.Status[0] != 'R')
            {
                email.Status = "Read @ " + DateTime.Now.ToString("HH:mm") + " on " + DateTime.Today.ToString("dd-MM-yyyy");
                _context.Email.Update(email);
                _context.SaveChanges();
            }

            return RedirectToAction("Inbox");
        }

        public IActionResult Menu()
        {
            if (HttpContext.Session.GetInt32("type") == 0)
            {
                return RedirectToAction("EmailMenu", "Students");
            }
            else if (HttpContext.Session.GetInt32("type") == 1)
            {
                return RedirectToAction("EmailMenu", "Professors");
            }

            return RedirectToAction("Home", "Students");
        }

        public IActionResult Reply(string subject, string sender)
        {
            HttpContext.Session.SetString("subject", "RE: " + subject);
            HttpContext.Session.SetString("receiver", sender);

            return RedirectToAction("Create");
        }

        public IActionResult Forward(string subject, string sender, string message)
        {
            HttpContext.Session.SetString("subject", "FWD From " + sender + ": " + subject);
            HttpContext.Session.SetString("message", message);
            TempData["type"] = "fwd";

            return RedirectToAction("Create");
        }

        // GET: Emails/Inbox
        public IActionResult Inbox()
        {
            string username = HttpContext.Session.GetString("username");
            var emails = _context.Email.ToList();
            List<Email> inbox = new List<Email>();
            for (int i = 0; i < emails.Count; i++)
            {
                if (emails[i].Receiver.Equals(username))
                {
                    inbox.Add(emails[i]);
                }
            }
            inbox.Reverse();
            ViewData["Inbox"] = inbox;
            ViewData["Username"] = username;
            return View();
        }

        // GET: Emails/SentMail
        public IActionResult SentMail()
        {
            string username = HttpContext.Session.GetString("username");
            var emails = _context.Email.ToList();
            List<Email> inbox = new List<Email>();
            for (int i = 0; i < emails.Count; i++)
            {
                if (emails[i].Sender.Equals(username))
                {
                    inbox.Add(emails[i]);
                }
            }
            ViewData["Inbox"] = inbox;
            ViewData["Username"] = username;
            return View();
        }

        //GET: Emails/ContactsList
        public IActionResult ContactsList()
        {
            string username = HttpContext.Session.GetString("username");
            var allUsers = _context.Student
                               .Where(x => x.Username != username)
                               .Select(x => new { x.Username, x.FirstName, x.LastName })
                               .Union(

                                   _context.Professor
                                   .Where(x => x.Username != username)
                                   .Select(x => new { x.Username, x.FirstName, x.LastName })
                               ).ToList();

            int count = _context.Student.Count() + _context.Professor.Count() - 1;

            List<Tuple<string, string, string>> users = new List<Tuple<string, string, string>>();

            for (int i = 0; i < count; i++)
            {
                users.Add(Tuple.Create<string, string, string>(allUsers[i].Username, allUsers[i].FirstName, allUsers[i].LastName));
            }

            ViewData["contacts"] = users;

            return View();
        }

        public IActionResult SendContactEmail(string receiver)
        {
            HttpContext.Session.SetString("receiver", receiver);
            return RedirectToAction("Create");
        }

        // GET: Emails/Create
        public IActionResult Create()
        {


            ViewData["receiver"] = HttpContext.Session.GetString("receiver");
            ViewData["sender"] = HttpContext.Session.GetString("username");
            ViewData["subject"] = HttpContext.Session.GetString("subject");
            ViewData["message"] = HttpContext.Session.GetString("message");

        
            List<Student> students = _context.Student.ToList();
            List<Professor> profs = _context.Professor.ToList();
            List<string> usernames = new List<string>();
            for (int i = 0; i < students.Count; i++)
            {
                usernames.Add(students[i].Username);
            }

            for (int i = 0; i < profs.Count; i++)
            {
                usernames.Add(profs[i].Username);
            }

            ViewData["usernames"] = usernames;

            HttpContext.Session.SetString("receiver", "");
            HttpContext.Session.SetString("subject", "");
            HttpContext.Session.SetString("message", "");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string sender, string receiver, string subject, string message)
        {
            ViewData["sender"] = HttpContext.Session.GetString("username");
            List<Student> students = _context.Student.ToList();
            List<Professor> profs = _context.Professor.ToList();
            List<string> usernames = new List<string>();
            for (int i = 0; i < students.Count; i++)
            {
                usernames.Add(students[i].Username);
            }

            for (int i = 0; i < profs.Count; i++)
            {
                usernames.Add(profs[i].Username);
            }

            ViewData["usernames"] = usernames;
            Email email = new Email { Sender = sender, Receiver = receiver, Subject = subject, Message = message };

            email.Status = "Delivered @ " + DateTime.Now.ToString("HH:mm") + " on " + DateTime.Today.ToString("dd-MM-yyyy");
            _context.Add(email);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("receiver", "");
            HttpContext.Session.SetString("subject", "");
            HttpContext.Session.SetString("message", "");
            return RedirectToAction("Menu");
        }
    }
}

