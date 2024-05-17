using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace birthday.Models
{
    public class z_sqlUsers : DapperSql<Users>
    {
        public z_sqlUsers()
        {
            OrderByColumn = SessionService.SortColumn;  // 排序欄位
            OrderByDirection = SessionService.SortDirection;    // 排序方式
            DefaultOrderByColumn = "Users.UserNo";  // 預設排序欄位
            DefaultOrderByDirection = "ASC";    // 預設排序方式
            // 如果指定欄位是空的就用預設，不是空的就用指定的
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
            SELECT Users.UserNo, Users.UserName, Users.Bless
            From Users
            ";
            return str_query;
        }

        public Users GetData(string userNo)
        {
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Users.UserNo = @UserNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("UserNo", userNo);
            var model = dpr.ReadSingle<Users>(sql_query, parm);
            return model;
        }
        // 模糊搜尋使用
        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Users.UserNo",
                    "Users.UserName",
                    "Users.Bless"
                     };
            return searchColumn;
        }
    }
}