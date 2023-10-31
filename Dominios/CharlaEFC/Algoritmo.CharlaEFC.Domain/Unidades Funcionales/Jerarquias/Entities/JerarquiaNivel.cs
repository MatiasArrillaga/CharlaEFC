using Algoritmo.CharlaEFC.Domain.BaseClasses;
using Algoritmo.Microservices.Shared.Domain.Jerarquias.Interfaces;
using System;

namespace Algoritmo.CharlaEFC.Domain.Jerarquias.Entities
{
    public class JerarquiaNivel : BaseCharlaEFC, IJerarquiaNivel
    {
        protected JerarquiaNivel() { }
        public JerarquiaNivel(Jerarquia jerarquia, int nivel, string nombre)
        {
            Jerarquia = jerarquia;
            Nivel = nivel;
            Nombre = nombre;
        }

        public Jerarquia Jerarquia { get; set; } = null!;
        public int Nivel { get; set; } = -1;
        public string Nombre { get; set; } = string.Empty;

        #region  Implementaciones Base obligatorias
        IJerarquia IJerarquiaNivel.Jerarquia
        {
            get => Jerarquia;
            set => Jerarquia = (Jerarquia)value;
        }
        #endregion
    }
}