
using System.Linq;

namespace CQRSProfile
{
    class Program
    {
        static void Main(string[] args)
        {
            var oldController = new OldSchoolController(new Repository<OldProfile>());
            var newWriteController = new ProfileWriteController(new Repository<NewProfile>());
            var newReadController = new ProfileReadController(new Repository<ReadProfile>());

            // Old
            OldProfile profile = oldController.Get(1);
            profile.Url = "New URL";
            oldController.Update(profile);
            profile = oldController.Get(profile.Id);
            profile.Friends = profile.Friends.Concat(new[] { oldController.Get(2) });
            oldController.Update(profile);

            // New
            ReadProfile readProfile = newReadController.Get(1);
            newWriteController.SetPhotoUrl(readProfile.Id, "New URL");
            readProfile = newReadController.Get(readProfile.Id);
            newWriteController.Befriend(readProfile.Id, 2);
        }
    }
}
