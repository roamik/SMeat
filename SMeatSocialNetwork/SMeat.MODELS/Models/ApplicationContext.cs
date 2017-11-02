using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS
{
    public class ApplicationContext : DbContext
    {
        

        #region Config
        private readonly IOptions<AppConnectionStrings> _options;

        public ApplicationContext() {
        }

        public ApplicationContext( IOptions<AppConnectionStrings> options ) {
            _options = options;
        }

        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder ) {
            optionsBuilder.UseSqlServer(_options.Value.DefaultConnection);
          //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SMSNv1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        #endregion
        

        public DbSet<User> Users { get; set; }

        public DbSet<UserChat> UserChats { get; set; }

        public DbSet<Chat> Chats { get; set; }

        public DbSet<UserGroupChat> UserGroupChats { get; set; }

        public DbSet<GroupChat> GroupChats { get; set; }

        public DbSet<Workplace> WorkPlaces { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Contacts> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserChat>().HasKey(e => new { e.UserId, e.ChatId });

            modelBuilder.Entity<User>()    // User 1 => N UserChats
               .HasMany(u => u.UserChats)
               .WithOne(uc => uc.User)
               .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<User>()         //User 1 =>
               .HasMany(u => u.Chats)          //=> N Chats each =>
               .WithOne(c => c.User)           //=> 1 User
               .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<User>()         //User 1 =>
                .HasMany(u => u.Messages)          //=> N Messages each =>
                .WithOne(m => m.User)           //=> 1 User
                .HasForeignKey(m => m.UserId);


            modelBuilder.Entity<User>()    // User N => 1 Location
                .HasOne(u => u.Location)
                .WithMany(l => l.Users)
                .HasForeignKey(u => u.LocationId);

            modelBuilder.Entity<User>()    // User N => 1 Workplace
                .HasOne(u => u.Workplace)
                .WithMany(w => w.Users)
                .HasForeignKey(u => u.WorkplaceId);

            modelBuilder.Entity<UserGroupChat>().HasKey(e => new { e.UserId, e.GroupChatId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserGroupChats)
                .WithOne(ugc => ugc.User)
                .HasForeignKey(ugc => ugc.UserId);

            modelBuilder.Entity<GroupChat>() //GroupChat 1 =>
                 .HasMany(gc => gc.UserGroupChats) //=> N UserGroupChats each =>
                 .WithOne(ugc => ugc.GroupChat)// => 1 GroupChat
                 .HasForeignKey(ugc => ugc.GroupChatId);

            modelBuilder.Entity<GroupChat>()    // GroupChat 1 => N Messages
                .HasMany(gc => gc.Messages)
                .WithOne(m => m.GroupChat)
                .HasForeignKey(m => m.GroupChatId);

            modelBuilder.Entity<Workplace>()    // Workplaces N => 1 Location
                .HasOne(w => w.Location)
                .WithMany(l => l.Workplaces)
                .HasForeignKey(w => w.LocationId);

            modelBuilder.Entity<Chat>() //Chat 1 = > N Messages (Chat has many messages and one message has 1 chat)
                 .HasMany(c => c.Messages)
                 .WithOne(m => m.Chat)
                 .HasForeignKey(m => m.ChatId);


            // User 1 => N UserChats N => 1 Chat  // how the bridge connection works (users to chats)

            modelBuilder.Entity<UserChat>()    //UserChats N => 1 Chat 
               .HasOne(uc => uc.Chat)
               .WithMany(c => c.UserChats)
               .HasForeignKey(uc => uc.ChatId);

            modelBuilder.Entity<Contacts>().HasKey(e => new { e.FirstUserId, e.SecondUserId });

            modelBuilder.Entity<User>()    //Contacts N => 1 FirstUser (User)
               .HasMany(u => u.ContactsTo)
               .WithOne(c => c.FirstUser)
               .HasForeignKey(c => c.FirstUserId)
               .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()    //Contacts N => 1 SecondUser (User)
               .HasMany(u => u.ContactsFrom)
               .WithOne(c => c.SecondUser)
               .HasForeignKey(c => c.SecondUserId)
               .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

        }


    }
}
