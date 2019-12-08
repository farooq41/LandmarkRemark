using LandmarkRemarkData;
using LandmarkRemarkModel;
using LandmarkRemarkService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemarkTest.Service
{
    public class UserServiceTest
    {
        private Mock<Context> _mockLandmarkContext;
        private Mock<IOptions<AppSettings>> _mockAppsettings;
        private Mock<DbSet<User>> _mockUserDbSet;

        [SetUp]
        public void Setup()
        {
            var users = new TestAsyncEnumerable<User>(new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Username = "abc",
                    Password = "abc",
                    
                }, new User()
                {
                     Id = 2,
                    Username = "abc2",
                    Password = "abc",
                }, new User()
                {
                      Id = 3,
                    Username = "abc3",
                    Password = "abc"
                }
            }.AsQueryable());
            _mockLandmarkContext = new Mock<Context>(new DbContextOptions<Context>());
            _mockAppsettings = new Mock<IOptions<AppSettings>>();
            _mockUserDbSet = new Mock<DbSet<User>>();
            _mockLandmarkContext.Setup(m => m.Users).Returns(() => _mockUserDbSet.Object);

            var queryable = users.AsQueryable();
            _mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryable.Provider);
            _mockUserDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryable.Expression);
            _mockUserDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            _mockUserDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            _mockLandmarkContext.Setup(p => p.Set<User>().Add(It.IsAny<User>())).Returns(()=>null);
        }

        [Test]
        public async Task ShouldSuccessfullyAddAUser()
        {
            var sut = new UserService(_mockLandmarkContext.Object, _mockAppsettings.Object);

            var result = await sut.CreateUser(new User() {Username="abc4"});

            _mockUserDbSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            
        }
    }
}