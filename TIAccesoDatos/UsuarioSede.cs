using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class UsuarioSede : IUsuarioSede
    {
        #region Miembros de IUsuarioSede

        public IList<UsuarioSedeInfo> Listar(UsuarioSedeInfo oUsuarioSede)
        {
            var sqlParm = new SqlParameter[5];
            var oListaUsuarioSede = new List<UsuarioSedeInfo>();

            sqlParm[0] = new SqlParameter("@Id_Usuario_Sede", SqlDbType.Int);
            if (oUsuarioSede.IdUsuarioSede.HasValue) { sqlParm[0].Value = oUsuarioSede.IdUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oUsuarioSede.Nombre != null) { sqlParm[1].Value = oUsuarioSede.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuarioSede.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioSede.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
            if (oUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[3].Value = oUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Sede_Cliente", SqlDbType.Int);
            if (oUsuarioSede.IdSede.HasValue) { sqlParm[4].Value = oUsuarioSede.IdSede; } else { sqlParm[4].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO_SEDE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaUsuarioSede.Add(new UsuarioSedeInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(4).ToString()), new EstadoInfo(null, drd.GetString(5).Trim()),
                                Int32.Parse(drd.GetValue(2).ToString()), new AreaUsuarioSedeInfo(null, drd.GetString(3).Trim(), null, null, null, null), 
                                Int32.Parse(drd.GetValue(6).ToString()), new ClienteInfo(){RazonSocial = drd.GetString(7)}, 
                                Int32.Parse(drd.GetValue(8).ToString()), new SedeClienteInfo(){Nombre = drd.GetString(9).Trim()}));
                        }
                    }
                }
            }

            return oListaUsuarioSede;
        }

        public UsuarioSedeInfo Consultar(UsuarioSedeInfo oUsuarioSede)
        {
            var sqlParm = new SqlParameter[5];
            var oEntUsuarioSede = new UsuarioSedeInfo();

            sqlParm[0] = new SqlParameter("@Id_Usuario_Sede", SqlDbType.Int);
            if (oUsuarioSede.IdUsuarioSede.HasValue) { sqlParm[0].Value = oUsuarioSede.IdUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oUsuarioSede.Nombre != null) { sqlParm[1].Value = oUsuarioSede.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oUsuarioSede.IdEstado.HasValue) { sqlParm[2].Value = oUsuarioSede.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
            if (oUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[3].Value = oUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Sede_Cliente", SqlDbType.Int);
            if (oUsuarioSede.IdSede.HasValue) { sqlParm[4].Value = oUsuarioSede.IdSede; } else { sqlParm[4].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_USUARIO_SEDE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntUsuarioSede = new UsuarioSedeInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(4).ToString()), new EstadoInfo(null, drd.GetString(5).Trim()),
                                Int32.Parse(drd.GetValue(2).ToString()), new AreaUsuarioSedeInfo(null, drd.GetString(3).Trim(), null, null, null, null),
                                Int32.Parse(drd.GetValue(6).ToString()), new ClienteInfo() { RazonSocial = drd.GetString(7) },
                                Int32.Parse(drd.GetValue(8).ToString()), new SedeClienteInfo() { Nombre = drd.GetString(9).Trim() });
                    }
                }
            }

            return oEntUsuarioSede;
        }

        public bool Registrar(UsuarioSedeInfo oUsuarioSede, ref int? nId)
        {
            var sqlParm = new SqlParameter[7];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oUsuarioSede.Nombre != null) { sqlParm[0].Value = oUsuarioSede.Nombre; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuarioSede.IdEstado.HasValue) { sqlParm[1].Value = oUsuarioSede.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
                    if (oUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[2].Value = oUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oUsuarioSede.UsuarioCreacion != null) { sqlParm[3].Value = oUsuarioSede.UsuarioCreacion; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    sqlParm[5] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oUsuarioSede.IdCliente.HasValue) { sqlParm[5].Value = oUsuarioSede.IdCliente; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Sede", SqlDbType.Int);
                    if (oUsuarioSede.IdSede.HasValue) { sqlParm[6].Value = oUsuarioSede.IdSede; } else { sqlParm[6].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_USUARIO_SEDE", sqlParm);
                    nId = Int32.Parse(sqlParm[4].Value.ToString());
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

        public bool Actualizar(UsuarioSedeInfo oUsuarioSede)
        {
            var sqlParm = new SqlParameter[5];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Usuario_Sede", SqlDbType.Int);
                    if (oUsuarioSede.IdUsuarioSede.HasValue) { sqlParm[0].Value = oUsuarioSede.IdUsuarioSede; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Area_Usuario_Sede", SqlDbType.Int);
                    if (oUsuarioSede.IdAreaUsuarioSede.HasValue) { sqlParm[1].Value = oUsuarioSede.IdAreaUsuarioSede; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oUsuarioSede.Nombre != null) { sqlParm[2].Value = oUsuarioSede.Nombre; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oUsuarioSede.IdEstado.HasValue) { sqlParm[3].Value = oUsuarioSede.IdEstado; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oUsuarioSede.UsuarioModificacion != null) { sqlParm[4].Value = oUsuarioSede.UsuarioModificacion; } else { sqlParm[4].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_USUARIO_SEDE", sqlParm);
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
