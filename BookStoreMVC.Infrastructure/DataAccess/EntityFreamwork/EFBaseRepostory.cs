namespace BookStoreMVC.Infrastructure.DataAccess.EntityFreamwork
{
	public class EFBaseRepostory<TEntity> : IAsyncRepostory, IRepostory, IAsyncTransactionRepostory, IAsyncUpdatableRepostory<TEntity>, IAsyncQueryableRepostory<TEntity>, IAsyncFindableRepostory<TEntity>, IAsyncInsertable<TEntity>, IAsyncOrderableRepostory<TEntity>, IAsyncDeletableRepostory<TEntity>  where TEntity : BaseEntity
	{
		protected readonly DbContext _dbContext; //kalıtım alan claslarda örünsün diye protected yaptık
		protected readonly DbSet<TEntity> _table;


        public EFBaseRepostory(DbContext context)
        {
            _dbContext = context;
			_table=context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
		{
			var entry = await _table.AddAsync(entity);
			return entry.Entity;
		}

		public Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			return _table.AddRangeAsync(entities);
		}

		public Task<bool> AnyAsnc(Expression<Func<TEntity, bool>> expression = null)
		{
			return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
		}

		public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			return _dbContext.Database.BeginTransactionAsync(cancellationToken);
		}

		public Task<IExecutionStrategy> CreateExecutionStrategy()
		{
			return Task.FromResult(_dbContext.Database.CreateExecutionStrategy());
		}

		public Task DeleteAsync(TEntity entity)
		{
			return Task.FromResult(_dbContext.Remove(entity));
		}

		public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
		{
			_table.RemoveRange(entities);
			return _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
		{
			return await GetAllActives(tracking).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
		{
			return await GetAllActives(tracking).Where(expression).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool trancking = true)
		{
			return orderByDesc ? await GetAllActives(trancking).OrderByDescending(orderBy).ToListAsync() : await GetAllActives(trancking).OrderByDescending(orderBy).ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
		{
			var values = GetAllActives(tracking).Where(expression);
			return orderByDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync();
		}

		public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
		{
			return await GetAllActives(tracking).FirstOrDefaultAsync(expression);
		}

		public async Task<TEntity?> GetByIdAsync(Guid id, bool trancking = true)
		{
			return await GetAllActives(trancking).FirstOrDefaultAsync(x=> x.Id == id);
		}

		public int SaveChage()
		{
			return _dbContext.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _dbContext.SaveChangesAsync();
		}
		public IQueryable<TEntity> GetAllActives(bool tracking = true)
		{
			var values = _table.Where(x=> x.Status != Domain.Enums.Status.Deleted);
			return tracking ? values : values.AsNoTracking();
		}

		public async Task<TEntity> UpdateAsync(TEntity entity)
		{
			return await Task.FromResult(_table.Update(entity).Entity);
		}
	}
}
