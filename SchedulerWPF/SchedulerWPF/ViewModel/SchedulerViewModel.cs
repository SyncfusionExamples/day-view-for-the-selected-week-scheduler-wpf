using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfScheduler
{
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Meeting> meetings;
        private List<string> currentDayMeetings;
        public SchedulerViewModel()
        {
            this.Meetings = new ObservableCollection<Meeting>();
            this.AddAppointmentDetails();
            this.AddAppointments();
        }
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return this.meetings;
            }

            set
            {
                this.meetings = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        private void AddAppointmentDetails()
        {
            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("General Meeting");
            this.currentDayMeetings.Add("Plan Execution");
            this.currentDayMeetings.Add("Project Plan");
            this.currentDayMeetings.Add("Consulting");
            this.currentDayMeetings.Add("Support");
            this.currentDayMeetings.Add("Development Meeting");
            this.currentDayMeetings.Add("Scrum");
            this.currentDayMeetings.Add("Project Completion");
            this.currentDayMeetings.Add("Release updates");
            this.currentDayMeetings.Add("Performance Check");
        }
        private void AddAppointments()
        {
            var today = DateTime.Now.Date;
            var random = new Random();
            var meeting = new Meeting();
            meeting.From = today.AddHours(11);
            meeting.To = meeting.From.AddHours(1);
            meeting.EventName = this.currentDayMeetings[random.Next(7)];
            meeting.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#889e81"));
            this.Meetings.Add(meeting);

            var meeting1 = new Meeting();
            meeting1.From = today.AddHours(13);
            meeting1.To = meeting1.From.AddHours(2);
            meeting1.EventName = this.currentDayMeetings[random.Next(7)];
            meeting1.Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#889e81"));
            this.Meetings.Add(meeting1);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
   