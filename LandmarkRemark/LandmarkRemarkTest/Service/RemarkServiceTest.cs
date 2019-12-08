using LandmarkRemarkData;
using LandmarkRemarkModel;
using LandmarkRemarkService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LandmarkRemarkTest.Service
{
    public class RemarkServiceTest
    {
        private Mock<Context> _mockLandmarkContext;
        private Mock<DbSet<Remark>> _mockRemarkDbSet;

        [SetUp]
        public void Setup()
        {
            var remarks = new TestAsyncEnumerable<Remark>(new List<Remark>()
            {
                new Remark()
                {
                    Id = 1,
                    Note = "Sample note 1",
                    User =  new User(){Id=1},
                    UserId =1
                }, new Remark()
                {
                     Id = 2,
                    Note = "Sample note 2",
                    User =  new User(){Id=1},
                    UserId =1
                }, new Remark()
                {
                      Id = 3,
                    Note = "Sample note 3",
                    User =  new User(){Id=2},
                    UserId =2
                }
            }.AsQueryable());
            _mockLandmarkContext = new Mock<Context>(new DbContextOptions<Context>());
            _mockRemarkDbSet = new Mock<DbSet<Remark>>();
            _mockLandmarkContext.Setup(m => m.Remarks).Returns(() => _mockRemarkDbSet.Object);

            var queryable = remarks.AsQueryable();
            _mockRemarkDbSet.As<IQueryable<Remark>>().Setup(m => m.Provider).Returns(queryable.Provider);
            _mockRemarkDbSet.As<IQueryable<Remark>>().Setup(m => m.Expression).Returns(queryable.Expression);
            _mockRemarkDbSet.As<IQueryable<Remark>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            _mockRemarkDbSet.As<IQueryable<Remark>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            _mockLandmarkContext.Setup(p => p.Set<Remark>().Add(It.IsAny<Remark>())).Returns(()=>null);
        }

        [Test]
        public async Task ToValidateAllRemarksAreReturned()
        {
            var sut = new RemarkService(_mockLandmarkContext.Object);

            var result = await sut.GetAllRemarks();

            Assert.AreEqual(result.Count(), 3);
        }

        [Test]
        public async Task ShouldSuccessfullyAddARemark()
        {
            var sut = new RemarkService(_mockLandmarkContext.Object);

            var result = await sut.RemarkLandmark(new Remark() {Note="abc", User= new User() {Id=2,Username="test" } });

            _mockRemarkDbSet.Verify(m => m.Add(It.IsAny<Remark>()), Times.Once());
            
        }
    }
}