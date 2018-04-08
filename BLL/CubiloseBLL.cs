using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using DAL;

namespace BLL
{
    public class CubiloseBLL
    {
        private static MSSQLMy ms = new MSSQLMy();

        public List<P_Cubilose> GetCubiloseList()
        {
            string sql = "select cubilose_id,cubilose_name,cubilose_state,cubilose_img,cubilose_info from p_cubilose order by cubilose_state desc, cubilose_name";
            return ms.GetListModel<P_Cubilose>(sql);
        }
        public List<P_Cubilose> GetNormalCubiloseList()
        {
            string sql = "select cubilose_id,cubilose_name from p_cubilose where cubilose_state = 1 order by cubilose_name";
            return ms.GetListModel<P_Cubilose>(sql);
        }

        public P_Cubilose GetCubilose(int id)
        {
            string sql = "select * from p_cubilose where cubilose_id = " + id;
            return ms.GetModel<P_Cubilose>(sql);
        }

        public bool AddCubilose(string name, int type, string origin, string effect, string info, string memo, string img)
        {
            string sql = "insert into p_cubilose values(@name,@type,@info,@effect,@memo,'',@img,@origin,1)";
            return ms.Insert(sql, new { name, type, origin, effect, info, memo, img });
        }

        public bool UpdateCubilose(int id, string name, int type, string origin, string effect, string info, string memo, string img)
        {
            string sql = "update p_cubilose set cubilose_name = @name, cubilose_type=@type,cubilose_origin=@origin,cubilose_effect=@effect,cubilose_info=@info,cubilose_memo=@memo,cubilose_img=@img where cubilose_id = @id";
            return ms.Update(sql, new { id, name, type, origin, effect, info, memo, img });
        }

        public bool DeleteCubilose(int id, int state)
        {
            string sql = "update p_cubilose set cubilose_state=@state where cubilose_id = @id";
            return ms.Update(sql, new { id, state });
        }

        public List<P_Cubilose> GetSearchCubiloseList(string name)
        {
            string sql = "select cubilose_id,cubilose_name,cubilose_state,cubilose_img,cubilose_info from p_cubilose where cubilose_name like '%" + name + "%' order by cubilose_state desc, cubilose_name";
            return ms.GetListModel<P_Cubilose>(sql);
        }
    }
}