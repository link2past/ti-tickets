using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class TipoUsuario : ITipoUsuario
    {
        #region Miembros de ITipoUsuario

        public IList<TipoUsuarioInfo> Listar(TipoUsuarioInfo oTipoUsuario)
        {
            var sqlParm = new SqlParameter[3];
            var oListaTipoUsuario = new List<TipoUsuarioInfo>();

            sqlParm[0] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
            if (oTipoUsuario.IdTipoUsuario.HasValue) { sqlParm[0].Value = oTipoUsuario.IdTipoUsuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oTipoUsuario.Descripcion != null) { sqlParm[1].Value = oTipoUsuario.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oTipoUsuario.IdEstado.HasValue) { sqlParm[2].Value = oTipoUsuario.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_TIPO_USUARIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaTipoUsuario.Add(new TipoUsuarioInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()), null, null));
                        }
                    }
                }
            }

            return oListaTipoUsuario;
        }

        public TipoUsuarioInfo Consultar(TipoUsuarioInfo oTipoUsuario)
        {
            var sqlParm = new SqlParameter[3];
            var oEntTipoUsuario = new TipoUsuarioInfo();

            sqlParm[0] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
            if (oTipoUsuario.IdTipoUsuario.HasValue) { sqlParm[0].Value = oTipoUsuario.IdTipoUsuario; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oTipoUsuario.Descripcion != null) { sqlParm[1].Value = oTipoUsuario.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oTipoUsuario.IdEstado.HasValue) { sqlParm[2].Value = oTipoUsuario.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_TIPO_USUARIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntTipoUsuario = new TipoUsuarioInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                             Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()), null, null);
                    }
                }
            }

            return oEntTipoUsuario;
        }

        public bool Registrar(TipoUsuarioInfo oTipoUsuario, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oTipoUsuario.Descripcion != null){ sqlParm[0].Value = oTipoUsuario.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oTipoUsuario.IdEstado.HasValue) { sqlParm[1].Value = oTipoUsuario.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oTipoUsuario.UsuarioCreacion != null) { sqlParm[2].Value = oTipoUsuario.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) {Direction = ParameterDirection.Output};

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_TIPO_USUARIO",
                                              sqlParm);
                    nId = Int32.Parse(sqlParm[3].Value.ToString());
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

                return true;
            }
        }

        public bool Actualizar(TipoUsuarioInfo oTipoUsuario)
        {
            var sqlParm = new SqlParameter[4];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Tipo_Usuario", SqlDbType.Int);
                    if (oTipoUsuario.IdTipoUsuario.HasValue) { sqlParm[0].Value = oTipoUsuario.IdTipoUsuario; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oTipoUsuario.Descripcion != null) { sqlParm[1].Value = oTipoUsuario.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oTipoUsuario.IdEstado.HasValue) { sqlParm[2].Value = oTipoUsuario.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oTipoUsuario.UsuarioModificacion != null) { sqlParm[3].Value = oTipoUsuario.UsuarioModificacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_TIPO_USUARIO",
                                              sqlParm);
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

                return true;
            }
        }

        #endregion
    }
}
