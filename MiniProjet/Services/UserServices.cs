using MiniProjet.Models;
using MiniProjet.IServices;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using MiniProjet.Common;

namespace MiniProjet.Services
{
     public class UserServices : IUserServices
        {
        User _oUser = new User();
        List<User> _oUsers = new List<User>();

        public string delete(int id)
        {
            string message = "";
            try
            {
                _oUser = new User()
                {
                    Id = id
                };
                using(IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var oUsers = con.Query<User>("sp_user",
                        this.setParameters(_oUser, (int)OperationType.Delete),
                        commandType: CommandType.StoredProcedure);
                    message = "utilisateur supprimé";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public List<User> getAll()
        {
            _oUsers = new List<User>();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var oUsers = con.Query<User>("select * from User ").ToList();
                if (oUsers != null && oUsers.Count > 0)
                {
                    _oUser = oUsers[0];
                }
                return _oUsers;
            }
        }

        public User getById(int id)
        {
            _oUser = new User();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var oUsers = con.Query<User>("select * from User where id="+id).ToList();
                if (oUsers != null && oUsers.Count > 0)
                {
                    _oUser =  oUsers[0];
                }
                return _oUser;
            }
        }

        public User save(User oUser)
        {
            try
            {
                int operationType = Convert.ToInt32(oUser.Id == 0 ?
                    OperationType.Insert : OperationType.Update);
                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var oUsers = con.Query<User>("sp_user",
                        this.setParameters(oUser, operationType),
                        commandType: CommandType.StoredProcedure);
                    if (oUsers!= null && oUsers.Count() > 0)
                    {
                        _oUser = _oUsers.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return _oUser;
        }
        private object setParameters(User oUser, int operationType)
        { 
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", oUser.Id);
            parameters.Add("@username", oUser.Name);
            parameters.Add("@email", oUser.Email);
            parameters.Add("@mdp", oUser.Password);
            parameters.Add("@valide", oUser.valide);
            return parameters; 
        }
    }

}
