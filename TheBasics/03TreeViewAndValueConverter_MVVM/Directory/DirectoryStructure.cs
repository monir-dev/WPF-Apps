using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace _03TreeViewAndValueConverter_MVVM
{
    /// <summary>
    /// A helper class to query information about directory
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical drive on the machine
            return Directory.GetLogicalDrives()
                .Select(drive => new DirectoryItem
                {
                    FullPath = drive,
                    Type = DirectoryItemType.Drive
                }).ToList();
        }

        /// <summary>
        /// Get the directories top level content
        /// </summary>
        /// <param name="fullPath">The full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders

            // Try and get directories from the folder
            // ignoring any issues doing it
            try
            {

                var dirs = System.IO.Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem() { FullPath = dir, Type = DirectoryItemType.Folder}));
            }
            catch { }

            #endregion

            #region Get Files

            // Try and get files from the folder
            // ignoring any issues doing it
            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(dir => new DirectoryItem() { FullPath = dir, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFilesFolderName(string path)
        {
            // if we have no path, return empty
            if (String.IsNullOrEmpty(path)) return String.Empty;

            // Make all slashes back slashes
            var normalizePath = path.Replace('/', '\\');

            // find the last index of backslashes
            var lastIndex = normalizePath.LastIndexOf('\\');

            // if we don't find a backslash, return the path itself
            if (lastIndex <= 0) return path;

            // return name after the las back slash
            return path.Substring(lastIndex + 1);
        }

        #endregion
    }
}
