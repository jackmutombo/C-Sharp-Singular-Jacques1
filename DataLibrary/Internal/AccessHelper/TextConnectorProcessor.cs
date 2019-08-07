using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Internal.AccessHelper
{
    public static class TextConnectorProcessor
    {
        // Files name 
        public const string UserFile = "UserModels.csv";
        public const string AddressFile = "AddressModels.csv";
        public const string ProductFile = "ProductModels.csv";
        public const string BasketFile = "BasketModels.csv";
        public const string BasketDetailFile = "BasketDetailModels.csv";

        /*
         *  This method call FullFilePath
         *  goes in the web config get the file path
         *  once return the full path with the file name
         */
        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        /*
         *  Method calll Load File, which takes the file path and return
         *  all line in the line in a list and id the file do not exist it return
         *  an empty list
         */
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }


        /********************************User************************************/
        /*
         *  This method Called ConvertTo UserModel takes all line form the text file and convert
         *  to the object usermodel
         */

        public static List<UserModel> ConvertToUserModels(this List<string> lines)
        {
            List<UserModel> output = new List<UserModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                UserModel sf = new UserModel();
                sf.Id = int.Parse(cols[0]);
                sf.AuthUserId = cols[1];
                sf.FirstName = cols[2];
                sf.LastName = cols[3];
                sf.EmailAddress = cols[4];
                sf.CellPhone = cols[5];
                sf.CreateDate = DateTime.Parse(cols[6]);
                sf.Address = cols[1].LookupAddressByUserID();
                output.Add(sf);
            }
            return output;
        }

        public static void SaveToPeopleFile(this List<UserModel> models)
        {
            List<string> lines = new List<string>();

            foreach (UserModel u in models)
            {
                lines.Add($"{ u.Id },{ u.AuthUserId },{ u.FirstName },{ u.LastName },{ u.EmailAddress }," +
                          $"{ u.CellPhone },{u.CreateDate}");
            }

            File.WriteAllLines(UserFile.FullFilePath(), lines);
        }

        public static UserModel LookupUserById(this string AuthUserId)
        {
            List<string> staffs = UserFile.FullFilePath().LoadFile();

            foreach (string staff in staffs)
            {
                string[] cols = staff.Split(',');
                if (cols[1] == AuthUserId)
                {
                    List<string> matchingStaff = new List<string>();
                    matchingStaff.Add(staff);
                    return matchingStaff.ConvertToUserModels().First();
                }
            }
            return null;
        }


        /********************************Address************************************/

        /*
         * ConvertToAddressModel takes the lines from the text file and convert to address model
         *
         */
        public static List<AddressModel> ConvertToAddressModels(this List<string> lines)
        {
            //id,Type,Name,Street,Suburb,city,PostalCode,AuthUserId
            List<AddressModel> output = new List<AddressModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                AddressModel ad = new AddressModel();
                ad.Id = int.Parse(cols[0]);
                ad.Type = cols[1];
                ad.AddressName = cols[2];
                ad.Street = cols[3];
                ad.Suburb = cols[4];
                ad.City = cols[5];
                ad.PostalCode = cols[6];
                ad.AuthUserId = cols[7];
                output.Add(ad);
            }
            return output;
        }

        /*
         * This method lookupaddress look by id the address for a particula user
         */
        public static AddressModel LookupAddressById(this string id)
        {
            List<string> addressStrLines = AddressFile.FullFilePath().LoadFile();
            foreach (string addressStrLine in addressStrLines)
            {
                string[] colStr = addressStrLine.Split(',');
                if (colStr[0] == id)
                {
                    List<string> matchingAddress = new List<string>();
                    matchingAddress.Add(addressStrLine);
                    return matchingAddress.ConvertToAddressModels().First();
                }
            }
            return null;
        }

        public static AddressModel LookupAddressByUserID(this string id)
        {
            List<string> addressStrLines = AddressFile.FullFilePath().LoadFile();
            foreach (string addressStrLine in addressStrLines)
            {
                string[] colStr = addressStrLine.Split(',');
                if (colStr[7] == id)
                {
                    List<string> matchingAddress = new List<string>();
                    matchingAddress.Add(addressStrLine);
                    return matchingAddress.ConvertToAddressModels().First();
                }
            }
            return null;
        }


        public static void SaveToAddressFile(this List<AddressModel> models)
        {
            List<string> lines = new List<string>();

            foreach (AddressModel ad in models)
            {
                lines.Add($"{ ad.Id },{ ad.Type },{ ad.AddressName },{ ad.Street },{ ad.Suburb },{ ad.City },{ ad.PostalCode },{ad.AuthUserId}");
            }
            File.WriteAllLines(AddressFile.FullFilePath(), lines);
        }
        /**********************************************************Product*******************************************/

        public static List<ProductModel> ConvertToProductModels(this List<string> lines)
        {
            //id,productName,description,Price,Quantity,Image
            List<ProductModel> output = new List<ProductModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                ProductModel pr = new ProductModel();
                pr.Id = int.Parse(cols[0]);
                pr.ProductName = cols[1];
                pr.Description = cols[2];
                pr.Price = decimal.Parse(cols[3]);
                pr.Image = Convert.FromBase64String(cols[4]);//https://www.dotnetperls.com/tobase64string
                output.Add(pr);
            }
            return output;
        }

        public static void SaveToProductFile(this List<ProductModel> models)
        {
            List<string> lines = new List<string>();

            foreach (ProductModel p in models)
            {
                string image = Convert.ToBase64String(p.Image);
                lines.Add($"{ p.Id },{ p.ProductName },{ p.Description },{ p.Price },{image}");
            }

            File.WriteAllLines(ProductFile.FullFilePath(), lines);
        }

        public static ProductModel LookupProductById(this string id)
        {
            List<string> productStrLines = ProductFile.FullFilePath().LoadFile();
            foreach (string productStrLine in productStrLines)
            {
                string[] colStr = productStrLine.Split(',');
                if (colStr[0] == id)
                {
                    List<string> matchingProduct = new List<string>();
                    matchingProduct.Add(productStrLine);
                    return matchingProduct.ConvertToProductModels().First();
                }
            }
            return null;
        }

        /***********************************************Basket***********************************************/


        public static List<BasketModel> ConvertToBasketModels(this List<string> lines)
        {
            //id,totalAmount,time,active,staff_id,basketDetail_id1|basketDetail_id2|basketDetail_id3
            List<BasketModel> outputList = new List<BasketModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                BasketModel bsk = new BasketModel();
                bsk.Id = int.Parse(cols[0]);
                if (cols[1].Length == 0)
                {
                    bsk.TotalAmount = decimal.Zero;
                }
                else
                {
                    bsk.TotalAmount = decimal.Parse(cols[1]);
                }
                bsk.Time = DateTime.Parse(cols[2]);
                bsk.Active = Boolean.Parse(cols[3]);
                bsk.OrderBy = (cols[4]).LookupUserById();

                string[] basketDetail_id = new string[0];
                if (!cols[5].Contains('|'))
                {
                    if (cols[5] != "")
                    {
                        basketDetail_id = new string[1];
                        basketDetail_id[0] = cols[5];
                    }
                    else
                    {
                        //been take care in the for loop and size = 0
                    }
                }
                else
                {
                    basketDetail_id = cols[5].Split('|');
                }
                foreach (string id in basketDetail_id)
                {
                    bsk.BasketDetails.Add(id.LookupBasketDetailById());
                }

                outputList.Add(bsk);
            }
            return outputList;
        }


        public static void SaveToBasketFile(this List<BasketModel> models)
        {
            //id,totalAmount,time,active,userID,basketDetail_id1|basketDetail_id2|basketDetail_id3
            List<string> lines = new List<string>();
            foreach (BasketModel or in models)
            {
                string totalAm = "";
                if (or.TotalAmount != decimal.Zero)
                {
                    totalAm = or.TotalAmount.ToString();
                }
                lines.Add($"{or.Id},{totalAm},{or.Time},{or.Active},{or.OrderBy.AuthUserId},{or.BasketDetails.ConvertBasketDetailListToString()}");
            }
            File.WriteAllLines(BasketFile.FullFilePath(), lines);
        }

        private static string ConvertBasketDetailListToString(this List<BasketDetailModel> basketDetails)
        {
            if (basketDetails.Count == 0)
            {
                return "";
            }
            string output = "";
            foreach (BasketDetailModel ordD in basketDetails)
            {
                if (ordD.Quantity < 1) continue;
                output += $"{ordD.Id}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }
        public static BasketModel LookupBasketById(this string id)
        {
            List<string> basketLines = BasketFile.FullFilePath().LoadFile();
            foreach (string basketLine in basketLines)
            {
                string[] cols = basketLine.Split(',');
                if (cols[0] == id)
                {
                    List<string> matchingBasket = new List<string>();
                    matchingBasket.Add(basketLine);
                    return matchingBasket.ConvertToBasketModels().First();
                }
            }
            return null;
        }



        /************************************************BasketDeatil************************************/

        public static BasketDetailModel LookupBasketDetailById(this string id)
        {
            List<string> basketDetailLines = BasketDetailFile.FullFilePath().LoadFile();
            foreach (string basketDetailLine in basketDetailLines)
            {
                string[] cols = basketDetailLine.Split(',');
                if (cols[0] == id)
                {
                    List<string> matchingBasketDetail = new List<string>();
                    matchingBasketDetail.Add(basketDetailLine);
                    return matchingBasketDetail.ConvertToBasketDetailModels().First();
                }
            }
            return null;
        }
        public static List<BasketDetailModel> ConvertToBasketDetailModels(this List<string> lines)
        {
            List<BasketDetailModel> outputList = new List<BasketDetailModel>();
            //id,quantity,price,productId
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                BasketDetailModel basketDetail = new BasketDetailModel();
                basketDetail.Id = Int32.Parse(cols[0]);
                basketDetail.Quantity = Int32.Parse(cols[1]);
                basketDetail.Price = Decimal.Parse(cols[2]);
                basketDetail.Product = cols[3].LookupProductById();
                basketDetail.Time = DateTime.Parse(cols[4]);
                outputList.Add(basketDetail);
            }
            return outputList;
        }

        public static void SaveToBasketDetailFile(this List<BasketDetailModel> models)
        {
            //id,quantity,price,productId
            List<string> lines = new List<string>();
            foreach (BasketDetailModel od in models)
            {
                lines.Add($"{od.Id},{od.Quantity},{od.Price},{od.Product.Id},{od.Time}");
            }
            File.WriteAllLines(BasketDetailFile.FullFilePath(), lines);
        }
    }
}
