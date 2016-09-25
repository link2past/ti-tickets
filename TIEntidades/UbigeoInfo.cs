using System;

namespace TIEntidades
{
    [Serializable]
    public class UbigeoInfo
    {
        private String _idDepartamento;
        private String _idProvincia;
        private String _idDistrito;
        private String _descripcion;

        public UbigeoInfo()
        {
        }

        public UbigeoInfo(String sIdDepartamento, String sIdProvincia, String sIdDistrito, String sDescripcion)
        {
            _idDepartamento = sIdDepartamento;
            _idProvincia = sIdProvincia;
            _idDistrito = sIdDistrito;
            _descripcion = sDescripcion;
        }

        public string IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        public string IdProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }

        public string IdDistrito
        {
            get { return _idDistrito; }
            set { _idDistrito = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
