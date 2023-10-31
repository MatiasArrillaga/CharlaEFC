using Algoritmo.CharlaEFC.Portable.BaseClasses;
using Algoritmo.Microservices.Shared.Portable.Enums.Jerarquias;
using Algoritmo.Microservices.Shared.Portable.SharedEntitiesDTO.Jerarquias.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmo.CharlaEFC.Portable.Jerarquias.DTOs
{
    public class JerarquiaDTO : EntidadTransaccionalDTO, IJerarquiaDTO
    {
        [JsonConstructor]
        protected JerarquiaDTO() { }
        public JerarquiaDTO(string codigo, string nombre, Type tipoEntidadHoja)
        {
            Codigo = codigo;
            Nombre = nombre;
            SetEntityType( tipoEntidadHoja);
        }

        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
       
        [JsonIgnore]
        public Type? TipoEntidadHoja { get; set; }
        [JsonProperty]
        public string TipoEntidadAssembly { get; protected set; }
        [JsonProperty]
        public string TipoEntidadFullName { get; protected set; } = string.Empty;
        public bool PermiteHojasYRamasEnMismoNivel { get; set; }
        public List<JerarquiaNivelDTO> Niveles { get; set; } = new();
        public List<JerarquiaItemDTO> Arbol { get; set; } = new();
        public JerarquiaItemDTO? Root => Arbol.SingleOrDefault(r => r.Tipo == TipoItemJerarquia.Raiz);
        public IReadOnlyList<JerarquiaItemDTO> Hojas => Arbol.Where(t => t.Tipo == TipoItemJerarquia.Hoja).ToList();
        public bool Activo { get; set; }


        #region Implementación obligatoria de la interfaz
        IJerarquiaItemDTO IJerarquiaDTO.Root => Root;

        IReadOnlyList<IJerarquiaNivelDTO> IJerarquiaDTO.Niveles => Niveles;

        IReadOnlyList<IJerarquiaItemDTO> IJerarquiaDTO.Arbol => Arbol;

        IReadOnlyList<IJerarquiaItemDTO> IJerarquiaDTO.Hojas => Hojas;

        #endregion

        #region Administración de Niveles
        public JerarquiaDTO AddRange(List<JerarquiaNivelDTO> jerarquiaNivelDTOs)
        {
            jerarquiaNivelDTOs.ForEach(n => n.Jerarquia = this);
            Niveles.AddRange(jerarquiaNivelDTOs);
            return this;
        }
        public JerarquiaDTO Add(JerarquiaNivelDTO jerarquiaNivelDTO)
        {
            jerarquiaNivelDTO.Jerarquia = this;
            Niveles.Add(jerarquiaNivelDTO);
            return this;
        }

        #endregion

        #region Administración de Árbol
        public JerarquiaDTO AddRange(List<JerarquiaItemDTO> jerarquiaItemDTOs)
        {
            jerarquiaItemDTOs.ForEach(i => i.Jerarquia = this);
            Arbol.AddRange(jerarquiaItemDTOs);
            return this;
        }
        public JerarquiaDTO Add(JerarquiaItemDTO jerarquiaItemDTO)
        {
            jerarquiaItemDTO.Jerarquia = this;
            Arbol.Add(jerarquiaItemDTO);
            return this;
        }
        public void Remove(JerarquiaItemDTO? item)
        {
            if (item is null) return;
            Remove(item.Codigo);
        }
        public JerarquiaDTO Remove(string codigo)
        {
            var item = Arbol.SingleOrDefault(i => i.Codigo.Equals(codigo));
            if (item is not null)
                Arbol.Remove(item);

            Root.Hijos.ToList().ForEach(h =>
            {
                h.Remove(codigo);
            });
            return this;

        }
        #endregion

        private Type _tipoEntidadHoja = null!;
        public IJerarquiaDTO SetEntityType(Type type)
        {
            _tipoEntidadHoja = type;
            if (type is null) return this;

            TipoEntidadFullName = type.FullName;
            TipoEntidadAssembly = type.AssemblyQualifiedName;
            return this;
        }

    }
    public class JerarquiaNivelDTO : BaseDTOPortable, IJerarquiaNivelDTO
    {
        [JsonConstructor]
        private JerarquiaNivelDTO() { }
        public JerarquiaNivelDTO(JerarquiaDTO jerarquia, int nivel, string nombre) : this(nivel, nombre)
        {
            Jerarquia = jerarquia;
        }
        public JerarquiaNivelDTO(int nivel, string nombre)
        {
            Nivel = nivel;
            Nombre = nombre;
        }

        public JerarquiaDTO Jerarquia { get; set; } = null!;
        public int Nivel { get; set; }
        public string Nombre { get; set; } = string.Empty;

        #region Implementación obligatoria de la interfaz
        IJerarquiaDTO IJerarquiaNivelDTO.Jerarquia
        {
            get => Jerarquia;
            set => Jerarquia = (JerarquiaDTO)value;
        }
        #endregion
    }
    public class JerarquiaItemDTO : EntidadTransaccionalDTO, IJerarquiaItemDTO
    {
        [JsonConstructor]
        protected JerarquiaItemDTO() { }
        public JerarquiaItemDTO(string codigo, string nombre, TipoItemJerarquia tipo, JerarquiaItemDTO? padre, JerarquiaDTO jerarquia) : this(codigo, nombre, tipo, padre)
        {
            Jerarquia = jerarquia;
        }
        public JerarquiaItemDTO(string codigo, string nombre, TipoItemJerarquia tipo, JerarquiaItemDTO? padre)
        {
            Codigo = codigo;
            Nombre = nombre;
            Tipo = tipo;
            Padre = padre;
            Nivel = -1;
        }

        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public TipoItemJerarquia Tipo { get; set; }
        public int Nivel { get; set; }
        public JerarquiaNivelDTO? JerarquiaNivel { get; set; }
        public string CodigoConcatenado { get; set; } = string.Empty;
        public string NombreConcatenado { get; set; } = string.Empty;
        public JerarquiaDTO Jerarquia { get; set; } = null!;
        public JerarquiaItemDTO? Padre { get; set; }

        public IEntidadInfoDTO? EntidadInformacion { get; set; }

        public IReadOnlyList<JerarquiaItemDTO> Hijos => _hijos;

        private List<JerarquiaItemDTO> _hijos { get; set; } = new();
        public JerarquiaItemDTO Add(JerarquiaItemDTO jerarquiaItemDTO)
        {
            _hijos.Add(jerarquiaItemDTO);
            return this;
        }
        /// <summary>
        /// Borra un hijo dentro de la lista de hijos sin recursar.
        /// </summary>
        /// <param name="jerarquiaItemDTO"></param>
        /// <returns></returns>
        public JerarquiaItemDTO Remove(JerarquiaItemDTO jerarquiaItemDTO)
        {
            _hijos.Remove(jerarquiaItemDTO);
            return this;
        }

        /// <summary>
        /// Borra un item según el código indicado. Hace un recorrido recursivo dentro de la lista de hijos
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public JerarquiaItemDTO Remove(string codigo)
        {
            JerarquiaItemDTO itmRmv = null;

            _hijos.ForEach(i =>
            {
                if (i.Codigo.Equals(codigo))
                {
                    itmRmv = i;
                    return;
                }
                i.Hijos.ToList().ForEach(h =>
                {
                    if (h.Codigo.Equals(codigo))
                    {
                        i.Remove(h);
                        return;
                    };
                    h.Remove(codigo);
                });
            });

            if (itmRmv is not null) Remove(itmRmv);
            return this;
        }


        [JsonProperty]
        public string? TipoEntidadFullName { get; private set; }
        [JsonProperty]
        public string? TipoEntidadAssembly { get; private set; }
        [JsonProperty]
        public int? EntidadMaestraId { get; private set; }
        [JsonProperty]
        public Guid? EntidadTransaccionalId { get; private set; }

        #region Implementación obligatoria de la interfaz
        IJerarquiaDTO IJerarquiaItemDTO.Jerarquia => Jerarquia;

        IJerarquiaNivelDTO? IJerarquiaItemDTO.JerarquiaNivel => JerarquiaNivel;

        IJerarquiaItemDTO? IJerarquiaItemDTO.Padre => Padre;

        IReadOnlyList<IJerarquiaItemDTO> IJerarquiaItemDTO.Hijos => Hijos;

        #endregion

    }

}


