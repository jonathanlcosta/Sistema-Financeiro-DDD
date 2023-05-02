using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HandlebarsDotNet;
using SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos.Interfaces;

namespace SistemaFinanceiros.Aplicacao.TemplatesTexto.Servicos
{
    public class GeradorTemplateTexto : IGeradorTemplateTexto
    {
         private string pathArquivo;
        private Func<object, string> templateCompilado;

        public GeradorTemplateTexto() { }

        public GeradorTemplateTexto(string pathArquivo, string pathBase)
        {
            if (string.IsNullOrWhiteSpace(pathArquivo))
                throw new ArgumentException($"Path inválido: '{pathArquivo}'");

            if (string.IsNullOrWhiteSpace(pathBase))
                throw new ArgumentException($"Path base inválido: '{pathBase}'");

            this.pathArquivo = GerarPathAbsoluto(pathBase, pathArquivo);
        }

        public void CarregarTemplate(string pathArquivo)
        {
            if (string.IsNullOrWhiteSpace(pathArquivo))
                throw new ArgumentException($"Path inválido: '{pathArquivo}'");

            var pathBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            this.pathArquivo = GerarPathAbsoluto(pathBase, pathArquivo);
        }

        public void CarregarTemplate(string pathArquivo, string pathBase)
        {
            if (string.IsNullOrWhiteSpace(pathArquivo))
                throw new ArgumentException($"Path inválido: '{pathArquivo}'");

            if (string.IsNullOrWhiteSpace(pathBase))
                throw new ArgumentException($"Path base inválido: '{pathBase}'");

            this.pathArquivo = GerarPathAbsoluto(pathBase, pathArquivo);
        }

        private void CompilarTemplate()
        {
            if (this.templateCompilado == null)
                this.templateCompilado = Handlebars.Compile(File.ReadAllText(this.pathArquivo));
        }

        private static string GerarPathAbsoluto(string pathBase, string pathRelativo)
        {
            pathRelativo = pathRelativo.Replace('/', Path.DirectorySeparatorChar)
                                       .Replace('\\', Path.DirectorySeparatorChar);

            return Path.Combine(pathBase, pathRelativo);
        }

        public string Executar(object conteudo)
        {
            CompilarTemplate();
            return this.templateCompilado(conteudo);
        }
    }
}