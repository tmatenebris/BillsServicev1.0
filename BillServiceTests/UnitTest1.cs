using BillsServiceLibrary.Models;
using System.Configuration;

namespace BillServiceTests
{
    public class BillsRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UpdateUserTest()
        {
            Users user = new Users();
            user.UserId = int.MaxValue;

            string response = BillsServiceLibrary.BillsRepository.UpdateUser(user);

            Assert.AreEqual("error", response);

        }

        [Test]
        public void UpdateUserTest2()
        {
            Users user = new Users();
            user.UserId = -1;

            Assert.Throws<ArgumentException>(() => BillsServiceLibrary.BillsRepository.UpdateUser(user));

        }

        [Test]
        public void GetPdfTest()
        {
            int billid = 0;
            ConfigurationManager.AppSettings.Set("FolderPath", @"C:\Users\Dawid\OneDrive\Pulpit\za\");

            
            Assert.DoesNotThrow(() => BillsServiceLibrary.BillsRepository.GetPdf(billid));
        
            

        }

        [Test]
        public void GetUserDataTest()
        {
            int userId = -1;

            Assert.Throws<ArgumentException>(() => BillsServiceLibrary.BillsRepository.GetUserData(userId));

        }

    
        
    }
}