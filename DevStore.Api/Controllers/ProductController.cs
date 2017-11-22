using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace DevStore.Api.Controllers {

    public class ProductController : ApiController {

        //  Executar no CMD dentro da pasta C:\Program Files\IIS Express 
        //  appcmd set config /section:directoryBrowse /enabled:true

        private DataContext db = new DataContext();

        // GET: api/products
        [Route("api/products")]
        public IQueryable<Product> GetProducts() {
            //  Inclui o relacionamento da categoria no retorno
            return db.Products.Include("Category");
        }

        // GET: api/products/5
        [Route("api/products/{id}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id) {
            Product product = db.Products.Find(id);
            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }

        // PUT: api/products/5
        [Route("api/products/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if (id != product.Id) {
                return BadRequest();
            }
            db.Entry(product).State = EntityState.Modified;
            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/products
        [Route("api/products")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            db.Products.Add(product);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/products/5
        [Route("api/products/{id}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id) {
            Product product = db.Products.Find(id);
            if (product == null) {
                return NotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return Ok(product);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id) {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}