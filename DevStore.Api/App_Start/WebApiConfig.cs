using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DevStore.Api {

    public static class WebApiConfig {

        public static void Register(HttpConfiguration config) {

            // Serviços e configuração da API da Web

            //  JSON como formato principal de retorno da API REST
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            //  Sempre traz o objeto, nao faca nenhuma referencia
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            //  Remove o XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //  Identa o formato exposto pela API
            settings.Formatting = Formatting.Indented;
            //  O retorno das propriedades sao em minusculo (REST)
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
