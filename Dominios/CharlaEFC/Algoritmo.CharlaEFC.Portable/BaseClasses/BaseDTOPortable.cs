using Algoritmo.Microservices.Shared.Portable.BaseClassesDTO;
using System;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
    public class BaseDTOPortable : BaseDTO
    {
      
    }
   public class EntidadMaestraDTO : BaseDTOPortable, IMasterEntityDTO
    {
        public string Codigo { get; set; } = string.Empty;
    }
    public class EntidadTransaccionalDTO : BaseDTOPortable, ITransactionalEntityDTO
    {
        #region ID Transaccional
        //public new Guid? Id { get; set; }
        private Guid? id;
        public new Guid? Id
        {
            get
            {
                return ITransactionalEntityDTO.EvaluateId(id, TemporalId);
            }
            set
            {
                id = value;
            }
        }
        #endregion
    }

}
