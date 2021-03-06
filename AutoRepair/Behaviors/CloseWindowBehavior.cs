﻿using System.Windows;
using System.Windows.Interactivity;

namespace AutoRepair.Behaviors
{
    public class CloseWindowBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty CloseTriggerProperty =
                DependencyProperty.Register("CloseTrigger", typeof(bool), typeof(CloseWindowBehavior),
                        new PropertyMetadata(false, OnCloseTriggerChanged));

        public bool CloseTrigger
        {
            get => (bool) GetValue(CloseTriggerProperty);
            set => SetValue(CloseTriggerProperty, value);
        }

        private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CloseWindowBehavior behavior) behavior.OnCloseTriggerChanged();
        }

        private void OnCloseTriggerChanged()
        {
            if (CloseTrigger) AssociatedObject.Close();
        }
    }
}