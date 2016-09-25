using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class ReporteClienteUn : IReporteClienteUn
    {
        #region Miembros de IReporteClienteInfoUn

        public IList<ReporteClienteUnInfo> Procesar(ReporteClienteUnInfo oReporte)
        {
            var sqlParm = new SqlParameter[8];
            var oListaReporte = new List<ReporteClienteUnInfo>();

            sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oReporte.IdCliente.HasValue) { sqlParm[0].Value = oReporte.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oReporte.IdSede.HasValue) { sqlParm[1].Value = oReporte.IdSede; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oReporte.IdUnidadNegocio.HasValue) { sqlParm[2].Value = oReporte.IdUnidadNegocio; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Fecha_Desde", SqlDbType.DateTime);
            if (oReporte.FechaDesde.HasValue) { sqlParm[3].Value = oReporte.FechaDesde; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Fecha_Hasta", SqlDbType.DateTime);
            if (oReporte.FechaHasta.HasValue) { sqlParm[4].Value = oReporte.FechaHasta; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oReporte.NroTicket.HasValue) { sqlParm[5].Value = oReporte.NroTicket; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oReporte.IdCategoriaProblema.HasValue) { sqlParm[6].Value = oReporte.IdCategoriaProblema; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oReporte.IdEstadoTicket.HasValue) { sqlParm[7].Value = oReporte.IdEstadoTicket; } else { sqlParm[7].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_REPORTE_CLIENTE_UN", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntReporte = new ReporteClienteUnInfo();
                            
                            oEntReporte.NroTicket = Int32.Parse(drd.GetValue(0).ToString());
                            oEntReporte.Ticket = new TicketInfo();
                            oEntReporte.Ticket.NroTicket = Int32.Parse(drd.GetValue(0).ToString());
                            oEntReporte.Ticket.NroTicketCliente = drd.GetString(1).Trim();
                            oEntReporte.Ticket.FechaTicket = drd.GetDateTime(2);

                            oEntReporte.IdCliente = Int32.Parse(drd.GetValue(3).ToString());
                            oEntReporte.Ticket.Cliente = new ClienteInfo();
                            oEntReporte.Ticket.Cliente.RazonSocial = drd.GetString(4).Trim();
                            oEntReporte.IdSede = Int32.Parse(drd.GetValue(5).ToString());
                            oEntReporte.Ticket.Sede = new SedeClienteInfo();
                            oEntReporte.Ticket.Sede.Nombre = drd.GetString(6).Trim();
                            oEntReporte.IdUnidadNegocio = Int32.Parse(drd.GetValue(7).ToString());
                            oEntReporte.Ticket.Sede.UnidadNegocio = new UnidadNegocioInfo();
                            oEntReporte.Ticket.Sede.UnidadNegocio.Descripcion = drd.GetString(8).Trim();
                            oEntReporte.Ticket.Sede.CentroCosto = drd.GetString(9).Trim();
                            oEntReporte.Ticket.IdEstadoTicket = Int32.Parse(drd.GetValue(18).ToString());
                            oEntReporte.Ticket.EstadoTicket = new EstadoTicketInfo{Descripcion = drd.GetString(19).Trim()};
                            oEntReporte.Ticket.IdCategoriaProblema = Int32.Parse(drd.GetValue(20).ToString());
                            oEntReporte.Ticket.CategoriaProblema = new CategoriaProblemaInfo{Descripcion = drd.GetString(21).Trim()};

                            oEntReporte.Ticket.Titulo = drd.GetString(10).Trim();
                            oEntReporte.Ticket.Detalle = drd.GetString(11).Trim();
                            oEntReporte.Ticket.Solucion = drd.GetString(12).Trim();
                            oEntReporte.Ticket.Observaciones = drd.GetString(13).Trim();
                            oEntReporte.Ticket.OrdenServicio = drd.GetString(22).Trim();
                            oEntReporte.UsuarioTicket = drd.GetString(14).Trim();
                            oEntReporte.FechaHoraRegistro = drd.GetDateTime(15);
                            
                            oEntReporte.Tarifa = Double.Parse(drd.GetValue(16).ToString());
                            oEntReporte.TotalRepuestos = Double.Parse(drd.GetValue(17).ToString());

                            oEntReporte.TotalGeneral = oEntReporte.Tarifa + oEntReporte.TotalRepuestos;

                            oListaReporte.Add(oEntReporte);
                        }
                    }
                }
            }
            return oListaReporte;
        }

        public IList<ReporteClienteUnInfo> ProcesarDetalle(ReporteClienteUnInfo oReporte)
        {
            var sqlParm = new SqlParameter[1];
            var oListaReporte = new List<ReporteClienteUnInfo>();

            sqlParm[0] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oReporte.NroTicket.HasValue) { sqlParm[0].Value = oReporte.NroTicket; } else { sqlParm[0].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_TICKET_DETALLE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntReporte = new ReporteClienteUnInfo();

                            oEntReporte.NroTicket = Int32.Parse(drd.GetValue(0).ToString());

                            oEntReporte.DetalleTicket = new TicketDetalleInfo();
                            oEntReporte.DetalleTicket.IdRepuesto = Int32.Parse(drd.GetValue(1).ToString());
                            oEntReporte.DetalleTicket.Repuesto = new RepuestoInfo();
                            oEntReporte.DetalleTicket.Repuesto.Descripcion = drd.GetString(2).Trim();
                            oEntReporte.DetalleTicket.Cantidad = Double.Parse(drd.GetValue(3).ToString());
                            oEntReporte.DetalleTicket.Precio = Double.Parse(drd.GetValue(6).ToString());
                            oEntReporte.PrecioTotal = oEntReporte.DetalleTicket.Cantidad*
                                                      oEntReporte.DetalleTicket.Precio;

                            oListaReporte.Add(oEntReporte);
                        }
                    }
                }
            }
            return oListaReporte;
        }

        #endregion


        public IList<ReporteClienteUnInfo> Procesar2(ReporteClienteUnInfo oReporte)
        {
            var sqlParm = new SqlParameter[8];
            var oListaReporte = new List<ReporteClienteUnInfo>();

            sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oReporte.IdCliente.HasValue) { sqlParm[0].Value = oReporte.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oReporte.IdSede.HasValue) { sqlParm[1].Value = oReporte.IdSede; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oReporte.IdUnidadNegocio.HasValue) { sqlParm[2].Value = oReporte.IdUnidadNegocio; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Fecha_Desde", SqlDbType.DateTime);
            if (oReporte.FechaDesde.HasValue) { sqlParm[3].Value = oReporte.FechaDesde; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Fecha_Hasta", SqlDbType.DateTime);
            if (oReporte.FechaHasta.HasValue) { sqlParm[4].Value = oReporte.FechaHasta; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Nro_Ticket", SqlDbType.Int);
            if (oReporte.NroTicket.HasValue) { sqlParm[5].Value = oReporte.NroTicket; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oReporte.IdCategoriaProblema.HasValue) { sqlParm[6].Value = oReporte.IdCategoriaProblema; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oReporte.IdEstadoTicket.HasValue) { sqlParm[7].Value = oReporte.IdEstadoTicket; } else { sqlParm[7].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_REPORTE_CLIENTE_UN_2", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntReporte = new ReporteClienteUnInfo();

                            oEntReporte.NroTicket = Int32.Parse(drd.GetValue(0).ToString());
                            oEntReporte.Ticket = new TicketInfo();
                            oEntReporte.Ticket.NroTicket = Int32.Parse(drd.GetValue(0).ToString());
                            oEntReporte.Ticket.NroTicketCliente = drd.GetString(1).Trim();
                            oEntReporte.Ticket.FechaTicket = drd.GetDateTime(2);

                            oEntReporte.IdCliente = Int32.Parse(drd.GetValue(3).ToString());
                            oEntReporte.Ticket.Cliente = new ClienteInfo();
                            oEntReporte.Ticket.Cliente.RazonSocial = drd.GetString(4).Trim();
                            oEntReporte.IdSede = Int32.Parse(drd.GetValue(5).ToString());
                            oEntReporte.Ticket.Sede = new SedeClienteInfo();
                            oEntReporte.Ticket.Sede.Nombre = drd.GetString(6).Trim();
                            oEntReporte.IdUnidadNegocio = Int32.Parse(drd.GetValue(7).ToString());
                            oEntReporte.Ticket.Sede.UnidadNegocio = new UnidadNegocioInfo();
                            oEntReporte.Ticket.Sede.UnidadNegocio.Descripcion = drd.GetString(8).Trim();
                            oEntReporte.Ticket.Sede.CentroCosto = drd.GetString(9).Trim();
                            oEntReporte.Ticket.IdEstadoTicket = Int32.Parse(drd.GetValue(18).ToString());
                            oEntReporte.Ticket.EstadoTicket = new EstadoTicketInfo { Descripcion = drd.GetString(19).Trim() };
                            oEntReporte.Ticket.IdCategoriaProblema = Int32.Parse(drd.GetValue(20).ToString());
                            oEntReporte.Ticket.CategoriaProblema = new CategoriaProblemaInfo { Descripcion = drd.GetString(21).Trim() };

                            oEntReporte.Ticket.Titulo = drd.GetString(10).Trim();
                            oEntReporte.Ticket.Detalle = drd.GetString(11).Trim();
                            oEntReporte.Ticket.Solucion = drd.GetString(12).Trim();
                            oEntReporte.Ticket.Observaciones = drd.GetString(13).Trim();
                            oEntReporte.Ticket.OrdenServicio = drd.GetString(27).Trim();
                            oEntReporte.UsuarioTicket = drd.GetString(14).Trim();
                            oEntReporte.FechaHoraRegistro = drd.GetDateTime(15);

                            oEntReporte.Tarifa = Double.Parse(drd.GetValue(16).ToString());
                            oEntReporte.TotalRepuestos = Double.Parse(drd.GetValue(17).ToString());

                            oEntReporte.TotalGeneral = Double.Parse(drd.GetValue(25).ToString());
                            oEntReporte.DetalleTicket = new TicketDetalleInfo
                                {
                                    Repuesto = new RepuestoInfo
                                        {
                                            Descripcion = drd.GetString(22).Trim()
                                        },
                                    Cantidad = Double.Parse(drd.GetValue(23).ToString()),
                                    Precio = Double.Parse(drd.GetValue(24).ToString())
                                };

                            oListaReporte.Add(oEntReporte);
                        }
                    }
                }
            }
            return oListaReporte;
        }
    }
}
