using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Usuario : IUsuario
    {
        #region Miembros de IUsuario

        public bool ValidarAcceso(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[2];
            Boolean bExito = false;

            sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oUsuario.Usuario != null) { sqlParm[0].Value = oUsuario.Usuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Contrasena", SqlDbType.VarChar);
            if (oUsuario.Contraseña != null) { sqlParm[1].Value = oUsuario.Contraseña; } else { sqlParm[1].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_VALIDAR_USUARIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        bExito = Convert.ToBoolean(drd.GetValue(0));
                    }
                }
            }

            return bExito;
        }

        public UsuarioInfo Consultar(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[4];
            var oUsuarioEnt = new UsuarioInfo();

            sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oUsuario.Usuario != null) { sqlParm[0].Value = oUsuario.Usuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oUsuario.Nombre != null) { sqlParm[1].Value = oUsuario.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuario.IdEstado.HasValue) { sqlParm[2].Value = oUsuario.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
            if (oUsuario.IdTipoUsuario.HasValue) { sqlParm[3].Value = oUsuario.IdTipoUsuario; } else { sqlParm[3].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                      CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oUsuarioEnt = new UsuarioInfo
                            {
                                Usuario = drd.GetString(0).Trim(),
                                Nombre = drd.GetString(1).Trim(),
                                FechaCreacion = drd.GetDateTime(2),
                                DiasSolicitudCambio = Int32.Parse(drd.GetValue(3).ToString()),
                                FechaUltimoCambio = drd.GetDateTime(4),
                                RequiereCambioClave = drd.GetString(5).Trim(),
                                IdEstado = Int32.Parse(drd.GetValue(6).ToString()),
                                Estado = new EstadoInfo(null, drd.GetString(7).Trim()),
                                IdTipoUsuario = Int32.Parse(drd.GetValue(8).ToString()),
                                TipoUsuario = new TipoUsuarioInfo(null, drd.GetString(9).Trim(), null, null, null, null),
                                Email = drd.GetString(10).Trim(),
                                Contraseña = drd.GetString(11).Trim()
                            };
                    }
                }
            }
            return oUsuarioEnt;
        }

        public IList<UsuarioInfo> Listar(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[4];
            var oUsuarioLista = new List<UsuarioInfo>();

            sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oUsuario.Usuario != null) { sqlParm[0].Value = oUsuario.Usuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oUsuario.Nombre != null) { sqlParm[1].Value = oUsuario.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuario.IdEstado.HasValue) { sqlParm[2].Value = oUsuario.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
            if (oUsuario.IdTipoUsuario.HasValue) { sqlParm[3].Value = oUsuario.IdTipoUsuario; } else { sqlParm[3].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                      CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oUsuarioEnt = new UsuarioInfo
                            {
                                Usuario = drd.GetString(0).Trim(),
                                Nombre = drd.GetString(1).Trim(),
                                FechaCreacion = drd.GetDateTime(2),
                                DiasSolicitudCambio = Int32.Parse(drd.GetValue(3).ToString()),
                                FechaUltimoCambio = drd.GetDateTime(4),
                                RequiereCambioClave = drd.GetString(5).Trim(),
                                IdEstado = Int32.Parse(drd.GetValue(6).ToString()),
                                Estado = new EstadoInfo(null, drd.GetString(7).Trim()),
                                IdTipoUsuario = Int32.Parse(drd.GetValue(8).ToString()),
                                TipoUsuario = new TipoUsuarioInfo(null, drd.GetString(9).Trim(), null, null, null, null),
                                Email = drd.GetString(10).Trim()
                            };
                            oUsuarioLista.Add(oUsuarioEnt);
                        }

                    }
                }
            }
            return oUsuarioLista;
        }

        public bool Registrar(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[8];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oUsuario.Usuario != null) { sqlParm[0].Value = oUsuario.Usuario; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Contrasena", SqlDbType.VarChar);
                    if (oUsuario.Contraseña != null) { sqlParm[1].Value = oUsuario.Contraseña; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oUsuario.Nombre != null) { sqlParm[2].Value = oUsuario.Nombre; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Dias_Solicitud_Cambio", SqlDbType.Int);
                    if (oUsuario.DiasSolicitudCambio.HasValue) { sqlParm[3].Value = oUsuario.DiasSolicitudCambio; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Requiere_Cambio_Clave", SqlDbType.VarChar);
                    if (oUsuario.RequiereCambioClave != null) { sqlParm[4].Value = oUsuario.RequiereCambioClave; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuario.IdEstado.HasValue) { sqlParm[5].Value = oUsuario.IdEstado; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
                    if (oUsuario.IdTipoUsuario.HasValue) { sqlParm[6].Value = oUsuario.IdTipoUsuario; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Email", SqlDbType.VarChar);
                    if (oUsuario.Email != null) { sqlParm[7].Value = oUsuario.Email; } else { sqlParm[7].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_USUARIO", sqlParm);
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        public bool Actualizar(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[8];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oUsuario.Usuario != null) { sqlParm[0].Value = oUsuario.Usuario; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Contrasena", SqlDbType.VarChar);
                    if (oUsuario.Contraseña != null) { sqlParm[1].Value = oUsuario.Contraseña; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oUsuario.Nombre != null) { sqlParm[2].Value = oUsuario.Nombre; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Dias_Solicitud_Cambio", SqlDbType.Int);
                    if (oUsuario.DiasSolicitudCambio.HasValue) { sqlParm[3].Value = oUsuario.DiasSolicitudCambio; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Requiere_Cambio_Clave", SqlDbType.VarChar);
                    if (oUsuario.RequiereCambioClave != null) { sqlParm[4].Value = oUsuario.RequiereCambioClave; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuario.IdEstado.HasValue) { sqlParm[5].Value = oUsuario.IdEstado; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
                    if (oUsuario.IdTipoUsuario.HasValue) { sqlParm[6].Value = oUsuario.IdTipoUsuario; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Email", SqlDbType.VarChar);
                    if (oUsuario.Email != null) { sqlParm[7].Value = oUsuario.Email; } else { sqlParm[7].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_USUARIO", sqlParm);
                    trx.Commit();
                }
                catch (Exception)
                {
                    trx.Rollback();
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
            return true;
        }

        public IList<UsuarioInfo> ListarTecnicosLibres(UsuarioInfo oUsuario)
        {
            var sqlParm = new SqlParameter[1];
            var oUsuarioLista = new List<UsuarioInfo>();

            sqlParm[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oUsuario.Nombre != null) { sqlParm[0].Value = oUsuario.Nombre; } else { sqlParm[0].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                      CommandType.StoredProcedure, "TI_SP_CONSULTAR_TECNICOS_LIBRES", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oUsuarioEnt = new UsuarioInfo
                            {
                                Usuario = drd.GetString(0).Trim(),
                                Nombre = drd.GetString(1).Trim()
                            };
                            oUsuarioLista.Add(oUsuarioEnt);
                        }

                    }
                }
            }
            return oUsuarioLista;            
        }

        #endregion
    }
}
