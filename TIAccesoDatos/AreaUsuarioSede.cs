using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class AreaUsuarioSede : IAreaUsuarioSede
    {
        #region Miembros de IAreaUsuarioSede

        public IList<AreaUsuarioSedeInfo> Listar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            var sqlParm = new SqlParameter[3];
            var oListaAreaUsuarioSede = new List<AreaUsuarioSedeInfo>();

            sqlParm[0] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
            if (oAreaUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[0].Value = oAreaUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oAreaUsuarioSede.Descripcion != null) { sqlParm[1].Value = oAreaUsuarioSede.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oAreaUsuarioSede.IdEstado.HasValue) { sqlParm[2].Value = oAreaUsuarioSede.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_AREA_USUARIO_SEDE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaAreaUsuarioSede.Add(new AreaUsuarioSedeInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                                null, null));
                        }
                    }
                }
            }

            return oListaAreaUsuarioSede;
        }

        public AreaUsuarioSedeInfo Consultar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            var sqlParm = new SqlParameter[3];
            AreaUsuarioSedeInfo oEntAreaUsuarioSede = null;

            sqlParm[0] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
            if (oAreaUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[0].Value = oAreaUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oAreaUsuarioSede.Descripcion != null) { sqlParm[1].Value = oAreaUsuarioSede.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oAreaUsuarioSede.IdEstado.HasValue) { sqlParm[2].Value = oAreaUsuarioSede.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_AREA_USUARIO_SEDE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        {
                            oEntAreaUsuarioSede = new AreaUsuarioSedeInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                                null, null);
                        }
                    }
                }
            }

            return oEntAreaUsuarioSede;
        }

        public bool Registrar(AreaUsuarioSedeInfo oAreaUsuarioSede, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oAreaUsuarioSede.Descripcion != null) { sqlParm[0].Value = oAreaUsuarioSede.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oAreaUsuarioSede.IdEstado.HasValue) { sqlParm[1].Value = oAreaUsuarioSede.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oAreaUsuarioSede.UsuarioCreacion != null) { sqlParm[2].Value = oAreaUsuarioSede.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_AREA_USUARIO_SEDE", sqlParm);
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

        public bool Actualizar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
                    if (oAreaUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[0].Value = oAreaUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oAreaUsuarioSede.Descripcion != null) { sqlParm[1].Value = oAreaUsuarioSede.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oAreaUsuarioSede.IdEstado.HasValue) { sqlParm[2].Value = oAreaUsuarioSede.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oAreaUsuarioSede.UsuarioModificacion != null) { sqlParm[3].Value = oAreaUsuarioSede.UsuarioModificacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_AREA_USUARIO_SEDE", sqlParm);
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
