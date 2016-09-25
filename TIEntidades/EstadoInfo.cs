using System;

namespace TIEntidades
{
    [Serializable]
    public class EstadoInfo
    {
        private int? _idEstado;
        private String _descripcion;

        public EstadoInfo()
        {
        }

        public EstadoInfo(int? nIdEstado, String sDescripcion)
        {
            _idEstado = nIdEstado;
            _descripcion = sDescripcion;
        }

        public int? IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
    }
}
