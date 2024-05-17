using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace birthday.Controllers
{
    public class UserController : Controller
    {
        private readonly dbEntities db;
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 控制器建構子
        /// </summary>
        /// <param name="configuration">環境設定物件</param>
        /// <param name="entities">EF資料庫管理物件</param>
        public UserController(IConfiguration configuration, dbEntities entities)
        {
            db = entities;
            Configuration = configuration;
        }

        /// <summary>
        /// 使用者資料初始事件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Init()
        {
            // 初始化
            SessionService.SearchText = "";
            SessionService.SortColumn = "";
            SessionService.SortDirection = "asc";
            //返回使用者列表
            return RedirectToAction("Index", "User", new { area = "" });
        }

        /// <summary>
        /// 使用者資料列表
        /// </summary>
        /// <param name="page">目前頁數</param>
        /// <param name="pageSize">每頁筆數</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            //取得使用者資料列表集合
            using var sqlU = new z_sqlUsers();
            var model = sqlU.GetDataList(SessionService.SearchText).ToPagedList(page, pageSize);

            ViewBag.PageInfo = $"第 {page} 頁，共 {model.PageCount}頁";
            ViewBag.SearchText = SessionService.SearchText;
            return View(model);
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search()
        {
            object obj_text = Request.Form["SearchText"];
            SessionService.SearchText = (obj_text == null) ? string.Empty : obj_text.ToString();
            return RedirectToAction("Index", "User", new { area = "" });
        }

        /// <summary>
        /// 欄位排序
        /// </summary>
        /// <param name="id">指定排序的欄位</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Sort(string id)
        {
            if (SessionService.SortColumn == id)    // 如果上一次跟這一次是按同一個欄位
            {
                SessionService.SortDirection = (SessionService.SortDirection == "asc") ? "desc" : "asc";
            }
            else
            {
                SessionService.SortColumn = id;     // 如果上一次跟這一次是不同欄位就改變欄位
                SessionService.SortDirection = "asc";
            }
            return RedirectToAction("Index", "User", new { area = "" });
        }
    }
}