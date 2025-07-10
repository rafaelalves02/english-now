using EnglishNow.Services.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services
{
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string usuario, string senha);
    }

    public class UsuarioService : IUsuarioService
    {
        public ValidarLoginResult ValidarLogin(string usuario, string senha)
        {
            var result = new ValidarLoginResult();
            
            return result;
        }
    }
}
