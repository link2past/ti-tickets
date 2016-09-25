using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class EstadoTicket : IEstadoTicket
    {
        #region Miembros de IEstadoTicket

        public IList<EstadoTicketInfo> Listar(EstadoTicketInfo oEstadoTicket)
        {
            var sqlParm = new SqlParameter[3];
            var oListaEstadoTicket = new List<EstadoTicketInfo>();

            sqlParm[0] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oEstadoTicket.IdEstadoTicket.HasValue) { sqlParm[0].Value = oEstadoTicket.IdEstadoTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oEstadoTicket.Descripcion != null) { sqlParm[1].Value = oEstadoTicket.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oEstadoTicket.IdEstado.HasValue) { sqlParm[2].Value = oEstadoTicket.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_ESTADO_TICKET", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaEstadoTicket.Add(new EstadoTicketInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                                null, null));
                        }
                    }
                }
            }

            return oListaEstadoTicket;
        }

        public EstadoTicketInfo Consultar(EstadoTicketInfo oEstadoTicket)
        {
            var sqlParm = new SqlParameter[3];
            var oEntEstadoTicket = new EstadoTicketInfo();

            sqlParm[0] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
            if (oEstadoTicket.IdEstadoTicket.HasValue) { sqlParm[0].Value = oEstadoTicket.IdEstadoTicket; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oEstadoTicket.Descripcion != null) { sqlParm[1].Value = oEstadoTicket.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oEstadoTicket.IdEstado.HasValue) { sqlParm[2].Value = oEstadoTicket.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_ESTADO_TICKET", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntEstadoTicket = new EstadoTicketInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                            Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                            null, null);
                    }
                }
            }

            return oEntEstadoTicket;
        }

        public bool Registrar(EstadoTicketInfo oEstadoTicket, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oEstadoTicket.Descripcion != null) { sqlParm[0].Value = oEstadoTicket.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oEstadoTicket.IdEstado.HasValue) { sqlParm[1].Value = oEstadoTicket.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oEstadoTicket.UsuarioCreacion != null) { sqlParm[2].Value = oEstadoTicket.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_ESTADO_TICKET", sqlParm);
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

        public bool Actualizar(EstadoTicketInfo oEstadoTicket)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Estado_Ticket", SqlDbType.Int);
                    if (oEstadoTicket.IdEstadoTicket.HasValue) { sqlParm[0].Value = oEstadoTicket.IdEstadoTicket; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oEstadoTicket.Descripcion != null) { sqlParm[1].Value = oEstadoTicket.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oEstadoTicket.IdEstado.HasValue) { sqlParm[2].Value = oEstadoTicket.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oEstadoTicket.UsuarioModificacion != null) { sqlParm[3].Value = oEstadoTicket.UsuarioModificacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_ESTADO_TICKET", sqlParm);
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
