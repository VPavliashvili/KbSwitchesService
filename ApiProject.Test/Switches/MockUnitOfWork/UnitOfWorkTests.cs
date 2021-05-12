using ApiProject.Services.UnitsOfWork;
using Xunit;

namespace ApiProject.Test.Switches.MockUnitOfWorkPart
{
    public class UnitOfWorkTests
    {

        private IUnitOfWork UnitOfWork => new MockUnitOfWork();

        [Fact]
        public void Test_IUnitOfWork_Interface()
        {
            Assert.NotNull(UnitOfWork);
        }

        [Fact]
        public void Complete_Method_ShouldReturnTrue()
        {
            Assert.True(UnitOfWork.Complete() > 0);
        }
    }
}