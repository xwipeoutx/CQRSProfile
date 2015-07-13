using System;

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
    }
    public class NewProfile
    {
        private string _name;
        private string _url;

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetPhotoUrl(string url)
        {
            if (url == "Some Shock Site")
                throw new InvalidOperationException();

            _url = url;
        }
    }

    class ProfileReadController
    {
        private readonly IRepository<ReadProfile> _profileRepository;

        public ProfileReadController(IRepository<ReadProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public ReadProfile Get(int id)
        {
            return _profileRepository.Get(id);
        }
    }

    public class ReadProfile
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
    }
}