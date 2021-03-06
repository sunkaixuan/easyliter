﻿using Sqlite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easyliter;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            var connstr = "DataSource=" + System.AppDomain.CurrentDomain.BaseDirectory + "mapping.sqllite";

            //reference System.Data.SQLite
            //引用 System.Data.SQLite
            Client e = new Client(connstr);

            //Generate entity classes from a database
            //从数据库生成实体类
            //CreateClassFile(e);

            //Delete operation
            //删除操作
            DeleteData(e);

            //update operation
            //更新操作
            UpdateData(e);

            //insert operation
            //插入数据
            InsertData(e);

            //Search operation
            //查询操作
            Search(e);

            //基本操作
            //Basic operation
            BasicOperation(e);
        }




        //search operation
        //查询操作
        private static void Search(Client e)
        {
            ////By sql
            List<Product> list = e.Select<Product>("select * from product where  id>@num", new { num = 100 });

            //No parameter
            List<Category> list2 = e.Select<Category>();

            //Single parameter
            List<Product> list3 = e.Select<Product>(x => x.id > 200);

            //Multiple parameter
            List<Product> list4 = e.Select<Product>(x => x.id > 200,
                                                    x => x.sku == "skx" || x.sku == null);
            //By page
            int count = 0;
            List<Product> list5 = e.SelectPage<Product>(1, 10, ref count, " id  desc",
                                            x => x.id > 10,//条件1
                                            x => true);//条件2 ...条件N


            //Query extenions
            var extObj = e.Query<Product>().Where(x => x.id > 10).Where(x => x.id > 2).Select("id,sku")
                .OrderBy(El_Sort.asc, "id")
                .OrderBy(El_Sort.desc, "sku").Take(100);

            //get list
            var list6 = extObj.ToList();

            //get dataTable
            var dataTable = extObj.ToDataTable();

            //get single
            int num = 500;
            var item = e.Query<Product>().Where(c=>c.id==num).Single();

            //get first
            var first = extObj.First();

            //get sql
            string sql = extObj.ToSql();

            //left join
            List<V_Product> VProductList = e.Query<Product>()
                .Join<Product, Category>(false /* true is  inner join*/ ) 
                .On(" Product.category_id=Category.id ")
                .OrderBy(El_Sort.desc, " Product.id")
                .Select("Product.*,Category.name as category_name").JoinWhere("Product.id>300").ToNewList<Product, V_Product>();

            //left join
            string VProductSql = e.Query<Product>()
                 .Join<Product, Category>(false /* true is  inner join*/ )
                 .On(" Product.category_id=Category.id ")
                 .OrderBy(El_Sort.desc, " Product.id")
                 .Select("Product.*,Category.name as category_name").JoinWhere("Product.id>300").ToSql();
        }


        //insert operation
        //插入数据
        private static void InsertData(Client e)
        {
            //添加
            Product p = new Product()
            {
                category_id = 2,
                sku = "sku",
                title = "title"
            };
            e.Insert<Product>(p);
        }


        //update operation
        //更新操作
        private static void UpdateData(Client e)
        {
            e.Update<Product>(new { sku = "AGA123101", category_id = 1 } /*update columns*/, new { id = 434 }/*where columns*/);
        }


        //Delete operation
        //删除操作
        private static void DeleteData(Client e)
        {
            e.Delete<Product>(100);//primary key
            e.Delete<Product>(new int[] { 1, 2, 3 });
        }

        //Generate entity classes from a database
        //从数据库生成实体类
        private static void CreateClassFile(Client e)
        {
            //by database
            var createCalss1 = e.CreateClass("Sqlite.Model"/*命名空间*/, @"D:\TFS\EmailBackup\Easyliter\Test\model"/*路径*/);

            
            //by sql
            var createCalss2 = e.CreateClassBySql("Sqlite.Model", @"D:\TFS\EmailBackup\Easyliter\Test\model1", "viewproduct", "select id,sku from product ");
        }

        //基本操作
        //Basic operation
        private static void BasicOperation(Client e)
        {
            var dt = e.GetDataTable("select * from product");
            var intVal = e.GetInt("select count(*) from product");
            var stringVal = e.GetString("select sku from product where id=500 ");
            //e.ExecuteNonQuery("inset into ..");
        }
    }
}
