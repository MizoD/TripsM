using DataAccess.Repositories.IRepositories;
using Models;

namespace DataAccess.Repositories
{
    public class SubscriberRepository : Repository<NewsletterSubscriber>, ISubscriberRepository
    {
        public SubscriberRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
