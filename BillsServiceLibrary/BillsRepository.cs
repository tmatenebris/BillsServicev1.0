using BillsServiceLibrary.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsServiceLibrary.Map;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Aspose.Pdf;
using System.Configuration;

namespace BillsServiceLibrary
{
    public static class BillsRepository
    {
       
        private static string path = ConfigurationManager.AppSettings["FolderPath"];
        public static void AddBills(string filePath)
        {
            
                using (BillsContext _context = new BillsContext())
                {
                    using (var reader = new StreamReader(filePath))
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            Delimiter = ";"
                        };
                        using (var csv = new CsvReader(reader, config))
                        {
                            csv.Context.RegisterClassMap<BillMap>();
                            var records = csv.GetRecords<Bills>();


                            foreach (var record in records)
                            {
                                var seller = _context.Users.Where(x => x.FirstName == record.SellerFirstName && x.LastName == record.SellerLastName && x.PhoneNumber == record.SellerPhone).FirstOrDefault();
                                var buyer = _context.Users.Where(x => x.FirstName == record.BuyerFirstName && x.LastName == record.BuyerLastName).FirstOrDefault();

                                if (seller == null)
                                {
                                    Mailing.SendError(record.Product, seller.Email, seller.FirstName + " " + seller.LastName);
                                    continue;
                                }
                                if (buyer == null)
                                {
                                    Mailing.SendError(record.Product, buyer.Email, buyer.FirstName + " " + buyer.LastName);
                                    continue;
                                }
                                UsersXBills usersXBills = new UsersXBills();
                                _context.Bills.Add(record);
                                _context.SaveChanges();

                                usersXBills.BillId = record.BillId;
                                usersXBills.UserId = seller.UserId;
                                usersXBills.IsOwner = true;

                                _context.UsersXBills.Add(usersXBills);
                                _context.SaveChanges();
                                UsersXBills usersXBills2 = new UsersXBills();
                                usersXBills2.BillId = record.BillId;
                                usersXBills2.UserId = buyer.UserId;
                                usersXBills2.IsOwner = false;
                                _context.UsersXBills.Add(usersXBills2);
                                _context.SaveChanges();

                                Document pdf = PDFGenerator.GeneratePdf(record);
                                pdf.Save(path + record.BillId.ToString() + ".pdf");
                                Mailing.SendMail(record.BuyerEmail, record.Product, record.BuyerFirstName + " " + record.BuyerLastName, path + record.BillId.ToString() + ".pdf");




                            }

                            _context.SaveChanges();



                        }
                    }
                }
          
        }

        public static byte [] GetPdf(int billid)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(path + billid.ToString() + ".pdf", FileMode.Open, FileAccess.Read);
            try
            { 
                int length = (int)fileStream.Length;  
                buffer = new byte[length];           
                int count;                           
                int sum = 0;                        
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;

        }

        public static List<Bills> GetBills(int id)
        {
            List<Bills> bills = new List<Bills>();
                using (BillsContext _context = new BillsContext())
                {
                    var userXBills = _context.UsersXBills.Where(x => x.UserId == id && x.IsOwner == true).Include(x => x.Bill);
               
                    foreach (var rel in userXBills)
                    {
                        bills.Add(rel.Bill);
                    }

                }

            return bills; 
        }

        public static List<Bills> GetLiabilities(int id)
        {
            List<Bills> bills = new List<Bills>();
            using (BillsContext _context = new BillsContext())
            {
                var userXBills = _context.UsersXBills.Where(x => x.UserId == id && x.IsOwner == false).Include(x => x.Bill);

                foreach (var rel in userXBills)
                {
                    bills.Add(rel.Bill);
                }
            }

            return bills;
        }

        public static Users GetUserData(int id)
        {
          
            Users user = new Users();
            using (BillsContext _context = new BillsContext())
            {
                if (id < 0) throw new ArgumentException();
                var userData = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
                user = userData;
            }

            return user;
        }

        public static string UpdateUser(Users newData)
        {
            if (newData.UserId < 0) throw new ArgumentException();
            try
            {
                using (BillsContext _context = new BillsContext())
                {
                    var user = _context.Users.Where(x => x.UserId == newData.UserId).FirstOrDefault();
                    if (user == null) throw new ArgumentException();
                    else
                    {
                        user.Email = newData.Email;
                        user.FirstName = newData.FirstName;
                        user.LastName = newData.LastName;
                        user.PhoneNumber = newData.PhoneNumber;
                        _context.SaveChanges();
                    }
                }

                return "success";
            }
            catch(Exception ex)
            {
                return "error";
            }
          
        }

        public static string DeleteUser(int id)
        {
            try
            {
                using (BillsContext _context = new BillsContext())
                {
                    var user = _context.Users.Where(x => x.UserId == id).FirstOrDefault();
                    if (user == null) throw new ArgumentException();

                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }

                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }

        }

        public static string Register(string username, string first_name, string last_name, string password, string email, string phone)
        {
            string status = "error";
            try
            {
                using (BillsContext _context = new BillsContext())
                {
                    var user = _context.Users.Where(x => x.Username == username).FirstOrDefault();

                    if (user != null) throw new ArgumentException();

                    Users newUser = new Users();

                    newUser.Username = username;
                    newUser.Password = password;
                    newUser.Email = email;
                    newUser.PhoneNumber = phone;
                    newUser.FirstName = first_name;
                    newUser.LastName = last_name;
                 

                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    status = "success";
                }
            }
            catch(Exception ex)
            {
                status = "error";
            }

            return status;
        }


        public static Users Login(string username, string password)
        {
          
            try
            {
                using (BillsContext _context = new BillsContext())
                {
                    var user = _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

                    if (user == null) throw new ArgumentException();

                    return user;
   
                }
            }
            catch (Exception ex)
            {
                return null;
            }

          
        }
    }

    
}
