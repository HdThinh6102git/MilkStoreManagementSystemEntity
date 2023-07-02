using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;


namespace FinalADOdotNET.BS_Layer
{
    public class BL_HoaDon
    {

        

        #region Hàm xử lí

        public DataTable GetMilk()
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var milks =
                from p in milkShopEntity.Milk
                select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên sữa");
            dt.Columns.Add("Giá");
            dt.Columns.Add("Số lượng còn");
            dt.Columns.Add("Số lượng đã bán");
            foreach (var p in milks)
            {
                dt.Rows.Add(p.milkID, p.MilkName, p.price, p.extantAmount, p.soldAmount);
            }
            return dt;
           
        }

        public bool AddCustomer(string name, string gender,int phoneNum ,float totalBoughtMoney, ref string err)
        {
            
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Customer newCus = new Customer();
            newCus.name = name;
            newCus.gender = gender;
            newCus.phoneNum = phoneNum;
            newCus.totalBoughtMoney = totalBoughtMoney;
            milkShopEntity.Customers.Add(newCus);
            milkShopEntity.SaveChanges();
            return true;
            
        }
        public bool UpdateCustomer(int phoneNum, float totalBoughtMoney, ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var cusQuery = (from tp in milkShopEntity.Customers
                            where tp.phoneNum == phoneNum
                            select tp).SingleOrDefault();
            if(cusQuery != null)
            {
                cusQuery.totalBoughtMoney = totalBoughtMoney;
                milkShopEntity.SaveChanges();
            }
            return true;
           
        }

        public bool UpdateMilkAmount(int milkID,int extantAmount,int soldAmount, ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var milkQuery = (from m in milkShopEntity.Milk
                             where m.milkID == milkID
                             select m).SingleOrDefault();
            if(milkQuery != null)
            {
                milkQuery.extantAmount = extantAmount;
                milkQuery.soldAmount = soldAmount;
                milkShopEntity.SaveChanges();
            }
            return true;
            
        }

        public bool InsertBill(string cusName, int milkID, float totalPrice, int totalAmount, ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Bill newBill = new Bill();
            newBill.cusName = cusName;
            newBill.milkID = milkID;
            newBill.billDate = DateTime.Now;
            newBill.totalPrice = totalPrice;
            newBill.totalAmount = totalAmount;
            newBill.status = 1;
            milkShopEntity.Bills.Add(newBill);
            milkShopEntity.SaveChanges();
            
            return true;
        }

        public DataTable GetBillByDate(DateTime fromDate, DateTime toDate)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var bills =
                milkShopEntity.USP_GetBillBydate(fromDate, toDate);
            DataTable dt = new DataTable();
            dt.Columns.Add("Tên khách hàng");
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Tên sữa");
            dt.Columns.Add("Số lượng");
            dt.Columns.Add("Tổng tiền");
            foreach (var p in bills)
            {
                dt.Rows.Add(p.Tên_khách_hàng, p.Ngày, p.Tên_sữa, p.Số_lượng, p.Tổng_tiền);
            }
            return dt;
           
        }

        public bool UpdateStatusBill(ref string err)
        {
            
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();


            var validBills = from b in milkShopEntity.Bills
                                 select b;
            foreach (var e in validBills)
            {
                e.status = 0;
            }
            milkShopEntity.SaveChanges();

            return true;
        }


        #endregion 


    }
}
