using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TIEntidades;
using TIBDUtil;
using TIInterfaces;

namespace TIAccesoDatos
{
    public class CategoriaProblema : ICategoriaProblema
    {
        #region Miembros de ICategoriaProblema

        public IList<CategoriaProblemaInfo> Listar(CategoriaProblemaInfo oCategoriaProblema)
        {
            var sqlParm = new SqlParameter[3];
            var oListaCategoriaProblema = new List<CategoriaProblemaInfo>();

            sqlParm[0] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oCategoriaProblema.IdCategoriaProblema.HasValue) { sqlParm[0].Value = oCategoriaProblema.IdCategoriaProblema; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oCategoriaProblema.Descripcion != null) { sqlParm[1].Value = oCategoriaProblema.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oCategoriaProblema.IdEstado.HasValue) { sqlParm[2].Value = oCategoriaProblema.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_CATEGORIA_PROBLEMA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        while (drd.Read())
                        {
                            oListaCategoriaProblema.Add(new CategoriaProblemaInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                                Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                                null, null));
                        }
                    }
                }
            }

            return oListaCategoriaProblema;
        }

        public CategoriaProblemaInfo Consultar(CategoriaProblemaInfo oCategoriaProblema)
        {
            var sqlParm = new SqlParameter[3];
            var oEntCategoriaProblema = new CategoriaProblemaInfo();

            sqlParm[0] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
            if (oCategoriaProblema.IdCategoriaProblema.HasValue) { sqlParm[0].Value = oCategoriaProblema.IdCategoriaProblema; } else { sqlParm[0].Value = DBNull.Value; }

            sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
            if (oCategoriaProblema.Descripcion != null) { sqlParm[1].Value = oCategoriaProblema.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

            sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
            if (oCategoriaProblema.IdEstado.HasValue) { sqlParm[2].Value = oCategoriaProblema.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

            using (var drd = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringDistributedTransaction, CommandType.StoredProcedure, "TI_SP_CONSULTAR_CATEGORIA_PROBLEMA", sqlParm))
            {
                if (drd != null)
                {
                    if (drd.HasRows)
                    {
                        drd.Read();
                        oEntCategoriaProblema = new CategoriaProblemaInfo(Int32.Parse(drd.GetValue(0).ToString()), drd.GetString(1).Trim(),
                            Int32.Parse(drd.GetValue(2).ToString()), new EstadoInfo(null, drd.GetString(3).Trim()),
                            null, null);
                    }
                }
            }

            return oEntCategoriaProblema;
        }

        public bool Registrar(CategoriaProblemaInfo oCategoriaProblema, ref int? nId)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oCategoriaProblema.Descripcion != null) { sqlParm[0].Value = oCategoriaProblema.Descripcion; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oCategoriaProblema.IdEstado.HasValue) { sqlParm[1].Value = oCategoriaProblema.IdEstado; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Usuario_Creacion", SqlDbType.VarChar);
                    if (oCategoriaProblema.UsuarioCreacion != null) { sqlParm[2].Value = oCategoriaProblema.UsuarioCreacion; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Id", SqlDbType.Int) {Direction = ParameterDirection.Output};

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_REGISTRAR_CATEGORIA_PROBLEMA", sqlParm);
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

        public bool Actualizar(CategoriaProblemaInfo oCategoriaProblema)
        {
            var sqlParm = new SqlParameter[4];
            using (var con = new SqlConnection(SqlHelper.ConnectionStringDistributedTransaction))
            {
                con.Open();
                var trx = con.BeginTransaction(IsolationLevel.ReadCommitted);

                try
                {
                    sqlParm[0] = new SqlParameter("@Id_Categoria_Problema", SqlDbType.Int);
                    if (oCategoriaProblema.IdCategoriaProblema.HasValue) { sqlParm[0].Value = oCategoriaProblema.IdCategoriaProblema; } else { sqlParm[0].Value = DBNull.Value; }

                    sqlParm[1] = new SqlParameter("@Descripcion", SqlDbType.VarChar);
                    if (oCategoriaProblema.Descripcion != null) { sqlParm[1].Value = oCategoriaProblema.Descripcion; } else { sqlParm[1].Value = DBNull.Value; }

                    sqlParm[2] = new SqlParameter("@Id_Estado", SqlDbType.Int);
                    if (oCategoriaProblema.IdEstado.HasValue) { sqlParm[2].Value = oCategoriaProblema.IdEstado; } else { sqlParm[2].Value = DBNull.Value; }

                    sqlParm[3] = new SqlParameter("@Usuario_Modificacion", SqlDbType.VarChar);
                    if (oCategoriaProblema.UsuarioModificacion != null) { sqlParm[3].Value = oCategoriaProblema.UsuarioModificacion; } else { sqlParm[3].Value = DBNull.Value; }

                    SqlHelper.ExecuteNonQuery(trx, CommandType.StoredProcedure, "TI_SP_ACTUALIZAR_CATEGORIA_PROBLEMA", sqlParm);
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
