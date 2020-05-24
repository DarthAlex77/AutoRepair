using System;
using System.Collections.Generic;
using System.Text;
using AutoRepair.Model;
using DynamicData;
using DynamicData.Binding;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AutoRepair.ViewModel
{
    public class OrderTabViewModel:ReactiveObject
    {
        #region Constructor
        public OrderTabViewModel()
        {
            using (AppContext db=new AppContext())
            {
                Orders=new ObservableCollectionExtended<Order>(db.Orders);
                db.CarModels.Load();
                db.Cars.Load();
                db.Clients.Load();
            }
        }
        #endregion

        #region OrdersProperty
        public ObservableCollectionExtended<Order>Orders { get; set; }

        #endregion

        #region SelectedorderProperty

        private Order _order;
        public Order SelectedOrder
        {
            get => _order;
            set => this.RaiseAndSetIfChanged(ref _order, value);
        }

        #endregion

    }
}
