using AddressBook.API.DataContext;
using AddressBook.API.DataInterface;
using AddressBook.API.DataModel.Types;
using AddressBook.API.Service;
using Funq;
using ServiceStack;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.OrmLite;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


namespace AddressBook.API.Host
{
    public class Global : System.Web.HttpApplication
    {
        public class ContactApiAppHost : AppHostBase
        {
            public ContactApiAppHost() : base("AddressBook System API", typeof(ContactService).Assembly) { }
            public override void Configure(Container container)
            {
                container.Register<IDbConnectionFactory>(
                    new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(), PostgreSqlDialect.Provider));

                this.PreRequestFilters.Add((httpReq, httpRes) =>
                {
                    //Handles Request and closes Responses after emitting global HTTP Headers
                    if (httpReq.HttpMethod == "OPTIONS")
                        httpRes.EndRequest(); //add a 'using ServiceStack;'
                });

                container.Register<ICacheClient>(new MemoryCacheClient());
                container.Register<IContactRepository>(c => new ContactRepository(c.Resolve<IDbConnectionFactory>()));
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            new ContactApiAppHost().Init();
            var db = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(), PostgreSqlDialect.Provider);
            using (var dbcon = db.OpenDbConnection())
            {
                dbcon.CreateTable<Contact>();
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}