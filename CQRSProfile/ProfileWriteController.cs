using System;
using System.Collections.Generic;

namespace CQRSProfile
{
    class ProfileWriteController
    {
        private readonly IRepository<NewProfile> _profileRepository;

        public ProfileWriteController(IRepository<NewProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public void SetPhotoUrl(int id, string photoUrl)
        {
            NewProfile profile = _profileRepository.Get(id);
            profile.SetPhotoUrl(photoUrl);
            _profileRepository.Save(profile);
        }

        public void Befriend(int id, int friendId)
        {
            NewProfile profile = _profileRepository.Get(id);
            profile.Befriend(friendId);
            _profileRepository.Save(profile);
        }
    }

    public class NewProfile
    {
        private readonly IEventBus _bus;
        private int _id;
        private HashSet<int> _friends;

        public NewProfile(int id, IEventBus bus)
        {
            _id = id;
            _bus = bus;
        }

        public void SetName(string name)
        {
            _bus.Publish(new NameChanged { Id = _id, Name = name });
        }

        public void SetPhotoUrl(string url)
        {
            if (url == "Some Shock Site")
                throw new InvalidOperationException();

            _bus.Publish(new PhotoUrlChanged { Id = _id, Url = url });
        }

        public void Befriend(int friendId)
        {
            if (_friends.Contains(friendId))
                throw new InvalidOperationException();

            _friends.Add(friendId);
            _bus.Publish(new FriendAdded { Id = _id, FriendId = friendId });
        }
    }

    class ProfileReadController
    {
        private readonly IRepository<ReadProfile> _profileDatabase;

        public ProfileReadController(IRepository<ReadProfile> profileDatabase)
        {
            _profileDatabase = profileDatabase;
        }

        public ReadProfile Get(int id)
        {
            return _profileDatabase.Get(id);
        }
    }

    class ProfileView
        : IEventHandler<NameChanged>, IEventHandler<PhotoUrlChanged>, IEventHandler<FriendAdded>
    {
        private readonly IRepository<ReadProfile> _profileDatabase;

        public ProfileView(IRepository<ReadProfile> profileDatabase)
        {
            _profileDatabase = profileDatabase;
        }

        public void Handle(NameChanged nameChanged)
        {
            var profile = _profileDatabase.Get(nameChanged.Id);
            profile.Name = nameChanged.Name;
            _profileDatabase.Save(profile);
        }

        public void Handle(PhotoUrlChanged @event)
        {
            var profile = _profileDatabase.Get(@event.Id);
            profile.Url = @event.Url;
            _profileDatabase.Save(profile);
        }

        public void Handle(FriendAdded @event)
        {
            var profile = _profileDatabase.Get(@event.Id);
            profile.FriendNames.Add(_profileDatabase.Get(@event.FriendId).Name);
            _profileDatabase.Save(profile);
        }
    }

    public class ReadProfile
    {
        private List<string> _friendNames;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<string> FriendNames
        {
            get { return _friendNames; }
            set { _friendNames = value; }
        }

        public void AddFriendName(string name)
        {
            _friendNames.Add(name);
        }
    }
}