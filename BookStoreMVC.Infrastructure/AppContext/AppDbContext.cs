using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookStoreMVC.Infrastructure.AppContext
{
	public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
	{
		private readonly IHttpContextAccessor _contexAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options,IHttpContextAccessor contexAccessor) : base(options)
        {
            _contexAccessor = contexAccessor;
        }

        public const string DevConnectionString = "AppConnectionDev";
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public virtual DbSet<Admin> Admins { get; set; }
		public virtual DbSet<AppUser> AppUsers { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Publisher> Publishers { get; set; }
		public virtual DbSet<Book> Books { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
			base.OnModelCreating(builder);
		}
		public override int SaveChanges()
		{
			SetBaseProperties();
			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			SetBaseProperties();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void SetBaseProperties()
		{
			var entries = ChangeTracker.Entries<BaseEntity>();
			var userId = _contexAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)?? "UserIdBulunamadı";
			
			foreach (var entry in entries)
			{
				SetIfAdded(entry, userId);
				SetIfModified(entry, userId);
				SetIfDeleted(entry, userId);
			}
		}

		private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.Status = Domain.Enums.Status.Created;
				entry.Entity.CreatedBy = userId;
				entry.Entity.CreatedDate = DateTime.Now;
			}
		}

		private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State == EntityState.Modified)
			{
				entry.Entity.Status = Domain.Enums.Status.Updated;
				entry.Entity.UpdateedBy = userId;
				entry.Entity.UpdatedDate = DateTime.Now;
			}
		}

		private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State != EntityState.Deleted)
			{
				return;
			}
			if (entry.Entity is not AuiditableEntity entity)
			{
				return;

			}
			entry.State = EntityState.Modified;
			entry.Entity.Status = Domain.Enums.Status.Deleted;
			entity.DeletedDate = DateTime.Now;
			entity.DeletedBy = userId;

		}
	}
}
