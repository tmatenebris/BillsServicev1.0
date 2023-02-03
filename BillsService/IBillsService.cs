using Aspose.Pdf;
using BillsServiceLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyBillsService
{

    [ServiceContract]
    public interface IBillsService
    {

        [OperationContract]
        string Hello();

        [OperationContract]
        Task<string> GetBills(int userId);

        [OperationContract]
        Task<string> Register(string username, string first_name, string last_name, string password, string email, string phone);

        [OperationContract]
        Task<string> Login(string username, string password);

        [OperationContract]
        Task<string> GetLiabilities(int userId);


        [OperationContract]
        Task<string> GetUserData(int userId);

        [OperationContract]
        Task<string> UpdateUser(string serialized);

        [OperationContract]
        Task<string> DeleteUser(int userId);

        [OperationContract]
        Task<byte []> GetPdf(int billId);

    }
}
