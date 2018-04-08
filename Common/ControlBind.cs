using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Common
{
    public class ControlBind
    {
        public static void DropDownListBind<T>(DropDownList drop, List<T> List, string ValueField, string TextField, string DefaultValue) where T : class
        {
            drop.Items.Clear();
            if (List != null)
            {
                drop.DataSource = List;
                drop.DataTextField = TextField;
                drop.DataValueField = ValueField;
                drop.DataBind();
            }
            drop.Items.Insert(0, new ListItem("请选择", "0"));
            if (DefaultValue != "")
                drop.SelectedValue = DefaultValue;
        }
    }
}
