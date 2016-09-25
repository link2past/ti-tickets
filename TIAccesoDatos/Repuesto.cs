using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Repuesto : IRepuesto
    {
        #region Miembros de IRepuesto

        public IList<RepuestoInfo> Listar(RepuestoInfo oRepuesto)
        {
            var sqlParm = new SqlParameter[3];
            var oListaRepuesto = new List<RepuestoInfo>();

            sqlParm[0] = new SqlParameter("@Id_Repuesto", SqlDbType.Int);
            if (oRepuesto.IdRepuesto.HasValue) { sqlParm[0].Value = oRepuesto.IdRepuesto; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oRepuesto.Descripcion != null) { sqlParm[1].Value = oRepuesto.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oRepuesto.IdEstado.HasValue) { sqlParm[2].Value = oRepuesto.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_REPUESTO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaRepuesto.Add(new RepuestoInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                drd.GetString(2).Trim(), new MonedaInfo(null, drd.GetString(3).Trim()),
                                Double.Parse(drd.GetValue(6).ToString()), Double.Parse(drd.GetValue(7).ToString()),
                                null, null,
                                Int32.Parse(drd.GetValue(4).ToString()), new EstadoInfo(null, drd.GetString(5).Trim())
                                ));
                        }
                    }
                }
            }

            return oListaRepuesto;
        }

        public RepuestoInfo Consultar(RepuestoInfo oRepuesto)
        {
            var sqlParm = new SqlParameter[3];
            var oEntRepuesto = new RepuestoInfo();

            sqlParm[0] = new SqlParameter("@Id_Repuesto", SqlDbType.Int);
            if (oRepuesto.IdRepuesto.HasValue) { sqlParm[0].Value = oRepuesto.IdRepuesto; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oRepuesto.Descripcion != null) { sqlParm[1].Value = oRepuesto.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oRepuesto.IdEstado.HasValue) { sqlParm[2].Value = oRepuesto.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_REPUESTO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntRepuesto = new RepuestoInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                              drd.GetString(2).Trim(), new MonedaInfo(null, drd.GetString(3).Trim()),
                              Double.Parse(drd.GetValue(6).ToString()), Double.Parse(drd.GetValue(7).ToString()),
                              null, null,
                              Int32.Parse(drd.GetValue(4).ToString()), new EstadoInfo(null, drd.GetString(5).Trim())
                              );
                    }
                }
            }

            return oEntRepuesto;
        }

        public bool Registrar(RepuestoInfo oRepuesto, ref int? nId)
        {
            var sqlParm = new SqlParameter[7];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oRepuesto.Descripcion != null) { sqlParm[0].Value = oRepuesto.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
                    if (oRepuesto.IdMoneda != null) { sqlParm[1].Value = oRepuesto.IdMoneda; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oRepuesto.IdEstado.HasValue) { sqlParm[2].Value = oRepuesto.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Precio_Actual", SqlDbType.Decimal);
                    if (oRepuesto.PrecioActual.HasValue) { sqlParm[3].Value = oRepuesto.PrecioActual; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Stock_Actual", SqlDbType.Decimal);
                    if (oRepuesto.StockActual.HasValue) { sqlParm[4].Value = oRepuesto.StockActual; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oRepuesto.UsuarioCreacion != null) { sqlParm[5].Value = oRepuesto.UsuarioCreacion; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_REPUESTO", sqlParm);
                    nId = Int32.Parse(sqlParm[6].Value.ToString());
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

        public bool Actualizar(RepuestoInfo oRepuesto)
        {
            var sqlParm = new SqlParameter[7];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Repuesto", SqlDbType.Int);
                    if (oRepuesto.IdRepuesto.HasValue) { sqlParm[0].Value = oRepuesto.IdRepuesto; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oRepuesto.Descripcion != null) { sqlParm[1].Value = oRepuesto.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
                    if (oRepuesto.IdMoneda != null) { sqlParm[2].Value = oRepuesto.IdMoneda; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oRepuesto.IdEstado.HasValue) { sqlParm[3].Value = oRepuesto.IdEstado; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Precio_Actual", SqlDbType.Decimal);
                    if (oRepuesto.PrecioActual.HasValue) { sqlParm[4].Value = oRepuesto.PrecioActual; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Stock_Actual", SqlDbType.Decimal);
                    if (oRepuesto.StockActual.HasValue) { sqlParm[5].Value = oRepuesto.StockActual; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oRepuesto.UsuarioModificacion != null) { sqlParm[6].Value = oRepuesto.UsuarioModificacion; } else { sqlParm[6].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_REPUESTO", sqlParm);
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
