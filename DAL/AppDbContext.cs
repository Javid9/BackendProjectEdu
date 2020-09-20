using EducationBackendFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<NoticeRightInfo> NoticeRightInfos { get; set; }
        public DbSet<WhyUs>WhyUs { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UpComingEvent>UpComingEvents{ get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<WelcomeAbout>WelcomeAbouts { get; set; }
        public DbSet<Bio> Bios { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<VideoTour> VideoTours { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Speaker> Speakers  { get; set; }
        public DbSet<SpeakerEvent> SpeakerEvents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
        public DbSet<TeacherUser> TeacherUsers { get; set; }
        public DbSet<HomeBio> HomeBios { get; set; }
        public DbSet<CourseCategory> CourseCategories{ get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}
