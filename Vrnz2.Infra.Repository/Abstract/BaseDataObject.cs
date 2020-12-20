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

        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonIgnore]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonIgnore]
        public bool Deleted { get; set; }

        [JsonIgnore]
        public bool Processed { get; set; }

        [JsonIgnore]
        public DateTimeOffset? ProcessedAt { get; set; }

        #endregion
    }
}
