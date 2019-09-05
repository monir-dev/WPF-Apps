using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _03TreeViewAndValueConverter_MVVM
{
    /// <summary>
    /// The type of a directory item
    /// </summary>
    public  enum DirectoryItemType
    {
        /// <summary>
        /// A logical drive
        /// </summary>
        Drive,

        /// <summary>
        /// A Folder
        /// </summary>
        Folder,

        /// <summary>
        /// A physical File
        /// </summary>
        File
    }
}
