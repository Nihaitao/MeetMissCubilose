using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;
using System.Web;
using System.IO;
using Common;

namespace MissMeetCubilose.ApiControllers.Product
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [HttpGet]
        public dynamic GetProductList()
        {
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetProductList();
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.state = item.product_state;
                p.img = item.product_img;
                pList.Add(p);
            }
            return pList;
        }
        [HttpGet]
        public dynamic GetNormalProductList()
        {
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetNormalProductList();
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.img = item.product_img;
                p.info = item.product_info;
                pList.Add(p);
            }
            return pList;
        }
        [HttpGet]
        public dynamic GetSearchNormalProductList()
        {
            string name = Fun.Query("name");
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetSearchNormalProductList(name);
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.img = item.product_img;
                p.info = item.product_info;
                pList.Add(p);
            }
            return pList;
        }
        [HttpGet]
        public dynamic GetSearchProductList()
        {
            string name = Fun.Query("name");
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetSearchProductList(name);
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.state = item.product_state;
                pList.Add(p);
            }
            return pList;
        }
        [HttpGet]
        public dynamic GetProduct()
        {
            int id = Fun.Query("id", 0);
            ProductBLL bll = new ProductBLL();
            P_ProductInfo p = bll.GetProduct(id);
            List<P_Cubilose> list = null;
            ProductInfo product = new ProductInfo();
            product.id = p.product_id;
            if (!string.IsNullOrEmpty(p.product_component))
            {
                list = new CubiloseBLL().GetCubiloseList();
                string[] arr = p.product_component.Split(',');
                string[] arr2 = p.component_num.Split('|');
                for (int i = 0; i < arr.Length; i++ )
                {
                    product.component += "<span data-id = '" + arr[i] + "'>" + list.Where(x => x.cubilose_id == int.Parse(arr[i])).FirstOrDefault().cubilose_name.Trim() + "(" + arr2[i] + ")</span>,";
                }
                product.component = product.component.Substring(0, product.component.Length - 1);
            }
            product.componentId = p.product_component;
            product.num = p.component_num;
            product.name = p.product_name.Trim();
            product.img = p.product_img;
            product.info = p.product_info;
            product.type = p.product_type;
            product.mode = p.product_mode.Trim();
            product.price = p.product_price;
            switch (p.product_type)
            {
                case 1:
                    product.typeDescript = "月卡";
                    break;
                case 2:
                    product.typeDescript = "礼盒";
                    break;
                case 3:
                    product.typeDescript = "干货";
                    break;
            }
            return product;
        }

        [HttpPost]
        public dynamic AddProduct()
        {
            string name = Fun.Form("name");
            int type = Fun.Form("type",0);
            string mode = Fun.Form("mode");
            decimal price = Fun.Form("price",0);
            string component = Fun.Form("component");
            string num = Fun.Form("num");
            string info = Fun.Form("info");
            string img = Fun.Form("img");
            ProductBLL bll = new ProductBLL();
            if (!bll.AddProduct(name, type, mode, component, num,price,info,img))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateProduct()
        {
            int id = Fun.Form("id",0);
            string name = Fun.Form("name");
            int type = Fun.Form("type", 0);
            string mode = Fun.Form("mode");
            decimal price = Fun.Form("price", 0);
            string component = Fun.Form("component");
            string num = Fun.Form("num");
            string info = Fun.Form("info");
            string img = Fun.Form("img");
            ProductBLL bll = new ProductBLL();
            if (!bll.UpdateProduct(id, name, type, mode, component, num, price, info, img))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic DeleteProduct()
        {
            int id = Fun.Form("id", 0);
            int state = Fun.Form("state", 0);
            ProductBLL bll = new ProductBLL();
            if (!bll.DeleteProduct(id, state))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic DeleteCustomerProducts()
        {
            int productId = Fun.Form("productId", 0);
            int customerId = Fun.Form("customerId", 0);
            int type = Fun.Form("type", -1);
            ProductBLL bll = new ProductBLL();
            if (!bll.DeleteCustomerProducts(productId, customerId, type))
                return "失败";
            else
                return "成功";
        }

        [HttpGet]
        public dynamic GetCustomerProductList()
        {
            int type = Fun.Query("type", -1);
            int customerId = Fun.Query("customerId", 0);
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetCustomerProductList(customerId,type);
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.img = item.product_img;
                p.info = item.product_info;
                p.state = item.product_state;
                p.price = item.product_price;
                pList.Add(p);
            }
            return pList; 
        }
        [HttpGet]
        public dynamic GetSearchCustomerProductList()
        {
            int type = Fun.Query("type", -1);
            int customerId = Fun.Query("customerId", 0);
            string name = Fun.Query("name");
            ProductBLL bll = new ProductBLL();
            List<P_ProductInfo> list = bll.GetSearchCustomerProductList(customerId, type, name);
            List<ProductInfo> pList = new List<ProductInfo>();
            ProductInfo p = null;
            foreach (var item in list)
            {
                p = new ProductInfo();
                p.id = item.product_id;
                p.name = item.product_name.Trim();
                p.img = item.product_img;
                p.info = item.product_info;
                p.state = item.product_state;
                pList.Add(p);
            }
            return pList;
        }
        [HttpPost]
        public dynamic UploadImages() 
        {
            string type = Fun.Form("type");
            HttpRequest request = System.Web.HttpContext.Current.Request;
            Upload Up = new Upload();
            System.Web.HttpFileCollection uploadFiles = request.Files;//获取需要上传的文件
            string fileUrl = Up.SaveAs("img", uploadFiles[0], uploadFiles[0].FileName, type);
            return fileUrl;
        }
    }
}
