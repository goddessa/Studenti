using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Predmet
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; } = null!;
        [Range(1, 5)]
        public int Godina { get; set; }
        public List<IspitniRok>? PredmetStudent {get; set; } //tabela spoja
        public List<IspitniRok>? IspitniRokovi {get; set;}

    }
}