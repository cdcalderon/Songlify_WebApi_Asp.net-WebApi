using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Cors;
using SonglifyApi.DataObjects;
using SonglifyApi.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SonglifyApi
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : ClearDatabaseSchemaIfModelChanges<MobileServiceContext>
    {
        //protected override void Seed(MobileServiceContext context)
        //{
        //    List<TodoItem> todoItems = new List<TodoItem>
        //    {
        //        new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
        //        new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
        //    };

        //    foreach (TodoItem todoItem in todoItems)
        //    {
        //        context.Set<TodoItem>().Add(todoItem);
        //    }

        //    base.Seed(context);
        //}
    }
}

