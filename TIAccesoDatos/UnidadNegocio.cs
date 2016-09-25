using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class UnidadNegocio : IUnidadNegocio
    {
        #region Miembros de IUnidadNegocio

        public IList<UnidadNegocioInfo> Listar(UnidadNegocioInfo oUnidadNegocio)
        {
            var sqlParm = new SqlParameter[3];
            var oListaUnidadNegocio = new List<UnidadNegocioInfo>();

            sqlParm[0] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oUnidadNegocio.IdUnidadNegocio.HasValue) { sqlParm[0].Value = oUnidadNegocio.IdUnidadNegocio; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oUnidadNegocio.Descripcion != null) { sqlParm[1].Value = oUnidadNegocio.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUnidadNegocio.IdEstado.HasValue) { sqlParm[2].Value = oUnidadNegocio.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_UNIDAD_NEGOCIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaUnidadNegocio.Add(new UnidadNegocioInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()), null, null));
                        }
                    }
                }
            }

            return oListaUnidadNegocio;
        }

        public UnidadNegocioInfo Consultar(UnidadNegocioInfo oUnidadNegocio)
        {
            var sqlParm = new SqlParameter[3];
            var oEntUnidadNegocio = new UnidadNegocioInfo();

            sqlParm[0] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oUnidadNegocio.IdUnidadNegocio.HasValue) { sqlParm[0].Value = oUnidadNegocio.IdUnidadNegocio; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oUnidadNegocio.Descripcion != null) { sqlParm[1].Value = oUnidadNegocio.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUnidadNegocio.IdEstado.HasValue) { sqlParm[2].Value = oUnidadNegocio.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_UNIDAD_NEGOCIO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntUnidadNegocio = new UnidadNegocioInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                            Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()), null, null);
                    }
                }
            }

            return oEntUnidadNegocio;
        }

        public bool Registrar(UnidadNegocioInfo oUnidadNegocio, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oUnidadNegocio.Descripcion != null) { sqlParm[0].Value = oUnidadNegocio.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUnidadNegocio.IdEstado.HasValue) { sqlParm[1].Value = oUnidadNegocio.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oUnidadNegocio.UsuarioCreacion != null) { sqlParm[2].Value = oUnidadNegocio.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_UNIDAD_NEGOCIO",
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

        public bool Actualizar(UnidadNegocioInfo oUnidadNegocio)
        {
            var sqlParm = new SqlParameter[4];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
                    if (oUnidadNegocio.IdUnidadNegocio.HasValue) { sqlParm[0].Value = oUnidadNegocio.IdUnidadNegocio; } else { sqlParm[0].Value = DBNull.Value; }
                    
                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oUnidadNegocio.Descripcion != null) { sqlParm[1].Value = oUnidadNegocio.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUnidadNegocio.IdEstado.HasValue) { sqlParm[2].Value = oUnidadNegocio.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oUnidadNegocio.UsuarioModificacion != null) { sqlParm[3].Value = oUnidadNegocio.UsuarioModificacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_UNIDAD_NEGOCIO",
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


        public IList<UnidadNegocioInfo> ListarPorCliente(UnidadNegocioInfo oUnidadNegocio, int? nIdCliente)
        {
            var sqlParm = new SqlParameter[4];
            var oListaUnidadNegocio = new List<UnidadNegocioInfo>();

            sqlParm[0] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oUnidadNegocio.IdUnidadNegocio.HasValue) { sqlParm[0].Value = oUnidadNegocio.IdUnidadNegocio; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oUnidadNegocio.Descripcion != null) { sqlParm[1].Value = oUnidadNegocio.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUnidadNegocio.IdEstado.HasValue) { sqlParm[2].Value = oUnidadNegocio.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            sqlParm[3].Value = nIdCliente;

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_UNIDAD_NEGOCIO_X_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaUnidadNegocio.Add(new UnidadNegocioInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()), null, null));
                        }
                    }
                }
            }

            return oListaUnidadNegocio;
        }
    }
}
