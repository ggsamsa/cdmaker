using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Site
{
    /**
     * Código fornecido nas aulas. Não alterei.
     * */

    public class UserReg
    {
        public UserReg(string pUser, string pPass, string end)
        {
            user = pUser;
            pass = pPass;
            session = "";
            url = "";
        }
        public string user;
        public string pass;
        public string session;
        public string url;
    }

    public class SiteDatabase
    {
        private static Dictionary<string, UserReg> utilizadores = null;
        private static int nextSession = 0;

        public static Dictionary<string, UserReg> getChatServerUsers()
        {
            if (utilizadores == null)
            {
                utilizadores = new System.Collections.Generic.Dictionary<string, UserReg>();
                init();
            }

            return utilizadores;
        }

        public static string getNextSessionToken()
        {
            nextSession += 1;
            return nextSession.ToString();
        }

        private static void init()
        {
            utilizadores.Add("joao", new UserReg("joao", "passjoao", ""));
            utilizadores.Add("maria", new UserReg("maria", "passmaria", ""));

        }

    }

    public class LoginResponseStructure
    {
        public string session;
        public string error;
        public Boolean status;       // true = ok; false = error
    }

    public class SessionManager
    {
        public LoginResponseStructure login(string user, string pass, string end)
        {
            LoginResponseStructure r = new LoginResponseStructure();

            Dictionary<string, UserReg> users = SiteDatabase.getChatServerUsers();
            if (users.ContainsKey(user))
            {
                UserReg ur = users[user];

                if (ur.pass.CompareTo(pass) != 0)
                {
                    // password invalida
                    r.session = "";
                    r.status = false;
                    r.error = "password invalida!";

                }
                else
                {
                    if (ur.session.Length != 0)
                    {
                        // utilizador ja tem sessao
                        r.session = "";
                        r.status = false;
                        r.error = "Utilizador ja tem sessao activa!";
                    }
                    else
                    {
                        ur.session = SiteDatabase.getNextSessionToken();
                        ur.url = end;

                        r.session = ur.session;
                        r.status = true;
                        r.error = "";
                    }
                }
            }
            else
            {
                r.session = "";
                r.status = false;
                r.error = "Utilizador inexistente!";
            }
            return r;
        }

        public bool logout(String session)
        {
            Dictionary<string, UserReg> users = SiteDatabase.getChatServerUsers();

            foreach (UserReg user in users.Values)
            {
                if (user.session.CompareTo(session) == 0)
                {
                    // sessao encontrada... vamos desactivar
                    user.session = "";
                    return true;
                }
            }

            return false;
        }

    }


}
