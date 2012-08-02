﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arma2Net.AddInProxy;

namespace Arma2NETMySQLPlugin
{
    class Startup
    {
        public static Logger logger_object = null;
        public static Boolean started_up = false;

        public static void StartupConnection()
        {
            if (started_up == false)
            {
                //Start up logging
                logger_object = new Logger();

                Logger.addMessage(Logger.LogType.Info, "Arma2NETMySQL Plugin Started.");

                //Use AssemblyInfo.cs version number
                //Holy cow this is confusing...
                //http://stackoverflow.com/questions/909555/how-can-i-get-the-assembly-file-version
                //http://all-things-pure.blogspot.com/2009/09/assembly-version-file-version-product.html
                //http://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin
                Logger.addMessage(Logger.LogType.Info, "Version number: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());

                //Utils.FileVersion returns the current version that is running
                //Utils.Version returns the API version, when this changes, all plugins need to be recompiled
                Logger.addMessage(Logger.LogType.Info, "Compiled against Arma2NET Version: " + Utils.FileVersion);

                //Load in Databases.txt file
                //This also sets up the SQLProvider associated with the database
                Logger.addMessage(Logger.LogType.Info, "Loading databases...");
                MySQL.dbs = new Databases();

                //set mutex so we know we've started everything up
                started_up = true;
            }
        }

        public static void Unload()
        {
            Logger.addMessage(Logger.LogType.Info, "Unloading plugin.");
            MySQL.dbs.shutdown();
            Logger.Stop();
            Startup.started_up = false;
        }
    }
}