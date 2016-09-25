using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class TicketDetalle : ITicketDetalle
    {
        #region Miembros de ITicketDetalle

        public List<TicketDetalleInfo> Listar(TicketDetalleInfo oTicketDetalle)
        {
            var sqlParm = new SqlParameter[1];
            var oListaTicketDetalle = new List<TicketDetalleInfo>();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oTicketDetalle.NroTicket.HasValue) { sqlParm[0].Value = oTicketDetalle.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET_DETALLE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oDetalle = new TicketDetalleInfo();
                            oDetalle.NroTicket = Int32.Parse(drd.GetValue(0).ToString());
                            oDetalle.IdRepuesto = Int32.Parse(drd.GetValue(1).ToString());
                            oDetalle.Repuesto = new RepuestoInfo() { Descripcion = drd.GetString(2).Trim() };
                            oDetalle.Cantidad = Double.Parse(drd.GetValue(3).ToString());
                            oDetalle.IdMoneda = drd.GetString(4);
                            oDetalle.Moneda = new MonedaInfo(null, drd.GetString(5).Trim());
                            oDetalle.Precio = Double.Parse(drd.GetValue(6).ToString());

                            oListaTicketDetalle.Add(oDetalle);
                        }
                    }
                }
            }
            return oListaTicketDetalle;
        }

        public bool Registrar(TicketDetalleInfo oTicketDetalle)
        {
            var sqlParm = new SqlParameter[6];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
                    if (oTicketDetalle.NroTicket.HasValue) { sqlParm[0].Value = oTicketDetalle.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Repuesto", SqlDbType.Int);
                    if (oTicketDetalle.IdRepuesto.HasValue) { sqlParm[1].Value = oTicketDetalle.IdRepuesto; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Cantidad", SqlDbType.Int);
                    if (oTicketDetalle.Cantidad.HasValue) { sqlParm[2].Value = oTicketDetalle.Cantidad; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
                    if (oTicketDetalle.IdMoneda != null) { sqlParm[3].Value = oTicketDetalle.IdMoneda; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Precio", SqlDbType.Decimal);
                    if (oTicketDetalle.Precio.HasValue) { sqlParm[4].Value = oTicketDetalle.Precio; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oTicketDetalle.UsuarioCreacion != null) { sqlParm[5].Value = oTicketDetalle.UsuarioCreacion; } else { sqlParm[5].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_TICKET_DETALLE",
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
            }
            return true;
        }

        public bool Actualizar(TicketDetalleInfo oTicketDetalle)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(TicketDetalleInfo oTicketDetalle)
        {
            var sqlParm = new SqlParameter[2];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
                    if (oTicketDetalle.NroTicket.HasValue) { sqlParm[0].Value = oTicketDetalle.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Repuesto", SqlDbType.Int);
                    if (oTicketDetalle.IdRepuesto.HasValue) { sqlParm[1].Value = oTicketDetalle.IdRepuesto; } else { sqlParm[1].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ELIMINAR_TICKET_DETALLE",
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
            }
            return true;
        }

        #endregion
    }
}
