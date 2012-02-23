using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CengZai;
using System.Data;
using System.IO;
using CengZai.Helper;
using CengZai.Web.Code;

namespace CengZai.Web.Controllers
{
    public class ArticleController : BaseController
    {
        //
        // GET: /Article/
        [CheckAuthFilter]
        public ActionResult Index(string domain)
        {
            BLL.Article bll = new BLL.Article();
            int pageSize = 20;
            int pageIndex = 1;
            string where = "";

            List<Model.Article> artList = bll.GetModelList("State=1");
            
            ViewBag.Domain = domain + "";
            return View(artList);
        }

        [CheckAuthFilter]
        public ActionResult PostText()
        {
            _InitPostForm();    //初始化
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        [CheckAuthFilter]
        public ActionResult PostText(string title, string content, int? categoryid, int? privacy, int? top, int? draft)
        {
            try
            {
                
                if (string.IsNullOrEmpty(content))
                {
                    ModelState.AddModelError("Error", "内容不能为空！");
                    _InitPostForm();    //初始化
                    return View();
                }
                if (!string.IsNullOrEmpty(title) && title.Length > 50)
                {
                    ModelState.AddModelError("Error", "标题不能超过50个字符！");
                    _InitPostForm();    //初始化
                    return View();
                }
                Model.Article model = new Model.Article();
                model.CategoryID = (categoryid == null ? 0 : categoryid);
                model.Title = Server.HtmlEncode(title); //过滤html代码
                model.Content = Helper.Util.GetSafeHtml(content);
                model.IsTop = (top == null ? 0 : top);
                model.PostIP = Helper.Util.GetIP();
                model.PostTime = DateTime.Now;
                model.Privacy = (privacy == null ? 0 : privacy);
                model.ReplyCount = 0;
                model.ReportCount = 0;
                if (draft == 1)
                {
                    model.State = 0;
                }
                else
                {
                    model.State = 1;
                }
                
                model.Type = 0;
                model.UserID = GetLoginUser().UserID;
                model.ViewCount = 0;
                BLL.Article bll = new BLL.Article();
                bll.Add(model);

                return RedirectToAction("User", "Home", new { ID = 1 });
            }
            catch (Exception ex)
            {
                Log.AddErrorInfo(GetLoginUser().Email + "提交文章出现异常：" + ex.Message + ",详细：" + ex.StackTrace);
                ModelState.AddModelError("Error", "提交出现异常，请稍后重试！");
                _InitPostForm();    //初始化
                return View();
            }
        }

        private void _InitPostForm()
        {
            //分类设置
            List<Model.Category> categories = new BLL.Category().GetModelList(string.Format("UserID={0}", GetLoginUser().UserID));
            if (categories == null)
            {
                ViewBag.CategoryList = new List<SelectListItem>(){
                    new SelectListItem(){ Text = "默认分类", Value = "0"}
                };
            }
            else
            {
                ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            }
            //隐私设置
            var privateList = new List<SelectListItem>(){
                new SelectListItem(){ Text = "所有人可见", Value="0", Selected=true},
                new SelectListItem(){ Text = "仅好友可见", Value="1"},
                new SelectListItem(){Text = "仅自己可见", Value="2"},
            };
            ViewBag.PrivateList = privateList;
        }


        /************* old ****************/

        /*
        [CheckAuthFilter]
        public ActionResult Post()
        {
            int type = 0;
            int.TryParse(Request["type"], out type);


            if (type == (int)Model.ArtType.Audio)
            {
                return View("PostAudio");
            }
            else if (type == (int)Model.ArtType.Image)
            {
                return View("PostImage");
            }
            else if (type == (int)Model.ArtType.Link)
            {
                return View("PostLink");
            }
            else if (type == (int)Model.ArtType.Video)
            {
                return View("PostVideo");
            }
            else if (type == (int)Model.ArtType.Text)
            {
                //文章
                return View("PostText");
            }
            else if (type == (int)Model.ArtType.Weibo)
            {
                return View("PostText");
            }
            return View("PostText");
        }


        [HttpPost]
        [CheckAuthFilter]
        public ActionResult Post(FormCollection form, bool? bIsTop)
        {
            try
            {
                int type = 0;
                int.TryParse(form["type"], out type);
                int categoryID = 0;
                int.TryParse(form["type"], out categoryID);
                int isTop = 0;
                if (form["isTop"] == "1")
                {
                    isTop = 1;
                }
                int.TryParse(form["isTop"], out isTop);
                int privat = 0;
                int.TryParse(form["private"], out privat);

                BLL.Article bll = new BLL.Article();
                Model.Article model = new Model.Article();
                model.CategoryID = categoryID;
                model.Content = form["content"];
                model.DownCount = 0;
                model.IsTop = isTop;
                model.PostIP = Helper.Util.GetIP();
                model.PostTime = DateTime.Now;
                model.Private = privat;
                model.ReportCount = 0;
                model.State = 1;
                model.Title = form["title"];
                model.TopCount = 0;
                model.Type = type;
                model.UserID = GetLoginUser().UserID;
                model.ViewCount = 0;


                if (type == (int)Model.ArtType.Audio)
                {
                    if (string.IsNullOrEmpty(form["source"]))
                    {
                        return AlertAndBack("请上传图片");
                    }
                }
                else if (type == (int)Model.ArtType.Image)
                {
                    return View("PostImage");
                }
                else if (type == (int)Model.ArtType.Link)
                {
                    return View("PostLink");
                }
                else if (type == (int)Model.ArtType.Video)
                {
                    return View("PostVideo");
                }
                else if (type == (int)Model.ArtType.Text)
                {
                    if (string.IsNullOrEmpty(model.Title) && string.IsNullOrEmpty(model.Content))
                    {
                        return AlertAndBack("标题或者内容至少一项不能为空！");
                    }
                    //文章
                }
                //else if (strType == Model.ArtType.Weibo.ToString().ToLower())
                //{
                //    return View("PostText");
                //}

                bll.Add(model);
            }
            catch (Exception ex)
            {
                Helper.Log.AddErrorInfo("Article/Post操作异常:" + ex.Message);
                return AlertAndBack("操作异常，请检查输入是否合法！");
            }

            return AlertAndBack("提交成功！");
        }
        */

        [HttpPost]
        [CheckAuthFilter]
        public ActionResult UploadImage()
        {
            System.Drawing.Image thumbnail_image = null;
            System.Drawing.Image original_image = null;
            //System.Drawing.Bitmap thumbnail_image = null;
            System.Drawing.Graphics graphic = null;
            System.IO.MemoryStream ms = null;

            try
            {
                // Get the data
                HttpPostedFileBase jpeg_image_upload = Request.Files["Filedata"];

                // Retrieve the uploaded image
                original_image = System.Drawing.Image.FromStream(jpeg_image_upload.InputStream);

                // Calculate the new width and height
                int max = Config.ThumbImageMax > 0 ? Config.ThumbImageMax : 200;
                int width = original_image.Width;
                int height = original_image.Height;
                int thumb_width = max, thumb_height = max;
                if (width > height)
                {
                    thumb_width = max;
                    thumb_height = max * height / width;
                }
                else
                {
                    thumb_width = max * width / height;
                    thumb_height = max;
                }

                // Create the thumbnail
                thumbnail_image = original_image.GetThumbnailImage(thumb_width, thumb_height, null, System.IntPtr.Zero);

                string _path = "/upload/photo/{##$$##}u" + GetLoginUser().UserID + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString().Substring(0, 5) + ".jpg";
                string original_id = _path.Replace("{##$$##}", "");
                string thumbnail_id = _path.Replace("{##$$##}", "thumb-");

                //保存
                try
                {
                    string _base = Server.MapPath(_path.Substring(0, _path.LastIndexOf('/')));
                    if(!Directory.Exists(_base))
                    {
                        string[] dirs = _path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        _base = Server.MapPath("/");
                        for(int i=0 ; i < (dirs.Length - 1); i++)
                        {
                            _base += dirs[i] + "\\";
                            if (!Directory.Exists(_base))
                            {
                                Directory.CreateDirectory(_base);
                            }
                        }
                    }
                }
                catch { }
                original_image.Save(Server.MapPath(original_id), System.Drawing.Imaging.ImageFormat.Jpeg);
                thumbnail_image.Save(Server.MapPath(thumbnail_id) , System.Drawing.Imaging.ImageFormat.Jpeg);

                return Content(thumbnail_id);
            }
            catch(Exception ex)
            {
                // If any kind of error occurs return a 500 Internal Server error
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
            finally
            {
                // Clean up
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (graphic != null) graphic.Dispose();
                if (original_image != null) original_image.Dispose();
                if (thumbnail_image != null) thumbnail_image.Dispose();
                if (ms != null) ms.Close();
            }
        }

    }
}
