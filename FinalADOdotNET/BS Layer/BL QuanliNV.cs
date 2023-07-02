using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace FinalADOdotNET.BS_Layer
{
    public class BL_QuanliNV
    {

       

        #region Hàm xử lí 

        public DataTable GetEmployee()
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var ems =
                from p in milkShopEntity.Employees
                select p;
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên nhân viên:");
            dt.Columns.Add("Lương");
            dt.Columns.Add("Số giờ làm");
            foreach (var p in ems)
            {
                dt.Rows.Add(p.ID, p.name, p.salary, p.workhour);
            }
            return dt;
        }
        public bool AddEmployee( string name,float workhour ,ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Employee newEm = new Employee();
            newEm.name = name;
            newEm.workhour = workhour;
            milkShopEntity.Employees.Add(newEm);
            milkShopEntity.SaveChanges();
            return true;
            
        }
        public bool DeleteEmployee(ref string err, int IDEmployee)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            Employee newEm = new Employee();
            newEm.ID = IDEmployee;
            milkShopEntity.Employees.Attach(newEm);
            milkShopEntity.Employees.Remove(newEm);
            milkShopEntity.SaveChanges();
            return true;
            
        }
        public bool UpdateEmployee(int IDEmployee, string name,  float workhour, ref string err)
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var emQuery = (from em in milkShopEntity.Employees
                           where em.ID == IDEmployee
                           select em).SingleOrDefault();
            if (emQuery != null)
            {
                emQuery.name = name;
                emQuery.workhour = workhour;
                milkShopEntity.SaveChanges();
            }
            return true;
            
        }
        public DataTable FindEmployee()
        {
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();
            var maxWorkHour = milkShopEntity.Employees.Max(x => x.workhour);
            var ems =
                from p in milkShopEntity.Employees
                where p.workhour == maxWorkHour
                select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tên nhân viên:");
            dt.Columns.Add("Lương");
            dt.Columns.Add("Số giờ làm");
            foreach (var p in ems)
            {
                dt.Rows.Add(p.ID, p.name, p.salary, p.workhour);
            }
            return dt;
            

        }
        public bool  ComputeSalary( ref string err)
        {
            int salaryPerHour = 25000;
            MilkShopProjectEntities milkShopEntity = new MilkShopProjectEntities();

            
            var validEmployees = from em in milkShopEntity.Employees
                                 select em;
            foreach (var e in validEmployees)
            {
                    e.salary = e.workhour * salaryPerHour;
            }
                milkShopEntity.SaveChanges();
           
            return true;
            
        }

        #endregion

    }
}
