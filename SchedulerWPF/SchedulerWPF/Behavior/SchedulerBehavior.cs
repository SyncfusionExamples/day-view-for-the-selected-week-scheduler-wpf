using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using Syncfusion.UI.Xaml.Scheduler;

namespace WpfScheduler
{
    public class SchedulerBehavior : Behavior<Grid>
    {
        SfScheduler scheduler;
        Button dayButton, weekButton;
        private DateTime minDate, maxDate;

        protected override void OnAttached()
        {
            base.OnAttached();
            scheduler = this.AssociatedObject.FindName("Schedule") as SfScheduler;
            dayButton = this.AssociatedObject.FindName("DayButton") as Button;
            weekButton = this.AssociatedObject.FindName("WeekButton") as Button;
            scheduler.DisplayDate = DateTime.Now.Date.AddHours(9);
            this.OnWirEvents();
        }

        private void OnWirEvents()
        {
            this.dayButton.Click += DayButton_Click;
            this.weekButton.Click += WeekButton_Click;
            this.scheduler.ViewChanged += Scheduler_ViewChanged;
        }

        private void Scheduler_ViewChanged(object sender, ViewChangedEventArgs e)
        {
            if (this.scheduler.ViewType == SchedulerViewType.Week)
            {
                minDate = e.NewValue.ActualStartDate;
                maxDate = e.NewValue.ActualEndDate;

                this.scheduler.MinimumDate = new DateTime(01, 01, 01);
                this.scheduler.MaximumDate = new DateTime(9999, 12, 31);
            }
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            this.scheduler.ViewType = SchedulerViewType.Week;
        }

        private void DayButton_Click(object sender, RoutedEventArgs e)
        {
            this.scheduler.MinimumDate = minDate;
            this.scheduler.MaximumDate = maxDate;
            this.scheduler.ViewType = SchedulerViewType.Day;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.UnWireEvents();
            this.scheduler = null;
            this.dayButton = null;
            this.weekButton = null;
        }

        private void UnWireEvents()
        {
            this.dayButton.Click -= DayButton_Click;
            this.weekButton.Click -= WeekButton_Click;
            this.scheduler.ViewChanged -= Scheduler_ViewChanged;
        }
    }
}