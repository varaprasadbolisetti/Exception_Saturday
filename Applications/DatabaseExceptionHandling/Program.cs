using Microsoft.Extensions.Configuration;
using System;
using System.IO;

using DatabaseExceptionHandling.DataAccessLayer;

namespace DatabaseExceptionHandling
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
           
            ShowDepartments();
            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }
       
        static void ShowDepartments()
        {
            var deptDAL = new DepartmentDAL(_iconfiguration);
            var lstDepartment = deptDAL.GetAllDepartments();
            lstDepartment.ForEach(item =>
            {
                Console.WriteLine($"DeptID: {item.DepartmentID}" +
                    $" Name: {item.Name}" +
                    $" Grp Name: {item.GroupName}" +
                    $" Date: {item.ModifiedDate.ToShortDateString()}");
            });
        }
    }
}
