using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using static System.Collections.Specialized.BitVector32;

namespace negocio
{
    public class AuthorizationManager
    {

        public enum PERMISSIONS
        {
            USER_MANAGEMENT,
            CREATE_TICKETS,
            SEE_ALL_TICKETS,
            CLIENT_MANAGENT,
            TICKET_ASSIGN
        }

        private static Dictionary<PERMISSIONS, List<Role.ROLES>> AUTHORIZATIONS = new Dictionary<PERMISSIONS, List<Role.ROLES>> {
            { PERMISSIONS.USER_MANAGEMENT, new List<Role.ROLES> { Role.ROLES.ADMIN } },
            { PERMISSIONS.CREATE_TICKETS, new List<Role.ROLES> { Role.ROLES.OPERATOR } },
            { PERMISSIONS.SEE_ALL_TICKETS, new List<Role.ROLES> { Role.ROLES.ADMIN, Role.ROLES.SUPERVISOR } },
            { PERMISSIONS.CLIENT_MANAGENT, new List<Role.ROLES> { Role.ROLES.ADMIN, Role.ROLES.SUPERVISOR, Role.ROLES.OPERATOR } },
            { PERMISSIONS.TICKET_ASSIGN, new List<Role.ROLES> { Role.ROLES.SUPERVISOR } }
        };

        private static AuthorizationManager instance;
        private User user;
        public User User { get {
                return user;    
        } }
        
        private AuthorizationManager() {
            if (!sessionExists())
            {
                return;
            }
            var value = getSession()["usuario"];

            if (value == null) {
                return;
            }
            user = (User)value;
        }

        public static AuthorizationManager getInstance()
        {
            if (instance == null)
            {
                instance = new AuthorizationManager();
            }

            return instance;
        }

        private static HttpSessionState getSession()
        {
            return HttpContext.Current.Session;
        }

        private static bool sessionExists()
        {
            return HttpContext.Current != null && HttpContext.Current.Session != null;
        }

        public bool isLogIn()
        {
            return getInstance().User != null;
        }

        public bool isRegitered()
        {
            return user.Estado.Id == (int)UserState.USER_STATES.ACTIVE;
        }

        public User logIn(string username, string password) {

            UserBusiness userBusiness = new UserBusiness();

            User u = userBusiness.getOneByUserPass(username, password);

            if (u == null)
            {
                return null;
            }

            getSession().Add("usuario", u);

            user = u;

            return u;
        }

        public void logOut()
        {
            this.user = null;

            getSession()["usuario"] = null;

            return;
        }

        public bool hasPermission(PERMISSIONS permission)
        {

            if (!isLogIn()) return false;

            if (!AUTHORIZATIONS.ContainsKey(permission)) return false;

            List<Role.ROLES> roles = AUTHORIZATIONS[permission];

            return roles.Contains(User.Rol.Id);
        }
    }
}
