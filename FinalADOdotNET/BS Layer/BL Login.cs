using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace FinalADOdotNET.BS_Layer
{
    public class BL_Login
    {

       
        

        #region Hàm xử lí 

        public bool Login(string userName, string passWord)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var accounts =
                from p in milkShopEntity.Accounts
                where p.userName == userName && p.passWord == passWord
                select p;
            DataTable dt = new DataTable();
           
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên hiển thị");
            dt.Columns.Add("Tên đăng nhập");
            dt.Columns.Add("Mật khẩu");
            dt.Columns.Add("Loại");
            foreach (var p in accounts)
            {
                dt.Rows.Add(p.accountID, p.displayName, p.userName, p.passWord, p.type);
            }
            return dt.Rows.Count > 0;
            
        }


        #endregion 

    }
}
