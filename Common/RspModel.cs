using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class RspModel
    {
        public RspModel() { }
        public RspModel(String message)
        {
            if (message != null)
            {
                this.error = true;
                this.message = message;
            }
        }
        public bool error { get; set; }
        public String message { get; set; }
    }
    public class RspModel<TSource> : RspModel
    {
        public RspModel(String message)
            : base(message)
        {
        }
        public RspModel(TSource data)
            : base(null)
        {
            this.data = data;
            if (data != null)
            {
                this.error = true;
            }
        }
        public TSource data { get; set; }
    }
    public class RspModelList<TSource> : RspModel<List<TSource>>
    {
        public RspModelList(String message)
            : base(message)
        {
        }
        public RspModelList(TSource data)
            : base("")
        {
            this.data = data;
            if (data != null)
            {
                this.error = true;
            }
        }
        public TSource data { get; set; }
    }
}
