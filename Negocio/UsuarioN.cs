using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace Negocio
{
    public class UsuarioN
    {
        UsuarioD usuD;
        public List<UsuarioE> AccesoUsuario(string cod_usu, string password)
        {
            usuD = new UsuarioD();
            var passEncript = Seguridad.EncriptarTexto(password);
            return usuD.AccesoUsuario(cod_usu, passEncript);
        }

        public List<MenuE> MenuUsuario(bool booEstado, string strRoles)
        {
            usuD = new UsuarioD();
            return usuD.MenuUsuario(booEstado, strRoles);
        }

        public bool ActualizaClave(string cod_usu, string pass_ant, string pass_new)
        {
            usuD = new UsuarioD();
            var pass_ant_enc = Seguridad.EncriptarTexto(pass_ant);
            var pass_new_enc = Seguridad.EncriptarTexto(pass_new);
            return usuD.ActualizaClave(cod_usu, pass_ant_enc, pass_new_enc);
        }

        public List<MenuNuevoE> MenuUsuarioNuevo(bool booEstado, string strRoles)
        {
            usuD = new UsuarioD();
            return usuD.MenuUsuarioNuevo(booEstado, strRoles);
        }

        public List<ListaPuntos> ListaPuntos(string CodigoUsuario)
        {
            usuD = new UsuarioD();
            return usuD.ListaPuntos(CodigoUsuario);
        }
        public List<ListaCasillero> ListaCasillero(string CodigoUsuario)
        {
            usuD = new UsuarioD();
            return usuD.ListaCasillero(CodigoUsuario);
        }
        public List<ListaValorFob> ListaValorFob(string CodigoUsuario)
        {
            usuD = new UsuarioD();
            return usuD.ListaValorFob(CodigoUsuario);
        }

        public bool ReseteoClave(string password, string cod_usu, string identificacion)
        {
            usuD = new UsuarioD();
            string new_pass = Seguridad.EncriptarTexto(password);
            return usuD.ReseteoClave(new_pass, cod_usu, identificacion);
        }

        public bool CambioEstadoUsuario(bool estado, string cod_usu, string identificacion, int id_rol)
        {
            usuD = new UsuarioD();
            return usuD.CambioEstadoUsuario(estado, cod_usu, identificacion, id_rol);
        }
        public List<UsuariosSistemaE> ListadoUsuarios()
        {
            usuD = new UsuarioD();
            return usuD.ListadoUsuarios();
        }

        public bool RecuperacionClaveMail(string strCedulaUsuario)
        {
            usuD = new UsuarioD();
            return usuD.RecuperacionClaveMail(strCedulaUsuario);
        }

        public bool EliminaClienteEmpleado(string strCedulaUsuario)
        {
            usuD = new UsuarioD();
            return usuD.EliminaClienteEmpleado(strCedulaUsuario);
        }
    }
}
