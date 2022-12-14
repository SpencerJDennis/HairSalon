using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class CustomersController: Controller
  {
    private readonly HairSalonContext _db;
    public CustomersController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Customer> model = _db.Customers.ToList();
      return View(model);
    }

    public ActionResult Create(int stylistId, string stylistName)
    {
      ViewBag.StylistName = stylistName;
      ViewBag.StylistId = stylistId;
      return View();
    }

    [HttpPost]
    public ActionResult Create(Customer customer)
    {
      _db.Customers.Add(customer);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = customer.CustomerId});
    }

    public ActionResult Details(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    public ActionResult Edit(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName", thisCustomer.StylistId);
      return View(thisCustomer);
    }

    [HttpPost]
    public ActionResult Edit(Customer customer)
    {
      _db.Entry(customer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = customer.CustomerId } );
    }

    public ActionResult Delete(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      _db.Customers.Remove(thisCustomer);
      _db.SaveChanges();
      return RedirectToAction("Details", "Stylists", new { id = thisCustomer.StylistId});
    }
  }
}