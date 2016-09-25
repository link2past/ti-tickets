using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class SedeCliente : ISedeCliente
    {
        #region Miembros de ISedeCliente

        public IList<SedeClienteInfo> Listar(SedeClienteInfo oSedeCliente)
        {
            var sqlParm = new SqlParameter[8];
            var oListaSedes = new List<SedeClienteInfo>();

            sqlParm[0] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oSedeCliente.IdSedeCliente.HasValue) { sqlParm[0].Value = oSedeCliente.IdSedeCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oSedeCliente.Nombre != null) { sqlParm[1].Value = oSedeCliente.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oSedeCliente.IdCliente.HasValue) { sqlParm[2].Value = oSedeCliente.IdCliente; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oSedeCliente.IdUnidadNegocio.HasValue) { sqlParm[3].Value = oSedeCliente.IdUnidadNegocio; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
            if (oSedeCliente.IdDepartamento != null) { sqlParm[4].Value = oSedeCliente.IdDepartamento; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
            if (oSedeCliente.IdProvincia != null) { sqlParm[5].Value = oSedeCliente.IdProvincia; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
            if (oSedeCliente.IdDistrito != null) { sqlParm[6].Value = oSedeCliente.IdDistrito; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oSedeCliente.IdEstado.HasValue) { sqlParm[7].Value = oSedeCliente.IdEstado; } else { sqlParm[7].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_SEDE_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            var oEntSede = new SedeClienteInfo
                                {
                                    IdSedeCliente = Int32.Parse(drd.GetValue(0).ToString()),
                                    Nombre = drd.GetString(1).Trim(),
                                    IdCliente = Int32.Parse(drd.GetValue(2).ToString()),
                                    Cliente = new ClienteInfo
                                        {
                                            RazonSocial = drd.GetString(3).Trim(),
                                            NroDi = drd.GetString(4).Trim()
                                        },
                                    IdUnidadNegocio = Int32.Parse(drd.GetValue(5).ToString()),
                                    UnidadNegocio =
                                        new UnidadNegocioInfo(null, drd.GetString(6).Trim(), null, null, null, null),
                                    IdDepartamento = drd.GetString(7).Trim(),
                                    Departamento = new UbigeoInfo(null, null, null, drd.GetString(8).Trim()),
                                    IdProvincia = drd.GetString(9).Trim(),
                                    Provincia = new UbigeoInfo(null, null, null, drd.GetString(10).Trim()),
                                    IdDistrito = drd.GetString(11).Trim(),
                                    Distrito = new UbigeoInfo(null, null, null, drd.GetString(12).Trim()),
                                    Telefono = drd.GetString(13).Trim(),
                                    NombreContacto = drd.GetString(14).Trim(),
                                    CargoContacto = drd.GetString(15).Trim(),
                                    IdEstado = Int32.Parse(drd.GetValue(16).ToString()),
                                    Estado = new EstadoInfo(null, drd.GetString(17).Trim()),
                                    CentroCosto = drd.GetString(18).Trim(),
                                    Direccion = drd.GetString(19).Trim()
                                };
                            oListaSedes.Add(oEntSede);
                        }
                    }
                }
            }

            return oListaSedes;
        }

        public SedeClienteInfo Consultar(SedeClienteInfo oSedeCliente)
        {
            var sqlParm = new SqlParameter[8];
            var oEntSede = new SedeClienteInfo();

            sqlParm[0] = new SqlParameter("@Id_Sede", SqlDbType.Int);
            if (oSedeCliente.IdSedeCliente.HasValue) { sqlParm[0].Value = oSedeCliente.IdSedeCliente; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
            if (oSedeCliente.Nombre != null) { sqlParm[1].Value = oSedeCliente.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
            if (oSedeCliente.IdCliente.HasValue) { sqlParm[2].Value = oSedeCliente.IdCliente; } else { sqlParm[2].Value = DBNull.Value; }

            sqlParm[3] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
            if (oSedeCliente.IdUnidadNegocio.HasValue) { sqlParm[3].Value = oSedeCliente.IdUnidadNegocio; } else { sqlParm[3].Value = DBNull.Value; }

            sqlParm[4] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
            if (oSedeCliente.IdDepartamento != null) { sqlParm[4].Value = oSedeCliente.IdDepartamento; } else { sqlParm[4].Value = DBNull.Value; }

            sqlParm[5] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
            if (oSedeCliente.IdProvincia != null) { sqlParm[5].Value = oSedeCliente.IdProvincia; } else { sqlParm[5].Value = DBNull.Value; }

            sqlParm[6] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
            if (oSedeCliente.IdDistrito != null) { sqlParm[6].Value = oSedeCliente.IdDistrito; } else { sqlParm[6].Value = DBNull.Value; }

            sqlParm[7] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oSedeCliente.IdEstado.HasValue) { sqlParm[7].Value = oSedeCliente.IdEstado; } else { sqlParm[7].Value = DBNull.Value; }

            using (
                var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction,
                                                  CommandType.StoredProcedure, "TI_SP_CONSULTAR_SEDE_CLIENTE", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntSede = new SedeClienteInfo
                            {
                                IdSedeCliente = Int32.Parse(drd.GetValue(0).ToString()),
                                Nombre = drd.GetString(1).Trim(),
                                IdCliente = Int32.Parse(drd.GetValue(2).ToString()),
                                Cliente = new ClienteInfo
                                {
                                    RazonSocial = drd.GetString(3).Trim(),
                                    NroDi = drd.GetString(4).Trim()
                                },
                                IdUnidadNegocio = Int32.Parse(drd.GetValue(5).ToString()),
                                UnidadNegocio =
                                    new UnidadNegocioInfo(null, drd.GetString(6).Trim(), null, null, null, null),
                                IdDepartamento = drd.GetString(7).Trim(),
                                Departamento = new UbigeoInfo(null, null, null, drd.GetString(8).Trim()),
                                IdProvincia = drd.GetString(9).Trim(),
                                Provincia = new UbigeoInfo(null, null, null, drd.GetString(10).Trim()),
                                IdDistrito = drd.GetString(11).Trim(),
                                Distrito = new UbigeoInfo(null, null, null, drd.GetString(12).Trim()),
                                Telefono = drd.GetString(13).Trim(),
                                NombreContacto = drd.GetString(14).Trim(),
                                CargoContacto = drd.GetString(15).Trim(),
                                IdEstado = Int32.Parse(drd.GetValue(16).ToString()),
                                Estado = new EstadoInfo(null, drd.GetString(17).Trim()),
                                CentroCosto = drd.GetString(18).Trim(),
                                Direccion = drd.GetString(19).Trim()
                            };
                    }
                }
            }

            return oEntSede;
        }

        public bool Registrar(SedeClienteInfo oSedeCliente, ref int? nId)
        {
            var sqlParm = new SqlParameter[14];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oSedeCliente.Nombre != null) { sqlParm[0].Value = oSedeCliente.Nombre; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oSedeCliente.IdCliente.HasValue) { sqlParm[1].Value = oSedeCliente.IdCliente; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
                    if (oSedeCliente.IdUnidadNegocio.HasValue) { sqlParm[2].Value = oSedeCliente.IdUnidadNegocio; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    if (oSedeCliente.Direccion != null) { sqlParm[3].Value = oSedeCliente.Direccion; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
                    if (oSedeCliente.IdDepartamento != null) { sqlParm[4].Value = oSedeCliente.IdDepartamento; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
                    if (oSedeCliente.IdProvincia != null) { sqlParm[5].Value = oSedeCliente.IdProvincia; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
                    if (oSedeCliente.IdDistrito != null) { sqlParm[6].Value = oSedeCliente.IdDistrito; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    if (oSedeCliente.Telefono != null) { sqlParm[7].Value = oSedeCliente.Telefono; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Nombre_Contacto", SqlDbType.VarChar);
                    if (oSedeCliente.NombreContacto != null) { sqlParm[8].Value = oSedeCliente.NombreContacto; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Cargo_Contacto", SqlDbType.VarChar);
                    if (oSedeCliente.CargoContacto != null) { sqlParm[9].Value = oSedeCliente.CargoContacto; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oSedeCliente.IdEstado.HasValue) { sqlParm[10].Value = oSedeCliente.IdEstado; } else { sqlParm[10].Value = DBNull.Value; }

                    sqlParm[11] = new SqlParameter("@Centro_Costo", SqlDbType.VarChar);
                    if (oSedeCliente.CentroCosto != null) { sqlParm[11].Value = oSedeCliente.CentroCosto; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oSedeCliente.UsuarioCreacion != null) { sqlParm[12].Value = oSedeCliente.UsuarioCreacion; } else { sqlParm[12].Value = DBNull.Value; }

                    sqlParm[13] = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_SEDE_CLIENTE", sqlParm);
                    nId = Int32.Parse(sqlParm[13].Value.ToString());
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

        public bool Actualizar(SedeClienteInfo oSedeCliente)
        {
            var sqlParm = new SqlParameter[14];

            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Sede", SqlDbType.Int);
                    if (oSedeCliente.IdSedeCliente.HasValue) { sqlParm[0].Value = oSedeCliente.IdSedeCliente; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    if (oSedeCliente.Nombre != null) { sqlParm[1].Value = oSedeCliente.Nombre; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Cliente", SqlDbType.Int);
                    if (oSedeCliente.IdCliente.HasValue) { sqlParm[2].Value = oSedeCliente.IdCliente; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id_Unidad_Negocio", SqlDbType.Int);
                    if (oSedeCliente.IdUnidadNegocio.HasValue) { sqlParm[3].Value = oSedeCliente.IdUnidadNegocio; } else { sqlParm[3].Value = DBNull.Value; }

                    sqlParm[4] = new SqlParameter("@Direccion", SqlDbType.VarChar);
                    if (oSedeCliente.Direccion != null) { sqlParm[4].Value = oSedeCliente.Direccion; } else { sqlParm[4].Value = DBNull.Value; }

                    sqlParm[5] = new SqlParameter("@Id_Departamento", SqlDbType.VarChar);
                    if (oSedeCliente.IdDepartamento != null) { sqlParm[5].Value = oSedeCliente.IdDepartamento; } else { sqlParm[5].Value = DBNull.Value; }

                    sqlParm[6] = new SqlParameter("@Id_Provincia", SqlDbType.VarChar);
                    if (oSedeCliente.IdProvincia != null) { sqlParm[6].Value = oSedeCliente.IdProvincia; } else { sqlParm[6].Value = DBNull.Value; }

                    sqlParm[7] = new SqlParameter("@Id_Distrito", SqlDbType.VarChar);
                    if (oSedeCliente.IdDistrito != null) { sqlParm[7].Value = oSedeCliente.IdDistrito; } else { sqlParm[7].Value = DBNull.Value; }

                    sqlParm[8] = new SqlParameter("@Telefono", SqlDbType.VarChar);
                    if (oSedeCliente.Telefono != null) { sqlParm[8].Value = oSedeCliente.Telefono; } else { sqlParm[8].Value = DBNull.Value; }

                    sqlParm[9] = new SqlParameter("@Nombre_Contacto", SqlDbType.VarChar);
                    if (oSedeCliente.NombreContacto != null) { sqlParm[9].Value = oSedeCliente.NombreContacto; } else { sqlParm[9].Value = DBNull.Value; }

                    sqlParm[10] = new SqlParameter("@Cargo_Contacto", SqlDbType.VarChar);
                    if (oSedeCliente.CargoContacto != null) { sqlParm[10].Value = oSedeCliente.CargoContacto; } else { sqlParm[10].Value = DBNull.Value; }

                    sqlParm[11] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oSedeCliente.IdEstado.HasValue) { sqlParm[11].Value = oSedeCliente.IdEstado; } else { sqlParm[11].Value = DBNull.Value; }

                    sqlParm[12] = new SqlParameter("@Centro_Costo", SqlDbType.VarChar);
                    if (oSedeCliente.CentroCosto != null) { sqlParm[12].Value = oSedeCliente.CentroCosto; } else { sqlParm[12].Value = DBNull.Value; }

                    sqlParm[13] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oSedeCliente.UsuarioModificacion != null) { sqlParm[13].Value = oSedeCliente.UsuarioModificacion; } else { sqlParm[13].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_SEDE_CLIENTE", sqlParm);
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
