using IaeBoraLibrary.Model;
using IaeBoraLibrary.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IaeBoraLibrary.Service
{
    public static class AnswerService
    {
        public static Answer GetAnswers(string userId)
        {
            using (var context = new Context())
            {
                return context.Answers.Where(a => a.User.GoogleId == userId).Include("User").First();
            }
        }

        public static void UpdateAnswers(Answer answer)
        {
            using (var context = new Context())
            {
                context.Entry(answer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void SaveAnswers(Answer answers)
        {
            using (var context = new Context())
            {
                context.Entry(answers.User).State = EntityState.Unchanged;
                context.Answers.Add(answers);
                context.SaveChanges();
            }
        }
    }
}