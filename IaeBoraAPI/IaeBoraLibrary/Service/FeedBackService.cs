using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class FeedBackService
    {
        public static List<FeedBack> GetAllFeedBacks(string userId)
        {
            using(var context = new Context())
            {
                return context.FeedBacks.Where(f => f.Route.User.GoogleId == userId).Include("Route").Include("Route.User").ToList();
            }
        }

        public static void CreateFeedBack(FeedBack feedBack)
        {
            using (var context = new Context())
            {
                feedBack.Route = context.Routes.Include("User").Single(r => r.Id == feedBack.UserRouteId);
                context.FeedBacks.Add(feedBack);
                context.SaveChanges();
            }
        }

        public static void DeleteFeedBack(int feedBackId, string userId)
        {
            using (var context = new Context())
            {
                var feedback = context.FeedBacks.Single(f => f.Id == feedBackId && f.Route.User.GoogleId == userId);
                context.Entry(feedback).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
