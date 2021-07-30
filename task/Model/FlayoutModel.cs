using System;
using Prism.Mvvm;

namespace task.Model
{
    public class FlayoutModel:BindableBase
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public string PageName { get; set; }
        bool _IsSellected;
        public bool IsSellected { get { return _IsSellected; } set { _IsSellected = value; RaisePropertyChanged(); } }
    }
}
