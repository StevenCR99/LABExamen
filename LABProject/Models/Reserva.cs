using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LABproject.Models
{
    public class Reserva : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del profesor es obligatorio")]
        [MinLength(3, ErrorMessage = "El nombre debe tener al menos 3 caracteres")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string ProfesorNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo institucional es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [RegularExpression(@"^[^@\s]+@campus\.edu$", ErrorMessage = "El correo debe pertenecer al dominio @campus.edu")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un laboratorio")]
        [RegularExpression(@"^(Lab-01|Lab-02|Lab-03|Lab-Redes|Lab-IA)$", ErrorMessage = "Laboratorio inválido")]
        public string Laboratorio { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de la reserva es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El motivo es obligatorio")]
        [MinLength(5, ErrorMessage = "El motivo debe tener al menos 5 caracteres")]
        [StringLength(200, ErrorMessage = "Máximo 200 caracteres")]
        public string Motivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código de reserva es obligatorio")]
        [RegularExpression(@"^RES-\d{3}$", ErrorMessage = "El código debe tener el formato RES-###")]
        public string Codigo { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Fecha.Date < DateTime.Today)
            {
                yield return new ValidationResult("La fecha de la reserva no puede ser en el pasado", new[] { nameof(Fecha) });
            }

            if (HoraFin <= HoraInicio)
            {
                yield return new ValidationResult("La hora de fin debe ser mayor que la hora de inicio", new[] { nameof(HoraFin), nameof(HoraInicio) });
            }

            var duracion = HoraFin - HoraInicio;
            if (duracion.TotalMinutes <= 0)
            {
                yield return new ValidationResult("La reserva debe tener una duración positiva", new[] { nameof(HoraInicio), nameof(HoraFin) });
            }
        }
    }
}
