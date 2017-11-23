using DevStore.Api.Response;
using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
//  using static DevStore.Api.Route.ProductRoute;

namespace DevStore.Api.Controller {

    //  origins -> URL
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1")]
    public class ProductController : ApiController {

        //  Executar no CMD dentro da pasta C:\Program Files\IIS Express 
        //  appcmd set config /section:directoryBrowse /enabled:true

        private DataContext db = new DataContext();

        //  GET
        [HttpGet]
        [Route("products")]
        public HttpResponseMessage GetProducts() {
            //  Inclui o relacionamento da categoria no retorno
            var response = db.Products.Include("Category").ToList();
            if (response.Count <= 0) {
                return Request.CreateResponse(HttpStatusCode.NotFound, new ServerResponse(404, "Nenhum produto encontrado.", string.Empty));
            } else {
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        // GET - ById
        [HttpGet]
        [Route("products/{id}")]
        public HttpResponseMessage GetProduct(int id) {
            Product product = db.Products.Find(id);
            if (product == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound, new ServerResponse(404, "Nenhum produto encontrado.", string.Empty));
            } else {
                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
        }

        // POST
        [HttpPost]
        [Route("products")]
        public HttpResponseMessage PostProduct(Product product) {
            if (product == null) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ServerResponse(400, "Não foi possível incluir o produto.", string.Empty));
            } else {
                try {
                    db.Products.Add(product);
                    db.SaveChanges();
                    var response = product;
                    return Request.CreateResponse(HttpStatusCode.Created, response);
                } catch (Exception e) {
                    Debug.WriteLine("PostProduct error: " + e.Message);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new ServerResponse(500, "Erro no servidor.", e.Message));
                }
            }
        }

        // PUT
        [HttpPut]
        [Route("products")]
        public HttpResponseMessage PutProduct(Product product) {
            if (product == null) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ServerResponse(400, "Não foi possível atualizar o produto.", string.Empty));
            } else {
                try {
                    db.Entry<Product>(product).State = EntityState.Modified;
                    db.SaveChanges();
                    var response = product;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                } catch (Exception e) {
                    Debug.WriteLine("PutProduct error: " + e.Message);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new ServerResponse(500, "Erro no servidor.", e.Message));
                }
            }
        }

        // DELETE
        [HttpDelete]
        [Route("products/{id}")]
        public HttpResponseMessage DeleteProduct(int id) {
            if (id <= 0) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ServerResponse(400, "Não foi possível excluir o produto.", string.Empty));
            } else {
                try {
                    db.Products.Remove(db.Products.Find(id));
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new ServerResponse(200, "Produto excluído com sucesso.", string.Empty));
                } catch (Exception e) {
                    Debug.WriteLine("DeleteProduct error: " + e.Message);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new ServerResponse(500, "Erro no servidor.", e.Message));
                }
            }
        }

        protected override void Dispose(bool disposing) {
            db.Dispose();
        }
    }
}