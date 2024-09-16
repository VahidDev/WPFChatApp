using ChatApp.Commands;
using ChatApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ChatApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly static string _basePath = AppDomain.CurrentDomain.BaseDirectory;

        public ObservableCollection<User> Users { get; }
        public ObservableCollection<Message> Messages => SelectedUser?.Messages;

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value) return;

                SaveDraftMessage();
                _selectedUser = value;
                OnPropertyChanged();
                LoadDraftMessage();
                ResetUnreadMessagesCount();
            }
        }

        private string _newMessage;
        public string NewMessage
        {
            get => _newMessage;
            set
            {
                if (_newMessage == value) return;
                _newMessage = value;
                OnPropertyChanged();
                RaiseSendMessageCommandCanExecute();
            }
        }

        public ICommand SendMessageCommand { get; }

        public ChatViewModel()
        {
            Users =
            [
                new() { Name = "User1", Icon = GetIconPath("gamer.png") },
                new() { Name = "User2", Icon = GetIconPath("ghostBlue.png") },
                new() { Name = "User3", Icon = GetIconPath("ghostRed.png") }
            ];

            SelectedUser = Users[0];
            SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
        }

        private void SaveDraftMessage()
        {
            if (_selectedUser == null)
            {
                return;
            }
         
            _selectedUser.DraftMessage = NewMessage;
        }

        private void LoadDraftMessage()
        {
            NewMessage = _selectedUser?.DraftMessage ?? string.Empty;
            OnPropertyChanged(nameof(NewMessage));
            OnPropertyChanged(nameof(Messages));
        }

        private void ResetUnreadMessagesCount()
        {
            if (_selectedUser == null) return;

            _selectedUser.UnreadMessagesCount = 0;
        }

        private void RaiseSendMessageCommandCanExecute()
        {
            if (SendMessageCommand is RelayCommand command)
            {
                command.RaiseCanExecuteChanged();
            }
        }

        private void SendMessage()
        {
            if (!CanSendMessage()) return;

            var newMessage = new Message
            {
                SenderName = "You",
                SenderIcon = GetIconPath("me.png"),
                TimeSent = DateTime.Now.ToString("hh:mm tt"),
                Content = NewMessage
            };

            SelectedUser?.Messages.Add(newMessage);
            NewMessage = string.Empty;
            _selectedUser.DraftMessage = string.Empty;

            _ = SimulateChatBotResponseAsync(SelectedUser).ConfigureAwait(false);
        }

        private bool CanSendMessage() => !string.IsNullOrWhiteSpace(NewMessage) && SelectedUser != null;

        private async Task SimulateChatBotResponseAsync(User chatUser)
        {
            await Task.Delay(5000);

            var botResponse = new Message
            {
                SenderName = chatUser.Name,
                SenderIcon = chatUser.Icon,
                TimeSent = DateTime.Now.ToString("hh:mm tt"),
                Content = "This is a random response!"
            };

            AddBotResponseToChat(chatUser, botResponse);
        }

        private void AddBotResponseToChat(User chatUser, Message botResponse)
        {
            if (chatUser == null)
            {
                return;
            }

            chatUser.Messages.Add(botResponse);

            if (chatUser == SelectedUser)
            {
                return;
            }

            chatUser.UnreadMessagesCount++;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static string GetIconPath(string iconName)
        {
            return Path.Combine(_basePath, "Icons", iconName);
        }
    }
}
