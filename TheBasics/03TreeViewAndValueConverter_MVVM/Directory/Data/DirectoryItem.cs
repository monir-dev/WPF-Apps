using System;
using System.Collections.Generic;
using System.Text;

namespace _03TreeViewAndValueConverter_MVVM
{
    /// <summary>
    /// Information about a directory item such as a drive, a file or folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of directory item
        /// </summary>
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFilesFolderName(FullPath); } }
    }
}
