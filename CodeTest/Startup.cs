﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CodeTest.Startup))]

namespace CodeTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
