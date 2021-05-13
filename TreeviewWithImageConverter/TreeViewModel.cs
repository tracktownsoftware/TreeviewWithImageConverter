using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace TreeviewWithImageConverter
{
    public enum TreeItemType
    {
        Folder,
        EngineCode,
        WindowsCode
    }

    public class TreeViewModel : INotifyPropertyChanged
    {
        TreeViewModel(string name, TreeItemType treeItemType)
        {
            Name = name;
            Children = new List<TreeViewModel>();
            TreeItemType = treeItemType;
            IsNodeExpanded = (bool)(treeItemType == TreeItemType.Folder);
        }

        #region Properties

        public TreeItemType TreeItemType { get; set; }
        public string Name { get; set; }
        public List<TreeViewModel> Children { get; set; }
        public bool IsInitiallySelected { get; set; }
        //public bool IsRoot { get; private set; }

        private bool _isNodeExpanded;
        public bool IsNodeExpanded
        {
            get { return _isNodeExpanded; }
            set
            {
                _isNodeExpanded = value;
                NotifyPropertyChanged("IsNodeExpanded");
            }
        }

        TreeViewModel _parent;

        #endregion

        void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public static List<TreeViewModel> SetTree(string topLevelName)
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();
            TreeViewModel tv = new TreeViewModel(topLevelName, TreeItemType.Folder);

            treeView.Add(tv);

            //Perform recursive method to build treeview 

            #region Test Data
            //Doing this below for this example, you should do it dynamically 
            //***************************************************

            tv.Children.Add(new TreeViewModel("Child1 - Engine sample", TreeItemType.EngineCode));
            tv.Children.Add(new TreeViewModel("Child2 - Engine sample", TreeItemType.EngineCode));
            tv.Children.Add(new TreeViewModel("Child3 - Windows sample", TreeItemType.WindowsCode));
            TreeViewModel tvChild4 = new TreeViewModel("Child4", TreeItemType.Folder);
            tv.Children.Add(tvChild4);
            tvChild4.Children.Add(new TreeViewModel("GrandChild4.1 - Windows sample", TreeItemType.WindowsCode));
            tvChild4.Children.Add(new TreeViewModel("GrandChild4.2 - Windows sample", TreeItemType.WindowsCode));
            TreeViewModel tvGrandChild3 = new TreeViewModel("GrandChild4.3", TreeItemType.Folder);
            tvChild4.Children.Add(tvGrandChild3);
            tvGrandChild3.Children.Add(new TreeViewModel("GrandChild4.3.1 - Engine sample", TreeItemType.EngineCode));
            tvGrandChild3.Children.Add(new TreeViewModel("GrandChild4.3.2 - Engine sample", TreeItemType.EngineCode));
            tv.Children.Add(new TreeViewModel("Child5", TreeItemType.EngineCode));

            tv.Children.Add(new TreeViewModel("Empty folder", TreeItemType.Folder));
            //***************************************************
            #endregion

            tv.Initialize();

            return treeView;
        }

        public static List<string> GetTree()
        {
            List<string> selected = new List<string>();

            //select = recursive method to check each tree view item for selection (if required)

            return selected;

            //***********************************************************
            //From your window capture selected your treeview control like:   TreeViewModel root = (TreeViewModel)TreeViewControl.Items[0];
            //                                                                List<string> selected = new List<string>(TreeViewModel.GetTree());
            //***********************************************************
        }

        #region INotifyPropertyChanged Members

        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
