using System.Web;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.Core.ViewModels;

namespace SportsWebPt.Identity.Services.Core
{
    public class ViewService : IViewService
    {
        public virtual Task<System.IO.Stream> Login(IDictionary<string, object> env, LoginViewModel model, SignInMessage message)
        {
            var loginViewModel = new SwptLoginModel(model);
            var returnUrlQueryStrings = HttpUtility.ParseQueryString(message.ReturnUrl);
            loginViewModel.ViewType = returnUrlQueryStrings.Get("viewType");

            return Render(loginViewModel, "login");
        }

        public virtual Task<System.IO.Stream> Logout(IDictionary<string, object> env, LogoutViewModel model)
        {
            return Render(model, "logout");
        }

        public virtual Task<System.IO.Stream> LoggedOut(IDictionary<string, object> env, LoggedOutViewModel model)
        {
            return Render(model, "loggedOut");
        }

        public virtual Task<System.IO.Stream> Consent(IDictionary<string, object> env, ConsentViewModel model)
        {
            return Render(model, "consent");
        }

        public virtual Task<System.IO.Stream> Error(IDictionary<string, object> env, ErrorViewModel model)
        {
            return Render(model, "error");
        }

        protected virtual Task<System.IO.Stream> Render(CommonViewModel model, string page)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });

            string html = LoadHtml(page);
            html = Replace(html, new
            {
                siteName = model.SiteName,
                model = json,
            });

            return Task.FromResult(StringToStream(html));
        }

        private string LoadHtml(string name)
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"content\app");
            file = Path.Combine(file, name + ".html");
            return File.ReadAllText(file);
        }

        string Replace(string value, IDictionary<string, object> values)
        {
            foreach (var key in values.Keys)
            {
                value = value.Replace("{" + key + "}", values[key].ToString());
            }
            return value;
        }

        string Replace(string value, object values)
        {
            return Replace(value, Map(values));
        }

        IDictionary<string, object> Map(object values)
        {
            var dictionary = values as IDictionary<string, object>;

            if (dictionary == null)
            {
                dictionary = new Dictionary<string, object>();
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(values));
                }
            }

            return dictionary;
        }

        Stream StringToStream(string s)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        public Task<Stream> ClientPermissions(IDictionary<string, object> env, ClientPermissionsViewModel model)
        {
            return Render(model, "permissions");
        }
    }

    public class SwptLoginModel : LoginViewModel
    {
        #region Properties

        public String ViewType { get; set; }

        #endregion

        #region

        public SwptLoginModel(LoginViewModel loginViewModel)
        {
            //TODO: move to a library mapping function
            base.AdditionalLinks = loginViewModel.AdditionalLinks;
            base.AllowRememberMe = loginViewModel.AllowRememberMe;
            base.AntiForgery = loginViewModel.AntiForgery;
            base.CurrentUser = loginViewModel.CurrentUser;
            base.ErrorMessage = loginViewModel.ErrorMessage;
            base.ExternalProviders = loginViewModel.ExternalProviders;
            base.LoginUrl = loginViewModel.LoginUrl;
            base.LogoutUrl = loginViewModel.LogoutUrl;
            base.RememberMe = loginViewModel.RememberMe;
            base.SiteName = loginViewModel.SiteName;
            base.SiteUrl = loginViewModel.SiteUrl;
            base.Username = loginViewModel.Username;
            
        }

        #endregion
    }
}