﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace FastFood.Startup
{
    public static class SwaggerConfiguration
    {
        public static WebApplication ConfigureSwagger(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            return app;
        }
    }
}
