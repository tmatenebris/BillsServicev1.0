using Aspose.Pdf;
using BillsServiceLibrary;
using BillsServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MyBillsService
{
    public class BillsServiceController : IBillsService
    {
        private static EventLog _serviceEventLog = new EventLog();
        public BillsServiceController()
        {
            _serviceEventLog.Source = "BillsServiceSource";
            _serviceEventLog.Log = "BillsServiceLog";
        }
        
        public string Hello()
        {
            return "Hello";
        }

        public async Task<string> GetBills(int userID)
        {
            _serviceEventLog.WriteEntry("Get bills, bill_id: " + userID.ToString());
           var result = await Task<List<Bills>>.Factory.StartNew(() => BillsRepository.GetBills(userID));
            return JsonSerializer.Serialize(result);

        }

        public async Task<string> Register(string username, string first_name, string last_name, string password, string email, string phone)
        {
            _serviceEventLog.WriteEntry("Register : " + username);
            var result = await Task<string>.Factory.StartNew(() => BillsRepository.Register(username, first_name, last_name, password, email, phone));
            return JsonSerializer.Serialize(result);
        }

        public async Task<string> Login(string username, string password)
        {
            _serviceEventLog.WriteEntry("Login: " + username);
            var result = await Task<Users>.Factory.StartNew(() => BillsRepository.Login(username, password));
            return JsonSerializer.Serialize(result);
        }
        public async Task<string> GetLiabilities(int userId)
        {
            _serviceEventLog.WriteEntry("GetLiabilities, user_id: " + userId.ToString());
            var result = await Task<List<Bills>>.Factory.StartNew(() => BillsRepository.GetLiabilities(userId));
            return JsonSerializer.Serialize(result);
        }

        public async Task<string> GetUserData(int userId)
        {
            _serviceEventLog.WriteEntry("GetUserData, user_id: " + userId.ToString());
            var result = await Task<Users>.Factory.StartNew(() => BillsRepository.GetUserData(userId));
            return JsonSerializer.Serialize(result);
        }

        public async Task<string> UpdateUser(string serialized)
        {
            _serviceEventLog.WriteEntry("Update user");
            var newUserData = JsonSerializer.Deserialize<Users>(serialized);
            var result = await Task<string>.Factory.StartNew(() => BillsRepository.UpdateUser(newUserData));
            return JsonSerializer.Serialize(result);
        }

        public async Task<string> DeleteUser(int userId)
        {
            _serviceEventLog.WriteEntry("Delete user, user_id: " + userId.ToString());
            var result = await Task<string>.Factory.StartNew(() => BillsRepository.DeleteUser(userId));
            return JsonSerializer.Serialize(result);
        }

        public async Task<byte[]> GetPdf(int billId)
        {
            _serviceEventLog.WriteEntry("GetPdf, billId: " + billId.ToString());
            return await Task<byte[]>.Factory.StartNew(() => BillsRepository.GetPdf(billId));
           
        }
    }
}
