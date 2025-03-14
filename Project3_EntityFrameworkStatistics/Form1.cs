using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3_EntityFrameworkStatistics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Db3Project20Entities db = new Db3Project20Entities();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Toplam Kategori sayısı
            int categoryCount = db.TblCategory.Count();
            lblCategoryCount.Text = categoryCount.ToString();

            //Toplam Ürün sayısı
            int productCount = db.TblProduct.Count();
            lblProductCount.Text = productCount.ToString();
            //Toplam Müşteri sayısı
            int customerCount = db.TblCustomer.Count();
            lblCustomerCount.Text = customerCount.ToString();
            //Toplam Sipariş sayısı
            int orderCount = db.TblOrder.Count();
            lblOrderCount.Text = orderCount.ToString();

            //Select sum(ProductStock) as Toplam_Stok  from TblProduct
            //Toplam Stok sayısı
            var totalProductStockCount = db.TblProduct.Sum(x => x.ProductStock);
            lblTotalProductStockCount.Text = totalProductStockCount.ToString();

            //Ortalama Ürün Fiyatı sayısı
            var productAvgPrice = db.TblProduct.Average(x => x.ProductPrice);
            lblProductAvgPrice.Text = productAvgPrice.ToString() + "₺" ;

            //Toplam meyve sayısı
            var fruitCount=db.TblProduct.Where(x=>x.CategoryId==1).Select(y=>y.ProductStock).Sum();
            lblTotalProductFruitCount.Text = fruitCount.ToString();

            //Gazoz Toplam İşlem Hacmi
           // select(ProductStock * ProductPrice) from TblProduct
             //where ProductName = 'Gazoz'
            var productPriceByProductNameGazozGetStock=db.TblProduct.Where(x=>x.ProductName=="Gazoz").Select(y=>y.ProductStock).FirstOrDefault();
            var productPriceByProductNameGazozGetPrice=db.TblProduct.Where(x=>x.ProductName=="Gazoz").Select(y=>y.ProductPrice).FirstOrDefault();
            var productPriceByProductNameGazozTotal = productPriceByProductNameGazozGetStock * productPriceByProductNameGazozGetPrice;
            lblTotalPriceByProductNameGazoz.Text = productPriceByProductNameGazozTotal.ToString() + "₺";

            //select COUNT(*) from TblProduct where ProductStock<100;
            //Stokta 100'den az olan ürün sayısı
            var productStockLessThan100Count = db.TblProduct.Where(x => x.ProductStock < 100).Count();
            lblProductStockSmaller100.Text = productStockLessThan100Count.ToString();

            //Aktif Sebze Stoğu
           var productStockCounntByCategoryNameIsSebzeAndStatusIsTrue=db.TblProduct.Where(x=>x.CategoryId==(db.TblCategory.Where(w=>w.CategoryName=="Sebze").Select(y=>y.CategoryId).FirstOrDefault())&&
           x.ProductStatus==true).Sum(z => z.ProductStock);
            lblCountByCategoryActiveSebzeStock.Text = productStockCounntByCategoryNameIsSebzeAndStatusIsTrue.ToString();

            //Türkiyeden yapılan siparişler Sql Query
            //select COUNT(*) from TblOrder where CustomerId In(select  CustomerId From TblCustomer Where CustomerCountry = 'Türkiye')

            var orderCountByCountryNameIsTurkey =db.Database.SqlQuery<int>("select COUNT(*) from TblOrder where CustomerId In(select  CustomerId From TblCustomer Where CustomerCountry = 'Türkiye')").FirstOrDefault();
            lblOrderCountFromTurkeySql.Text = orderCountByCountryNameIsTurkey.ToString();

            //Türkiyeden yapılan siparişler Ef metodu
            var turkishCustomerIds=db.TblCustomer.Where(x=>x.CustomerCountry=="Türkiye")
                                                       .Select(y => y.CustomerId).ToList();
            var orderCountFromTurkiyeWithEf = db.TblOrder.Count(z => turkishCustomerIds.Contains(z.CustomerId.Value));
            lblOrderCountFromTurkeyEf.Text = orderCountFromTurkiyeWithEf.ToString();

            //lblOrderTotalPriceByCategoryIsMeyveSql
            //Siparişler içerisindeki meyve kategorisindeki ürünlerin toplam satış fiyatı

            var OrderTotalPriceByCategoryIsMeyveSql = db.Database.SqlQuery<decimal>("Select Sum(o.TotalPrice) as TotalPrice\r\nFrom Tblorder o\r\nJoin TblProduct p On o.ProductId=p.ProductId\r\nJoin TblCategory c On p.CategoryId=p.CategoryId\r\nwhere c.CategoryName='Meyve'").FirstOrDefault();
            lblOrderTotalPriceByCategoryIsMeyveSql.Text = OrderTotalPriceByCategoryIsMeyveSql.ToString() + "₺";

            //lblOrderTotalPriceByCategoryIsMeyveEf
            var orderTotalPriceByCategoryIsMeyveEf = (from o in db.TblOrder
                                                      join p in db.TblProduct on o.ProductId equals p.ProductId
                                                      join c in db.TblCategory on p.CategoryId equals c.CategoryId
                                                      where c.CategoryName == "Meyve"
                                                      select o.TotalPrice).Sum();
            lblOrderTotalPriceByCategoryIsMeyveEf.Text = orderTotalPriceByCategoryIsMeyveEf.ToString() + "₺";


            //Son Eklenen Ürün Adı
            var lastProductName=db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.ProductName).FirstOrDefault();
            lblLastProductName.Text = lastProductName.ToString();

            //lblLastProdutNameByCategory

            //Son Eklenen Ürün kategori  Adı
            var lastProductNameByCategory = db.TblProduct.OrderByDescending(x => x.ProductId).Select(y => y.TblCategory.CategoryName).FirstOrDefault();
            lblLastProdutNameByCategory.Text = lastProductNameByCategory.ToString();

            //lblActiveProductCount
            //Aktif Ürün Sayısı
            var activeProductCount=db.TblProduct.Count(x => x.ProductStatus == true);
            lblActiveProductCount.Text = activeProductCount.ToString();

            //Toplam kola kazanç
            var productPriceByProductNameKolaGetStock = db.TblProduct.Where(x => x.ProductName == "Kola").Select(y => y.ProductStock).FirstOrDefault();
            var productPriceByProductNamekolaGetPrice = db.TblProduct.Where(x => x.ProductName == "Kola").Select(y => y.ProductPrice).FirstOrDefault();
            var productPriceByProductNameKolaTotal = productPriceByProductNameKolaGetStock * productPriceByProductNamekolaGetPrice;
            lblTotalPriceByProductNameKola.Text = productPriceByProductNameKolaTotal.ToString() + "₺";

            //son Sipariş veren müşteri adı
            var orderByLastCutomerName = db.TblOrder.OrderByDescending(x => x.OrderId).Select(y => y.TblCustomer.CustomerName).FirstOrDefault();
            lblOrderByLastCutomerName.Text = orderByLastCutomerName.ToString();

            //kaç farklı ülke var 
            //select COUNT(DISTINCT(CustomerCountry)) from TblCustomer
            var countryDifferentCount=db.TblCustomer.Select(x=>x.CustomerCountry).Distinct().Count();
            lblCountryDifferentCount.Text = countryDifferentCount.ToString();



        }


    }
}
