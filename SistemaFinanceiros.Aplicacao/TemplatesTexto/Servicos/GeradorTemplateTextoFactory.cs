using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos.Interfaces;

namespace SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos
{
    public class GeradorTemplateTextoFactory : IGeradorTemplateTextoFactory
    {
        private readonly string pathBase;
        private ConcurrentDictionary<string, GeradorTemplateTexto> cacheTemplates;

        public GeradorTemplateTextoFactory()
        {

        }

        public GeradorTemplateTextoFactory(string pathBase)
        {
            this.pathBase = pathBase;
        }

        public IGeradorTemplateTexto Recuperar(string pathRelativo)
        {
            if (this.cacheTemplates == null)
                this.cacheTemplates = new ConcurrentDictionary<string, GeradorTemplateTexto>();

            if (string.IsNullOrWhiteSpace(pathRelativo))
                throw new ArgumentException($"Path inválido: '{pathRelativo}'");

            if (string.IsNullOrWhiteSpace(pathBase))
                throw new ArgumentException($"Path inválido: '{pathBase}'");

            pathRelativo = pathRelativo.Replace('/', Path.DirectorySeparatorChar)
                       .Replace('\\', Path.DirectorySeparatorChar);

            if (!this.cacheTemplates.ContainsKey(pathRelativo))
            {
                this.cacheTemplates[pathRelativo] = new GeradorTemplateTexto(pathRelativo, pathBase);
            }

            return this.cacheTemplates[pathRelativo];
        }
    }
}