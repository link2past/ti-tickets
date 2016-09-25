using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class TicketRegistro : ITicketRegistro
    {
        public IList<TicketRegistroInfo> Listar(TicketRegistroInfo oTicketRegistro)
        {
            var sqlParm = new SqlParameter[4];
            var oListaRegistro = new List<TicketRegistroInfo>();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oTicketRegistro.NroTicket.HasValue) { sqlParm[0].Value = oTicketRegistro.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oTicketRegistro.IdEstadoTicket.HasValue) { sqlParm[1].Value = oTicketRegistro.IdEstadoTicket; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oTicketRegistro.IdUsuario != null) { sqlParm[2].Value = oTicketRegistro.IdUsuario; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Usuario_Asignado", SqlDbType.VarChar);
            if (oTicketRegistro.IdUsuarioAsignado != null) { sqlParm[3].Value = oTicketRegistro.IdUsuarioAsignado; } else { sqlParm[3].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET_REGISTRO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntRegistro = new TicketRegistroInfo
                                {
                                    NroTicket = Int32.Parse(drd.GetValue(0).ToString()),
                                    IdEstadoTicket = Int32.Parse(drd.GetValue(1).ToString()),
                                    EstadoTicket =
                                        new EstadoTicketInfo(null, drd.GetString(2).Trim(), null, null, null, null),
                                    IdUsuario = drd.GetString(3).Trim(),
                                    Usuario = new UsuarioInfo(null, null, drd.GetString(4).Trim(), null),
                                    FechaHoraRegistro = drd.GetDateTime(5),
                                    IdUsuarioAsignado = drd.GetString(6).Trim(),
                                    UsuarioAsignado = new UsuarioInfo(null, null, drd.GetString(7).Trim(), null), 
                                    Observacion = drd.GetString(8).Trim()
                                };

                            oListaRegistro.Add(oEntRegistro);
                        }
                    }
                }
            }
            return oListaRegistro;
        }

        public TicketRegistroInfo Consultar(TicketRegistroInfo oTicketRegistro)
        {
            var sqlParm = new SqlParameter[4];
            var oEntRegistro = new TicketRegistroInfo();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oTicketRegistro.NroTicket.HasValue) { sqlParm[0].Value = oTicketRegistro.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oTicketRegistro.IdEstadoTicket.HasValue) { sqlParm[1].Value = oTicketRegistro.IdEstadoTicket; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
            if (oTicketRegistro.IdUsuario != null) { sqlParm[2].Value = oTicketRegistro.IdUsuario; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Usuario_Asignado", SqlDbType.VarChar);
            if (oTicketRegistro.IdUsuarioAsignado != null) { sqlParm[3].Value = oTicketRegistro.IdUsuarioAsignado; } else { sqlParm[3].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET_REGISTRO", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntRegistro = new TicketRegistroInfo
                        {
                            NroTicket = Int32.Parse(drd.GetValue(0).ToString()),
                            IdEstadoTicket = Int32.Parse(drd.GetValue(1).ToString()),
                            EstadoTicket =
                                new EstadoTicketInfo(null, drd.GetString(2).Trim(), null, null, null, null),
                            IdUsuario = drd.GetString(3).Trim(),
                            Usuario = new UsuarioInfo(null, null, drd.GetString(4).Trim(), null),
                            FechaHoraRegistro = drd.GetDateTime(5),
                            IdUsuarioAsignado = drd.GetString(6).Trim(),
                            UsuarioAsignado = new UsuarioInfo(null, null, drd.GetString(7).Trim(), null),
                            Observacion = drd.GetString(8).Trim()
                        };
                    }
                }
            }
            return oEntRegistro;
        }

        public bool Registrar(TicketRegistroInfo oTicketRegistro)
        {
            var sqlParm = new SqlParameter[5];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
                    if (oTicketRegistro.NroTicket.HasValue) { sqlParm[0].Value = oTicketRegistro.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
                    if (oTicketRegistro.IdEstadoTicket.HasValue) { sqlParm[1].Value = oTicketRegistro.IdEstadoTicket; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oTicketRegistro.IdUsuario != null) { sqlParm[2].Value = oTicketRegistro.IdUsuario; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Usuario_Asignado", SqlDbType.VarChar);
                    if (oTicketRegistro.IdUsuarioAsignado != null) { sqlParm[3].Value = oTicketRegistro.IdUsuarioAsignado; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Observacion", SqlDbType.VarChar);
                    if (oTicketRegistro.Observacion != null) { sqlParm[4].Value = oTicketRegistro.Observacion; } else { sqlParm[4].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_TICKET_REGISTRO",
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


        public bool ActualizarHoraRegistro(TicketRegistroInfo oTicketRegistro)
        {
            var sqlParm = new SqlParameter[2];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
                    if (oTicketRegistro.NroTicket.HasValue) { sqlParm[0].Value = oTicketRegistro.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Fecha_Hora_Nueva", SqlDbType.DateTime);
                    if (oTicketRegistro.FechaHoraRegistro.HasValue) { sqlParm[1].Value = oTicketRegistro.FechaHoraRegistro; } else { sqlParm[1].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_HORA_REGISTRO",
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
    }
}
