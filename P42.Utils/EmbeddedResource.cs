﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace P42.Utils
{
    // DO NOT MAKE PUBLIC.  THIS RUNS VERY SLOWLY ON UWP .NET TOOL CHAIN.  USE EmbeddedResourceCache.GetStreamAsync instead.
    internal static class EmbeddedResource
    {
        public static System.IO.Stream GetStream(string resourceId, Assembly assembly = null)
        {
            assembly = assembly ?? Environment.EmbeddedResourceAssemblyResolver?.Invoke(resourceId);
            if (assembly == null)
                return null;
            // the following is very bad for UWP?
            //if (!Available(resourceId, assembly))
            //    return null;
            return assembly.GetManifestResourceStream(resourceId);
        }

        static Dictionary<Assembly, List<string>> Resources = new Dictionary<Assembly, List<string>>();

        public static bool Available(string resourceId, Assembly assembly=null)
        {
            assembly = assembly ?? Environment.EmbeddedResourceAssemblyResolver?.Invoke(resourceId);
            if (assembly == null)
                return false;
            if (!Resources.ContainsKey(assembly))
                Resources[assembly] = new List<string>(assembly.GetManifestResourceNames());
            return Resources[assembly].Contains(resourceId);
        }
    }
}
