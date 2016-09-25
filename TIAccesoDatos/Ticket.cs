using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Ticket : ITicket
    {
        public IList<TicketInfo> Listar(TicketInfo oTicket)
        {
            var sqlParm = new SqlParameter[9];
            var oListaTickets = new List<TicketInfo>();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oTicket.NroTicket.HasValue) { sqlParm[0].Value = oTicket.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oTicket.IdCliente.HasValue) { sqlParm[1].Value = oTicket.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oTicket.IdSede.HasValue) { sqlParm[2].Value = oTicket.IdSede; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oTicket.IdCategoriaProblema.HasValue) { sqlParm[3].Value = oTicket.IdCategoriaProblema; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
            if (oTicket.IdNivelUrgencia.HasValue) { sqlParm[4].Value = oTicket.IdNivelUrgencia; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Fecha_Desde", SqlDbType.DateTime);
            if (oTicket.FechaDesde.HasValue) { sqlParm[5].Value = oTicket.FechaDesde; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Fecha_Hasta", SqlDbType.DateTime);
            if (oTicket.FechaHasta.HasValue) { sqlParm[6].Value = oTicket.FechaHasta; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oTicket.IdEstadoTicket.HasValue) { sqlParm[7].Value = oTicket.IdEstadoTicket; } else { sqlParm[7].Value = DBNull.Value; }

            sqlParm[8] = new SqlParameter("@Id_Usuario_Asignado", SqlDbType.VarChar);
            if (oTicket.IdUsuarioAsignado != null) { sqlParm[8].Value = oTicket.IdUsuarioAsignado; } else { sqlParm[8].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntTicket = new TicketInfo
                                {
                                    NroTicket = Int32.Parse(drd.GetValue(0).ToString()),
                                    IdCliente = Int32.Parse(drd.GetValue(1).ToString()),
                                    Cliente = new ClienteInfo {RazonSocial = drd.GetString(2).Trim()},
                                    IdSede = Int32.Parse(drd.GetValue(3).ToString()),
                                    Sede = new SedeClienteInfo {Nombre = drd.GetString(4).Trim(), Direccion = drd.GetString(17).Trim()},
                                    FechaTicket = drd.GetDateTime(5),
                                    IdCategoriaProblema = Int32.Parse(drd.GetValue(6).ToString()),
                                    CategoriaProblema = new CategoriaProblemaInfo {Descripcion = drd.GetString(7).Trim()},
                                    IdNivelUrgencia = Int32.Parse(drd.GetValue(8).ToString()),
                                    NivelUrgencia = new NivelUrgenciaInfo {Descripcion = drd.GetString(9).Trim()},
                                    Titulo = drd.GetString(10).Trim(),
                                    Detalle = drd.GetString(11).Trim(),
                                    Solucion = drd.GetString(12).Trim(),
                                    Observaciones = drd.GetString(13).Trim(),
                                    IdEstadoTicket = Int32.Parse(drd.GetValue(14).ToString()),
                                    EstadoTicket = new EstadoTicketInfo {Descripcion = drd.GetString(15).Trim()} ,
                                    TiempoTranscurrido = TiempoDesdeMinutos(Int32.Parse(drd.GetValue(16).ToString())), 
                                    UsuarioAsignado = drd.GetString(18).Trim(),
                                    NroTicketCliente = drd.GetString(19).Trim(),
                                    IdUsuarioSede = Int32.Parse(drd.GetValue(20).ToString()), 
                                    UsuarioSede = new UsuarioSedeInfo(null, drd.GetString(21).Trim(), null, null, 
                                        Int32.Parse(drd.GetValue(22).ToString()), new AreaUsuarioSedeInfo(null, drd.GetString(23).Trim(), null, null, null, null), null, null, null, null),
                                    OrdenServicio = drd.GetString(24).Trim(),
                                    CostoCero = drd.GetString(25).Trim(),
                                    IdMoneda = drd.GetString(26).Trim(),
                                    Tarifa = Double.Parse(drd.GetValue(27).ToString())
                                };
                            oListaTickets.Add(oEntTicket);
                        }
                    }
                }
            }

            return oListaTickets;
        }

        private String TiempoDesdeMinutos(int nTotalMinutos)
        {
            int nMinutos;
            var nHoras = Math.DivRem(nTotalMinutos, 60, out nMinutos);

            return nHoras + ":" + nMinutos;
        }

        public TicketInfo Consultar(TicketInfo oTicket)
        {
            var sqlParm = new SqlParameter[9];
            var oEntTicket = new TicketInfo();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oTicket.NroTicket.HasValue) { sqlParm[0].Value = oTicket.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oTicket.IdCliente.HasValue) { sqlParm[1].Value = oTicket.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oTicket.IdSede.HasValue) { sqlParm[2].Value = oTicket.IdSede; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oTicket.IdCategoriaProblema.HasValue) { sqlParm[3].Value = oTicket.IdCategoriaProblema; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
            if (oTicket.IdNivelUrgencia.HasValue) { sqlParm[4].Value = oTicket.IdNivelUrgencia; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Fecha_Desde", SqlDbType.DateTime);
            if (oTicket.FechaDesde.HasValue) { sqlParm[5].Value = oTicket.FechaDesde; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Fecha_Hasta", SqlDbType.DateTime);
            if (oTicket.FechaHasta.HasValue) { sqlParm[6].Value = oTicket.FechaHasta; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oTicket.IdEstadoTicket.HasValue) { sqlParm[7].Value = oTicket.IdEstadoTicket; } else { sqlParm[7].Value = DBNull.Value; }

            sqlParm[8] = new SqlParameter("@Id_Usuario_Asignado", SqlDbType.VarChar);
            if (oTicket.IdUsuarioAsignado != null) { sqlParm[8].Value = oTicket.IdUsuarioAsignado; } else { sqlParm[8].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oEntTicket = new TicketInfo
                            {
                                NroTicket = Int32.Parse(drd.GetValue(0).ToString()),
                                IdCliente = Int32.Parse(drd.GetValue(1).ToString()),
                                Cliente = new ClienteInfo { RazonSocial = drd.GetString(2).Trim() },
                                IdSede = Int32.Parse(drd.GetValue(3).ToString()),
                                Sede = new SedeClienteInfo { Nombre = drd.GetString(4).Trim(), Direccion = drd.GetString(17).Trim() },
                                FechaTicket = drd.GetDateTime(5),
                                IdCategoriaProblema = Int32.Parse(drd.GetValue(6).ToString()),
                                CategoriaProblema = new CategoriaProblemaInfo { Descripcion = drd.GetString(7).Trim() },
                                IdNivelUrgencia = Int32.Parse(drd.GetValue(8).ToString()),
                                NivelUrgencia = new NivelUrgenciaInfo { Descripcion = drd.GetString(9).Trim() },
                                Titulo = drd.GetString(10).Trim(),
                                Detalle = drd.GetString(11).Trim(),
                                Solucion = drd.GetString(12).Trim(),
                                Observaciones = drd.GetString(13).Trim(),
                                IdEstadoTicket = Int32.Parse(drd.GetValue(14).ToString()),
                                EstadoTicket = new EstadoTicketInfo { Descripcion = drd.GetString(15).Trim() },
                                UsuarioAsignado = drd.GetString(18).Trim(),
                                NroTicketCliente = drd.GetString(19).Trim(),
                                IdUsuarioSede = Int32.Parse(drd.GetValue(20).ToString()),
                                UsuarioSede = new UsuarioSedeInfo(null, drd.GetString(21).Trim(), null, null,
                                    Int32.Parse(drd.GetValue(22).ToString()), new AreaUsuarioSedeInfo(null, drd.GetString(23).Trim(), null, null, null, null), null, null, null, null),
                                OrdenServicio = drd.GetString(24).Trim(),
                                CostoCero = drd.GetString(25).Trim(),
                                IdMoneda = drd.GetString(26).Trim(),
                                Tarifa = Double.Parse(drd.GetValue(27).ToString())
                            };
                        }
                    }
                }
            }

            return oEntTicket;
        }

        public bool Registrar(TicketInfo oTicket, ref int? nId)
        {
            var sqlParm = new SqlParameter[14];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oTicket.IdCliente.HasValue) { sqlParm[0].Value = oTicket.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Sede", SqlDbType.Int);
                    if (oTicket.IdSede.HasValue) { sqlParm[1].Value = oTicket.IdSede; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Fecha_Ticket", SqlDbType.DateTime);
                    if (oTicket.FechaTicket.HasValue) { sqlParm[2].Value = oTicket.FechaTicket; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
                    if (oTicket.IdCategoriaProblema.HasValue) { sqlParm[3].Value = oTicket.IdCategoriaProblema; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
                    if (oTicket.IdNivelUrgencia.HasValue) { sqlParm[4].Value = oTicket.IdNivelUrgencia; } else { sqlParm[4].Value = DBNull.Value; }
                    
                    sqlParm[5] = new SqlParameter("@Titulo", SqlDbType.VarChar);
                    if (oTicket.Titulo != null) { sqlParm[5].Value = oTicket.Titulo; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Detalle", SqlDbType.VarChar);
                    if (oTicket.Detalle != null) { sqlParm[6].Value = oTicket.Detalle; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Solucion", SqlDbType.VarChar);
                    if (oTicket.Solucion != null) { sqlParm[7].Value = oTicket.Solucion; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                    if (oTicket.Observaciones != null) { sqlParm[8].Value = oTicket.Observaciones; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Id_Usuario", SqlDbType.VarChar);
                    if (oTicket.IdUsuario != null) { sqlParm[9].Value = oTicket.IdUsuario; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    sqlParm[11] = new SqlParameter("@Nro_Ticket_Cliente", SqlDbType.VarChar);
                    if (oTicket.NroTicketCliente != null) { sqlParm[11].Value = oTicket.NroTicketCliente; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Id_Usuario_Sede", SqlDbType.Int);
                    if (oTicket.IdUsuarioSede.HasValue) { sqlParm[12].Value = oTicket.IdUsuarioSede; } else { sqlParm[12].Value = DBNull.Value; }

                    sqlParm[13] = new SqlParameter("@Orden_Servicio", SqlDbType.VarChar);
                    if (oTicket.OrdenServicio != null) { sqlParm[13].Value = oTicket.OrdenServicio; } else { sqlParm[13].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_TICKET", sqlParm);
                    nId = Int32.Parse(sqlParm[10].Value.ToString());
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

        public bool Actualizar(TicketInfo oTicket)
        {
            var sqlParm = new SqlParameter[13];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
                    if (oTicket.NroTicket.HasValue) { sqlParm[0].Value = oTicket.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Sede", SqlDbType.Int);
                    if (oTicket.IdSede.HasValue) { sqlParm[1].Value = oTicket.IdSede; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
                    if (oTicket.IdCategoriaProblema.HasValue) { sqlParm[2].Value = oTicket.IdCategoriaProblema; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Nivel_Urgencia", SqlDbType.Int);
                    if (oTicket.IdNivelUrgencia.HasValue) { sqlParm[3].Value = oTicket.IdNivelUrgencia; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Titulo", SqlDbType.VarChar);
                    if (oTicket.Titulo != null) { sqlParm[4].Value = oTicket.Titulo; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Detalle", SqlDbType.VarChar);
                    if (oTicket.Detalle != null) { sqlParm[5].Value = oTicket.Detalle; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Solucion", SqlDbType.VarChar);
                    if (oTicket.Solucion != null) { sqlParm[6].Value = oTicket.Solucion; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Observaciones", SqlDbType.VarChar);
                    if (oTicket.Observaciones != null) { sqlParm[7].Value = oTicket.Observaciones; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Nro_Ticket_Cliente", SqlDbType.VarChar);
                    if (oTicket.NroTicketCliente != null) { sqlParm[8].Value = oTicket.NroTicketCliente; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Id_Usuario_Sede", SqlDbType.Int);
                    if (oTicket.IdUsuarioSede.HasValue) { sqlParm[9].Value = oTicket.IdUsuarioSede; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Orden_Servicio", SqlDbType.VarChar);
                    if (oTicket.OrdenServicio != null) { sqlParm[10].Value = oTicket.OrdenServicio; } else { sqlParm[10].Value = DBNull.Value; }

                    sqlParm[11] = new SqlParameter("@Flag_Costo_Cero", SqlDbType.VarChar);
                    if (oTicket.CostoCero != null) { sqlParm[11].Value = oTicket.CostoCero; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Fecha_Ticket", SqlDbType.DateTime);
                    if (oTicket.FechaTicket.HasValue) { sqlParm[12].Value = oTicket.FechaTicket; } else { sqlParm[12].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_TICKET", sqlParm);
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
