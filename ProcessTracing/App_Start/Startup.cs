using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Configuration(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseTrelloAuthentication(
                key: "f27de49532bf871ca048feeb9e3ea8dc",
                secret: "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f",
                appName: "ZPI nasz apka przykladowa nazwa"
                );
        }
    }
}