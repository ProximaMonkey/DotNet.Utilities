﻿using System;
using System.Data;
using System.Web.UI;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.DataOperator;
using YanZhiwei.DotNet2.Utilities.Enum;
using YanZhiwei.DotNet2.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet2.Utilities.WebFormExamples
{
    public partial class ExecutePageQuery : System.Web.UI.Page
    {
        private SqlServerDataOperator sqlHelper = new SqlServerDataOperator(@"server=YANZHIWEI-IT-PC\SQLEXPRESS;database=Northwind;uid=sa;pwd=sasa;");

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                //string _sql = "select OrderID,CustomerID,OrderDate, ShipName,ShippedDate,ShipCity,ShipRegion,ShipCountry from [Orders]";
                //GridView1.DataSource = sqlHelper.ExecuteDataTable(_sql, null);
                //GridView1.DataBind();
                int _totalPage = 0,
                    _totalCount = 0;
                int _index = Request.QueryString["pageIndex"].ToIntOrDefault(1);
                DataTable _result = sqlHelper.ExecutePageQuery("Orders", "OrderID,CustomerID,OrderDate, ShipName,ShippedDate,ShipCity,ShipRegion,ShipCountry", "OrderID", OrderType.Desc, "OrderID <10000000", 10, _index, out _totalPage, out _totalCount);
                GridView1.DataSource = _result;
                GridView1.DataBind();
                Label1.Text = PagerHtmlScript.BuilderNormal(_index, _totalPage);
            }
        }
    }
}