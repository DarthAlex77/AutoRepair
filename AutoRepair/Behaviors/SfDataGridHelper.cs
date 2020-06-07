using System;
using System.Collections.ObjectModel;
using System.Windows;
using Syncfusion.UI.Xaml.Grid;

namespace AutoRepair.Behaviors
{
    class SfDataGridHelper
    {
        public static void SetSelectedItems(DependencyObject element, object value)
        {
            if (element is SfDataGrid)
                element.SetValue(SelectedItemsProperty, value);
            else
                throw new NotSupportedException("This property can be used only with SfDataGrid");
        }
        public static object GetSelectedItems(DependencyObject element)
        {
            return (object)element.GetValue(SelectedItemsProperty);
        }
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.RegisterAttached(
            "SelectedItems", typeof(ObservableCollection<Object>), typeof(SfDataGridHelper), new PropertyMetadata(new ObservableCollection<Object>(), OnSelectedItemsChanged));
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (!(d is SfDataGrid sfDataGrid))
                return;
            //SfDataGridHelper.SelectedItems property updated based on SfDataGrid.SelectedItems Collectionchanged event.
            sfDataGrid.SelectedItems.CollectionChanged += (sender, e) =>
            {
                SfDataGridHelper.SetSelectedItems(sfDataGrid, sfDataGrid.SelectedItems);
            };
        }
    }
}