using MiniProjet.Models;
namespace MiniProjet.IServices
{
    public interface IUserServices
    {
        User save(User oUser);
        List<User> getAll();
        User getById(int id);
        string delete(int id);
    }
}
