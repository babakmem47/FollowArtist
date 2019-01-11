﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FollowArtist.Models;
using FollowArtist.ViewModels;

namespace FollowArtist.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var gigs = _context.Gigs.Include(gg => gg.Genre).Include(gg => gg.Atrist).ToList();
            return View(gigs);
        }

//        public ActionResult About()
//        {
//            ViewBag.Message = "Your application description page.";
//
//            return View();
//        }

//        public ActionResult Contact()
//        {
//            ViewBag.Message = "Your contact page.";
//
//            return View();
//        }

    }
}