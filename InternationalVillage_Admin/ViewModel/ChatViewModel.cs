using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Services;
using InternationalVillage_Admin.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace InternationalVillage_Admin.ViewModel
{
    class ChatViewModel : BaseViewModel
    {
        private IDialogService dialogService;
        private TaskFactory ctxTaskFactory;

        public ChatViewModel(IDialogService diagSvc)
        {
            dialogService = diagSvc;

            ChatStore.Instance.ChatService.NewTextMessage += NewTextMessage;
            ChatStore.Instance.ChatService.NewImageMessage += NewImageMessage;
            ChatStore.Instance.ChatService.ParticipantLoggedIn += ParticipantLogin;
            ChatStore.Instance.ChatService.ParticipantLoggedOut += ParticipantDisconnection;
            ChatStore.Instance.ChatService.ParticipantDisconnected += ParticipantDisconnection;
            ChatStore.Instance.ChatService.ParticipantReconnected += ParticipantReconnection;
            ChatStore.Instance.ChatService.ParticipantTyping += ParticipantTyping;
            ChatStore.Instance.ChatService.ConnectionReconnecting += Reconnecting;
            ChatStore.Instance.ChatService.ConnectionReconnected += Reconnected;
            ChatStore.Instance.ChatService.ConnectionClosed += Disconnected;

            ctxTaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Participant> _participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> Participants
        {
            get { return _participants; }
            set
            {
                _participants = value;
                OnPropertyChanged();
            }
        }

        private UserModes _userMode;
        public UserModes UserMode
        {
            get { return _userMode; }
            set
            {
                _userMode = value;
                OnPropertyChanged();
            }
        }

        #region Login Command
        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged();
            }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand =
                    new RelayCommandAsync(() => Login(), (o) => CanLogin()));
            }
        }

        private async Task<bool> Login()
        {
            IsConnected = ChatStore.Instance.IsConnected;
            try
            {
                List<User> users = new List<User>();
                users = await ChatStore.Instance.ChatService.LoginAsync(AccountStore.Instance.Role, AccountStore.Instance.Avatar);
                if (users != null)
                {
                    users.ForEach(u => Participants.Add(new Participant { Name = u.Name, Avatar = u.Avatar }));
                    UserMode = UserModes.Chat;
                    ChatStore.Instance.IsLoggedIn = true;
                    IsLoggedIn = ChatStore.Instance.IsLoggedIn;
                    return true;
                }
                else
                {
                    dialogService.ShowNotification("Username is already in use");
                    return false;
                }

            }
            catch (Exception) { return false; }
        }

        private bool CanLogin()
        {
            return !ChatStore.Instance.IsLoggedIn;
        }
        #endregion

        #region Typing Command
        private Participant _selectedParticipant;
        public Participant SelectedParticipant
        {
            get { return _selectedParticipant; }
            set
            {
                _selectedParticipant = value;
                if (SelectedParticipant.HasSentNewMessage) SelectedParticipant.HasSentNewMessage = false;
                OnPropertyChanged();
            }
        }

        private ICommand _typingCommand;
        public ICommand TypingCommand
        {
            get
            {
                return _typingCommand ?? (_typingCommand =
                    new RelayCommandAsync(() => Typing(), (o) => CanUseTypingCommand()));
            }
        }

        private async Task<bool> Typing()
        {
            try
            {
                await ChatStore.Instance.ChatService.TypingAsync(SelectedParticipant.Name);
                return true;
            }
            catch (Exception) { return false; }
        }

        private bool CanUseTypingCommand()
        {
            return (SelectedParticipant != null && SelectedParticipant.IsLoggedIn);
        }
        #endregion

        #region Event Handlers
        private void NewTextMessage(string name, string msg, MessageType mt)
        {
            if (mt == MessageType.Unicast)
            {
                ChatMessage cm = new ChatMessage { Author = name, Message = msg, Time = DateTime.Now };
                var sender = _participants.Where((u) => string.Equals(u.Name, name)).FirstOrDefault();
                ctxTaskFactory.StartNew(() => sender.Chatter.Add(cm)).Wait();

                if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
                {
                    ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true).Wait();
                }
            }
        }

        private void NewImageMessage(string name, byte[] pic, MessageType mt)
        {
            if (mt == MessageType.Unicast)
            {
                var imgsDirectory = Path.Combine(Environment.CurrentDirectory, "Image Messages");
                if (!Directory.Exists(imgsDirectory)) Directory.CreateDirectory(imgsDirectory);

                var imgsCount = Directory.EnumerateFiles(imgsDirectory).Count() + 1;
                var imgPath = Path.Combine(imgsDirectory, $"IMG_{imgsCount}.jpg");

                ImageConverter converter = new ImageConverter();
                using (Image img = (Image)converter.ConvertFrom(pic))
                {
                    img.Save(imgPath);
                }

                ChatMessage cm = new ChatMessage { Author = name, Picture = imgPath, Time = DateTime.Now };
                var sender = _participants.Where(u => string.Equals(u.Name, name)).FirstOrDefault();
                ctxTaskFactory.StartNew(() => sender.Chatter.Add(cm)).Wait();

                if (!(SelectedParticipant != null && sender.Name.Equals(SelectedParticipant.Name)))
                {
                    ctxTaskFactory.StartNew(() => sender.HasSentNewMessage = true).Wait();
                }
            }
        }

        private void ParticipantLogin(User u)
        {
            var ptp = Participants.FirstOrDefault(p => string.Equals(p.Name, u.Name));
            if (ChatStore.Instance.IsLoggedIn && ptp == null)
            {
                ctxTaskFactory.StartNew(() => Participants.Add(new Participant
                {
                    Name = u.Name,
                    Avatar = u.Avatar
                })).Wait();
            }
            else
            {
                ptp.IsLoggedIn = true;
            }
        }

        private void ParticipantDisconnection(string name)
        {
            var person = Participants.Where((p) => string.Equals(p.Name, name)).FirstOrDefault();
            if (person != null) person.IsLoggedIn = false;
        }

        private void ParticipantReconnection(string name)
        {
            var person = Participants.Where((p) => string.Equals(p.Name, name)).FirstOrDefault();
            if (person != null) person.IsLoggedIn = true;
        }

        private void Reconnecting()
        {
            ChatStore.Instance.IsConnected = false;
            ChatStore.Instance.IsLoggedIn = false;
        }

        private async void Reconnected()
        {
            await ChatStore.Instance.ChatService.LoginAsync(AccountStore.Instance.Username, AccountStore.Instance.Avatar);
            ChatStore.Instance.IsConnected = true;
            ChatStore.Instance.IsLoggedIn = true;
        }

        private async void Disconnected()
        {
            var connectionTask = ChatStore.Instance.ChatService.ConnectAsync();
            await connectionTask.ContinueWith(t => {
                if (!t.IsFaulted)
                {
                    ChatStore.Instance.IsConnected = true;
                    ChatStore.Instance.ChatService.LoginAsync(AccountStore.Instance.Name, AccountStore.Instance.Avatar).Wait();
                    ChatStore.Instance.IsLoggedIn = true;
                }
            });
        }

        private void ParticipantTyping(string name)
        {
            var person = Participants.Where((p) => string.Equals(p.Name, name)).FirstOrDefault();
            if (person != null && !person.IsTyping)
            {
                person.IsTyping = true;
                Observable.Timer(TimeSpan.FromMilliseconds(1500)).Subscribe(t => person.IsTyping = false);
            }
        }
        #endregion

        #region Send Text Message Command

        private ICommand _sendTextMessageCommand;
        public ICommand SendTextMessageCommand
        {
            get
            {
                return _sendTextMessageCommand ?? (_sendTextMessageCommand =
                    new RelayCommandAsync(() => SendTextMessage(), (o) => CanSendTextMessage()));
            }
        }

        private string _textMessage;
        public string TextMessage
        {
            get { return _textMessage; }
            set
            {
                _textMessage = value;
                OnPropertyChanged();
            }
        }

        private async Task<bool> SendTextMessage()
        {
            try
            {
                var recepient = _selectedParticipant.Name;
                await ChatStore.Instance.ChatService.SendUnicastMessageAsync(recepient, _textMessage);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                ChatMessage msg = new ChatMessage
                {
                    Author = AccountStore.Instance.Name,
                    Message = _textMessage,
                    Time = DateTime.Now,
                    IsOriginNative = true
                };
                SelectedParticipant.Chatter.Add(msg);
                TextMessage = string.Empty;
            }
        }

        private bool CanSendTextMessage()
        {
            return (!string.IsNullOrEmpty(TextMessage) && ChatStore.Instance.IsConnected &&
                _selectedParticipant != null && _selectedParticipant.IsLoggedIn);
        }
        #endregion

        #region Send Picture Message Command
        private ICommand _sendImageMessageCommand;
        public ICommand SendImageMessageCommand
        {
            get
            {
                return _sendImageMessageCommand ?? (_sendImageMessageCommand =
                    new RelayCommandAsync(() => SendImageMessage(), (o) => CanSendImageMessage()));
            }
        }

        private async Task<bool> SendImageMessage()
        {
            var pic = dialogService.OpenFile("Select image file", "Images (*.jpg;*.png)|*.jpg;*.png");
            if (string.IsNullOrEmpty(pic)) return false;

            var img = await Task.Run(() => File.ReadAllBytes(pic));

            try
            {
                var recepient = _selectedParticipant.Name;
                await ChatStore.Instance.ChatService.SendUnicastMessageAsync(recepient, img);
                return true;
            }
            catch (Exception) { return false; }
            finally
            {
                ChatMessage msg = new ChatMessage { Author = AccountStore.Instance.Name, Picture = pic, Time = DateTime.Now, IsOriginNative = true };
                SelectedParticipant.Chatter.Add(msg);
            }
        }

        private bool CanSendImageMessage()
        {
            return (IsConnected && _selectedParticipant != null && _selectedParticipant.IsLoggedIn);
        }
        #endregion
    }
}
