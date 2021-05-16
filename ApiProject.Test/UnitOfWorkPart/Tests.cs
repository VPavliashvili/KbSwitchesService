using ApiProject.Services.Repositories;
using ApiProject.Services.UnitsOfWork;
using Xunit;

namespace ApiProject.Test.UnitOfWorkPart
{
    public class Tests
    {

        private IUnitOfWork UnitOfWork => new MockUnitOfWork();

        [Fact]
        public void ShouldReturnPositive_WhenCompleting()
        {
            Assert.True(UnitOfWork.Complete() > 0);
        }

        [Fact]
        public void ShouldReturnNegative_WhenCompletingIsMocked()
        {
            IUnitOfWork unitOfWork = new MockUnitOfWork();
            ((MockUnitOfWork)unitOfWork).EnableMethodMocking();

            Assert.False(UnitOfWork.Complete() < 0);
        }

    }
}