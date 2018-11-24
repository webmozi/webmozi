using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UserManager
    {

        public static void AddUser(User user)
        {
            using (var context = new CinemaContext())
            {
                context.Users
                    .Add(user);

                context.SaveChanges();
            }
        }



        public static void DeleteUser(int id)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.SingleOrDefault(u => u.UserId == id);
                if (item == null)
                {
                    return;
                }
                context.Users.Remove(item);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new CinemaContext())
            {
                var item = context.Users.Find(user.UserId);
                if (item == null)
                {
                    return;
                }
                item.Name = user.Name;
                item.TelephoneNumber = user.TelephoneNumber;
                item.Email = user.Email;
                context.Users.Update(item);
                context.SaveChanges();
            }
        }


        public static List<User> ListUsers()
        {
            using (CinemaContext ctx = new CinemaContext())
            {

                return ctx.Users.ToList();
            }
        }



        public static DAL.User GetUserById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Users.Where(u => u.UserId == id).SingleOrDefault();
            }
        }


        public static List<Reservation> ListUserReservations(int userId)
        {
            using (CinemaContext ctx = new CinemaContext())
            {
                var q = ctx.Reservations.Include(r => r.MovieEvent).Include(r => r.MovieEvent.Movie).
                  Include(r => r.MovieEvent.Room)
                  .Where(r => r.UserId == userId);                
                return q.ToList();
            }
        }

    }
}
