using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagiApp.Models;
using PagiApp.ViewModels;
using PagiApp.Datas;
using PagiApp.Datas.Entities;

namespace PagiApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly pagiContext _dbcontext;
        public AdminController(pagiContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> LihatProduk()
        {
            var dbResult = await _dbcontext.Products.Select(x => new ProdukViewModel {
                Id = x.IdProduct,
                Nama = x.Nama,
                Desk = x.Deskripsi,
                Harga = x.Harga,
                Stok = x.Stock
            }).ToListAsync();
            return View(dbResult);
        }

        public ActionResult TambahProduk()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TambahProduk(Produk _produk)
        {   

            if (ModelState.IsValid)
            {
                Product emptyProduk = new Product
                {
                    Nama = _produk.Nama,
                    Deskripsi = _produk.Deskripsi,
                    Harga = _produk.Harga,
                    Stock = _produk.Stock,
                    Gambar = _produk.Gambar
                };
                await _dbcontext.Products.AddAsync(emptyProduk);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(LihatProduk));
            }

            return View();  
           
        }

        public ActionResult EditProduk(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var empfromdb = _dbcontext.Products.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduk(int? id, Produk _produk)
        {   
            Product _editProduk = new Product
            {
                IdProduct = _produk.IdProduct,
                Nama = _produk.Nama,
                Deskripsi = _produk.Deskripsi,
                Harga = _produk.Harga,
                Stock = _produk.Stock,
                Gambar = _produk.Gambar
            };
            if (ModelState.IsValid)
            {
                _dbcontext.Products.Update(_editProduk);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(LihatProduk));
            }

            return View();
        }

        public IActionResult HapusProduk (int? IdProduct)
        {
            if (IdProduct == null || IdProduct == 0)
            {
                return NotFound();
            }
            var empfromdb = _dbcontext.Products.Find(IdProduct);
         
            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HapusProduks(int? IdProduct, Produk _produk)
        {
            var HapusProduk = _dbcontext.Products.AsNoTracking()
            .FirstOrDefault(m => m.IdProduct == IdProduct);
//            var HapusProduk = _dbcontext.Products.Find(IdProduct);
            Product _hapusProduk = new Product
            {
                IdProduct = _produk.IdProduct,
                Nama = _produk.Nama,
                Deskripsi = _produk.Deskripsi,
                Harga = _produk.Harga,
                Stock = _produk.Stock,
                Gambar = _produk.Gambar
            };
            if (HapusProduk == null)
            {
                return NotFound();
            }
            _dbcontext.Products.Remove(_hapusProduk);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(LihatProduk));
        }

    }
}
