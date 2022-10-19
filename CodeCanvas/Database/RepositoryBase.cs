using System.Linq.Expressions;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace CodeCanvas.Database
{
    public class RepositoryBase<T> where T : class
    {
        protected ApplicationDbContext repositoryContext { get; set; }
        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.repositoryContext.Set<T>();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.repositoryContext.Set<T>()
                .Where(expression);
        }
        public void Create(T entity)
        {
            this.repositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            this.repositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            this.repositoryContext.Set<T>().Remove(entity);
        }
        public async Task SaveAsync()
        {
            await this.repositoryContext.SaveChangesAsync();
        } 
    }
}
