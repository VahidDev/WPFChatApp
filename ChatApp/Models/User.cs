using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatApp.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        private int _unreadMessagesCount;
        public int UnreadMessagesCount
        {
            get => _unreadMessagesCount;
            set
            {
                if (_unreadMessagesCount == value)
                {
                    return;
                }

                _unreadMessagesCount = value;
                OnPropertyChanged();
            }
        }

        private string _draftMessage;
        public string DraftMessage
        {
            get => _draftMessage;
            set
            {
                if (_draftMessage == value)
                {
                    return;
                }

                _draftMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Message> Messages { get; set; } = [];

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
