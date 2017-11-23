using DevStore.Infra.DataContexts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevStore.Api.Controllers {

    [RoutePrefix("api/v1")]
    public class CategoryController : ApiController {

        private DataContext db = new DataContext();

        //  GET
        [Route("categories")]
        public HttpResponseMessage GetCategories() {
            var response = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //  GET
        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategories(int categoryId) {
            var response = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
