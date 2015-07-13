using System;

namespace CQRSProfile
{
    class OldSchoolController
    {
        private readonly IRepository<OldProfile> _profileRepository;

        public OldSchoolController(IRepository<OldProfile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public OldProfile Get(int id)
        {
            return _profileRepository.Get(id);
        }

        public void Update(OldProfile profile)
        {
            if (profile.Url == "Some Shock Site")
                throw new InvalidOperationException();

            _profileRepository.Save(profile);
        }
    }

    public class OldProfile
    {
        public string Name { get; set; }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (value == "Some Shock Site")
                    throw new InvalidOperationException();

                _url = value;
            }
        }
    }
}