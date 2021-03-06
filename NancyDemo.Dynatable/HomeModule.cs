﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Dapper;
using Jinxintong.DAO.Dapper;

namespace NancyDemo.Grid
{
    public class HomeModule:NancyModule
    {   
        public HomeModule()
        {
            Get["/"] = x =>
            {

                return this.View["dynatable.html"];
            };

            Post["/pagelist"] = x =>
            {
                int page = this.Request.Form.page == null ? 1 : int.Parse(this.Request.Form.page);
                int rows = this.Request.Form.perPage == null ? 10 : int.Parse(this.Request.Form.perPage);

                string sort = this.Request.Query.sorts == null ? "" : this.Request.Form.sort;
                string order = this.Request.Form.order == null ? "" : this.Request.Form.order;
                
                string orderString = order;
                if (!string.IsNullOrWhiteSpace(orderString))
                    orderString = sort + " " + orderString;
                else
                    orderString = "SortCode";

                Dictionary<string, object> result = new Dictionary<string, object>();

                var providerName = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (var db = DataBaseContext.DbService(providerName, connectionString))
                {   
                    var list = db.PagedList("select * from Base_ProvinceCity", orderString, page, rows);

                    result.Add("totalRecordCount", list.TotalItemCount);
                    result.Add("queryRecordCount", list.TotalItemCount);
                    result.Add("records", list.Items);
                }
                
                return this.Response.AsJson<object>(result);
            };


        }
    }
}