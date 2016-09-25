using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class NivelUrgencia : INivelUrgencia
    {
        #region Miembros de INivelUrgencia

        public IList<NivelUrgenciaInfo> Listar(NivelUrgenciaInfo oNivelUrgencia)
        {
            var sqlParm = new SqlParameter[3];
            var oListaNivelUrgencia = new List<NivelUrgenciaInfo>();

            sqlParm[0] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
            if (oNivelUrgencia.IdNivelUrgencia.HasValue) { sqlParm[0].Value = oNivelUrgencia.IdNivelUrgencia; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oNivelUrgencia.Descripcion != null) { sqlParm[1].Value = oNivelUrgencia.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oNivelUrgencia.IdEstado.HasValue) { sqlParm[2].Value = oNivelUrgencia.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_NIVEL_URGENCIA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaNivelUrgencia.Add(new NivelUrgenciaInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                                null, null));
                        }
                    }
                }
            }

            return oListaNivelUrgencia;
        }

        public NivelUrgenciaInfo Consultar(NivelUrgenciaInfo oNivelUrgencia)
        {
            var sqlParm = new SqlParameter[3];
            var oEntNivelUrgencia = new NivelUrgenciaInfo();

            sqlParm[0] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
            if (oNivelUrgencia.IdNivelUrgencia.HasValue) { sqlParm[0].Value = oNivelUrgencia.IdNivelUrgencia; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oNivelUrgencia.Descripcion != null) { sqlParm[1].Value = oNivelUrgencia.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oNivelUrgencia.IdEstado.HasValue) { sqlParm[2].Value = oNivelUrgencia.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_NIVEL_URGENCIA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntNivelUrgencia = new NivelUrgenciaInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                            Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                            null, null);
                    }
                }
            }

            return oEntNivelUrgencia;
        }

        public bool Registrar(NivelUrgenciaInfo oNivelUrgencia, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oNivelUrgencia.Descripcion != null) { sqlParm[0].Value = oNivelUrgencia.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oNivelUrgencia.IdEstado.HasValue) { sqlParm[1].Value = oNivelUrgencia.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oNivelUrgencia.UsuarioCreacion != null) { sqlParm[2].Value = oNivelUrgencia.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_NIVEL_URGENCIA", sqlParm);
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

        public bool Actualizar(NivelUrgenciaInfo oNivelUrgencia)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
                    if (oNivelUrgencia.IdNivelUrgencia.HasValue) { sqlParm[0].Value = oNivelUrgencia.IdNivelUrgencia; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oNivelUrgencia.Descripcion != null) { sqlParm[1].Value = oNivelUrgencia.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oNivelUrgencia.IdEstado.HasValue) { sqlParm[2].Value = oNivelUrgencia.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oNivelUrgencia.UsuarioModificacion != null) { sqlParm[3].Value = oNivelUrgencia.UsuarioModificacion; } else { sqlParm[3 ].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_NIVEL_URGENCIA", sqlParm);
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
