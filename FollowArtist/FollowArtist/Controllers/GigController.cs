using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FollowArtist.Models;
using FollowArtist.ViewModels;
using Microsoft.AspNet.Identity;

namespace FollowArtist.Controllers
{
    public class GigController : Controller
    {
        private ApplicationDbContext _context;

        public GigController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var vm = new GigFomViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFomViewModel viewModel)
        {
            var vm = new GigFomViewModel
            {
                Venue = viewModel.Venue,
                Date = viewModel.Date,
                GenreId = viewModel.GenreId,
                Genres = _context.Genres.ToList()
            };
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }
            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                Venue = viewModel.Venue,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.GenreId
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        public ActionResult UpcomigGigs()
        {
            var currentUserId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Include(g => g.Atrist)
                .Include(g => g.Genre)
                .Where( g => g.ArtistId == currentUserId && !g.IsCanceled)
                .ToList();

            var viewModel = new GigViewModel
            {
                Gigs = gigs,
                Heading = "My Upcoming Gigs"
            };
            return View("UpcomingGigs", viewModel);
        }

    }

    public class GigViewModel
    {
        public IEnumerable<Gig> Gigs { get; set; }
        public string Heading { get; set; }
    }
}