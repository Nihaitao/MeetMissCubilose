using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;
using BLL;

namespace MissMeetCubilose.ApiControllers.Product
{
    [RoutePrefix("api/Cubilose")]
    public class CubiloseController : ApiController
    {
        [HttpGet]
        public dynamic GetCubiloseList()
        {
            CubiloseBLL bll = new CubiloseBLL();
            List<P_Cubilose> list = bll.GetCubiloseList();
            List<Cubilose> cList = new List<Cubilose>();
            Cubilose cubilose = null;
            foreach (var item in list)
            {
                cubilose = new Cubilose();
                cubilose.id = item.cubilose_id;
                cubilose.name = item.cubilose_name.Trim();
                cubilose.info = item.cubilose_info;
                cubilose.state = item.cubilose_state;
                cubilose.img = item.cubilose_img;
                cList.Add(cubilose);
            }
            return cList;
        }
        [HttpGet]
        public dynamic GetNormalCubiloseList()
        {
            CubiloseBLL bll = new CubiloseBLL();
            List<P_Cubilose> list = bll.GetNormalCubiloseList();
            List<Cubilose> cList = new List<Cubilose>();
            Cubilose cubilose = null;
            foreach (var item in list)
            {
                cubilose = new Cubilose();
                cubilose.id = item.cubilose_id;
                cubilose.name = item.cubilose_name.Trim();
                cubilose.img = item.cubilose_img;
                cubilose.info = item.cubilose_info;
                cList.Add(cubilose);
            }
            return cList;
        }
        [HttpGet]
        public dynamic GetSearchCubiloseList()
        {
            string name = Fun.Query("name");
            CubiloseBLL bll = new CubiloseBLL();
            List<P_Cubilose> list = bll.GetSearchCubiloseList(name);
            List<Cubilose> cList = new List<Cubilose>();
            Cubilose cubilose = null;
            foreach (var item in list)
            {
                cubilose = new Cubilose();
                cubilose.id = item.cubilose_id;
                cubilose.name = item.cubilose_name.Trim();
                cubilose.info = item.cubilose_info;
                cubilose.img = item.cubilose_img;
                cubilose.state = item.cubilose_state;
                cList.Add(cubilose);
            }
            return cList;
        }
        [HttpGet]
        public dynamic GetCubilose()
        {
            int id = Fun.Query("id", 0);
            CubiloseBLL bll = new CubiloseBLL();
            P_Cubilose c = bll.GetCubilose(id);
            Cubilose cubilose = new Cubilose();
            cubilose.id = c.cubilose_id;
            cubilose.name = c.cubilose_name.Trim();
            cubilose.type = c.cubilose_type;
            cubilose.typeDescript = c.cubilose_type == 1 ? "鲜炖" : "干货";
            cubilose.img = c.cubilose_img;
            cubilose.info = c.cubilose_info;
            cubilose.memo = c.cubilose_memo;
            cubilose.state = c.cubilose_state;
            cubilose.origin = c.cubilose_origin;
            cubilose.effect = c.cubilose_effect;
            cubilose.loves = c.love_customers;
            return cubilose;
        }

        [HttpPost]
        public dynamic AddCubilose()
        {
            string name = Fun.Form("name");
            int type = Fun.Form("type", 0);
            string origin = Fun.Form("origin");
            string effect = Fun.Form("effect");
            string info = Fun.Form("info");
            string memo = Fun.Form("memo");
            string img = Fun.Form("img");
            CubiloseBLL bll = new CubiloseBLL();
            if (!bll.AddCubilose(name, type, origin, effect, info, memo, img))
                return "失败";
            else
                return "成功";
        }

        [HttpPost]
        public dynamic UpdateCubilose()
        {
            int id = Fun.Form("id", 0);
            string name = Fun.Form("name");
            int type = Fun.Form("type", 0);
            string origin = Fun.Form("origin");
            string effect = Fun.Form("effect");
            string info = Fun.Form("info");
            string memo = Fun.Form("memo");
            string img = Fun.Form("img");
            CubiloseBLL bll = new CubiloseBLL();
            if (!bll.UpdateCubilose(id, name, type, origin, effect, info, memo, img))
                return "失败";
            else
                return "成功";
        }
        [HttpPost]
        public dynamic DeleteCubilose()
        {
            int id = Fun.Form("id", 0);
            int state = Fun.Form("state", 0);
            CubiloseBLL bll = new CubiloseBLL();
            if (!bll.DeleteCubilose(id,state))
                return "失败";
            else
                return "成功";
        }

    }
}