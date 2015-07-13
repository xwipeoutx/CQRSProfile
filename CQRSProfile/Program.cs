
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
            profile = oldController.Get(1);

            // New
            ReadProfile readProfile = newReadController.Get(1);
            newWriteController.SetPhotoUrl(1, "New URL");
            readProfile = newReadController.Get(1);
        }
    }
}
