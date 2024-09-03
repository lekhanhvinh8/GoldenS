using GoldenS.Domain;
using GoldenS.DatabaseContext;

namespace GoldenS.Graphql.Query
{
    public class Query
    {
        // private static AppDbContext? FirstDbContext = null;
        // private readonly AppDbContext _context;

        // public Query(AppDbContext context)
        // {
        //     this._context = context;

        //     if(FirstDbContext == null)
        //     {
        //         FirstDbContext = context;
        //     }
        // }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IQueryable<User>> GetUsers([Service] AppDbContext context) 
        {   
            var users = context.Users;
            return users;
        }


        // [UseDbContext(typeof(AppDbContext))]
        // [UseProjection]
        // [UseFiltering]
        // [UseSorting]
        // public IQueryable<Post> GetPosts([ScopedService] AppDbContext context) => context.Posts;

        // [UseDbContext(typeof(AppDbContext))]
        // [UseProjection]
        // [UseFiltering]
        // [UseSorting]
        // public IQueryable<Comment> GetComments([ScopedService] AppDbContext context) => context.Comments;
    }
}