﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS.Engine.Desktop
{
    /// <summary>
    /// Code adapted from Duality2D: https://github.com/AdamsLair/duality/blob/7febba7196982e9fa7903a501378bf6e121157fe/Source/Platform/DefaultOpenTK/Backend/Audio/AudioLibraryLoader.cs
    /// 
    /// This loads the OpenALSoft dll for Windows (basically by renaming the appropriate dll to the binary folder, OpenTK will take it from there
    /// </summary>
    public static class WindowsOpenALSoftLoader
    {
        public static void Load()
        {
            //OpenAL is built in on Mac & Linux, so we only need OpenALSoft on Windows
            bool isWindows =
                Environment.OSVersion.Platform == PlatformID.Win32NT ||
                Environment.OSVersion.Platform == PlatformID.Win32S ||
                Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                Environment.OSVersion.Platform == PlatformID.WinCE;            

            if (!isWindows)
                return;

            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            string source32BitFilePath = Path.Combine(exeFolder, "OpenALSoft32.dll");
            string source64BitFilePath = Path.Combine(exeFolder, "OpenALSoft64.dll");
            const string targetFileName = "OpenAL32.dll";

            copyCorrectFile(source32BitFilePath, source64BitFilePath, targetFileName);
        }

        private static void copyCorrectFile(string source32BitFilePath, string source64BitFilePath, string targetFilePath)
        {
            if (Environment.Is64BitProcess)
                copy(source64BitFilePath, targetFilePath);
            else
                copy(source32BitFilePath, targetFilePath);
        }
        
        private static void copy(string from, string to)
        {
            if (!File.Exists(from))
            {
                Debug.WriteLine("OpenAL Soft dll not found in path {0}, audio will not be played if OpenAL is not installed", from);
                return;
            }

            Exception e = null;
            try
            {
                File.Copy(from, to, true);
            }
            catch (IOException e1) { e = e1; }
            catch (UnauthorizedAccessException e2) { e = e2; }
            catch (NotSupportedException e3) { e = e3; }

            if (e == null)
                return;
            Debug.WriteLine("Failed to copy OpenAL Soft dll, audio will not be played if OpenAL is not installed. Exception: {0}",
                e.ToString());            
        }
    }
}
