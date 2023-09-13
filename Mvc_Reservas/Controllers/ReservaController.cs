using Microsoft.AspNetCore.Mvc;
using Mvc_Reservas.Models;

namespace Mvc_Reservas.Controllers
{
    public class ReservaController : Controller
    {
        private ReservasDB _db;
        public ReservaController()
        {
            _db = new ReservasDB();
        }
        public IActionResult Index()
        {

            return View(new ReservasDB().getList());
        }

        public IActionResult Filter(string inFiltro)
        {
            List<Reserva> reservas = _db.filter(inFiltro);
            return View("Index", reservas);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string Id)
        {
            Reserva aux = _db.getById(Id);
            return View(aux);
        }

        public IActionResult Delete(string Id)
        {
            Reserva aux = _db.getById(Id);
            return View(aux);
        }

        public IActionResult Save(
            string inId, string inAcomodacao, bool inCafeIncluso,DateOnly inDataChegada,
            DateOnly inDataSaida, decimal inTotal
        )
        {
            Reserva reserva = new Reserva
            {
                Acomodacao = inAcomodacao,
                CafeIncluso = inCafeIncluso,
                DataChegada = inDataChegada,
                DataSaida   = inDataSaida,
                Total = inTotal
            };

            if (inId == null)
            {
                reserva.Id = Guid.NewGuid();
                _db.insert(reserva);
            }
            else
            {
                reserva.Id = new Guid(inId);
                _db.update(reserva);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            _db.delete(id);
            return RedirectToAction("Index");
        }
    }
}
