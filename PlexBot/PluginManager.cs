using BotAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlexBot
{
    public static class PluginManager
    {
        public static string runningPath;
        public static List<IInitializable> inits;
        public static List<IInjectable> injects;

        public static void LoadPlugins(Assembly assembly)
        {
            Type initType = typeof(IInitializable);

            List<Type> initializables = assembly.GetTypes().Where(x => initType.IsAssignableFrom(x)).ToList();

            inits = new List<IInitializable>();

            foreach (Type iType in initializables)
            {
                IInitializable init = (IInitializable)Activator.CreateInstance(iType);
                string pluginPath = runningPath + "\\" +  init.initHandle + "\\";

                if (!Directory.Exists(pluginPath))
                    Directory.CreateDirectory(pluginPath);

                init.basePath = pluginPath;
                inits.Add(init);
            }

            inits.Sort((i1, i2) => i1.priority.CompareTo(i2.priority));
            for (int i = 0; i < inits.Count; i++)
            {
                IInitializable init = inits[i];
                bool didInit = init.Initialize();

                if (didInit) { Debug.Log("Initialized " + init.initHandle + " successfully."); }
                else { Debug.LogError("Initialization of " + init.initHandle + " failed."); }
            }
        }
        public static void LoadInjections(Assembly assembly, string fileName)
        {
            Type initType = typeof(IInjectable);

            List<Type> injectables = assembly.GetTypes().Where(x => initType.IsAssignableFrom(x)).ToList();

            List<IInjectable> injects = new List<IInjectable>();

            foreach (Type iType in injectables)
            {
                IInjectable inject = (IInjectable)Activator.CreateInstance(iType);
                string pluginPath = runningPath + "\\" +  inject.initHandle + "\\";

                if (!Directory.Exists(pluginPath))
                    Directory.CreateDirectory(pluginPath);

                inject.basePath = pluginPath;

                injects.Add(inject);
            }

            injects.Sort((i1, i2) => i1.priority.CompareTo(i2.priority));
        }

        public static void LoadCommands(Assembly assembly, string fileName)
        {
            var commandType = typeof(ICommandable);
            List<Type> commands = assembly.GetTypes().Where(x => commandType.IsAssignableFrom(x)).ToList();

            foreach (Type cType in commands)
            {
                ICommandable cmd = (ICommandable)Activator.CreateInstance(cType);
                bool canAdd = CommandHandler.AddCommand(cmd);

                if (canAdd)
                {
                    Debug.Log(string.Format("Command {0} from {1} added successfully", cmd.handle, fileName));
                }
                else
                {
                    Debug.LogError(string.Format("Command {0} from {1} failed", cmd.handle, fileName));
                }
            }
        }
    }
}
