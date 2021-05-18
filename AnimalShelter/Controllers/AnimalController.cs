using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AnimalShelter.Controllers
{
  public class AnimalsController : Controller
  {
    private readonly AnimalShelterContext _db;

    public AnimalsController(AnimalShelterContext db)
    {
      _db = db;
    }

    public ActionResult Index(string sortOrder)
    {
      ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
      ViewBag.DateSortParm = sortOrder == "Date" ? "date_dec" : "Date";
      ViewBag.TypeSortParm = sortOrder == "Type" ? "Type" : "Type";
      ViewBag.BreedSortParm = sortOrder == "Breed" ? "Breed" : "Breed";
      ViewBag.GenderSortParm = sortOrder == "Gender" ? "Gender" : "Gender";
      List<Animal> animals = _db.Animals.ToList();
      switch (sortOrder)
      {
        case "Gender":
          animals = animals.OrderBy(animals => animals.Gender).ToList();
          break;
        case "Breed":
          animals = animals.OrderBy(animals => animals.Breed).ToList();
          break;
        case "Type":
          animals = animals.OrderBy(animals => animals.Type).ToList();
          break;
        case "Name":
          animals = animals.OrderByDescending(animals => animals.Name).ToList();
          break;
        case "Date":
          animals = animals.OrderBy(animals => animals.DateOfAdmittance).ToList();
          break;
        case "date_desc":
          animals = animals.OrderByDescending(animals => animals.DateOfAdmittance).ToList();
          break;
        default:
          animals = animals.OrderBy(animals => animals.Name).ToList();
          break;
      }

      return View(animals);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Animal animal)
    {
      _db.Animals.Add(animal);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Animal thisAnimal = _db.Animals.FirstOrDefault(animal => animal.AnimalId == id);
      return View(thisAnimal);
    }
  }
}