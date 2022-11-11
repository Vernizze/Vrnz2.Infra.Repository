using Newtonsoft.Json;
using System;
using Vrnz2.Infra.Repository.Interfaces.Base;

namespace Vrnz2.Infra.Repository.Abstract
{
    [Serializable]
    public abstract class BaseDataObject
        : IBaseDataObject
    {
        #region Constructors

        public BaseDataObject()
            => Id = Guid.NewGuid().ToString();

        #endregion

        #region Attributes

        public string Id { get; set; }

        #endregion
    }
}
