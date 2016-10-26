﻿using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using YanZhiwei.DotNet.WebApiExample.Filters;

namespace YanZhiwei.DotNet.WebApiExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            // 将 Web API 配置为仅使用不记名令牌身份验证。
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new TokenProjectorAttribute());
            config.Filters.Add(new APIExceptionFilterAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}