using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
namespace FinalADOdotNET.BS_Layer
{
    class BL_QuanLiSua
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
            foreach(var p in milks)
            {
                dt.Rows.Add(p.milkID, p.MilkName, p.price, p.extantAmount, p.soldAmount);
            }
            return dt;
            
        }
        public bool AddMilk(string name, float price,int extantAmount ,ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Milk newMilk = new Milk();
            newMilk.MilkName = name;
            newMilk.price = price;
            newMilk.extantAmount = extantAmount;
            milkShopEntity.Milk.Add(newMilk);
            milkShopEntity.SaveChanges();
            return true;
            
        }
        public bool DeleteMilk(ref string err, int milkID)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Milk newMilk = new Milk();
            newMilk.milkID = milkID;
            milkShopEntity.Milk.Attach(newMilk);
            milkShopEntity.Milk.Remove(newMilk);
            milkShopEntity.SaveChanges();
            return true;
            
        }
        public bool UpdateMilk(int milkID, string name, float price,int extantAmount, ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var mQuery = (from m in milkShopEntity.Milk
                           where m.milkID == milkID
                           select m).SingleOrDefault();
            if (mQuery != null)
            {
                mQuery.MilkName = name;
                mQuery.price = price;
                mQuery.extantAmount = extantAmount;
                milkShopEntity.SaveChanges();
            }
            return true;
            
        }
        public DataTable FindInterestedMilk()
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var maxSoldAmount = milkShopEntity.Milk.Max(x => x.soldAmount);
            var milks =
                from p in milkShopEntity.Milk
                where p.soldAmount == maxSoldAmount
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

        #endregion

    }
}
