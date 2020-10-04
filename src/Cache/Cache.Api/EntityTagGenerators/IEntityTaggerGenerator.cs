using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cache.Api.EntityTagsGenerators
{
    public interface IEntityTaggerGenerator
    {
        public bool IsEtagSupported(string result);
        public string CalculateEtag(string result);
    }
}
