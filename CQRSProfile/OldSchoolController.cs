using System;
using System.Collections.Generic;
using System.Linq;

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
        public int Id { get; set; }
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

        private IEnumerable<OldProfile> _friends;
        public IEnumerable<OldProfile> Friends
        {
            get
            {
                return _friends;
            }
            set
            {
                var hasDuplicates = value.Select(f => f.Id).Distinct().Count() < value.Count();
                if (hasDuplicates)
                    throw new InvalidOperationException();

                _friends = value;
            }
        }
    }
}