using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using task.Model;
using Xamarin.Forms;

namespace task.ViewModel
{
    public class FlyoutViewModel:ViewModelBase
    {
        ObservableCollection<FlayoutModel> _MeuItemsLst = new ObservableCollection<FlayoutModel>();
        public ObservableCollection<FlayoutModel> MeuItemsLst

        {
            set
            {
                _MeuItemsLst = value;
                RaisePropertyChanged();
            }
            get
            {
                return _MeuItemsLst;
            }
        }


        private FlayoutModel selectedMenuItem;
        public FlayoutModel SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }
        private FlayoutModel prevSelectedMenuItem;
        public FlayoutModel PrevSelectedMenuItem
        {
            get => prevSelectedMenuItem;
            set => SetProperty(ref prevSelectedMenuItem, value);
        }
        public DelegateCommand NavigateCommand { get; private set; }
        INavigationService _navigationService;
        public FlyoutViewModel(INavigationService navigationService, IPageDialogService dialogService):base(navigationService,dialogService)
        {
            _navigationService = navigationService;
            MeuItemsLst = new ObservableCollection<FlayoutModel>()
            {
                new FlayoutModel()
                {
                    Title="Articles",IconSource="explore.png",PageName="Articles"
                },
                  new FlayoutModel()
                {
                    Title="Life Chat",IconSource="live.png",PageName="LiveChat"
                },
                    new FlayoutModel()
                {
                    Title="Gallery",IconSource="gallery.png",PageName="Gallery"
                },
                      new FlayoutModel()
                {
                    Title="Wish List",IconSource="wishlist.png",PageName="WishList"
                },
                        new FlayoutModel()
                {
                    Title="Explore Online News",IconSource="emagazine.png",PageName="OnlineNews"
                }
            };

            NavigateCommand = new DelegateCommand(async () => {await Navigate(); });
        }

        async Task Navigate()
        {
            if (SelectedMenuItem!=null)
            {
                if (PrevSelectedMenuItem!=null)
                {
                    PrevSelectedMenuItem.IsSellected = false;
                }          
                SelectedMenuItem.IsSellected = true;
                await _navigationService.NavigateAsync($"NavigationPage/{SelectedMenuItem.PageName}");
                PrevSelectedMenuItem = SelectedMenuItem;
                SelectedMenuItem = null;
            }
   
        }
    }
}
