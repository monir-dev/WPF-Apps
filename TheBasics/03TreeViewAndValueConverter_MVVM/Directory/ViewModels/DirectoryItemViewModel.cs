using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace _03TreeViewAndValueConverter_MVVM
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

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
        public string Name => this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFilesFolderName(FullPath);


        /// <summary>
        /// A list of all children inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand => Type != DirectoryItemType.File;


        /// <summary>
        /// Indicate if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get { return Children?.Count(c => c != null) > 0; }
            set
            {
                // if the UI tells us to expand
                if (value == true)
                    // find all children
                    Expand();
                // if the UI tells us to close
                else
                    // remove all children
                    ClearChildren();
            }
        }

        #endregion


        #region Public Commands

        /// <summary>
        /// A command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor
       
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="fullPath">the full path of this item</param>
        /// <param name="type">the type of item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create Command
            ExpandCommand = new RelayCommand(Expand);

            // set path and type
            FullPath = fullPath;
            Type = type;

            // setup the children as needed
            ClearChildren();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Remove all children from the list, adding a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if it is not a file
            if (Type != DirectoryItemType.File)
                Children.Add(null);

        }

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            // we cannot expand a file
            if (Type == DirectoryItemType.File) return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(FullPath);
            Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }

        #endregion
    }
}
