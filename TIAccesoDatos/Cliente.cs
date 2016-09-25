using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class Cliente : ICliente
    {
        #region Miembros de ICliente

        public IList<ClienteInfo> Listar(ClienteInfo oCliente)
        {
            var sqlParm = new SqlParameter[7];
            var oListaClientes = new List<ClienteInfo>();

            sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oCliente.IdCliente.HasValue) { sqlParm[0].Value = oCliente.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Razon_Social", SqlDbType.VarChar);
            if (oCliente.RazonSocial != null) { sqlParm[1].Value = oCliente.RazonSocial; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Nro_Di", SqlDbType.VarChar);
            if (oCliente.NroDi != null) { sqlParm[2].Value = oCliente.NroDi; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
            if (oCliente.IdDepartamento != null) { sqlParm[3].Value = oCliente.IdDepartamento; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
            if (oCliente.IdProvincia != null) { sqlParm[4].Value = oCliente.IdProvincia; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
            if (oCliente.IdDistrito != null) { sqlParm[5].Value = oCliente.IdDistrito; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oCliente.IdEstado.HasValue) { sqlParm[6].Value = oCliente.IdEstado; } else { sqlParm[6].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntCliente = new ClienteInfo
                                {
                                    IdCliente = Int32.Parse(drd.GetValue(0).ToString()),
                                    RazonSocial = drd.GetString(1).Trim(),
                                    NroDi = drd.GetString(2).Trim(),
                                    Direccion = drd.GetString(3).Trim(),
                                    IdDepartamento = drd.GetString(4).Trim(),
                                    Departamento = new UbigeoInfo(null, null, null, drd.GetString(5).Trim()),
                                    IdProvincia = drd.GetString(6).Trim(),
                                    Provincia = new UbigeoInfo(null, null, null, drd.GetString(7).Trim()),
                                    IdDistrito = drd.GetString(8).Trim(),
                                    Distrito = new UbigeoInfo(null, null, null, drd.GetString(9).Trim()),
                                    Telefono = drd.GetString(10).Trim(),
                                    Email = drd.GetString(11).Trim(),
                                    NombreContacto = drd.GetString(12).Trim(),
                                    CargoContacto = drd.GetString(13).Trim(),
                                    IdEstado = Int32.Parse(drd.GetValue(14).ToString()),
                                    Estado = new EstadoInfo(null, drd.GetString(15).Trim()),
                                    IdMoneda = drd.GetString(16).Trim(),
                                    Moneda = new MonedaInfo(null, drd.GetString(17).Trim()),
                                    TarifaDiurna = Double.Parse(drd.GetValue(18).ToString()),
                                    TarifaNocturna = Double.Parse(drd.GetValue(19).ToString())
                                };
                            oListaClientes.Add(oEntCliente);
                        }
                    }
                }
            }

            return oListaClientes;
        }

        public ClienteInfo Consultar(ClienteInfo oCliente)
        {
            var sqlParm = new SqlParameter[7];
            var oEntCliente = new ClienteInfo();

            sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oCliente.IdCliente.HasValue) { sqlParm[0].Value = oCliente.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Razon_Social", SqlDbType.VarChar);
            if (oCliente.RazonSocial != null) { sqlParm[1].Value = oCliente.RazonSocial; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Nro_Di", SqlDbType.VarChar);
            if (oCliente.NroDi != null) { sqlParm[2].Value = oCliente.NroDi; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
            if (oCliente.IdDepartamento != null) { sqlParm[3].Value = oCliente.IdDepartamento; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
            if (oCliente.IdProvincia != null) { sqlParm[4].Value = oCliente.IdProvincia; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
            if (oCliente.IdDistrito != null) { sqlParm[5].Value = oCliente.IdDistrito; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oCliente.IdEstado.HasValue) { sqlParm[6].Value = oCliente.IdEstado; } else { sqlParm[6].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntCliente = new ClienteInfo
                        {
                            IdCliente = Int32.Parse(drd.GetValue(0).ToString()),
                            RazonSocial = drd.GetString(1).Trim(),
                            NroDi = drd.GetString(2).Trim(),
                            Direccion = drd.GetString(3).Trim(),
                            IdDepartamento = drd.GetString(4).Trim(),
                            Departamento = new UbigeoInfo(null, null, null, drd.GetString(5).Trim()),
                            IdProvincia = drd.GetString(6).Trim(),
                            Provincia = new UbigeoInfo(null, null, null, drd.GetString(7).Trim()),
                            IdDistrito = drd.GetString(8).Trim(),
                            Distrito = new UbigeoInfo(null, null, null, drd.GetString(9).Trim()),
                            Telefono = drd.GetString(10).Trim(),
                            Email = drd.GetString(11).Trim(),
                            NombreContacto = drd.GetString(12).Trim(),
                            CargoContacto = drd.GetString(13).Trim(),
                            IdEstado = Int32.Parse(drd.GetValue(14).ToString()),
                            Estado = new EstadoInfo(null, drd.GetString(15).Trim()),
                            IdMoneda = drd.GetString(16).Trim(),
                            Moneda = new MonedaInfo(null, drd.GetString(17).Trim()),
                            TarifaDiurna = Double.Parse(drd.GetValue(18).ToString()),
                            TarifaNocturna = Double.Parse(drd.GetValue(19).ToString())
                        };
                    }
                }
            }

            return oEntCliente;
        }

        public bool Registrar(ClienteInfo oCliente, ref int? nId)
        {
            var sqlParm = new SqlParameter[16];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Razon_Social", SqlDbType.VarChar);
                    if (oCliente.RazonSocial != null) { sqlParm[0].Value = oCliente.RazonSocial; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Nro_Di", SqlDbType.VarChar);
                    if (oCliente.NroDi != null) { sqlParm[1].Value = oCliente.NroDi; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
                    if (oCliente.IdDepartamento != null) { sqlParm[2].Value = oCliente.IdDepartamento; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    if (oCliente.Direccion != null) { sqlParm[3].Value = oCliente.Direccion; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
                    if (oCliente.IdProvincia != null) { sqlParm[4].Value = oCliente.IdProvincia; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
                    if (oCliente.IdDistrito != null) { sqlParm[5].Value = oCliente.IdDistrito; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    if (oCliente.Telefono != null) { sqlParm[6].Value = oCliente.Telefono; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Email", SqlDbType.VarChar);
                    if (oCliente.Email != null) { sqlParm[7].Value = oCliente.Email; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Nombre_Contacto", SqlDbType.VarChar);
                    if (oCliente.NombreContacto != null) { sqlParm[8].Value = oCliente.NombreContacto; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Cargo_Contacto", SqlDbType.VarChar);
                    if (oCliente.CargoContacto != null) { sqlParm[9].Value = oCliente.CargoContacto; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oCliente.IdEstado.HasValue) { sqlParm[10].Value = oCliente.IdEstado; } else { sqlParm[10].Value = DBNull.Value; }

                    sqlParm[11] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oCliente.UsuarioCreacion != null) { sqlParm[11].Value = oCliente.UsuarioCreacion; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    sqlParm[13] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
                    if (oCliente.IdMoneda != null) { sqlParm[13].Value = oCliente.IdMoneda; } else { sqlParm[13].Value = DBNull.Value; }

                    sqlParm[14] = new SqlParameter("@Tarifa_Diurna", SqlDbType.Decimal);
                    if (oCliente.TarifaDiurna.HasValue) { sqlParm[14].Value = oCliente.TarifaDiurna; } else { sqlParm[14].Value = DBNull.Value; }

                    sqlParm[15] = new SqlParameter("@Tarifa_Nocturna", SqlDbType.VarChar);
                    if (oCliente.TarifaNocturna.HasValue) { sqlParm[15].Value = oCliente.TarifaNocturna; } else { sqlParm[15].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_CLIENTE", sqlParm);
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

        public bool Actualizar(ClienteInfo oCliente)
        {
            var sqlParm = new SqlParameter[16];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oCliente.IdCliente.HasValue) { sqlParm[0].Value = oCliente.IdCliente; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Razon_Social", SqlDbType.VarChar);
                    if (oCliente.RazonSocial != null) { sqlParm[1].Value = oCliente.RazonSocial; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Nro_Di", SqlDbType.VarChar);
                    if (oCliente.NroDi != null) { sqlParm[2].Value = oCliente.NroDi; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
                    if (oCliente.IdDepartamento != null) { sqlParm[3].Value = oCliente.IdDepartamento; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    if (oCliente.Direccion != null) { sqlParm[4].Value = oCliente.Direccion; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
                    if (oCliente.IdProvincia != null) { sqlParm[5].Value = oCliente.IdProvincia; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
                    if (oCliente.IdDistrito != null) { sqlParm[6].Value = oCliente.IdDistrito; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    if (oCliente.Telefono != null) { sqlParm[7].Value = oCliente.Telefono; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Email", SqlDbType.VarChar);
                    if (oCliente.Email != null) { sqlParm[8].Value = oCliente.Email; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Nombre_Contacto", SqlDbType.VarChar);
                    if (oCliente.NombreContacto != null) { sqlParm[9].Value = oCliente.NombreContacto; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Cargo_Contacto", SqlDbType.VarChar);
                    if (oCliente.CargoContacto != null) { sqlParm[10].Value = oCliente.CargoContacto; } else { sqlParm[10].Value = DBNull.Value; }

                    sqlParm[11] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oCliente.IdEstado.HasValue) { sqlParm[11].Value = oCliente.IdEstado; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oCliente.UsuarioModificacion != null) { sqlParm[12].Value = oCliente.UsuarioModificacion; } else { sqlParm[12].Value = DBNull.Value; }

                    sqlParm[13] = new SqlParameter("@Id_Moneda", SqlDbType.VarChar);
                    if (oCliente.IdMoneda != null) { sqlParm[13].Value = oCliente.IdMoneda; } else { sqlParm[13].Value = DBNull.Value; }

                    sqlParm[14] = new SqlParameter("@Tarifa_Diurna", SqlDbType.Decimal);
                    if (oCliente.TarifaDiurna.HasValue) { sqlParm[14].Value = oCliente.TarifaDiurna; } else { sqlParm[14].Value = DBNull.Value; }

                    sqlParm[15] = new SqlParameter("@Tarifa_Nocturna", SqlDbType.VarChar);
                    if (oCliente.TarifaNocturna.HasValue) { sqlParm[15].Value = oCliente.TarifaNocturna; } else { sqlParm[15].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_CLIENTE", sqlParm);
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
