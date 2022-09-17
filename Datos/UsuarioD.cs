using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Datos
{
    public class UsuarioD
    {
        public List<UsuarioE> AccesoUsuario(string cod_usu, string password)
        {
            List<UsuarioE> usuarioRoles = new List<UsuarioE>();
            using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
            {
                var sp = context.sp_valida_usuario_ingresa(cod_usu, password);
                foreach (var item in sp)
                {
                    UsuarioE usu = new UsuarioE();
                    usu.cod_usu = item.cod_usu;
                    usu.identificacion = item.identificacion;
                    usu.rol = item.id_rol;
                    usu.Nombres = item.nombre;
                    usu.cambio_clave = item.cambio_clave;
                    usu.fecha_cambio_clave = item.fecha_cambio_clave;
                    usu.estado = item.estado;
                    usu.direccionEntrega = item.direccionEntrega;
                    usu.privilegiado = item.privilegiado;
                    usuarioRoles.Add(usu);
                }
            }
            return usuarioRoles;
        }


        public List<MenuE> MenuUsuario(bool booEstado, string strRoles)
        {
            List<MenuE> menu = new List<MenuE>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_menu(booEstado, strRoles);
                    foreach (var item in sp)
                    {
                        MenuE m = new MenuE();
                        m.id_nivel = item.id_nivel;
                        m.desc = item.descripcion;
                        m.id_padre = item.id_padre;
                        m.url = item.url;
                        menu.Add(m);
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return menu;
        }

        public bool ActualizaClave(string cod_usu, string pass_ant, string pass_new)
        {
            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var usu = context.Usuarios.Where(u => u.cod_usu == cod_usu && u.password == pass_ant).FirstOrDefault();
                    if (usu != null)
                    {
                        context.sp_actualiza_password(cod_usu, pass_ant, pass_new);
                        update = true;
                    }
                }
            }
            catch (Exception ex)

            {

            }
            return update;
        }

        public List<MenuNuevoE> MenuUsuarioNuevo(bool booEstado, string strRoles)
        {
            List<MenuNuevoE> menu = new List<MenuNuevoE>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_menu_nuevo(booEstado, strRoles);
                    foreach (var item in sp)
                    {
                        MenuNuevoE m = new MenuNuevoE();
                        m.MenuId = item.MenuId;
                        m.ParentMenuId = item.ParentMenuId;
                        m.Title = item.Title;
                        m.Description = item.Description;
                        m.Url = item.Url;
                        m.CssFont = item.CssFont;
                        menu.Add(m);
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return menu;
        }

        public List<ListaPuntos> ListaPuntos(string codigoUsuario)
        {
            List<ListaPuntos> puntos = new List<ListaPuntos>();
            DataSet dSet = null;

            
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_puntos_obtenidos(codigoUsuario);
                    foreach (var item in sp)
                    {
                        ListaPuntos m = new ListaPuntos();
                        m.puntosObtenidos = Convert.ToString( item.PuntosObtenidos);
                       

                        puntos.Add(m);
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return puntos;
        }
        public List<ListaCasillero> ListaCasillero(string codigoUsuario)
        {
            List<ListaCasillero> puntos = new List<ListaCasillero>();
            DataSet dSet = null;


            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_codigo_casillero(codigoUsuario);
                    foreach (var item in sp)
                    {
                        ListaCasillero m = new ListaCasillero();
                        m.codiCasillero = (item.codCasillero);


                        puntos.Add(m);
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return puntos;
        }
        public List<ListaValorFob> ListaValorFob(string codigoUsuario)
        {
            List<ListaValorFob> valorAcumuladoFob = new List<ListaValorFob>();
            DataSet dSet = null;


            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_acumuado_fob(codigoUsuario);
                    foreach (var item in sp)
                    {
                        ListaValorFob m = new ListaValorFob();
                        m.valorAcumulado = Math.Round( Convert.ToDouble(item.valorAcumuadoRestante),2);


                        valorAcumuladoFob.Add(m);
                    }
                }
            }

            catch (Exception ex)
            {

            }
            return valorAcumuladoFob;
        }

        public bool ReseteoClave(string password, string cod_usu, string identificacion)
        {
            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    context.sp_reseteo_clave(password, cod_usu, identificacion);
                    update = true;
                }
            }
            catch (Exception ex)

            {

            }
            return update;
        }


        public bool CambioEstadoUsuario(bool estado, string cod_usu, string identificacion, int id_rol)
        {
            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    context.sp_cambio_estado_usuario(estado, cod_usu, identificacion, id_rol);
                    update = true;
                }
            }

            catch (Exception ex)
            {

            }
            return update;
        }

        public List<UsuariosSistemaE> ListadoUsuarios()
        {
            List<UsuariosSistemaE> usuarios = new List<UsuariosSistemaE>();
            try
            {
                using (LOADIMPSAEntities1 context = new Datos.LOADIMPSAEntities1())
                {
                    var sp = context.sp_listado_usuarios();
                    foreach (var item in sp)
                    {
                        UsuariosSistemaE usuario = new UsuariosSistemaE();
                        usuario.cod_usu = item.cod_usu;
                        usuario.identificacion = item.numeroIdentificacion;
                        usuario.nombres = item.Nombres;
                        usuario.estado = item.estado;
                        usuario.tipo = item.tipo;
                        usuarios.Add(usuario);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return usuarios;
        }

        public bool RecuperacionClaveMail(string strCedulaUsuario)
        {
            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var usu = context.sp_recupera_clave_mail(strCedulaUsuario);
                    update = true;

                }
            }
            catch (Exception ex)
            {

            }
            return update;
        }


        public bool EliminaClienteEmpleado(string strCedulaUsuario)
        {
            bool update = false;
            try
            {
                using (LOADIMPSAEntities1 context = new LOADIMPSAEntities1())
                {
                    var usu = context.sp_eliminar_clientes(strCedulaUsuario);
                    update = true;

                }
            }
            catch (Exception ex)
            {

            }
            return update;
        }
    }
}
