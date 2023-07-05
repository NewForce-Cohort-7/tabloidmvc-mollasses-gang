using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByEmail(string email);
        void AddUserProfile(UserProfile userProfile);
        UserProfile GetUserProfileById(int id);

        List<UserProfile> GetAllUserProfiles();
    }
}