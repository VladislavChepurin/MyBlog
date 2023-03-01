using MyBlog.Models;

namespace MyBlog.Data.Repositiry.Repository
{
    public class InviteRepository : Repository<Invate>
    {
        public InviteRepository(ApplicationDbContext db) : base(db)
        {

        }

        public void CreateInvite(Invate invate)
        {
            Create(invate);
        }

        public Invate? GetInvite(string code)
        {
            var invite = Set.AsEnumerable().Where(x => x?.CodeInvite == code).FirstOrDefault();
            return invite;
        }

        public void ChangeStatusInvite(Invate invate)
        {
            invate.IsActive = false;
            Update(invate);
        }
    }
}