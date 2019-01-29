using FollowArtist.Models;
using FollowArtist.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == currentUserId && !g.IsCanceled)
                .ToList();

            var viewModel = new GigViewModel
            {
                Gigs = gigs,
                Heading = "My Upcoming Gigs"
            };
            return View("UpcomingGigs", viewModel);
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Single(g => g.Id == id);

            var viewModel = new GigDetailViewModel
            {
                Gig = gig
            };

            var currentUserId = User.Identity.GetUserId();
            viewModel.IsFollowing = _context.Followings
                .Any(f => f.FollowerId == currentUserId && f.FolloweeId == gig.Artist.Id);
            viewModel.ButtonType = "btn-default";
            viewModel.ButtonText = "Follow";
            if (viewModel.IsFollowing)
            {
                viewModel.ButtonType = "btn-info";
                viewModel.ButtonText = "Following";
            }
            viewModel.IsAttending = _context.Attendances
                .Any(at => at.AttendeeId == currentUserId && at.Gig.Id == id);

            return View("Details", viewModel);
        }

    }
}