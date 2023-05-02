using Identity_Session.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;

namespace Identity_Session.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ICategoryService _categoryService;
        readonly ICommentService _commentService;
        readonly ICountryService _countryService;
        readonly IProductService _productService;
        readonly IOrderService _orderService;
        readonly IPictureService _pictureService;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService, ICommentService commentService, ICountryService countryService, IProductService productService, IOrderService orderService, IPictureService pictureService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _commentService = commentService;
            _countryService = countryService;
            _productService = productService;
            _orderService = orderService;
            _pictureService = pictureService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("sitemap.xml")]
        public IActionResult OnGet()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8' ?><urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>");

            var country = _countryService.GetAllSync();
            foreach (var item in country)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.Name + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }

            var category = _categoryService.GetAllSync();
            foreach (var item in category)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.Name + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }

            var comment = _commentService.GetAllSync();
            foreach (var item in comment)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.Subject + item.Text + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }

            var product = _productService.GetAllSync();
            foreach (var item in product)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.Name + item.Desc + item.Price + item.UnitInStock + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }

            var order = _orderService.GetAllSync();
            foreach (var item in order)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.City + item.Province + item.Address + item.Quantity + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }

            var picture = _pictureService.GetAllSync();
            foreach (var item in picture)
            {
                string mDate = item.CreatedDate.ToString("yyyy-MM-dd");
                sb.Append("<url><loc>" + item.Title + item.ImageUrl + "</loc><lastmod>" + mDate + "</lastmod><priority> 0.8</priority></url>");
            }
            sb.Append("</urlset>");

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = sb.ToString(),
                StatusCode = 200
            };
        }

        [Route("rss.xml")]
        public IActionResult Rss()
        {
            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter rssWriter = new XmlTextWriter(Response.BodyWriter.AsStream(true), Encoding.UTF8);
            rssWriter.WriteStartDocument();
            rssWriter.WriteStartElement("rss");
            rssWriter.WriteAttributeString("version", "1.0");
            rssWriter.WriteStartElement("channel");
            rssWriter.WriteElementString("title", "practicodom.com RSS ÖRNEK");
            rssWriter.WriteElementString("link", "practicodom.com/rss.xml");
            rssWriter.WriteElementString("description", "practicodom.com");
            rssWriter.WriteElementString("copyright", "(c) 2023, practicodom.com");
            rssWriter.WriteElementString("pubDate", "15/03/2023");
            rssWriter.WriteElementString("language", "es-ES");
            rssWriter.WriteElementString("webMaster", "practicodom@gmail.com");

            var a = _categoryService.GetAllSync();
            foreach (var x in a)
            {
                rssWriter.WriteStartElement("item");
                rssWriter.WriteElementString("title", "http://localhost:7017/Name/" + x.Name);
                rssWriter.WriteElementString("pubDate", x.CreatedDate.ToShortDateString());
                rssWriter.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd"));
                rssWriter.WriteEndElement();
            }

            rssWriter.WriteEndDocument();
            rssWriter.Flush();
            rssWriter.Close();
            Response.Body.Close();
            return View();
        }

        
    }
}