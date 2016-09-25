using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class UsuarioCliente : IUsuarioCliente
    {
        #region Miembros de IUsuarioCliente

        public IList<UsuarioClienteInfo> Listar(UsuarioClienteInfo oUsuarioCliente)
        {
            var sqlParm = new SqlParameter[3];
            var oListaUsuarioCliente = new List<UsuarioClienteInfo>();

            sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oUsuarioCliente.IdUsuario != null) { sqlParm[0].Value = oUsuarioCliente.IdUsuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oUsuarioCliente.IdCliente.HasValue) { sqlParm[1].Value = oUsuarioCliente.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuarioCliente.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioCliente.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntUsuarioCliente = new UsuarioClienteInfo
                                {
                                    IdUsuario = drd.GetString(0).Trim(),
                                    Usuario = new UsuarioInfo(null, null, drd.GetString(1).Trim(), null, null,
                                                              null, null,
                                                              Int32.Parse(drd.GetValue(6).ToString()),
                                                              new EstadoInfo(null, drd.GetString(7).Trim()),
                                                              Int32.Parse(drd.GetValue(10).ToString()), new TipoUsuarioInfo(null, drd.GetString(11).Trim(), null, null, null, null), null),
                                    IdCliente = Int32.Parse(drd.GetValue(2).ToString()),
                                    Cliente = new ClienteInfo(null, drd.GetString(3).Trim(), null, null,
                                                              null, null, null, null, null, null, null, null,
                                                              null, null,
                                                              Int32.Parse(drd.GetValue(8).ToString()),
                                                              new EstadoInfo(null, drd.GetString(9).Trim()),
                                                              null, null),
                                    IdEstado = Int32.Parse(drd.GetValue(4).ToString()),
                                    Estado = new EstadoInfo(null, drd.GetString(5).Trim())
                                };
                            oListaUsuarioCliente.Add(oEntUsuarioCliente);
                        }
                    }
                }
            }

            return oListaUsuarioCliente;
        }

        public IList<UsuarioClienteInfo> ListarPendientes()
        {
            var oListaUsuarioCliente = new List<UsuarioClienteInfo>();

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_USUARIOS_CLIENTE_PENDIENTES_ASIGNAR"))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntUsuarioCliente = new UsuarioClienteInfo
                            {
                                IdUsuario = drd.GetString(0).Trim(),
                                Usuario = new UsuarioInfo(null, null, drd.GetString(1).Trim(), null, null,
                                                          null, null,
                                                          Int32.Parse(drd.GetValue(2).ToString()),
                                                          new EstadoInfo(null, drd.GetString(3).Trim()),
                                                          Int32.Parse(drd.GetValue(4).ToString()), new TipoUsuarioInfo(null, drd.GetString(5).Trim(), null, null, null, null), null)
                            };
                            oListaUsuarioCliente.Add(oEntUsuarioCliente);
                        }
                    }
                }
            }

            return oListaUsuarioCliente;
        }

        public UsuarioClienteInfo Consultar(UsuarioClienteInfo oUsuarioCliente)
        {
            var sqlParm = new SqlParameter[3];
            var oEntUsuarioCliente = new UsuarioClienteInfo();

            sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oUsuarioCliente.IdUsuario != null) { sqlParm[0].Value = oUsuarioCliente.IdUsuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oUsuarioCliente.IdCliente.HasValue) { sqlParm[1].Value = oUsuarioCliente.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuarioCliente.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioCliente.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntUsuarioCliente = new UsuarioClienteInfo
                        {
                            IdUsuario = drd.GetString(0).Trim(),
                            Usuario = new UsuarioInfo(null, null, drd.GetString(1).Trim(), null, null,
                                                      null, null,
                                                      Int32.Parse(drd.GetValue(6).ToString()),
                                                      new EstadoInfo(null, drd.GetString(7).Trim()),
                                                      Int32.Parse(drd.GetValue(10).ToString()), new TipoUsuarioInfo(null, drd.GetString(11).Trim(), null, null, null, null), null),
                            IdCliente = Int32.Parse(drd.GetValue(2).ToString()),
                            Cliente = new ClienteInfo(null, drd.GetString(3).Trim(), null, null,
                                                      null, null, null, null, null, null, null, null,
                                                      null, null,
                                                      Int32.Parse(drd.GetValue(8).ToString()),
                                                      new EstadoInfo(null, drd.GetString(9).Trim()),
                                                      null, null),
                            IdEstado = Int32.Parse(drd.GetValue(4).ToString()),
                            Estado = new EstadoInfo(null, drd.GetString(5).Trim())
                        };
                    }
                }
            }

            return oEntUsuarioCliente;
        }

        public bool Registrar(UsuarioClienteInfo oUsuarioCliente)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oUsuarioCliente.IdUsuario != null) { sqlParm[0].Value = oUsuarioCliente.IdUsuario; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oUsuarioCliente.IdCliente.HasValue) { sqlParm[1].Value = oUsuarioCliente.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuarioCliente.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioCliente.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oUsuarioCliente.UsuarioCreacion != null) { sqlParm[3].Value = oUsuarioCliente.UsuarioCreacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_USUARIO_CLIENTE", sqlParm);
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

        public bool Actualizar(UsuarioClienteInfo oUsuarioCliente)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oUsuarioCliente.IdUsuario != null) { sqlParm[0].Value = oUsuarioCliente.IdUsuario; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oUsuarioCliente.IdCliente.HasValue) { sqlParm[1].Value = oUsuarioCliente.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuarioCliente.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioCliente.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oUsuarioCliente.UsuarioCreacion != null) { sqlParm[3].Value = oUsuarioCliente.UsuarioCreacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_USUARIO_CLIENTE", sqlParm);
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

        #endregion
    }
}
