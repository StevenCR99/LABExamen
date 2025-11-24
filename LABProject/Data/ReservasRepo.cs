using LABproject.Models;
using System.Collections.Generic;
using System.Linq;

namespace LABproject.Data
{
    public static class ReservasRepo
    {
        private static readonly List<Reserva> _reservas = new();
        private static int _nextId = 1;

        public static IReadOnlyList<Reserva> ObtenerTodos() => _reservas.AsReadOnly();

        public static void Agregar(Reserva r)
        {
            r.Id = _nextId++;
            _reservas.Add(r);
        }

        public static bool ExisteCodigo(string codigo)
        {
            return _reservas.Any(x => x.Codigo == codigo);
        }
    }
}
