using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace _02TreeViewAndValueConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        #endregion


        #region On Loaded

        /// <summary>
        /// Where the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Get every logical drive on the machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                // create a new item for it
                var item = new TreeViewItem
                {
                    // set the header
                    Header = drive,
                    // add the full path
                    Tag = drive
                };

                // add a dummy item
                item.Items.Add(null);

                // listen out for item being expanded
                item.Expanded += Folder_Expanded;

                // add it to the main tree-view
                FolderTreeView.Items.Add(item);
            }
        }

        #endregion


        #region Folder Expanded

        /// <summary>
        /// When a folder is expended, find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            var item = (TreeViewItem)sender;

            // if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null) return;

            // clear dummy data
            item.Items.Clear();

            // get folder path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // create a blank list for directories
            var directories = new List<string>();

            // Try and get directories from the folder
            // ignoring any issues doing it
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // for each directories ...
            directories.ForEach(directoryPath =>
            {
                // create directory item
                var subItem = new TreeViewItem
                {
                    // set header as folder name
                    Header = GetFilesFolderName(directoryPath),
                    // add tag as full path
                    Tag = directoryPath
                };

                // Add dummy items so we can expand folder
                subItem.Items.Add(null);

                // handle expanding
                subItem.Expanded += Folder_Expanded;

                // add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files

            // create a blank list for files
            var files = new List<string>();

            // Try and get files from the folder
            // ignoring any issues doing it
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            // for each files ...
            files.ForEach(filePath =>
            {
                // create file item
                var subItem = new TreeViewItem
                {
                    // set header as file name
                    Header = GetFilesFolderName(filePath),
                    // add tag as full path
                    Tag = filePath
                };

                // add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion
        }

        #endregion


        #region Helpers

        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFilesFolderName(string path)
        {
            // if we have no path, return empty
            if (string.IsNullOrEmpty(path)) return string.Empty;

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
