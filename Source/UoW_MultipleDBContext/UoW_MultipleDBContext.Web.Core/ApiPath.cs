using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace UoW_MultipleDBContext.Web.Core
{
    public interface IApiPath
    {
        string BaseUrl { get; }
        string TokenUrl { get; }
        string GetTokenInfo { get; }
        string LoggedinUserInfo { get; }

        string GetAllCategories { get; }

        string GetCategorybyId { get; }

        string CreateCategory { get; }

        string UpdateCategory { get; }

        string DeleteCategory { get; }


        string GetAllDepartments { get; }

        string GetDepartmentbyId { get; }

        string CreateDepartment { get; }

        string UpdateDepartment { get; }

        string DeleteDepartment { get; }
    }


    public class ApiPath : IApiPath
    {
        readonly string _apiUrl;
        public ApiPath()
        {
            _apiUrl = ConfigurationManager.AppSettings["ApiUrl"].ToString();
        }
        public string BaseUrl
        {
            get { return _apiUrl + "/api"; }
        }
        public string TokenUrl
        {
            get { return _apiUrl + "/token"; }
        }
        public string GetTokenInfo
        {
            get { return BaseUrl + "/Account/GetTokenInfo"; }
        }

        public string LoggedinUserInfo
        {
            get
            {
                return BaseUrl + "/Account/GetLoggedinUserIfo";
            }
        }

        public string GetAllCategories { get { return BaseUrl + "/Category/GetAll"; } }

        public string GetCategorybyId { get { return BaseUrl + "/Category/GetById"; } }

        public string CreateCategory { get { return BaseUrl + "/Category/Insert"; } }

        public string UpdateCategory { get { return BaseUrl + "/Category/Update"; } }

        public string DeleteCategory { get { return BaseUrl + "/Category/Delete"; } }

        public string GetAllDepartments { get { return BaseUrl + "/Department/GetAll"; } }

        public string GetDepartmentbyId { get { return BaseUrl + "/Department/GetById"; } }

        public string CreateDepartment { get { return BaseUrl + "/Department/Insert"; } }

        public string UpdateDepartment { get { return BaseUrl + "/Department/Update"; } }

        public string DeleteDepartment { get { return BaseUrl + "/Department/Delete"; } }

    }
}
