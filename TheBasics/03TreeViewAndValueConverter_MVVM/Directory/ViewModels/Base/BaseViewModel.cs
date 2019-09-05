using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using _03TreeViewAndValueConverter_MVVM.Annotations;
using PropertyChanged;

namespace _03TreeViewAndValueConverter_MVVM
{
    /// <summary>
    /// A base view model that fires Property Changed event as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};

    }
}
