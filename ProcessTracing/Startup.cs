using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.Providers.Trello;

[assembly: OwinStartup(typeof(ProcessTracing.Startup))]

namespace ProcessTracing
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login")
            });

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            //  app.UseTrelloAuthentication(new TrelloAuthenticationOptions
            // ("f27de49532bf871ca048feeb9e3ea8dc", "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f", "ZPI nasz apka przykladowa nazwa"));
            //{
            //   key= "f27de49532bf871ca048feeb9e3ea8dc",
            //   secret= "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f",
            //   appName= "ZPI nasz apka przykladowa nazwa"
            //});

            app.UseTrelloAuthentication(
               key: "f27de49532bf871ca048feeb9e3ea8dc",
               secret: "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f",
               appName: "ZPI nasz apka przykladowa nazwa"
             );
        }
       
    }
}
