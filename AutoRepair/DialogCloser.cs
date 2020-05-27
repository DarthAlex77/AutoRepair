using System.Windows;

namespace AutoRepair
{
    public static class DialogCloser
    {
        public static DependencyProperty DialogCloseProperty =
            DependencyProperty.RegisterAttached ("DialogClose",
                typeof (bool), typeof (DialogCloser), new UIPropertyMetadata (false, (d, e) =>
                {
                    if (d is Window w && (bool) e.NewValue)
                    {
                        w.DialogResult = true ;
                        w.Close () ;
                    }
                })) ;

        public static bool GetDialogClose (DependencyObject obj)
        {
            return (bool) obj.GetValue (DialogCloseProperty) ;
        }

        public static void SetDialogClose (DependencyObject obj, bool value)
        {
            obj.SetValue (DialogCloseProperty, value) ;
        }
    }
}