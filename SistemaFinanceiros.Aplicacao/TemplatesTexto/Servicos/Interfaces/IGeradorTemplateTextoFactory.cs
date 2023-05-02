using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos.Interfaces
{
    public interface IGeradorTemplateTextoFactory
    {
        IGeradorTemplateTexto Recuperar(string path);
    }
}