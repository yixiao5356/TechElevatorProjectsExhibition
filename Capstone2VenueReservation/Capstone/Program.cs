﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            #region appsettings.json configuration
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            #endregion

            string connectionString = configuration.GetConnectionString("Project");

            UserInterface ui = new UserInterface(connectionString);
            ui.Run();

        }
    }
}
