using ApiProject.Services.Repositories;
using ApiProject.Services.Contexts;

namespace ApiProject.Services.UnitsOfWork
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private readonly MockDbContext _context;

        public IMechaSwitchRepository Switches { get; private set; }

        public MockUnitOfWork(MockDbContext context)
        {
            _context = context;

            Switches = new MockMechaSwitchRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}