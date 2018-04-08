using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
namespace Common
{
    public class Upload
    {
        #region 变量

        HttpPostedFile postedFile;
        /// <summary>
        /// 原文件名(含扩展名) 
        /// </summary>
        protected string localFileName;
        /// <summary>
        /// 原扩展名 
        /// </summary>
        protected string localFileExtension;
        /// <summary>
        /// 原文件大小 
        /// </summary>
        protected long localFileLength;

        /// <summary>
        /// 保存的文件名
        /// </summary>
        protected string saveFileName;
        /// <summary>
        /// 保存的扩展名
        /// </summary>
        protected string saveFileExtension;
        /// <summary>
        /// 保存文件的服务器端的完整路径
        /// </summary>
        protected string saveFilePath;
        /// <summary>
        /// 保存文件的服务器端的文件夹路径
        /// </summary>
        protected string saveFileFolderPath;

        private string path = null;
        private string fileType = null;
        private int sizes = 100;
        private string strError = null;
        #endregion

        public Upload()
        {
            path = @"Upload"; //上传路径 
            fileType = "jpg|gif|bmp|jpeg|png|rar|doc";
            sizes = 100; //传文件的大小,默认200KB 
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="files">控件</param>
        /// <param name="_saveFileName">指定文件名称</param>
        /// <param name="Directorys">目录名称</param>
        /// <returns></returns>
        public string SaveAs(int Sign, HttpFileCollection files, string _saveFileName, string Directorys)
        {
            strError = "";
            string myReturn = "";
            try
            {
                string BasePath = AppDomain.CurrentDomain.BaseDirectory; //根目录

                saveFileFolderPath = GetSaveFilePath(BasePath + path + "//" + Sign + "//" + Directorys + "//" + DateTime.Now.Year + "//" + DateTime.Now.Month);

                for (int iFile = 0; iFile < files.Count; iFile++)
                {
                    postedFile = files[iFile];
                    localFileName = postedFile.FileName;
                    if (localFileName != null && localFileName != "")
                    {
                        localFileLength = postedFile.ContentLength;
                        //if (localFileLength <= sizes * 1024 * 1024)
                        //{
                        localFileExtension = System.IO.Path.GetExtension(localFileName);

                        if (_saveFileName != "")
                        {
                            saveFilePath = saveFileFolderPath + "\\" + _saveFileName;
                            myReturn += "/" + Sign + Directorys + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + _saveFileName + ",";
                        }
                        else
                        {
                            localFileExtension = System.IO.Path.GetExtension(localFileName);
                            saveFileName = DateTime.UtcNow.ToString("yyyy" + "MM" + "dd" + "HH" + "mm" + "ss");
                            saveFileExtension = localFileExtension;
                            saveFilePath = saveFileFolderPath + "\\" + saveFileName + saveFileExtension;
                            myReturn += "/" + Sign + Directorys + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveFileName + saveFileExtension + ",";
                        }

                        postedFile.SaveAs(saveFilePath);
                        //}
                    }
                    else
                        myReturn += ",";
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            myReturn = myReturn.TrimEnd(',');
            return myReturn;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="postedFile">控件</param>
        /// <param name="_saveFileName">指定文件名称</param>
        /// <param name="Directorys">目录名称</param>
        /// <returns></returns>
        public string SaveAs(string Sign, HttpPostedFile postedFile, string _saveFileName, string Directorys)
        {
            strError = "";
            string myReturn = "";
            try
            {
                string BasePath = AppDomain.CurrentDomain.BaseDirectory; //根目录
                //string Sign = new Common.Databases().SyId;
                localFileName = postedFile.FileName;
                if (localFileName != null && localFileName != "")
                {
                    localFileLength = postedFile.ContentLength;
                    //if (localFileLength <= sizes * 1024 * 1024)
                    //{
                    saveFileFolderPath = GetSaveFilePath(BasePath + path + "//" + Sign + "//" + Directorys + "//" + DateTime.Now.Year + "//" + DateTime.Now.Month);

                    if (_saveFileName != "")
                    {
                        saveFilePath = saveFileFolderPath + "\\" + _saveFileName;
                        myReturn += "/" + Sign +"/"+ Directorys + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + _saveFileName + ",";
                    }
                    else
                    {
                        localFileExtension = System.IO.Path.GetExtension(localFileName);
                        saveFileName = DateTime.UtcNow.ToString("yyyy" + "MM" + "dd" + "HH" + "mm" + "ss");
                        saveFileExtension = localFileExtension;
                        saveFilePath = saveFileFolderPath + "\\" + saveFileName + saveFileExtension;

                        myReturn += "/" + Sign + Directorys + "/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + saveFileName + saveFileExtension + ",";
                    }
                    postedFile.SaveAs(saveFilePath);
                    //}
                }
                else
                    myReturn += ",";

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            myReturn = myReturn.TrimEnd(',');
            return myReturn;
        }

        /// <summary>
        /// 64位远程上传图片
        /// </summary>
        /// <param name="Sign"></param>
        /// <param name="baseFileString"></param>
        /// <param name="_saveFileName"></param>
        /// <param name="Directorys"></param>
        /// <returns></returns>
        public string SaveBase64(int Sign, string baseFileString, string _saveFileName, string Directorys)
        {
            string PathUrl = "https://images.chutou.net/Upload/Upload.ashx";
            string result = string.Empty;
            try
            {

                string postData = string.Format("sign={0}&data={1}&name={2}&directory={3}", Sign, baseFileString.Replace("+", "%2B"), _saveFileName, Directorys);
                PageHelper p = new PageHelper();
                result = p.ResponseToString(p.doPost(PathUrl, postData));

                JavaScriptSerializer json = new JavaScriptSerializer();
                // 反序列化JSON字符串到对象
                JsonModel jsonModel = json.Deserialize<JsonModel>(result);
                if (jsonModel.Status)
                    result = jsonModel.Message;
                else
                    result = "";

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }


        /// <summary>
        /// 远程上传文件\图片
        /// </summary>
        /// <param name="postedFile">控件</param>
        /// <param name="_saveFileName">指定文件名称</param>
        /// <param name="Directorys">目录名称</param>
        /// <returns></returns>
        public string SaveAsRemote(int Sign, HttpPostedFile postedFile, string _saveFileName, string Directorys)
        {
            string result = string.Empty;
            try
            {
                //Common.Databases db = new Common.Databases();
                string PathUrl = "https://images.chutou.net/Upload/Upload.ashx";
                //string PathUrl = "https://localhost:3048/Images/Upload.ashx";
                //string Sign = db.SyId;
                string fileName = postedFile.FileName;
                FileInfo file = new FileInfo(fileName);
                string ext = file.Extension;
                Stream stream = postedFile.InputStream;
                BinaryReader br = new BinaryReader(stream);
                byte[] fileByte = br.ReadBytes((int)stream.Length);
                string baseFileString = Convert.ToBase64String(fileByte);

                if (_saveFileName == "")
                {
                    _saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                }

                string postData = string.Format("sign={0}&data={1}&name={2}&directory={3}", Sign, baseFileString.Replace("+", "%2B"), _saveFileName, Directorys);
                PageHelper p = new PageHelper();
                result = p.ResponseToString(p.doPost(PathUrl, postData));

                JavaScriptSerializer json = new JavaScriptSerializer();
                // 反序列化JSON字符串到对象
                JsonModel jsonModel = json.Deserialize<JsonModel>(result);
                if (jsonModel.Status)
                    result = jsonModel.Message;
                else
                    result = "";
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public class JsonModel
        {
            private bool status;

            public bool Status
            {
                get { return status; }
                set { status = value; }
            }
            private string message;

            public string Message
            {
                get { return message; }
                set { message = value; }
            }
        }

        /// <summary>
        /// 远程上传文件\图片 题库图片,无客户站ID，文件共享
        /// </summary>
        /// <param name="postedFile">控件</param>
        /// <param name="_saveFileName">指定文件名称</param>
        /// <param name="Directorys">目录名称</param>
        /// <returns></returns>
        public string SaveAsEditor(HttpPostedFile postedFile, string _saveFileName, string Directorys)
        {
            string result = string.Empty;
            try
            {
                //Common.Databases db = new Common.Databases();
                string PathUrl = "https://images.chutou.net/Upload/Upload.ashx";
                //string PathUrl = "https://localhost:3048/Images/Upload.ashx";
                string Sign = "";
                string fileName = postedFile.FileName;
                FileInfo file = new FileInfo(fileName);
                string ext = file.Extension;
                Stream stream = postedFile.InputStream;
                BinaryReader br = new BinaryReader(stream);
                byte[] fileByte = br.ReadBytes((int)stream.Length);
                string baseFileString = Convert.ToBase64String(fileByte);

                if (_saveFileName == "")
                {
                    _saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                }

                string postData = string.Format("sign={0}&data={1}&name={2}&directory={3}", Sign, baseFileString.Replace("+", "%2B"), _saveFileName, Directorys);
                PageHelper p = new PageHelper();
                result = p.ResponseToString(p.doPost(PathUrl, postData));

                JavaScriptSerializer json = new JavaScriptSerializer();
                // 反序列化JSON字符串到对象
                JsonModel jsonModel = json.Deserialize<JsonModel>(result);
                if (jsonModel.Status)
                    result = jsonModel.Message;
                else
                    result = "";
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        /// <summary>    /// PostBinaryData    /// </summary> 
        /// <param name="path">文件地址</param> 
        /// <param name="filename">文件名字</param>        
        /// /// <returns></returns>   
        public string PostBinaryData(int Sign, string path, string filename, string Dictory)
        {
            string result = string.Empty;
            //Common.Databases db = new Common.Databases();
            string PathUrl = "https://images.chutou.net/Upload/Upload.ashx";
            //string PathUrl = "https://localhost:3082/Upload.ashx";
            //string Sign = db.SyId;
            byte[] bytes = File.ReadAllBytes(path);
            string baseFileString = Convert.ToBase64String(bytes);
            string postData = string.Format("sign={0}&data={1}&name={2}&directory={3}", Sign, baseFileString.Replace("+", "%2B"), filename, Dictory);
            PageHelper p = new PageHelper();
            result = p.ResponseToString(p.doPost(PathUrl, postData));

            JavaScriptSerializer json = new JavaScriptSerializer();
            // 反序列化JSON字符串到对象
            JsonModel jsonModel = json.Deserialize<JsonModel>(result);
            if (jsonModel.Status)
                result = jsonModel.Message;
            else
                result = "";

            return result;
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="picPath"></param>
        /// <returns></returns>
        private string GetSaveFilePath(string picPath)
        {
            if (!Directory.Exists(picPath))
            {
                Directory.CreateDirectory(picPath);
            }
            return picPath;
        }

        /// <summary>
        /// 限制大小
        /// </summary>
        public int Sizes
        {
            set
            {
                sizes = value;
            }
        }
        public string FileType
        {
            set
            {
                fileType = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return strError;
            }
        }
    }


}
