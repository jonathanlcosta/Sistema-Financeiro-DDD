using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos.Interfaces
{
    public interface IGeradorTemplateTexto
    {
        void CarregarTemplate(string pathArquivo, string pathBase);
        void CarregarTemplate(string pathArquivo);
        string Executar(object conteudo);
    }
}